using Common.Domain.Communication.Handlers;
using Common.Domain.Multi;
using Common.Infra.Data.NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Event;
using System.Linq;

namespace Common.Infra.Data.Nhibernate
{
    public class NHibernateSession
    {
        private readonly EventHandler eventHandler;

        public NHibernateSession(EventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public ISessionFactory Build()
        {
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Infra"));
            var configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(Env.GetString("DB_CONNECTION_STRING")))
                .Mappings(mappings =>
                {
                    assemblies.ForEach(assembly => mappings.FluentMappings.AddFromAssembly(assembly));
                    mappings.FluentMappings.Conventions.Setup(config =>
                    {
                        config.Add(DefaultLazy.Never());
                        config.Add(ForeignKey.EndsWith("Id"));
                    });
                })
                .ExposeConfiguration(config =>
                {
                    config.EventListeners.PostCommitInsertEventListeners = new IPostInsertEventListener[]
                    {
                        new NHibernateEvents(eventHandler)
                    };

                    config.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[]
                    {
                        new NHibernateEvents(eventHandler)
                    };

                    config.EventListeners.PostCollectionUpdateEventListeners = new IPostCollectionUpdateEventListener[]
                    {
                        new NHibernateEvents(eventHandler)
                    };

                    config.EventListeners.PostCommitDeleteEventListeners = new IPostDeleteEventListener[]
                    {
                        new NHibernateEvents(eventHandler)
                    };
                });

            return configuration.BuildSessionFactory();
        }
    }
}

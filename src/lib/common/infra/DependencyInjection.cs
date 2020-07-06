using Common.Domain.Communication.Handlers;
using Common.Domain.Communication.Messages;
using Common.Domain.Data;
using Common.Domain.Multi;
using Common.Infra.Data;
using Common.Infra.Data.Dapper;
using Common.Infra.Data.EventSourcing;
using Common.Infra.Data.Nhibernate;
using Common.Infra.Extension;
using Common.Infra.Multi;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Common.Infra
{
    public static class DependencyInjection
    {
        public static void AddCommonDependencies(this IServiceCollection services)
        {
            // Dapper connection
            services.AddScoped<ConnectionScope>();

            // Event sourcing            
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            // NHibernate connection
            services.AddScoped(provider =>
            {
                var eventHandler = provider.GetService<EventHandler>();
                return new NHibernateSession(eventHandler).Build();
            });

            services.AddScoped(provider =>
            {
                var factory = provider.GetService<ISessionFactory>();
                return factory.OpenSession();
            });

            // Mediator
            services.AddScoped<EventHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Util
            services.AddSingleton<ICryptography, Cryptography>();
            services.AddSingleton<IPhoneNumber, PhoneNumber>();

            // Integration events

            // Translation
            services.AddTranslation();
        }
    }
}

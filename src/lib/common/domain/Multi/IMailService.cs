using System.Threading.Tasks;

namespace Common.Domain.Multi
{
    public interface IMailService
    {
        Task<bool> Send(string from, string to, string subject, string body);
    }
}

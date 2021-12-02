using Application.DTOs.Mail;
using System.Threading.Tasks;

namespace Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
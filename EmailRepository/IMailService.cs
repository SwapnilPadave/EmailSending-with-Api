using Email_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email_Repository
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        //Task GenerateQRCode(string QrCodeImage);

    }
}

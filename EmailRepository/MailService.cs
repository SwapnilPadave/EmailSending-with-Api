using Email_Model;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

using System.Net.Mail;
using System.Threading.Tasks;
using System;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Email_Repository
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        // async Task generateqrcode(string qrcodeimage)
        //{
        //    random r = new random();
        //    int number = r.next(10, 1000000);
        //    *
        //    qrcodegenerator qrcodegenerator = new qrcodegenerator();
        //    qrcodedata qrcodedata = qrcodegenerator.createqrcode("your booking no is:-" + number.tostring(), qrcodegenerator.ecclevel.q);
        //    qrcode qrcode = new qrcode(qrcodedata);
        //    using (memorystream ms = new memorystream())
        //    {
        //        using (bitmap bitmap = qrcode.getgraphic(20))
        //        {
        //            bitmap.save(ms, imageformat.png);
        //            string qrcodeimage = "data:image/png;base64," + convert.tobase64string(ms.toarray());
        //        }
        //    }
        //}

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();            
            builder.HtmlBody = mailRequest.Body;            
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        } 

        
    }
}

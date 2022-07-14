using Email_Model;
using Email_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Email_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailWithQrCodeController : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailWithQrCodeController(IMailService mailService)
        {
            this.mailService = mailService;
        }
       //public async Task<IActionResult> GenerateQrCode(string QrCodeImage)
       // {
       //     return Ok();
       // }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using Serilog;
using System.Configuration;
using System.Net.Mail;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class EmailSender
    {
        public void SendEmail(MailMessage message)
        {
            Log.Information($"Sending email {message.Subject}");
            new SmtpClient(ConfigurationManager.AppSettings["EmailServer"], 25).Send(message);
            Log.Information($"...Email sent successfully");
            //new SmtpClient("192.168.100.8", 25).Send(message);
        }
    }
}
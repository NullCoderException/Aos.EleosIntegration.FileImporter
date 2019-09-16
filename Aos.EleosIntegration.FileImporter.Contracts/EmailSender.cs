//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Aos.EleosIntegration.FileImporter.Contracts
{
    public class EmailSender
    {
        public void SendEmail(MailMessage message)
        {
            new SmtpClient(ConfigurationManager.AppSettings["EmailServer"], 25).Send(message);
            //new SmtpClient("192.168.100.8", 25).Send(message);
        }
    }
}
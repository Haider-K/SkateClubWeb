using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkateClubWeb.Models;

namespace SkateClubWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();
                //my email address
                mail.From = new MailAddress("SkateClubWeb@gmail.com");

                //To Email Address
                mail.To.Add("haiderkhan343@gmail.com");

                mail.Subject = sendMailDto.Subject;

                mail.IsBodyHtml = true;

                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Message : " + sendMailDto.Message;

                mail.Body = content;

                //create SMTP instant
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                //network credential
                NetworkCredential networkCredential = new NetworkCredential("SkateClubWeb@gmail.com", "Haider12.");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                ViewBag.Message = "Mail Send";

                //Create the mail state
                ModelState.Clear();

            } catch (Exception ex)
            {
                //If any error occured it will show this message
                ViewBag.Message = ex.Message.ToString();
            }


            return View();
        }
    }
}

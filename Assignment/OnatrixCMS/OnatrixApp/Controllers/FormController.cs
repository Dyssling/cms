using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnatrixApp.Models;
using OnatrixApp.Services;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixApp.Controllers
{
    public class FormController : SurfaceController
    {
        private readonly ServiceBusService _serviceBusService;
        public FormController(ServiceBusService serviceBusService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _serviceBusService = serviceBusService;
        }

        public async Task<IActionResult> HandleSubmit(CustomForm customForm)
        {
            var name = customForm.Name ?? null;
            var email = customForm.Email ?? null;
            var phone = customForm.Phone ?? null;
            var service = customForm.Service ?? null;
            var message = customForm.Message ?? null;

            bool isValid = true;

            if (name != null && name.Length < 2) //Om ett fält är null betyder det att fältet inte finns i formuläret, alltså har man valt att inte inkludera fältet. Då ska det fältet inte påverka valideringen.
            {
                isValid = false;
                ViewData["NameError"] = "Please enter a valid name.";
            }

            if (email != null && (!Regex.IsMatch(email, "^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$") || email.Length == 0))
            {
                isValid = false;
                ViewData["EmailError"] = "Please enter a valid email address.";
            }

            if (phone != null && (!Regex.IsMatch(phone, "^[0-9*#+ .()-]{7,}$") || phone.Length == 0))
            {
                isValid = false;
                ViewData["PhoneError"] = "Please enter a valid phone number.";
            }

            if (message != null && message.Length == 0)
            {
                isValid = false;
                ViewData["MessageError"] = "Please enter a message.";
            }

            if (!isValid) //Om formuläret inte godkänns så fylls alla fält i igen.
            {
                ViewData["NameValue"] = name;
                ViewData["EmailValue"] = email;
                ViewData["PhoneValue"] = phone;
                ViewData["ServiceValue"] = service;
                ViewData["MessageValue"] = message;

                return CurrentUmbracoPage();
            }

            else
            {
                ViewData["SuccessMessage"] = "Thank you! A confirmation has been sent to your email.";

                if (email != null)
                {
                    var emailRequestModel = new EmailRequestModel()
                    {
                        To = email,
                        Subject = "A Message from Onatrix",
                        HtmlContent = $"<html><body><h1>Thank you {name ?? ""}!</h1><p>We have received your message, and we will get back to you shortly!</p></body></html>",
                        PlainText = $"Thank you {name ?? ""}! We have received your message, and we will get back to you shortly!"
                    };

                    var serviceBusMessage = JsonConvert.SerializeObject(emailRequestModel);

                    await _serviceBusService.PublishAsync(serviceBusMessage);
                }
                return CurrentUmbracoPage();
            }

        }
    }
}

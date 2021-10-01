using FrontierAccountApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace FrontierAccountApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static ApiCaller ApiCaller;
        static Uri GetAllAccountsUri
        {
            get { return new Uri("https://frontiercodingtests.azurewebsites.net/api/accounts/getall"); } // instead of hardcoded URL, would like to add this into appsettings.
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = GetAllAccounts().Result;
            return View(model);
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

        private async Task<List<Account>> GetAllAccounts()
        {
            ApiCaller = new ApiCaller(new ApiCallerInfo { BaseAddress = GetAllAccountsUri });
            String result = await ApiCaller.CallApi();

            //There is probably a better way to convert the data. The deserialize method was giving me issues. 
            JArray jArray = JsonConvert.DeserializeObject<JArray>(result);
            return ConvertToAccountList(jArray);
        }

        private List<Account> ConvertToAccountList(JArray jArray)
        {
            List<Account> accounts = new List<Account>();
            foreach (JObject jObject in jArray)
            {
                Account account = new Account();
                account.Id = jObject["AccountStatusId"].ToObject<int>();
                account.FirstName = jObject["FirstName"].ToString();
                account.LastName = jObject["LastName"].ToString();
                account.Email = jObject["Email"].ToString();
               

                //Again probably a much cleaner way to accomplish this.
                try
                {
                    double phoneNum = jObject["PhoneNumber"].ToObject<Double>();
                    account.PhoneNumber = phoneNum.ToString("(###)-###-####");
                }
                catch
                {
                    account.PhoneNumber = null;
                }

                try
                {
                    account.AmountDue = jObject["AmountDue"].ToObject<double>();
                }
                catch 
                {
                    account.AmountDue = null;
                }
                try
                {
                    account.PaymentDueDate = jObject["PaymentDueDate"].ToObject<DateTime>();
                }
                catch
                {
                    account.PaymentDueDate = null;
                }
                
                account.AccountStatusId = jObject["AccountStatusId"].ToObject<int>();
                accounts.Add(account);
            }

            return accounts;
        }
    }
}

using Microsoft.Bot.Connector;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string userid)
        {
            ///// Here I'm using im-memory session,
            ///// but please use other session store for scaling. 
            Session["skypeuserid"] = userid;

            var authContext = new AuthenticationContext("https://login.microsoftonline.com/common");
            var authUri = authContext.GetAuthorizationRequestUrlAsync(
                "https://outlook.office365.com/",
                ConfigurationManager.AppSettings["ClientId"],
                new Uri($"{ConfigurationManager.AppSettings["AppWebSite"]}/Home/Authorize"),
                UserIdentifier.AnyUser,
                null);
            return Redirect(authUri.Result.ToString());

            //return Redirect("https://localhost/test");
        }

        public async Task<ActionResult> Authorize(string code)
        {
            // Get access token
            var authContext = new AuthenticationContext("https://login.microsoftonline.com/common");
            var authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
                code,
                new Uri($"{ConfigurationManager.AppSettings["AppWebSite"]}/Home/Authorize"),
                new ClientCredential(
                    ConfigurationManager.AppSettings["ClientId"],
                    ConfigurationManager.AppSettings["ClientSecret"]));

            // Store access token to bot state
            ///// Here we store the only access token.
            ///// Please store refresh token, too.
            var botCred = new MicrosoftAppCredentials(
                ConfigurationManager.AppSettings["MicrosoftAppId"],
                ConfigurationManager.AppSettings["MicrosoftAppPassword"]);
            var stateClient = new StateClient(botCred);
            BotState botState = new BotState(stateClient);
            BotData botData = new BotData(eTag: "*");
            botData.SetProperty<string>("AccessToken", authResult.AccessToken);
            await stateClient.BotState.SetUserDataAsync("skype", Session["skypeuserid"].ToString(), botData);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
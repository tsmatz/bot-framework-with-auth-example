This project uses deprecated (obsolete) API. The following is new one.
https://github.com/tsmatz/AuthDemoBot2




This solution is a super super simple bot sample code which is integrated with the OAuth authentication.
Note that, because we want to show you the core logic (the code which shows how to integrate the authentication), this sample doesn't implement the several security code.
Please add the security code for your production. (Please see the source code comment.)

For this usage, please see my blog post :
https://blogs.msdn.microsoft.com/tsmatsuz/2016/09/06/microsoft-bot-framework-bot-with-authentication-and-signin-login/

Thanks,

///// Step for the setup

We assume that you host your application in the following location :
AuthDemoBot - https://{your demo bot}
AuthDemoWeb - https://{your demo web}

1.Please register your bot in Bot Framework (https://dev.botframework.com/bots).
  - Copy your bot id (bot handle), app id, and app password (secret).
  - Set https://{your demo bot}/api/messages as webhook url

2.Please fill your "BotId", "MicrosoftAppId", and "MicrosoftAppPassword" in AuthDemoBot\Web.config.

3.Please fill your "MicrosoftAppId" and "MicrosoftAppPassword" in AuthDemoWeb\Web.config.

4.Login to Azure Portal (https://portal.azure.com/) with Office 365 administrator account. (You need your organization account.)
  Go to Azure Active Directory management, and register your application.
  - Application type must be "Web app / API"
  - Copy app id (client id)
  - Create key (client secret) and copy the value
  - Register "https://{your demo web}/*" as Reply URLs.
  - Register the required permission "Read user mail" (Mail.Read) in "Office 365 Exchange Online"
  - Select "Yes" on "Multi-tenanted"

5.Please fill your "ClientId" and "ClientSecret" in AuthDemoWeb\Web.config.

6.Please fill https://{your demo web} as "AppWebSite" in both AuthDemoBot\Web.config and AuthDemoWeb\Web.config.

7.Host both your bot (https://{your demo bot}) and your web (https://{your demo web}) in the internet.

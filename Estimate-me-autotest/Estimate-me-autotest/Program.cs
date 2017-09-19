using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Estimate_me_autotest.ReusableRemoteWebDriverSettings;
using Estimate_me_autotest.Pages;


namespace Estimate_me_autotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = ReusableRemoteWebDriver.CreateRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());
             driver = ReusableRemoteWebDriver.LoadRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());

            LoginPage loginPage = new LoginPage(driver);
            loginPage.GoToLoginPage();
            loginPage.Submit("qaaestimateadmin", "qa1234");

          }
    }
}

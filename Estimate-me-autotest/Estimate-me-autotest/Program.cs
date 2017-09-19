using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Estimate_me_autotest.ReusableRemoteWebDriverSettings;
using Estimate_me_autotest.Pages;


namespace Estimate_me_autotest
{
    class Program
    {
        static void Main(string[] args)
        {
           // var driver = ReusableRemoteWebDriver.CreateRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());
            var driver = ReusableRemoteWebDriver.LoadRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());
            driver.Navigate().GoToUrl("http://estimate-qa.simbirsoft/login");
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginInput.TypeText("qaaestimateadmin");
            loginPage.PasswordInput.TypeText("qa1234");
            loginPage.LoginButton.Click();


        }
    }
}

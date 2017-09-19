using NUnit.Framework;
using System;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Estimate_me_autotest.ReusableRemoteWebDriverSettings;
using Estimate_me_autotest.Pages;
using RestSharp;
using Newtonsoft.Json;


namespace Estimate_me.Tests
{
    [TestFixture]
    public class LoginTests
    {
       public static RemoteWebDriver Driver;

        private string mainPageUrl = "http://estimate-qa.simbirsoft";
        private string loginPageUrl = "http://estimate-qa.simbirsoft/login";
        private string estimatesPageUrl = "http://estimate-qa.simbirsoft/estimates";


        [SetUp]
        public void SetUp()
        {
            Driver = ReusableRemoteWebDriver.CreateRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());
            Driver = ReusableRemoteWebDriver.LoadRemoteSession(new Uri("http://localhost:4444/wd/hub"), new ChromeOptions().ToCapabilities());
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }

        [Test]
        public void AuthorizationWithNonExistentLoginAndPassword()
        {
            
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.GoToLoginPage();
            loginPage.Submit("11qaaestimateadmin22", "qa1234");

            new WebDriverWait(Driver, TimeSpan.FromSeconds(15))
                .Until(ExpectedConditions.ElementIsVisible(By.XPath(loginPage.ErrorAuthElementXPathRoot)));
                 //driver => driver.FindElement(By.XPath(loginPage.ErrorAuthElementXPathRoot)).Enabled
                 //              && driver.FindElement(By.XPath(loginPage.ErrorAuthElementXPathRoot)).Displayed);

            Assert.AreEqual(loginPage.ErrorAuthElement, "User not found", "Сообщение об ошибке  не показано");
            Assert.AreEqual(Driver.Url, loginPageUrl, "Неверный редирект");
        }

        [Test]
        public void AuthorizationWithCorrectLoginAndEmptyPassword()
        {
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.GoToLoginPage();
            loginPage.Submit("qaaestimateadmin", "");
            Assert.AreEqual(Driver.Url, loginPageUrl, "Неверный редирект");
        }

        [Test]
        public void AuthorizationWithCorrectLoginAndPassword()
        {
            //Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.GoToLoginPage();
            loginPage.Submit("qaaestimateadmin", "qa1234");

           (new WebDriverWait(Driver, TimeSpan.FromSeconds(10))).
                Until(ExpectedConditions.UrlToBe(estimatesPageUrl));

            Assert.AreEqual(Driver.Url, estimatesPageUrl, "Неверный редирект");
        }

        [Test]
        public void AuthorizationWithEmptyLoginAndEmptyPassword()
        {
            //Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            LoginPage loginPage = new LoginPage(Driver);
            loginPage.GoToLoginPage();
            loginPage.Submit("", "");

            Assert.AreEqual(Driver.Url, loginPageUrl, "Неверный редирект");
        }

        [Test]
        public void CheckRedirectTOLoginPage()
        {
            Driver.Navigate().GoToUrl(mainPageUrl);

            (new WebDriverWait(Driver, TimeSpan.FromSeconds(10))).
               Until(ExpectedConditions.UrlToBe(loginPageUrl));

            Assert.AreEqual(Driver.Url, loginPageUrl, "Неверный редирект");
        }


        public void CheckAuthorizationAPI()
        {
            var client = new RestClient(loginPageUrl);
            var request = new RestRequest();
            string strJsonContent = @"{ ""login"":""qaaestimateadmin"", ""password"":""qa1234""}";

            request.Resource = "/api/auth/login";
            request.AddHeader("Content-Type", "application/json");        
            request.Method = Method.POST;
            request.AddParameter("application/json; charset=utf-8", strJsonContent, ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content;

            

            Assert.NotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Неверный код ответа " + response.StatusCode.ToString());
            // Assert.AreEqual(response.Content,


           


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimate_me_autotest.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using Estimate_me_autotest.Components.Common;

namespace Estimate_me_autotest.Pages
{
    public class LoginPage : BasePage<LoginPage>
    {

        private string loginPageUrl = "http://estimate-qa.simbirsoft/login";

        public LoginPage(RemoteWebDriver driver) : base(driver, () => new LoginPage(driver))
        {
        }
        
        public void GoToLoginPage()
        {
            Driver.Navigate().GoToUrl(loginPageUrl);
        }

        public string ErrorAuthElementXPathRoot = "//div[contains(@class, 'error-msg ng-binding')]";

        public string ErrorAuthElement => Driver.FindElement(By.XPath(ErrorAuthElementXPathRoot)).Text;

        public SimpleButton<LoginPage> LoginButton =>
            new SimpleButton<LoginPage>(Driver, "//div/button[contains(@class,'login-btn')]", () => new LoginPage(Driver));

        public SimpleInput<LoginPage> LoginInput =>
            new SimpleInput<LoginPage>(Driver, ".//*[@id='input_0']", () => new LoginPage(Driver));

        public SimpleInput<LoginPage> PasswordInput =>
            new SimpleInput<LoginPage>(Driver, ".//*[@id='input_1']", () => new LoginPage(Driver));

        public void Submit(string login, string password)
        {
            LoginInput.TypeText(login);
            PasswordInput.TypeText(password);
            LoginButton.Click();
            
        }

  

    }
}

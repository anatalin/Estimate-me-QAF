using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Estimate_me_autotest.Framework;

namespace Estimate_me_autotest.Components.Common
{
    public class SimpleInput<T> : BaseComponent<T> where T : BasePage<T>
    {
        public SimpleInput(RemoteWebDriver driver, string xpath, Func<T> page)
          : base(driver, xpath, page)
        {
        }

        public T TypeText(string text)
        {
            (new WebDriverWait(Driver, TimeSpan.FromSeconds(5))).Until(driver => driver.FindElement(By.XPath(RootXPath))).SendKeys(text);
            return Page();

        }

        public string Text()
        {
            return (new WebDriverWait(Driver, TimeSpan.FromSeconds(5))).Until(driver => driver.FindElement(By.XPath(RootXPath))).Text;
        }

      

        public T Confirm()
        {
            (new WebDriverWait(Driver, TimeSpan.FromSeconds(5))).Until(driver => driver.FindElement(By.XPath(RootXPath))).SendKeys(Keys.Enter);
            return Page();

        }

    }

}

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
    public class SimpleButton<T> : BaseComponent<T> where T : BasePage <T>
    {
        private readonly Action _afterClickAction;
        private readonly Action _beforeClickAction;

        public SimpleButton(RemoteWebDriver driver, string xpath, Func<T> page, Action beforeClickAction = null,
            Action afterClickAction = null) :base(driver, xpath, page)
        {
            _beforeClickAction = beforeClickAction;
            _afterClickAction = afterClickAction;
        }

        public T Click()
        {
            _beforeClickAction?.Invoke();

           (new WebDriverWait(Driver, TimeSpan.FromSeconds(5))).Until(driver => driver.FindElement(By.XPath(RootXPath))).Click();

            _afterClickAction?.Invoke();
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;

namespace Estimate_me_autotest.Framework
{
    public class BasePage<T> :IDriver where T : BasePage<T>
    {
        public BasePage(RemoteWebDriver driver, Func<T> page)
        {
            Driver = driver;
            Page = page;
            WindowsHandle = driver.CurrentWindowHandle;
        }

        public string WindowsHandle { get; }

        public Func<T> Page { get; }

        public RemoteWebDriver Driver { get; }

        public T SwitchToRememberedHAndle()
        {
            Driver.SwitchTo().Window(WindowsHandle);
            return Page();
        }

    }
}

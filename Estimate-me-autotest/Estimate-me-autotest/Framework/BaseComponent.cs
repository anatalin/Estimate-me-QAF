using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;

namespace Estimate_me_autotest.Framework
{
    public class BaseComponent<T> : IDriver, IRootXPath
        where T : BasePage<T>
    {
        public BaseComponent(RemoteWebDriver driver, string rootXPath, Func<T> page)
        {
            Driver = driver;
            RootXPath = rootXPath;
            Page = page;
        }

        public Func<T> Page { get; }
        public RemoteWebDriver Driver { get; }
        public string RootXPath { get; }

    }
}

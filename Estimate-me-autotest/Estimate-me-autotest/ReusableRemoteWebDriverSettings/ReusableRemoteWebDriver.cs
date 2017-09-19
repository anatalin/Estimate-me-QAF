using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Estimate_me_autotest.ReusableRemoteWebDriverSettings
{
    public class ReusableRemoteWebDriver
    {
        public static RemoteWebDriver CreateRemoteSession(Uri remoteAddress, ICapabilities desiredCapabilities)
        {
            return new SerializableRemoteDriver(remoteAddress, desiredCapabilities);
        }

        public static RemoteWebDriver LoadRemoteSession(Uri remoteAddress, ICapabilities desiredCapabilities)
        {
            return new DeserializableRemoteDriver(remoteAddress, desiredCapabilities);
        }
    }
}

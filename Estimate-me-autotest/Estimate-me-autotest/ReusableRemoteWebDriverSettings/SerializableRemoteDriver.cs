using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;

namespace Estimate_me_autotest.ReusableRemoteWebDriverSettings
{
    public class SerializableRemoteDriver : RemoteWebDriver
    {

        public SerializableRemoteDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            :base(remoteAddress, desiredCapabilities, RemoteWebDriver.DefaultCommandTimeout)
        {

        }

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            Response commandResponse = new Response();
            if(driverCommandToExecute == DriverCommand.NewSession)
            {
                var filePath = $"{CommonRemoteDriver.GetReusableRemoteWebdriverDirectory()}/default.json";
                commandResponse = base.Execute(driverCommandToExecute, parameters);

                Directory.CreateDirectory(CommonRemoteDriver.GetReusableRemoteWebdriverDirectory());
                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.Create(filePath).Dispose();
                File.WriteAllText(filePath, commandResponse.ToJson());
            }
            else
            {
                commandResponse = base.Execute(driverCommandToExecute, parameters);
            }
            return commandResponse;   
        }
    }
}

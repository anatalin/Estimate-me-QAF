using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Estimate_me_autotest.ReusableRemoteWebDriverSettings
{
    class DeserializableRemoteDriver : RemoteWebDriver
    {
        public DeserializableRemoteDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities, RemoteWebDriver.DefaultCommandTimeout)
        {

        }

        override protected Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            Response commandResponse = new Response();
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                var json = File.ReadAllText($"{CommonRemoteDriver.GetReusableRemoteWebdriverDirectory()}/default.json");
                commandResponse = Response.FromJson(json);

                var definition = new { SessionId = "", Status = 0, IsSpecificationCompliant = false };
                var session = JsonConvert.DeserializeAnonymousType(json, definition);
                commandResponse.SessionId = session.SessionId;
            }
            else
            {
                commandResponse = base.Execute(driverCommandToExecute, parameters);
            }
            return commandResponse;
        }
    }
}

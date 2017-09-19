using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Estimate_me_autotest.ReusableRemoteWebDriverSettings
{
    internal static class CommonRemoteDriver
    {
        public static string GetUserDirectory()
        {
            var path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
                path = Directory.GetParent(path).ToString();
            return path;
        }

        public static string GetReusableRemoteWebdriverDirectory()
        {
            return $"{GetUserDirectory()}/.ReusableRemoteWebdriver";
        }

        public static void UnpackAndThrowOnError(Response errorResponse)
        {
            // Check the status code of the error, and only handle if not success.
            if (errorResponse.Status != WebDriverResult.Success)
            {
                var errorAsDictionary = errorResponse.Value as Dictionary<string, object>;
                if (errorAsDictionary != null)
                {
                    var errorResponseObject = new ErrorResponse(errorAsDictionary);
                    var errorMessage = errorResponseObject.Message;
                    switch (errorResponse.Status)
                    {
                        case WebDriverResult.NoSuchElement:
                            throw new NoSuchElementException(errorMessage);

                        case WebDriverResult.NoSuchFrame:
                            throw new NoSuchFrameException(errorMessage);

                        case WebDriverResult.UnknownCommand:
                            throw new NotImplementedException(errorMessage);

                        case WebDriverResult.ObsoleteElement:
                            throw new StaleElementReferenceException(errorMessage);

                        case WebDriverResult.ElementNotDisplayed:
                            throw new ElementNotVisibleException(errorMessage);

                        case WebDriverResult.InvalidElementState:
                        case WebDriverResult.ElementNotSelectable:
                            throw new InvalidElementStateException(errorMessage);

                        case WebDriverResult.UnhandledError:
                            throw new InvalidOperationException(errorMessage);

                        case WebDriverResult.NoSuchDocument:
                            throw new NoSuchElementException(errorMessage);

                        case WebDriverResult.Timeout:
                            throw new WebDriverTimeoutException(errorMessage);

                        case WebDriverResult.NoSuchWindow:
                            throw new NoSuchWindowException(errorMessage);

                        case WebDriverResult.InvalidCookieDomain:
                        case WebDriverResult.UnableToSetCookie:
                            throw new WebDriverException(errorMessage);

                        case WebDriverResult.AsyncScriptTimeout:
                            throw new WebDriverTimeoutException(errorMessage);

                        case WebDriverResult.UnexpectedAlertOpen:
                            // TODO(JimEvans): Handle the case where the unexpected alert setting
                            // has been set to "ignore", so there is still a valid alert to be
                            // handled.
                            var alertText = string.Empty;
                            if (errorAsDictionary.ContainsKey("alert"))
                            {
                                var alertDescription = errorAsDictionary["alert"] as Dictionary<string, object>;
                                if (alertDescription != null && alertDescription.ContainsKey("text"))
                                    alertText = alertDescription["text"].ToString();
                            }

                            throw new UnhandledAlertException(errorMessage, alertText);

                        case WebDriverResult.NoAlertPresent:
                            throw new NoAlertPresentException(errorMessage);

                        case WebDriverResult.InvalidSelector:
                            throw new InvalidSelectorException(errorMessage);

                        case WebDriverResult.NoSuchDriver:
                            throw new WebDriverException(errorMessage);

                        default:
                            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "{0} ({1})",
                                errorMessage, errorResponse.Status));
                    }
                }
                throw new WebDriverException("Unexpected error. " + errorResponse.Value);
            }
        }
    }
}
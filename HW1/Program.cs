using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace HW1
{
    [TestFixture]
    class Program
    {
        readonly IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void Initialize()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.google.com/");
        }

        [Test]
        public void ExecuteTest()
        {
            IWebElement element = driver.FindElement(By.Name("q"));
            element.SendKeys("cheese");
            element.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });

            Console.WriteLine("Page title is: =" + driver.Title);

            List<IWebElement> listElement1 = new List<IWebElement>(driver.FindElements(By.XPath("//div[@class ='hdtb-mitem']")));
            listElement1[0].Click();

            List<IWebElement> listElement2 = new List<IWebElement>(driver.FindElements(By.XPath("//img[contains(@src,'image/jpeg')]")));
            Assert.NotNull(listElement2[15].Displayed);
            listElement2[15].Click();

            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(@"D:\Паша\EPAM\LAB\HomeWork\HW1\SeleniumTestingScreenshot.jpg", ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }

    }
}

using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test2
    {
        private IWebDriver driver = null!;
        private StringBuilder verificationErrors = null!;
        private WebDriverWait wait = null!;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver?.Quit();
                driver?.Dispose();
            }
            catch
            {
                // ignore
            }

            // kompatibilno s NUnit 3/4
            Assert.That(verificationErrors.ToString(), Is.EqualTo(""));
        }

        [Test]
        public void TC_02_Registracija_Test()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/register");

            // čekaj da se forma pojavi
            wait.Until(d => d.FindElement(By.Id("FirstName")));

            driver.FindElement(By.Id("gender-male")).Click();

            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("Test1");

            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("Test1");

            // unikatan email (da test svaki put prođe)
            string email = $"test{DateTime.Now.Ticks}@gmail.com";
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(email);

            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Test123!");

            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Test123!");

            driver.FindElement(By.Id("register-button")).Click();

            // provjeri uspjeh registracije (poruka na stranici)
            wait.Until(d => d.PageSource.Contains("Your registration completed"));
            Assert.That(driver.PageSource.Contains("Your registration completed"));
        }
    }
}

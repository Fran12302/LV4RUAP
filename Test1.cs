using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace RuapLV4
{
    [TestFixture]
    public class UntitledTestCase
    {
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void RegisterNewUserTest()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            driver.FindElement(By.LinkText("Register")).Click();
            driver.FindElement(By.Id("gender-male")).Click();

            driver.FindElement(By.Id("FirstName")).SendKeys("Test");
            driver.FindElement(By.Id("LastName")).SendKeys("User");

            // JEDINSTVEN EMAIL (da test uvijek prolazi)
            string email = $"test{DateTime.Now.Ticks}@gmail.com";
            driver.FindElement(By.Id("Email")).SendKeys(email);

            driver.FindElement(By.Id("Password")).SendKeys("Test123!");
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("Test123!");

            driver.FindElement(By.Id("register-button")).Click();

            // Provjera da je registracija uspjela
            Assert.That(driver.PageSource.Contains("Your registration completed"));
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}


using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test3_Login
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

       
        private const string Email = "test123451@gmail.com";
        private const string Password = "123456";

        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TC_03_Login_Existing_User()
        {
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            driver.FindElement(By.LinkText("Log in")).Click();

            wait.Until(d => d.FindElement(By.Id("Email")));

            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(Email);

            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(Password);

            driver.FindElement(By.CssSelector("input.button-1.login-button")).Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/logout']")));
            Assert.That(driver.PageSource.Contains("Log out"));
        }

        [TearDown]
        public void Teardown()
        {
            try { driver.Quit(); } catch { }
            try { driver.Dispose(); } catch { }
        }
    }
}

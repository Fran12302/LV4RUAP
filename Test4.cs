using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test4_UpdateCustomerInfo
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        // TODO: upiši isti postojeći račun kao u Test3
        private const string Email = "test123451@gmail.com";
        private const string Password = "123456";

        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void Login()
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
        }

        [Test]
        public void TC_04_Update_LastName_In_MyAccount()
        {
            Login();

            driver.FindElement(By.LinkText("My account")).Click();
            wait.Until(d => d.FindElement(By.Id("LastName")));

            string newLastName = "Test" + DateTime.Now.Ticks.ToString().Substring(10);

            var ln = driver.FindElement(By.Id("LastName"));
            ln.Clear();
            ln.SendKeys(newLastName);

            driver.FindElement(By.CssSelector("input.button-1.save-customer-info-button")).Click();

            // provjera: refresh i provjeri value
            driver.Navigate().Refresh();
            wait.Until(d => d.FindElement(By.Id("LastName")));
            string saved = driver.FindElement(By.Id("LastName")).GetAttribute("value");

            Assert.That(saved, Is.EqualTo(newLastName));
        }

        [TearDown]
        public void Teardown()
        {
            try { driver.Quit(); } catch { }
            try { driver.Dispose(); } catch { }
        }
    }
}

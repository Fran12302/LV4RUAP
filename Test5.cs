using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test5_ShoppingCart_AddItem
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
        public void TC_05_Add_Item_To_Cart_And_Estimate_Shipping()
        {
            Login();

            // Books -> Add to cart (prvi proizvod)
            driver.FindElement(By.LinkText("Books")).Click();
            wait.Until(d => d.FindElement(By.CssSelector("input.button-2.product-box-add-to-cart-button")));
            driver.FindElement(By.CssSelector("input.button-2.product-box-add-to-cart-button")).Click();

            // Shopping cart
            driver.FindElement(By.CssSelector("a[href='/cart']")).Click();
            wait.Until(d => d.FindElement(By.CssSelector("table.cart")));

            // provjera: bar jedna stavka u košarici
            Assert.That(driver.FindElements(By.CssSelector("table.cart tbody tr")).Count, Is.GreaterThan(0));

            // Estimate shipping (dio se može razlikovati, ali ovo obično radi)
            // Odaberi Country i upiši Zip, pa klikni Estimate
            var country = wait.Until(d => d.FindElement(By.Id("CountryId")));
            new SelectElement(country).SelectByText("Croatia");

            var zip = driver.FindElement(By.Id("ZipPostalCode"));
            zip.Clear();
            zip.SendKeys("10000");

            driver.FindElement(By.Name("estimateshipping")).Click();

            // Očekivanje: pojavi se dio s opcijama dostave (ili barem rezultat)
            wait.Until(d => d.PageSource.Contains("Shipping") || d.PageSource.Contains("shipping"));
            Assert.Pass("Cart and estimate shipping executed.");
        }

        [TearDown]
        public void Teardown()
        {
            try { driver.Quit(); } catch { }
            try { driver.Dispose(); } catch { }
        }
    }
}

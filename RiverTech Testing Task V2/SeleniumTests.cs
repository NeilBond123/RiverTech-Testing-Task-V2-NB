using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace RiverTech_Testing_Task_V2
{
    // Page Object for the Login Page
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Method to perform login
        public void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login-button")).Click();
        }
    }

    // Page Object for the Cart Page
    public class CartPage
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Method to add an item to the cart
        public void AddToCart()
        {
            driver.FindElement(By.XPath("//*[@id=\"add-to-cart-sauce-labs-fleece-jacket\"]")).Click();
        }

        // Method to proceed to checkout
        public void ProceedToCheckout()
        {
            driver.FindElement(By.CssSelector(".shopping_cart_link")).Click();
            driver.FindElement(By.Id("checkout")).Click();
        }
    }

    // Page Object for the Checkout Page
    public class CheckoutPage
    {
        private IWebDriver driver;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Method to fill checkout information
        public void FillCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            driver.FindElement(By.Id("first-name")).SendKeys(firstName);
            driver.FindElement(By.Id("last-name")).SendKeys(lastName);
            driver.FindElement(By.Id("postal-code")).SendKeys(postalCode);
            driver.FindElement(By.Id("continue")).Click();
        }

        // Method to assert the subtotal
        public void AssertSubtotal(string expectedSubtotal)
        {
            Assert.Equal(expectedSubtotal, driver.FindElement(By.XPath("//*[@id=\"checkout_summary_container\"]/div/div[2]/div[6]")).Text);
        }

        // Method to assert the tax
        public void AssertTax(string expectedTax)
        {
            Assert.Equal(expectedTax, driver.FindElement(By.XPath("//*[@id=\"checkout_summary_container\"]/div/div[2]/div[7]")).Text);
        }

        // Method to assert the total
        public void AssertTotal(string expectedTotal)
        {
            Assert.Equal(expectedTotal, driver.FindElement(By.XPath("//*[@id=\"checkout_summary_container\"]/div/div[2]/div[8]")).Text);
        }

        // Method to finish the checkout process
        public void FinishCheckout()
        {
            driver.FindElement(By.Id("finish")).Click();
        }

        // Method to assert order confirmation
        public void AssertOrderConfirmation(string expectedURL, string expectedMessage)
        {
            Assert.Equal(expectedURL, driver.Url);
            Assert.Equal(expectedMessage, driver.FindElement(By.XPath("//*[@id=\"checkout_complete_container\"]/div"))
                .Text);
        }
    }

    // Test class for Selenium tests
    public class SeleniumTests
    {
        private IWebDriver driver;

        public SeleniumTests()
        {
            driver = new ChromeDriver(); // Initialize Chrome WebDriver
        }

        [Fact]
        public void CreatePurchase()
        {
            // Initialize Page Objects
            LoginPage loginPage = new LoginPage(driver);
            CartPage cartPage = new CartPage(driver);
            CheckoutPage checkoutPage = new CheckoutPage(driver);

            // Open the web app
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            loginPage.Login("standard_user", "secret_sauce");

            // Add item to the cart
            cartPage.AddToCart();

            // Open the cart and proceed to checkout
            cartPage.ProceedToCheckout();

            // Input information and continue
            checkoutPage.FillCheckoutInformation("John", "Doe", "12345");

            // Assert values on the checkout page
            checkoutPage.AssertSubtotal("Item total: $49.99");
            checkoutPage.AssertTax("Tax: $4.00");
            checkoutPage.AssertTotal("Total: $53.99");

            // Click finish
            checkoutPage.FinishCheckout();

            // Verify order dispatch through URL and message
            checkoutPage.AssertOrderConfirmation("https://www.saucedemo.com/checkout-complete.html",
                "Your order has been dispatched, and will arrive just as fast as the pony can get there!");

            // Quit the browser
            driver.Quit();
        }
    }
}

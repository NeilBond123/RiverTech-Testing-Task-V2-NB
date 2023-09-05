using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace RiverTech_Testing_Task_V2
{
    public class SeleniumTests
    {
        private IWebDriver driver;

        public SeleniumTests()
        {
            driver = new ChromeDriver(); // Initialize Chrome WebDriver
        }

        [Fact]
        public void TestExecution()
        {
            // For element identification and selection, we make use of CSS selectors, ID's and XPath for demonstration purposes.

            // Open the web app
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            // Add item to the cart
            driver.FindElement(By.XPath("//*[@id=\"add-to-cart-sauce-labs-fleece-jacket\"]")).Click();

            // Open the cart and proceed to checkout
            driver.FindElement(By.CssSelector(".shopping_cart_link")).Click();
            driver.FindElement(By.Id("checkout")).Click();

            // Input information and continue
            driver.FindElement(By.Id("first-name")).SendKeys("John");
            driver.FindElement(By.Id("last-name")).SendKeys("Doe");
            driver.FindElement(By.Id("postal-code")).SendKeys("12345");
            driver.FindElement(By.Id("continue")).Click();

            // Assert values on the next page
            AssertValue(driver, "//*[@id=\"checkout_summary_container\"]/div/div[2]/div[6]", "Item total: $49.99");
            AssertValue(driver, "//*[@id=\"checkout_summary_container\"]/div/div[2]/div[7]", "Tax: $4.00");
            AssertValue(driver, "//*[@id=\"checkout_summary_container\"]/div/div[2]/div[8]", "Total: $53.99");

            // Click finish
            driver.FindElement(By.Id("finish")).Click();

            // Verify order dispatch through URL and message
            string URL = driver.Url.ToString();

            AssertValueURL(driver, URL, "https://www.saucedemo.com/checkout-complete.html");
            AssertValue(driver, "//*[@id=\"checkout_complete_container\"]/div", "Your order has been dispatched, " +
                "and will arrive just as fast as the pony can get there!");

            // Quit the browser
            driver.Quit();
        }

        static void AssertValue(IWebDriver driver, string selector, string expectedValue)
        {
            string actualValue = driver.FindElement(By.XPath(selector)).Text;
            if (actualValue != expectedValue)
            {
                throw new Exception($"Assertion failed: Expected {expectedValue}, but got {actualValue}");
            }
        }

        static void AssertValueURL(IWebDriver driver, string url, string expectedValue)
        {
            string actualValue = driver.Url.ToString();
            if (actualValue != expectedValue)
            {
                throw new Exception($"Assertion failed: Expected {expectedValue}, but got {actualValue}");
            }
        }
    }
}

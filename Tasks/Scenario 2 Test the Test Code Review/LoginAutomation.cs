using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Tasks.Scenario_2_Test_the_Test_Code_Review
{
    public class LoginAutomation
    {
        // Move driver to the top and make it private
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver("path_to_driver");
            _driver.Manage().Window.Maximize();
        }

        // Add new attribute Test for the test logic
        [Test]
        public void Login()
        {
            _driver.Navigate().GoToUrl("https://qa.sorted.com/newtrack");

            // Change Thread.Sleep to ImplicitWait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Make find element by Xpath instead of Id
            IWebElement username = _driver.FindElement(By.XPath("//form[@id='loginForm']/input[1]"));

            IWebElement password = _driver.FindElement(By.XPath("//form[@id='loginForm']/input[2]"));

            // Make find element by Id instead of Xpath (and driver.find_element_by_xpath method doesn`t exist)
            IWebElement loginButton = _driver.FindElement(By.Id("submit"));

            // Need to move test data to separate class
            string usernameValue = "john_smith@sorted.com";
            string passwordValue = "Pa55w0rd!";

            username.SendKeys(usernameValue);
            password.SendKeys(passwordValue);
            loginButton.Click();

            // Use ExplicitWait for url update
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => !string.IsNullOrEmpty(driver.Url));

            // Assert.Equals not recommended for using. Use Assert.That instead
            Assert.That(_driver.Url, Is.EqualTo("http://qa.sorted.com/newtrack/loginSuccess"));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
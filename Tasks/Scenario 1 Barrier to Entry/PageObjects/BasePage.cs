using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tasks.Scenario_1_Barrier_to_Entry.PageObjects
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Open(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public string GetCurrentUrl()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => !string.IsNullOrEmpty(driver.Url));
            return Driver.Url;
        }
    }
}

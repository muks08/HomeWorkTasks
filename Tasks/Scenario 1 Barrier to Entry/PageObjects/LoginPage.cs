using OpenQA.Selenium;

namespace Tasks.Scenario_1_Barrier_to_Entry.PageObjects
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        private By EmailInputLocator => By.XPath("//form[@id='loginForm']/input[1]");
        private By PasswordInputLocator => By.XPath("//form[@id='loginForm']/input[2]");
        private By LoginButtonLocator => By.Id("submit");

        private IWebElement EmailInput => Driver.FindElement(EmailInputLocator);
        private IWebElement PasswordInput => Driver.FindElement(PasswordInputLocator);
        private IWebElement LoginButton => Driver.FindElement(LoginButtonLocator);

        public void Login(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
    }
}

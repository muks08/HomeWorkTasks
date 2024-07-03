using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tasks.Scenario_1_Barrier_to_Entry.PageObjects;
using TechTalk.SpecFlow;

namespace Tasks.Scenario_1_Barrier_to_Entry.StepDefinitions
{
    [Binding]
    public class LoginFunctionalityStepDefinitions
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private HomePage _homePage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new ChromeDriver("path_to_driver");
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }

        [Given(@"I’m not logged in with a genuine user\.")]
        public void GivenIMNotLoggedInWithAGenuineUser_()
        {
            _homePage = new HomePage(_driver);
        }

        [When(@"I navigate to any page on the tracking site\.")]
        public void WhenINavigateToAnyPageOnTheTrackingSite_()
        {
            _homePage.Open("https://qa.sorted.com/homepage");
        }

        [Then(@"I am presented with a login screen\.")]
        public void ThenIAmPresentedWithALoginScreen_()
        {
            Assert.That(_homePage.GetCurrentUrl(), Is.EqualTo("http://qa.sorted.com/newtrack"));
        }

        [Given(@"valid user credentials are already registered\.")]
        public void GivenValidUserCredentialsAreAlreadyRegistered_()
        {
            _loginPage = new LoginPage(_driver);
        }

        [Given(@"I’m on the login screen\.")]
        public void GivenIMOnTheLoginScreen_()
        {
            _loginPage.Open("https://qa.sorted.com/newtrack");
        }

        [When(@"I enter a valid username and password and submit\.")]
        public void WhenIEnterAValidUsernameAndPasswordAndSubmit_()
        {
            _loginPage.Login(TestData.LoginTestData.Email, TestData.LoginTestData.Password);
        }

        [Then(@"I am logged in successfully\.")]
        public void ThenIAmLoggedInSuccessfully_()
        {
            Assert.That(_loginPage.GetCurrentUrl(), Is.EqualTo("http://qa.sorted.com/newtrack/loginSuccess"));
        }
    }
}

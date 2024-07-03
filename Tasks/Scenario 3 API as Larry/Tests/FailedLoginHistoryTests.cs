using NUnit.Framework;
using Tasks.Scenario_3_API_as_Larry.Services;

namespace Tasks.Scenario_3_API_as_Larry.Tests
{
    public class FailedLoginHistoryTests
    {
        private LoginService _loginService;

        [SetUp]
        public void Setup()
        {
            _loginService = new LoginService("https://api.com/");
        }

        [Test]
        public async Task GetLoginFailTotalReturnsAllUsersWhenNoParams()
        {
            var userNames = new List<string>
            {
                "testUser1",
                "testUser2",
                "testUser3"
            };

            foreach (var userName in userNames)
            {
                await _loginService.PutResetLoginFailTotal(userName);
                await _loginService.PostLogin(userName, "wrong_password");
            }

            var response =  await _loginService.GetLoginFailTotal();

            Assert.That(response.Results.Count, Is.EqualTo(userNames.Count));
            Assert.That(response.Results.Select(r => r.UserName), Is.EquivalentTo(userNames));
        }

        [Test]
        public async Task GetLoginFailTotalReturnsSpecificUser()
        {
            string userName = "testUser";
            int failCount = 5;

            await _loginService.PutResetLoginFailTotal(userName);

            for (int i = 0; i < failCount; i++)
            {
                await _loginService.PostLogin(userName, "wrong_password");
            }
            
            var response = await _loginService.GetLoginFailTotal(userName);

            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results.First().UserName, Is.EqualTo(userName));
            Assert.That(response.Results.First().FailCount, Is.EqualTo(failCount));
        }

        [Test]
        public async Task GetLoginFailTotalReturnsUsersAboveFailCount()
        {
            string userName = "testUser";
            int failCount = 3;

            await _loginService.PutResetLoginFailTotal(userName);

            for (int i = 0; i < failCount; i++)
            {
                await _loginService.PostLogin(userName, "wrong_password");
            }

            var response = await _loginService.GetLoginFailTotalWithFailCount(failCount);

            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results.First().UserName, Is.EqualTo(userName));
            Assert.That(response.Results.First().FailCount, Is.EqualTo(failCount));

            await _loginService.PostLogin(userName, "wrong_password");

            Assert.That(response.Results.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetLoginFailTotalReturnsResultsWithFetchLimit()
        {
            int fetchLimit = 3;

            var userNames = new List<string>
            {
                "testUser1",
                "testUser2",
                "testUser3",
                "testUser4",
                "testUser5"
            };

            foreach (var userName in userNames)
            {
                await _loginService.PutResetLoginFailTotal(userName);
                await _loginService.PostLogin(userName, "wrong_password");
            }

            var response = await _loginService.GetLoginFailTotalWithFetchLimit(fetchLimit);

            Assert.That(response.Results.Count, Is.EqualTo(fetchLimit));
        }

        [Test]
        public async Task PutResetLoginFailTotalResetsSpecificUser()
        {
            var userNames = new List<string>
            {
                "testUser1",
                "testUser2",
                "testUser3"
            };

            foreach (var userName in userNames)
            {
                await _loginService.PostLogin(userName, "wrong_password");
            }

            var responseWithLoginFails = await _loginService.GetLoginFailTotal();

            Assert.That(responseWithLoginFails.Results.Count, Is.EqualTo(userNames.Count));
            Assert.That(responseWithLoginFails.Results.Select(r => r.UserName), Is.EquivalentTo(userNames));

            foreach (var userName in userNames)
            {
                await _loginService.PutResetLoginFailTotal(userName);
            }

            var responseWithoutLoginFails = await _loginService.GetLoginFailTotal();

            Assert.That(responseWithoutLoginFails.Results.Count, Is.EqualTo(0));
        }
    }
}

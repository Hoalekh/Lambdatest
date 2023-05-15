using Lambdatest.Drivers;
using Lambdatest.Page;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
namespace Lambdatest.Steps
{


    [Binding]
    public class LoginPageSteps
    {

        private readonly Driver _driver;
        private readonly LoginPage loginPage;
        public LoginPageSteps(Driver driver)
        {
            _driver = driver;
            loginPage = new LoginPage(_driver.Page);
        }

     
        public static string Email => "//input[@id='email']";

        public static string Password => "//input[@id='password']";
        public static string LoginBtn => "//button[@id='login-button']";
        public static string MsgInvalidEmail => "//p[@data-testid='errors-email'] ";
        public static string MsgInvalidPassword => "//p[@data-testid='errors-password']";
        public static string Profile=> "//a[@id='profile__dropdown']";

        [Given(@"I navigate to login page ""([^""]*)""")]
        public async Task GivenINavigateToLoginPage(string p0)
        {
            await loginPage.NavigatePage(p0);
   
        }


        [Then(@"I verify page login")]
        public async Task ThenIVerifyPageLogin()
        {
            Assert.IsTrue(await loginPage.IsElementVisible(Email));
            Assert.IsTrue(await loginPage.IsElementVisible(Password));
            Assert.IsTrue(await loginPage.IsElementVisible(LoginBtn));
        }

        [Then(@"I enter following login details")]
        public async Task ThenIEnterFollowingLoginDetails(Table table)
        {

            dynamic data = table.CreateDynamicInstance();

            await loginPage.FillTextBox(Email,(string)data.Email);
            await loginPage.FillTextBox(Password,(string)data.Password);
        }

        [Then(@"I verify message ""([^""]*)""")]
        public async Task ThenIVerrifyMessage(string p0)
        {
          Assert.AreEqual( await loginPage.GetText(MsgInvalidEmail), p0);
          
        }

        [Then(@"I Click to login")]
        public async Task ThenIClickToLogin()
        {
           await loginPage.ClickToButton(LoginBtn);
        }

        [Then(@"I verify message password error")]
        public async Task ThenIVerifyWarningMessageUnderField()
        {
            Assert.IsTrue(await loginPage.IsElementVisible(MsgInvalidPassword));
        }

        [Then(@"I verify login successfull")]
        public async Task ThenIVerifyLoginSuccessfull()
        {
            Assert.IsTrue(await loginPage.IsElementVisible(Profile));
        }
    }
}

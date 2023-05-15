using System.Threading.Tasks;
using BoDi;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Lambdatest.Drivers
{
    [TestFixture]
    [Binding]
    public class Driver
    {
        private IPage? _page;
        private IBrowser? _browser;

        public IPage Page => _page;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                Channel = "chrome",
                SlowMo =2000
               
            });
            _page = await _browser.NewPageAsync();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
        }
    }
}

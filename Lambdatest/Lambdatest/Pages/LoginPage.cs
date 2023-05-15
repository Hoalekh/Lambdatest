using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lambdatest.page;
using Microsoft.Playwright;

namespace Lambdatest.Page
{
    public class LoginPage
    {

        private readonly IPage page;

        public LoginPage(IPage page) => this.page = page;
        public static string AmazonSearchPlaceholder => "Search Amazon";

        public static string EGiftCard => "Amazon.com eGift Card";
        public static string AmazonLogo => "//a[@aria-label='Amazon']";

        public IPage GetPage() => this.page;
        public async Task NavigatePage(string url)
        {
            await this.GetPage().GotoAsync(url);

        }


        public async Task ClickToButton(string locator)
        {
            await this.GetPage().Locator(locator).ClickAsync();

        }


        public async Task FillTextBox(string locator, string value)
        {
            try
            {
                await this.GetPage().Locator(locator).FillAsync(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<string> GetText(string locator)
        {
            try
            {
                var element = await this.GetPage().QuerySelectorAsync(locator);
                var text = await element.TextContentAsync();
                return text.TrimEnd();


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> IsElementVisible(string locator)
        {
            try
            {
                await GetPage().WaitForSelectorAsync(locator);
                var element = await GetPage().QuerySelectorAsync(locator);
                if (element == null)
                {
                    throw new Exception($"Element with locator '{locator}' not found.");
                }
                return await element.IsVisibleAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}

using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lambdatest.page
{
    public class BasePage
    {

        private readonly IPage page;

        public BasePage(IPage page) => this.page = page;

        public static string EnterbuttonName => "Enter";

        public IPage GetPage() => this.page;
        public async Task NavigatePage(string url)
        {
            await this.GetPage().GotoAsync(url);

        }

        public async Task ClickToButtonByName(string buttonName)
        {
            await this.GetPage().GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = buttonName, Exact = true }).ClickAsync();

        }

        public async Task ClickToButtonByLocator(string locator)
        {
            await this.GetPage().Locator(locator).ClickAsync();

        }

        public async Task MoveToElement(string locator)
        {
            var element = await page.QuerySelectorAsync(locator);
            await element.HoverAsync();
        }

        public async Task PressEnterButton(string locator)
        {
            await this.GetPage().Locator(locator).PressAsync(EnterbuttonName);
        }

        public async Task InputTextByLabel(string locator, string value)
        {
            try
            {
                await this.GetPage().GetByLabel(locator).ClickAsync();
                await this.GetPage().GetByLabel(locator).FillAsync(value);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task InputTextByPlaceholder(string locator, string value)
        {
            try
            {
                await this.GetPage().GetByPlaceholder(locator).FillAsync(value);


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task InputTextByLocator(string locator, string value)
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

        public async Task<string> TakeScreenshot()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\..\\..\\.." + ("/screenshot_" + DateTime.Now.ToString("yyyyMMdd")) + ".png";
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path
            });
            return path;
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
        public async Task<bool> IsContainText(string locator, string text)
        {
            try
            {
                await GetPage().WaitForSelectorAsync(locator);
                var element = await GetPage().QuerySelectorAsync(locator);
                if (element == null)
                {
                    throw new Exception($"Element with locator '{locator}' not found.");
                }

                string elementText = await element.InnerTextAsync();
                return elementText.Contains(text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task CheckTitleAsync(string url)
        {
            try
            {
                string title = url.Substring(url.IndexOf(".") + 1);
                string removeLastCharFromTitleResult = title.Remove(title.Length - 1, 1);

                removeLastCharFromTitleResult.ToUpper();
                string titleToValidate = removeLastCharFromTitleResult.Remove(1).ToUpper() + removeLastCharFromTitleResult.Substring(1);

                // validate URL link name
                await Assertions.Expect(this.GetPage()).ToHaveTitleAsync(new Regex(titleToValidate));

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task ChooseCalendarDate(string locatorCalendar, string date)
        {

            await this.GetPage().Locator(locatorCalendar).ClickAsync();
            await this.GetPage().GetByRole(AriaRole.Link, new() { Name = date, Exact = true }).ClickAsync();
        }
        public async Task SelectOptionFromDropdown(string dropdownSelector, string optionValue)
        {
            var dropdown = await page.QuerySelectorAsync(dropdownSelector);
            await dropdown.ClickAsync();

            var optionSelector = $"{dropdownSelector} >> option[value='{optionValue}']";
            var option = await page.QuerySelectorAsync(optionSelector);
            await option.ClickAsync();
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


        public void StopTestWithReason(string reason)
        {
            var bTesstFail = true;
            bTesstFail.Should().BeFalse($"{reason}");
        }

    }
}

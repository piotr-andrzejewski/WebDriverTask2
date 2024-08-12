using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverTask2
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        // Locators
        private readonly By newPasteTextArea = By.Id("postform-text");
        private readonly By syntaxHighlightingDropdown = By.Id("select2-postform-format-container");
        private readonly By pasteExpirationDropdown = By.Id("select2-postform-expiration-container");
        private readonly By pasteNameInput = By.Id("postform-name");
        private readonly By createNewPasteButton = By.XPath("//button[contains(text(),'Create New Paste')]");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl("https://pastebin.com/");
        }

        public void EnterPasteCode(string code)
        {
            driver.FindElement(newPasteTextArea).SendKeys(code);
        }

        public void SelectSyntaxHighlighting(string syntax)
        {
            driver.FindElement(syntaxHighlightingDropdown).Click();
            var selectElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//li[text()='{syntax}']")));
            selectElement.Click();
        }

        public void SelectPasteExpiration(string expirationTime)
        {
            driver.FindElement(pasteExpirationDropdown).Click();
            var expirationOption = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//li[text()='{expirationTime}']")));
            expirationOption.Click();
        }

        public void EnterPasteName(string name)
        {
            driver.FindElement(pasteNameInput).SendKeys(name);
        }

        public void CreateNewPaste()
        {
            driver.FindElement(createNewPasteButton).Click();
        }
    }
}

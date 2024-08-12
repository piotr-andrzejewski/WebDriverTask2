using OpenQA.Selenium;

namespace WebDriverTask2
{
    public class CreatedPastePage
    {
        private readonly IWebDriver driver;

        // Locators
        private readonly By pageTitle = By.XPath("//div[@class='info-top']/h1");
        private readonly By syntaxHighlighting = By.XPath("//a[@class='btn -small h_800']");
        private readonly By pasteCode = By.XPath("//ol[@class='bash']");

        public CreatedPastePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPasteTitle()
        {
            return driver.FindElement(pageTitle).Text;
        }

        public string GetSyntaxHighlighting()
        {
            return driver.FindElement(syntaxHighlighting).Text;
        }

        public string GetPasteCode()
        {
            return driver.FindElement(pasteCode).Text;
        }
    }
}

using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace WebDriverTask2
{
    public class Tests
    {
        private readonly IWebDriver driver;
        private const string baseUrl = "https://pastebin.com/";
        private const string content = """
git config --global user.name "New Sheriff in Town"
git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
git push origin master --force\r\n
""";
        private const string title = "how to gain dominance among developers";

        public Tests()
        {
            EdgeOptions options = new EdgeOptions();
            options.AddArgument("-inprivate");
            options.AddArgument("--start-maximized");
            driver = new EdgeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact]
        public void CreateNewPasteTest()
        {
            driver.Navigate().GoToUrl(baseUrl);

            // Accept cookies
            driver
                .FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"))
                .Click();

            // Enter the code into the "New Paste" text area
            IWebElement pasteCodeElement = driver.FindElement(By.Id("postform-text"));
            pasteCodeElement.SendKeys(content);

            // Set the "Syntax Highlighting dropdown to Bash"
            driver.FindElement(By.Id("select2-postform-format-container")).Click();
            driver.FindElement(By.XPath("//li[text()='Bash']")).Click();

            // Set the "Paste Expiration" dropdown to "10 Minutes"
            driver.FindElement(By.Id("select2-postform-expiration-container")).Click();
            driver.FindElement(By.XPath("//li[text()='10 Minutes']")).Click();

            // Set the "Paste Name / Title" to "helloweb"
            IWebElement pasteNameElement = driver.FindElement(By.Id("postform-name"));
            pasteNameElement.SendKeys(title);

            // Click the "Create New Paste" button
            IWebElement createPasteButton = driver.FindElement(By.XPath("//button[contains(text(),'Create New Paste')]"));
            createPasteButton.Click();

            // Verify that the paste name is correct
            IWebElement pasteTitle = driver.FindElement(By.XPath("//div[@class='info-top']//h1"));
            Assert.Equal(title, pasteTitle.Text);

            // Verify that syntax highlighting is correct
            IWebElement syntaxHighlighting = driver.FindElement(By.XPath("//div[@class='left']/a[1]"));
            Assert.Equal("bash", syntaxHighlighting.Text, StringComparer.OrdinalIgnoreCase);

            // Verify that expiration time is correct
            IWebElement expirationTime = driver.FindElement(By.XPath("//div[@class='expire']"));
            Assert.Equal("10 min", expirationTime.Text, StringComparer.OrdinalIgnoreCase);

            driver.Close();
        }
    }
}
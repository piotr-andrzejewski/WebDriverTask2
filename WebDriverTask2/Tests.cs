using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace WebDriverTask2
{
    public class Tests
    {
        private readonly IWebDriver driver;

        public Tests()
        {
            driver = new EdgeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Fact]
        public void Should_Create_New_Paste_With_Specified_Attributes()
        {
            // Arrange
            var pastebinHomePage = new HomePage(driver);
            var pastebinCreatedPastePage = new CreatedPastePage(driver);
            string pasteCode = """
git config --global user.name "New Sheriff in Town"
git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
git push origin master --force
""";
            string pasteName = "how to gain dominance among developers";

            // Act
            pastebinHomePage.Open();
            pastebinHomePage.EnterPasteCode(pasteCode);
            pastebinHomePage.SelectSyntaxHighlighting("Bash");
            pastebinHomePage.SelectPasteExpiration("10 Minutes");
            pastebinHomePage.EnterPasteName(pasteName);
            pastebinHomePage.CreateNewPaste();

            // Assert
            Assert.Equal(pasteName, pastebinCreatedPastePage.GetPasteTitle());
            Assert.Equal("Bash", pastebinCreatedPastePage.GetSyntaxHighlighting());
            Assert.Equal(pasteCode.Replace(" ", ""), pastebinCreatedPastePage.GetPasteCode().Replace(" ", ""));

            driver.Close();
        }
    }
}
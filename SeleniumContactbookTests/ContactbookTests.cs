using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class ContactbookTests
    {
        private WebDriver driver;
        //private const string baseUrl = "http://contactbook.damiant4.repl.co/";
        private const string baseUrl = "https://96ee60ba-099a-43c3-a31a-c3d4c96ef282-00-3pa386u0y4i19.riker.replit.dev/";

        [SetUp]
        public void OpenApp()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = baseUrl;
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test]
        public void Test_Run_Contactbook()
        {
            Assert.Pass();
        }

        [Test]
        public void Test_First_Contact_Steve_Jobs()
        {
            var buttonContact = driver.FindElement(By.LinkText("Contacts"));
            buttonContact.Click();

            var lincFirstContact = driver.FindElement(By.Id("contact1"));
            lincFirstContact.Click();

            // Assert
            var ViewFirstName = driver.FindElement(By.XPath("//td[contains(text(),'Steve')]"));
            var ViewLastName = driver.FindElement(By.XPath("//td[contains(text(),'Jobs')]"));

            Assert.That(ViewFirstName.Text, Is.EqualTo("Steve"));
            Assert.That(ViewLastName.Text, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test_Search_Task_By_Keyword()
        {
            var linkSearch = driver.FindElement(By.LinkText("Search"));
            linkSearch.Click();

            var fieldKeyword = driver.FindElement(By.Id("keyword"));
            fieldKeyword.Click();
            fieldKeyword.SendKeys("albert");

            var buttonSearch = driver.FindElement(By.CssSelector("#search"));
            buttonSearch.Click();

            // Assert
            var ViewFirstName = driver.FindElement(By.XPath("//td[contains(text(),'Albert')]"));
            var ViewLastName = driver.FindElement(By.XPath("//td[contains(text(),'Einstein')]"));

            Assert.That(ViewFirstName.Text, Is.EqualTo("Albert"));
            Assert.That(ViewLastName.Text, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test_Search_Missing_Contact()
        {
            var linkSearch = driver.FindElement(By.LinkText("Search"));
            linkSearch.Click();

            var fieldKeyword = driver.FindElement(By.Id("keyword"));
            fieldKeyword.Click();
            fieldKeyword.SendKeys("Contact");

            var buttonSearch = driver.FindElement(By.CssSelector("#search"));
            buttonSearch.Click();

            // Assert
            var actualResult = driver.FindElement(By.XPath("//div[@id='searchResult']"));
            Assert.That(actualResult.Text, Is.EqualTo("No contacts found."));
        }

        [Test]
        public void Test_Create_Invalid_Contact()
        {
            var linkCreate = driver.FindElement(By.LinkText("Create"));
            linkCreate.Click();

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            // Assert
            var errorMessage = driver.FindElement(By.XPath("//div[contains(.,'Error: First name cannot be empty!')]"));
            Assert.That(errorMessage.Text, Is.EqualTo("Error: First name cannot be empty!"));
        }

        [Test]
        public void Test_Create_Valid_Contact()
        {
            var linkCreate = driver.FindElement(By.LinkText("Create"));
            linkCreate.Click();

            var fieldFirstName = driver.FindElement(By.Id("firstName"));
            fieldFirstName.SendKeys("Test");

            var fieldLastName = driver.FindElement(By.Id("lastName"));
            fieldLastName.SendKeys("ForQA");

            var fieldEmail = driver.FindElement(By.Id("email"));
            fieldEmail.SendKeys("test.for.qa@abv.bg");

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            // Assert
            var pageTitle = driver.Title;
            Assert.That(pageTitle, Is.EqualTo("View Contacts – Contact Book"));

            // finde last contact
            var contactsGrid = driver.FindElement(By.XPath("//div[@class='contacts-grid']"));
            var lincLastContact = contactsGrid.FindElements(By.TagName("div")).Last();
            lincLastContact.Click();


            var ViewFirstName = driver.FindElement(By.XPath("//td[contains(.,'Test')]"));
            var ViewLastName = driver.FindElement(By.XPath("//td[contains(.,'ForQA')]"));

            Assert.That(ViewFirstName.Text, Is.EqualTo("Test"));
            Assert.That(ViewLastName.Text, Is.EqualTo("ForQA"));

        }
    }
}
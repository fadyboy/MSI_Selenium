//using System;
//using System.Text;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Support.UI;

//namespace MSI_Global
//{
//    [TestFixture]
//    public class Search
//    {
//        private IWebDriver driver;
//        private StringBuilder verificationErrors;
//        private string baseURL;
//        private bool acceptNextAlert = true;
        
//        [SetUp]
//        public void SetupTest()
//        {
//            driver = new FirefoxDriver();
//            baseURL = "http://msiumbraco.codehousegroup.com/";
//            verificationErrors = new StringBuilder();
//        }
        
//        [TearDown]
//        public void TeardownTest()
//        {
//            try
//            {
//                driver.Quit();
//            }
//            catch (Exception)
//            {
//                // Ignore errors if unable to close the browser
//            }
//            Assert.AreEqual("", verificationErrors.ToString());
//        }
        
//        [Test]
//        public void TheSearchTest()
//        {
//            driver.Navigate().GoToUrl(baseURL + "/");
//            driver.FindElement(By.XPath("//a[@id='lkbAdvSearch']")).Click();
//            driver.FindElement(By.Id("contenttype")).Click();
//            new SelectElement(driver.FindElement(By.XPath("//select[@id='contenttype']"))).SelectByText("Firms");
//            new SelectElement(driver.FindElement(By.XPath("//select[@id='region']"))).SelectByText("Europe");
//            new SelectElement(driver.FindElement(By.XPath("//select[@id='countryddlLightbox']"))).SelectByText("United Kingdom");
//            new SelectElement(driver.FindElement(By.XPath("//select[@id='firmtype']"))).SelectByText("Law");
//            driver.FindElement(By.XPath("//ul[@id='ui-id-1']/li/a")).Click();
//            driver.FindElement(By.XPath("(//input[@type='text'])[3]")).Clear();
//            driver.FindElement(By.XPath("(//input[@type='text'])[3]")).SendKeys("Tax Audit  (Audit)");
//            driver.FindElement(By.XPath("//input[@value='Search']")).Click();
//            driver.FindElement(By.XPath("//a[contains(text(),'Home')]")).Click();
//        }
//        private bool IsElementPresent(By by)
//        {
//            try
//            {
//                driver.FindElement(by);
//                return true;
//            }
//            catch (NoSuchElementException)
//            {
//                return false;
//            }
//        }
        
//        private string CloseAlertAndGetItsText() {
//            try {
//                IAlert alert = driver.SwitchTo().Alert();
//                if (acceptNextAlert) {
//                    alert.Accept();
//                } else {
//                    alert.Dismiss();
//                }
//                return alert.Text;
//            } finally {
//                acceptNextAlert = true;
//            }
//        }
//    }
//}

using System.Globalization;
using System.Text;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Drawing.Imaging;
using System.IO;

namespace MSI_Global
{
    public class Base
    {
        //declare variables that will be inherited by other sub-classes
        public static IWebDriver driver;
        public static string baseUrl;
        //public static WebDriverWait wait;
        
        //public static Screenshot myScreen;


        protected Base()
        {
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        //Function to navigate to any page/url in website
        public void NavigateTo(string url)
        {
            var navigateToUrl = baseUrl + url;
            driver.Navigate().GoToUrl(navigateToUrl);
        }

        protected static void GetDriver()
        {
            
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
             //driver = new InternetExplorerDriver();
        }

        //Create method to take screenshot and save file to folder as jpeg image
        protected static void SaveScreenShot(string imageName)
        {
            const string folderLocation = (@"D:\Projects\");
            const string ext = ".jpg";
            var filename = new StringBuilder(folderLocation);
            
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            //var apnd = 0;

            filename.Append(imageName);
            
            filename.Append(ext);

            if (File.Exists(filename.ToString()))//Check if file already exists and rename if it does
            {
                var apnd = 1;
                
                while (File.Exists(filename.ToString()))
                {
                    filename.Remove(folderLocation.Length, (filename.Length - folderLocation.Length));
                    filename.Append(imageName);
                    filename.Append("_" + apnd);
                    filename.Append(ext);
                    apnd++;
                }
                
            }

            screenShot.SaveAsFile(filename.ToString(), ImageFormat.Jpeg);
        }
    }
}

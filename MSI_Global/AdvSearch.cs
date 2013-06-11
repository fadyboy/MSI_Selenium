using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Excel = Microsoft.Office.Interop.Excel;

namespace MSI_Global
{
    class AdvSearch : Base
    {

        public void AdvancedSearch()
        {

            var xlApp = new Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(@"D:\Projects\Data\MSI_Data_file.xlsx", 0, true, 5, "", "", true,
                                                       Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.Item[2];
            var xlRange = xlWorksheet.UsedRange;
            var colms = xlRange.Columns.Count;

            
            int xlRowCnt;
            var xlString = new List<String>();


            for (xlRowCnt = 1; xlRowCnt <= xlRange.Rows.Count; xlRowCnt++)
            {
                int xlColCnt;
                for (xlColCnt = 1; xlColCnt <= xlRange.Columns.Count; xlColCnt++)
                {
                    var range = xlRange.Cells[xlRowCnt, xlColCnt] as Excel.Range;
                    if (range == null) continue;
                    var values = (string) range.Value2;
                    if (values == null)
                    {
                       break; 
                    }
                    xlString.Add(values);
                }


                driver.Navigate().GoToUrl(baseUrl);

                driver.FindElement(By.XPath("//a[@id= 'lkbAdvSearch']")).Click();
                //delay for 10 seconds for window to display
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                driver.FindElement(By.XPath("//select[@id= 'contenttype']")).SendKeys(xlString[0]);
                driver.FindElement(By.XPath("//select[@id= 'region']")).SendKeys(xlString[1]);
                driver.FindElement(By.XPath("//select[@id= 'countryddlLightbox']")).SendKeys(xlString[2]);
                driver.FindElement(By.XPath("//select[@id= 'firmtype']")).SendKeys(xlString[3]);
                driver.FindElement(By.XPath("//input[@class= 'ui-autocomplete-input']")).SendKeys(xlString[4]);
                driver.FindElement(By.XPath("//input[@class= 'ui-autocomplete-input']")).SendKeys(Keys.ArrowDown);
                driver.FindElement(By.XPath("//input[@class= 'ui-autocomplete-input']")).SendKeys(Keys.Enter);

                //take screenshot of search form
                SaveScreenShot("searchForm");

                driver.FindElement(By.XPath("//input[@value= 'Search']")).Click();

                //take screenshot of search results
                SaveScreenShot("Search_results");

                if (xlColCnt > colms)
                {
                    for (var i = 0; i < colms; i++)
                    {
                        xlString.Remove(xlString[0]);
                    }
                }
            }

            
            xlWorkbook.Close(true, null, null);
            xlApp.Quit();

        }
    }
}

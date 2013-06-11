using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Excel = Microsoft.Office.Interop.Excel;


namespace MSI_Global
{
    public class SubmitJoinForm : Base
    {
        
        //function to navigate to the Join page
        public void GoToPage()
        {
            //_base.NavigateTo("Join");
            driver.Navigate().GoToUrl(baseUrl + "/Join/");
            driver.FindElement(By.LinkText("Join")).Click();

            SaveScreenShot("JoinPage");

            driver.FindElement(By.XPath("//a[contains(text(), 'enquiry form')]")).Click();
            
            SaveScreenShot("enquiry form");
           
        }

        public void Fillform()
        //Function reads entries from an Excel spreadsheet then uses values to populate and fill the form
        {

            var shutdown = new JoinFormTest();//create instance of test object to shutdown if condition is met

            //Load Excel file
            
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range xlrange;

            //string xlString;
            var xlString = new List<String>();
            var xlRowCnt = 0;

            xlApp = new Excel.Application();
            //Open Excel file
            xlWorkBook = xlApp.Workbooks.Open(@"D:\Projects\Data\MSI_Data_file.xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Item[1];

            //This gives the used cells in the sheet

            xlrange = xlWorkSheet.UsedRange;
            var colms = xlrange.Columns.Count;
            
            //Read values from spreadsheet row and add to 
            for (xlRowCnt = 1; xlRowCnt <= xlrange.Rows.Count; xlRowCnt++)
            {
                var xlColCnt = 0;
                for (xlColCnt = 1; xlColCnt <= xlrange.Columns.Count; xlColCnt++)
                {
                    var range = xlrange.Cells[xlRowCnt, xlColCnt] as Excel.Range;
                    if (range == null) continue;
                    var values = (string) range.Value2;
                    if (values == null)
                    {
                        break;
                    }
                    xlString.Add(values);
                }
                
                //Go to page to fill form as long as the xlString list has values
                if (xlString.Count != 0)
                {
                    driver.Navigate().GoToUrl(baseUrl + "/Join/");
                    driver.FindElement(By.LinkText("Join")).Click();

                    SaveScreenShot("JoinPage");

                    driver.FindElement(By.XPath("//a[contains(text(), 'enquiry form')]")).Click();

                    //xlString.Add(values);
                    driver.FindElement(By.XPath("//input[contains(@name, 'FirmName')]")).SendKeys(xlString[0]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'FirstName')]")).SendKeys(xlString[1]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'LastName')]")).SendKeys(xlString[2]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'Email')]")).SendKeys(xlString[3]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'FirmAddress')]")).SendKeys(xlString[4]);
                    driver.FindElement(By.XPath("//select[@id= 'ddlCountry']")).SendKeys(xlString[5]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'PhoneNumber')]")).SendKeys(xlString[6]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'FaxNumber')]")).SendKeys(xlString[7]);
                    driver.FindElement(By.XPath("//input[contains(@name, 'Website')]")).SendKeys(xlString[8]);
                    driver.FindElement(By.XPath("//textarea[contains(@name, 'Comments')]")).SendKeys(xlString[9]);
                    driver.FindElement(By.XPath("//input[@id='chkFirm_Service_Accounting']")).Click();
                    driver.FindElement(By.XPath("//select[contains(@name, 'LeadSource')]")).SendKeys(xlString[10]);

                    //save screenshot of completed form
                    SaveScreenShot("CompleteForm");

                    driver.FindElement(By.XPath("//a[contains(text(), 'Submit')]")).Click();

                    driver.FindElement(By.XPath("//a[contains(text(), 'click here')]")).Click();

                    //Take screenshot of successful form submission
                    SaveScreenShot("Submission_Success");

                    //Clear entries already used in form
                    if (xlColCnt > colms)
                    {
                        for (var i = 0; i < colms; i++)
                        {
                            xlString.Remove(xlString[0]);
                        }
                    } 
                }

                else //shutdown test if spreadsheet has no more values
                {
                    shutdown.TearDownTest();
                }

                
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
        }

    }
}

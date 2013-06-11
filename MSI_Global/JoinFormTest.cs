using System;
using NUnit.Framework;


namespace MSI_Global
{
    [TestFixture]

    class JoinFormTest : Base
    {
        //Base _driver = new Base();

        [TestFixtureSetUp]

        public void Setup()
        {
            //driver.Navigate();
            GetDriver();
        }

        [Test]
        public void JoinPage()
        {
            var submitForm = new SubmitJoinForm();
            submitForm.Fillform();
            
        }

        [Test]

        public void AdvanSearch()
        {
            var search = new AdvSearch();
            search.AdvancedSearch();
        }

        
        [TestFixtureTearDown]
        public void TearDownTest()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}

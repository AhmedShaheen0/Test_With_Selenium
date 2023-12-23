using NUnit.Framework;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumTEST
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {

          driver = new EdgeDriver("E:\\programs\\edgedriver_win64\\msedgedriver.exe");
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
            Thread.Sleep(1000);
            PerformLogin();
        }

        [Test]
        public void loginTest()
        {
            
            /////////////navigate sidebar

            string[] links ={ "PIM",  "Recruitment",
                      "My Info"  ,  "Dashboard","Admin"};

            foreach (string link in links)
            {
                navigateThroughLinks(link);
                Thread.Sleep(2000);
            }
           TestSearchAdminUser("John Doe");
            PerformLogout();
            ForgettnPasswordTest();
            // TestAddAdminUser();

            ////////////////
            driver.Quit();
        }



        public void ForgettnPasswordTest()
        {
            driver.FindElement(By.ClassName("orangehrm-login-forgot-header")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.CssSelector("#app > div.orangehrm-forgot-password-container > div.orangehrm-forgot-password-wrapper > div > form > div.orangehrm-forgot-password-button-container > button.oxd-button.oxd-button--large.oxd-button--secondary.orangehrm-forgot-password-button.orangehrm-forgot-password-button--reset")).Click();
            Thread.Sleep(2000);
            driver.Quit();
        }



        private void PerformLogout()
        {

            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-navigation > header > div.oxd-topbar-header > div.oxd-topbar-header-userarea > ul > li > span")).Click();
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-navigation > header > div.oxd-topbar-header > div.oxd-topbar-header-userarea > ul > li > ul > li:nth-child(4) > a")).Click();
         }

        public void PerformLogin()
        {
            // Test login
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.CssSelector("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div.oxd-form-actions.orangehrm-login-action > button")).Click();
            Thread.Sleep(2000);
        }


        private  void openSidebar()
        {
             driver.FindElement(By.ClassName("#app > div.oxd-layout > div.oxd-layout-navigation > aside > nav > div.oxd-sidepanel-body > div > div > button")).Click();
        }

        private  void closeSidebar()
        {
            driver.FindElement(By.ClassName("#app > div.oxd-layout > div.oxd-layout-navigation > aside > nav > div.oxd-sidepanel-body > div > div > button")).Click();
        }

        private void navigateThroughLinks( string linkText)
        {
             driver.FindElement(By.LinkText(linkText)).Click();
        }

        private void TestAddAdminUser()
        {
            navigateThroughLinks("Admin");

            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div.orangehrm-paper-container > div.orangehrm-header-container > button")).Click();
            Thread.Sleep(1000);
            // Fill in the details for the new admin user
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div:nth-child(1) > div > div:nth-child(2) > div > div:nth-child(2) > div > div > input"))
                .SendKeys("Mason Aiyana Haag");
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div:nth-child(1) > div > div:nth-child(4) > div > div:nth-child(2) > input"))
                .SendKeys("john.doe");
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div.oxd-form-row.user-password-row > div > div.oxd-grid-item.oxd-grid-item--gutters.user-password-cell > div > div:nth-child(2) > input"))
                .SendKeys("password123");
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div.oxd-form-row.user-password-row > div > div:nth-child(2) > div > div:nth-child(2) > input"))
                .SendKeys("password123");

            IWebElement dropdown = driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div:nth-child(1) > div > div:nth-child(1) > div > div:nth-child(2) > div > div"));
            // Create a SelectElement object
            SelectElement select = new SelectElement(dropdown);

            // Get the selected value from the dropdown
            select.SelectByText("Admin");

            IWebElement dropdown2 = driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div:nth-child(1) > div > div:nth-child(3) > div > div:nth-child(2) > div > div"));
            // Create a SelectElement object
            SelectElement select2 = new SelectElement(dropdown2);

            // Get the selected value from the dropdown
            select.SelectByText("Enabled");

            // Click the "Save" button to add the new admin user
            driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div > form > div.oxd-form-actions > button.oxd-button.oxd-button--medium.oxd-button--secondary.orangehrm-left-space")).Click();

        }

        private void TestSearchAdminUser(string userName)
        {
            IWebElement searchBox = driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div.oxd-table-filter > div.oxd-table-filter-area > form > div.oxd-form-row > div > div:nth-child(1) > div > div:nth-child(2) > input"));
            searchBox.Clear();
            searchBox.SendKeys(userName);
           driver.FindElement(By.CssSelector("#app > div.oxd-layout > div.oxd-layout-container > div.oxd-layout-context > div > div.oxd-table-filter > div.oxd-table-filter-area > form > div.oxd-form-actions > button.oxd-button.oxd-button--medium.oxd-button--secondary.orangehrm-left-space")).Click();
         }





    }
}
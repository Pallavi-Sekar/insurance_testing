using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox; 
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers; 



[TestFixture]
public class InsuranceTest
{
    private IWebDriver driver;
    private WebDriverWait wait; 

    [SetUp]
    public void SetUp()
    {
        
        driver = new FirefoxDriver();

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        driver.Manage().Window.Maximize(); 
    }

    [TearDown]
    protected void TearDown()
    {
        driver?.Dispose();
    }

    private void EnterText(By locator, string text)
    {
        IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        element.Clear();
        element.SendKeys(text);
    }

    private void WaitAndClick(By locator)
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
    }

    [Test]
    public void TestCase1_ValidQuote()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn")); 

        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "226-748-9957");
        EnterText(By.Id("email"), "asd@gmail.com");
        EnterText(By.Id("age"), "24");
        EnterText(By.Id("experience"), "3");
        EnterText(By.Id("accidents"), "0");

        WaitAndClick(By.Id("btnSubmit"));

        string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
        Assert.That(value, Is.EqualTo("$5500"), "Test Case 1 Failed: Quote mismatch.");
    }

    [Test]
    public void TestCase2_TooManyAccidents()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));

        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "226-748-9957");
        EnterText(By.Id("email"), "asd@gmail.com");
        EnterText(By.Id("age"), "25");
        EnterText(By.Id("experience"), "3");
        EnterText(By.Id("accidents"), "4"); 

        WaitAndClick(By.Id("btnSubmit"));

        string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
        Assert.That(value, Is.EqualTo("No Insurance for you!!  Too many accidents - go take a course!"), "Test Case 2 Failed: Too many accidents message mismatch.");
    }

    [Test]
    public void TestCase3_ValidQuoteAge35Exp9Acc2()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));

        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "226-748-9957");
        EnterText(By.Id("email"), "asd@gmail.com");
        EnterText(By.Id("age"), "35");
        EnterText(By.Id("experience"), "9"); 
        EnterText(By.Id("accidents"), "2");

        WaitAndClick(By.Id("btnSubmit"));

        string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
        Assert.That(value, Is.EqualTo("$3905"), "Test Case 3 Failed: Quote mismatch.");
    }

    [Test]
    public void TestCase4_InvalidEmailFormat()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));

        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "226-748-9957");
        EnterText(By.Id("email"), "asd"); 
        EnterText(By.Id("age"), "28");
        EnterText(By.Id("experience"), "3");
        EnterText(By.Id("accidents"), "0");

        WaitAndClick(By.Id("btnSubmit"));

        IWebElement emailError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email-error")));
        Assert.That(emailError.Text, Is.EqualTo("Must be a valid email address"), "Test Case 4 Failed: Email validation message mismatch.");
        
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 4 Failed: Final quote should be empty on validation error.");
    }

    [Test]
    public void TestCase5_InvalidPhoneFormat()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));

        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "226"); 
        EnterText(By.Id("email"), "pallavi@gmail.com");
        EnterText(By.Id("age"), "35");
        EnterText(By.Id("experience"), "9");
        EnterText(By.Id("accidents"), "2");

        WaitAndClick(By.Id("btnSubmit"));

        IWebElement phoneError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone-error")));
        Assert.That(phoneError.Text, Is.EqualTo("Phone Number must follow the patterns 111-111-1111 or (111)111-1111"), "Test Case 5 Failed: Phone validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 5 Failed: Final quote should be empty on validation error.");
    }

}
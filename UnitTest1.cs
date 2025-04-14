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
    public void TestCase6_InvalidPostalCodeFormat()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));


        EnterText(By.Id("firstName"), "Pallavi");
        EnterText(By.Id("lastName"), "Sekar");
        EnterText(By.Id("address"), "50, University Ave");
        EnterText(By.Id("city"), "Waterloo");
        EnterText(By.Id("postalCode"), "N2J2V8"); // Invalid format (no space)
        EnterText(By.Id("phone"), "226-748-9957");
        EnterText(By.Id("email"), "asd@gmail.com");
        EnterText(By.Id("age"), "25");
        EnterText(By.Id("experience"), "3");
        EnterText(By.Id("accidents"), "0");


        WaitAndClick(By.Id("btnSubmit"));


        IWebElement postalCodeError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postalCode-error")));
        Assert.That(postalCodeError.Text, Is.EqualTo("Postal Code must follow the pattern A1A 1A1"), "Test Case 6 Failed: Postal code validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 6 Failed: Final quote should be empty on validation error.");
    }


    [Test]
    public void TestCase7_AgeMissingThenCorrected()
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

        EnterText(By.Id("experience"), "5");
        EnterText(By.Id("accidents"), "0");


        WaitAndClick(By.Id("btnSubmit"));



        IWebElement ageError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("age-error")));
        Assert.That(ageError.Text, Is.EqualTo("Age (>=16) is required"), "Test Case 7 Failed: Initial age validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 7 Failed: Final quote should be empty on validation error.");



        EnterText(By.Id("age"), "16");
        WaitAndClick(By.Id("btnSubmit"));



        string finalQuote = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
        Assert.That(finalQuote, Is.EqualTo("No Insurance for you!! Driver Age / Experience Not Correct"), "Test Case 7 Failed: Final quote after correcting age mismatch.");
    }




    [Test]
    public void TestCase8_AccidentsMissing()
    {
        driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
        WaitAndClick(By.CssSelector(".btn"));


        EnterText(By.Id("firstName"), "john");
        EnterText(By.Id("lastName"), "son");
        EnterText(By.Id("address"), "aslkdjalskjla");
        EnterText(By.Id("city"), "dsaaf");
        EnterText(By.Id("postalCode"), "N2J 2V8");
        EnterText(By.Id("phone"), "223-333-4566");
        EnterText(By.Id("email"), "aksdjlf@gmail.com");
        EnterText(By.Id("age"), "35");
        EnterText(By.Id("experience"), "6");



        WaitAndClick(By.Id("btnSubmit"));


        IWebElement accidentsError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("accidents-error")));

        Assert.That(accidentsError.Text, Is.EqualTo("Number of accidents is required"), "Test Case 8 Failed: Accidents required validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 8 Failed: Final quote should be empty on validation error.");
    }


    [Test]
    public void TestCase9_AccidentsMissingVariant()
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
        EnterText(By.Id("age"), "40");
        EnterText(By.Id("experience"), "5");

        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("accidents"))).Clear();




        WaitAndClick(By.Id("btnSubmit"));



        IWebElement accidentsError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("accidents-error")));

        Assert.That(accidentsError.Text, Is.EqualTo("Number of accidents is required"), "Test Case 9 Failed: Accidents required validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 9 Failed: Final quote should be empty on validation error.");
    }


    [Test]
    public void TestCase10_AgeBelowMinimum()
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
        EnterText(By.Id("age"), "15");
        EnterText(By.Id("experience"), "0");
        EnterText(By.Id("accidents"), "0");


        WaitAndClick(By.Id("btnSubmit"));



        IWebElement ageError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("age-error")));

        Assert.That(ageError.Text, Is.EqualTo("Please enter a value greater than or equal to 16."), "Test Case 10 Failed: Minimum age validation message mismatch.");
        Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 10 Failed: Final quote should be empty on validation error.");
    }


}
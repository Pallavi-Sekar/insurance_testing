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


   [Test]
   public void TestCase11_ExperienceExceedsAge()
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
       EnterText(By.Id("age"), "30");
       EnterText(By.Id("experience"), "35");
       EnterText(By.Id("accidents"), "0");


       WaitAndClick(By.Id("btnSubmit"));


       string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
      
       Assert.That(value, Is.EqualTo("No Insurance for you!! Driver Age / Experience Not Correct"), "Test Case 11 Failed: Age/Experience mismatch message incorrect.");
   }


   [Test]
   public void TestCase12_AllFieldsMissing()
   {
       driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
       WaitAndClick(By.CssSelector(".btn"));


  
       wait.Until(ExpectedConditions.ElementIsVisible(By.Id("firstName")));


  
       WaitAndClick(By.Id("btnSubmit"));


       Assert.Multiple(() =>
       {
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("firstName-error"))).Text, Is.EqualTo("First Name is required"), "TC12 Fail: First Name required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("lastName-error"))).Text, Is.EqualTo("Last Name is required"), "TC12 Fail: Last Name required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("address-error"))).Text, Is.EqualTo("Address is required"), "TC12 Fail: Address required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("city-error"))).Text, Is.EqualTo("City is required"), "TC12 Fail: City required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postalCode-error"))).Text, Is.EqualTo("Postal Code is required"), "TC12 Fail: Postal Code required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone-error"))).Text, Is.EqualTo("Phone Number is required"), "TC12 Fail: Phone required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email-error"))).Text, Is.EqualTo("email address is required"), "TC12 Fail: Email required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("age-error"))).Text, Is.EqualTo("Age (>=16) is required"), "TC12 Fail: Age required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("experience-error"))).Text, Is.EqualTo("Years of experience is required"), "TC12 Fail: Experience required msg mismatch.");
           Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("accidents-error"))).Text, Is.EqualTo("Number of accidents is required"), "TC12 Fail: Accidents required msg mismatch.");
       });
       Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 12 Failed: Final quote should be empty on validation error.");
   }


   [Test]
   public void TestCase13_ValidQuoteHighAgeExperience()
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
       EnterText(By.Id("age"), "100");
       EnterText(By.Id("experience"), "80");
       EnterText(By.Id("accidents"), "0");


       WaitAndClick(By.Id("btnSubmit"));


       string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
  
       Assert.That(value, Is.EqualTo("$2840"), "Test Case 13 Failed: Quote mismatch for high age/experience.");
   }


   [Test]
   public void TestCase14_InvalidPhoneFormatThenCorrected()
   {
       driver.Navigate().GoToUrl("http://localhost/prog8170a04/");
       WaitAndClick(By.CssSelector(".btn"));


       EnterText(By.Id("firstName"), "Pallavi");
       EnterText(By.Id("lastName"), "Sekar");
       EnterText(By.Id("address"), "50, University Ave");
       EnterText(By.Id("city"), "Waterloo");
       EnterText(By.Id("postalCode"), "N2J 2V8");
       EnterText(By.Id("phone"), "226-7489-957");
       EnterText(By.Id("email"), "asd@gmail.com");
       EnterText(By.Id("age"), "16");
       EnterText(By.Id("experience"), "0");
       EnterText(By.Id("accidents"), "0");


       WaitAndClick(By.Id("btnSubmit"));


      
       IWebElement phoneError = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone-error")));


       Assert.That(phoneError.Text, Is.EqualTo("Phone Number must follow the patterns 111-111-1111 or (111)111-1111"), "Test Case 14 Failed: Initial phone validation message mismatch.");
       Assert.That(driver.FindElement(By.Id("finalQuote")).GetAttribute("value"), Is.Empty, "Test Case 14 Failed: Final quote should be empty on validation error.");


       EnterText(By.Id("phone"), "226-748-9957");
       WaitAndClick(By.Id("btnSubmit"));


       string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
       Assert.That(value, Is.EqualTo("$7000"), "Test Case 14 Failed: Final quote after correcting phone mismatch.");
   }


   [Test]
   public void TestCase15_TooManyAccidentsHighAge()
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
       EnterText(By.Id("experience"), "20");
       EnterText(By.Id("accidents"), "10");


       WaitAndClick(By.Id("btnSubmit"));


       string value = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finalQuote"))).GetAttribute("value");
       Assert.That(value, Is.EqualTo("No Insurance for you!!  Too many accidents - go take a course!"), "Test Case 15 Failed: Too many accidents message mismatch.");
   }
}

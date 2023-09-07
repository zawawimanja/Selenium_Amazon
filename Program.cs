using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;


namespace ConsoleApp1
{
    class Program
    {
        private static IWebDriver driver;

        static void Main(string[] args)
        {
            Setup(); // Call the setup method to configure the Chrome WebDriver and navigate to the Amazon website
            PerformAmazonSearch("laptop"); // Perform an Amazon search for "laptop"
            CheckLaptopPriceGreaterThan100(); // Check if the laptop price is greater than $100
        }

        public static void Setup()
        {
            // Configure ChromeOptions with additional arguments
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--remote-allow-origins=*");
            options.AddArguments("--disable-notifications");

            // Initialize the ChromeDriver with the specified path to chromedriver.exe and options
            driver = new ChromeDriver("C:\\Users\\awi\\Downloads\\chromedriver.exe", options);

            // Maximize the browser window and navigate to the Amazon website
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://amazon.com");
        
        }

        public static void PerformAmazonSearch(string searchText)
        {
            // Create a WebDriverWait instance to wait for elements to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait until the search textbox is displayed
            wait.Until(driver => driver.FindElement(By.Id("twotabsearchtextbox")).Displayed);

            // Find the search textbox element and enter the search text ("laptop")
            IWebElement searchTextBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchTextBox.SendKeys(searchText);

            // Wait until the search button is displayed
            wait.Until(driver => driver.FindElement(By.Id("nav-search-submit-button")).Displayed);

            // Find the search button element and click it
            IWebElement searchButton = driver.FindElement(By.Id("nav-search-submit-button"));
            searchButton.Click();

            // Wait for the results page to be visible
            wait.Until(driver => driver.FindElement(By.CssSelector(".s-result-list")).Displayed);

            // Click on the first search result (laptop)
            IWebElement firstResult = driver.FindElements(By.CssSelector(".a-size-medium.a-color-base.a-text-normal"))[0];
            firstResult.Click();
        }

        public static void CheckLaptopPriceGreaterThan100()
        {
            // Create a WebDriverWait instance to wait for elements to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait until the laptop price element is displayed
            wait.Until(driver => driver.FindElement(By.CssSelector(".celwidget .a-price.priceToPay .a-offscreen")).Displayed);

            // Find the laptop price element and get its text
            IWebElement priceElement = driver.FindElement(By.CssSelector(".celwidget .a-price.priceToPay .a-offscreen"));
            string priceText = priceElement.GetAttribute("textContent");
            Console.WriteLine(priceText);

            // Remove the "$" symbol from the price text and parse it as a double
            string priceAfter = priceText.Replace("$", "");
            Console.WriteLine(priceAfter);
            double price = double.Parse(priceAfter);

            // Check if the laptop price is greater than $100 and print the result
            if (price > 100)
            {
                Console.WriteLine("The laptop price is greater than $100.");
            }
            else
            {
                Console.WriteLine("The laptop price is not greater than $100.");
            }

            driver.Close();
            

        }
    }
}

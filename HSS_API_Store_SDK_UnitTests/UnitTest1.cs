using HSS_API_Store_SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Threading.Tasks;
using static HSS_API_Store_SDK.apiResponsePasswords;
using static HSS_API_Store_SDK.apiResponseFoods;
using static HSS_API_Store_SDK.apiResponsePeople;
using System.Threading;

namespace HSS_API_Store_SDK_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethodPasswords1()
        {
            // Thread.Sleep(10000);
            for (int i = 0; i < 10; i++)
            {
                var apiResponse = await Passwords.GetPasswordsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
                    ConfigurationManager.AppSettings.Get("HSS_API_Store_PasswordsKey"), 10, 20, 8, "", true, RequiredSymbols.Digits);

                Console.WriteLine(apiResponse.responseStatus.ToString());
                Console.WriteLine(apiResponse.allTimeNumberOfCallsToThisAPI.ToString());

                apiResponse = await Passwords.GetPasswordsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
                    ConfigurationManager.AppSettings.Get("HSS_API_Store_PasswordsKey"), 10, 20, 8, "", true, RequiredSymbols.Digits);

                Console.WriteLine(apiResponse.responseStatus.ToString());
                Console.WriteLine(apiResponse.allTimeNumberOfCallsToThisAPI.ToString());

                var apiResponseFoods = await Foods.GetFoodsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
        ConfigurationManager.AppSettings.Get("HSS_API_Store_FoodsKey"));

                Console.WriteLine(apiResponseFoods.responseStatus.ToString());
                Console.WriteLine(apiResponseFoods.allTimeNumberOfCallsToThisAPI.ToString());

                apiResponse = await Passwords.GetPasswordsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
        ConfigurationManager.AppSettings.Get("HSS_API_Store_PasswordsKey"), 10, 20, 8, "", true, RequiredSymbols.Digits);

                Console.WriteLine(apiResponse.responseStatus.ToString());
                Console.WriteLine(apiResponse.allTimeNumberOfCallsToThisAPI.ToString());

                apiResponseFoods = await Foods.GetFoodsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
        ConfigurationManager.AppSettings.Get("HSS_API_Store_FoodsKey"));


                Console.WriteLine(apiResponseFoods.responseStatus.ToString());
                Console.WriteLine(apiResponseFoods.allTimeNumberOfCallsToThisAPI.ToString());
                Assert.IsNotNull(apiResponse);
                Assert.IsNotNull(apiResponse.passwordValues);
                Assert.AreEqual(ResponseEnumPasswords.Success, apiResponse.responseStatus);
                Assert.AreEqual(9, apiResponse.passwordValues.GetUpperBound(0));

                foreach (var password in apiResponse.passwordValues)
                {
                    Console.WriteLine(password);
                }
            }
        }

        [TestMethod]
        public async Task TestMethodPasswords2()
        {
            //Thread.Sleep(10000);

            var apiResponse = await Passwords.GetPasswordsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"), 
                ConfigurationManager.AppSettings.Get("HSS_API_Store_PasswordsKey"), PasswordCharacterSelection: RequiredSymbols.Digits);

            Assert.IsNotNull(apiResponse);
            Assert.IsNotNull(apiResponse.passwordValues);
            Assert.AreEqual(ResponseEnumPasswords.Success, apiResponse.responseStatus);
            Assert.AreEqual(0, apiResponse.passwordValues.GetUpperBound(0));

            foreach (var password in apiResponse.passwordValues)
            {
                Console.WriteLine(password);
            }
        }

        [TestMethod]
        public async Task TestMethodFoods1()
        {
            //Thread.Sleep(10000);

            var apiResponse = await Foods.GetFoodsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"), 
                ConfigurationManager.AppSettings.Get("HSS_API_Store_FoodsKey"));

            Assert.IsNotNull(apiResponse);
            Assert.IsNotNull(apiResponse.foodsList);
            Assert.AreEqual(ResponseEnumFoods.Success, apiResponse.responseStatus);
            Assert.AreEqual(0, apiResponse.foodsList.ToArray().GetUpperBound(0));

            foreach (var food in apiResponse.foodsList)
            {
                Console.WriteLine(food.foodInformation.Name);
            }
        }

        [TestMethod]
        public async Task TestMethodFoods2()
        {
            //Thread.Sleep(10000);

            var apiResponse = await Foods.GetFoodsAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"),
                ConfigurationManager.AppSettings.Get("HSS_API_Store_FoodsKey"), 20, "goat's milk");

            Assert.IsNotNull(apiResponse);
            Assert.IsNotNull(apiResponse.foodsList);
            Assert.AreEqual(ResponseEnumFoods.Success, apiResponse.responseStatus);
            Assert.AreEqual(19, apiResponse.foodsList.ToArray().GetUpperBound(0));

            foreach (var food in apiResponse.foodsList)
            {
                Console.WriteLine(food.foodInformation.BrandName + " " + food.foodInformation.Name + " " + food.foodInformation.HouseholdServing);
                Console.WriteLine(food.foodInformation.Ingredients);
                Console.WriteLine(" ");
            }
        }

        [TestMethod]
        public async Task TestMethodPeople1()
        {
            //Thread.Sleep(10000);

            var apiResponse = await People.GetPeopleAsync(ConfigurationManager.AppSettings.Get("HSS_API_Store_EmailAddress"), 
                ConfigurationManager.AppSettings.Get("HSS_API_Store_PeopleKey"), 5, StateSelection: StateIDEnum.AR);

            Assert.IsNotNull(apiResponse);
            Assert.IsNotNull(apiResponse.peopleList);
            Assert.AreEqual(ResponseEnumPeople.Success, apiResponse.responseStatus);
            Assert.AreEqual(4, apiResponse.peopleList.ToArray().GetUpperBound(0));

            foreach (var person in apiResponse.peopleList)
            {
                Console.WriteLine(person.Prefix + " " + person.FirstName + " " + person.LastName);
                Console.WriteLine(person.AddressLine1);
                if (person.AddressLine2 != null && person.AddressLine2 != "")
                    Console.WriteLine(person.AddressLine2);
                Console.WriteLine(person.City + ", " + person.StateID + " " + person.ZipCode);
                Console.WriteLine(person.PhoneNumber.FormatPhoneNumber());
                Console.WriteLine(" ");
            }
        }
    }
}

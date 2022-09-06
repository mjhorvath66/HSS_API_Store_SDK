using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using static HSS_API_Store_SDK.apiResponsePasswords;

namespace HSS_API_Store_SDK
{
    public class apiResponseFoods
    {
        /// <summary>
        /// This enum type defines the status results returned by GetFoodsAPI GET api/Foods call.
        /// </summary>
        public enum ResponseEnumFoods
        {
            /// <summary>
            /// The get succeeded and the foods list can be used.
            /// </summary>
            Success = 0,
            /// <summary>
            /// The Count parameter is not in range of 1 to 20.
            /// </summary>
            InvalidCount = 1,
            /// <summary>
            /// There was a problem finding the supplied EmailAddress and APIKey combination; therefore, access to api is denied.
            /// </summary>
            AccessDenied = 2,
            /// <summary>
            /// There was an unknown error making call to api.  We did not receive a response we expected.
            /// </summary>
            UnknownAPIError = 3
        }

        /// <summary>
        /// The status of GetFoodsAPI GET api/Foods call.
        /// </summary>
        public ResponseEnumFoods responseStatus { get; set; }
        /// <summary>
        /// Foods list containing the foods returned by search if status == Success.
        /// </summary>
        public List<foodResult> foodsList { get; set; }
        /// <summary>
        /// The all time number of calls made by this client to the GetFoodsAPI. (Will always return 1 for failed calls to the API.)
        /// </summary>
        public int allTimeNumberOfCallsToThisAPI { get; set; }
    }
    /// <summary>
    /// This class represents the results of querying the food products database table. This information is from the USDA food database from https://fdc.nal.usda.gov/download-datasets.html.
    /// </summary>
    public partial class FoodQueryResults
    {
        /// <summary>
        /// Key to the food products table used to reference the nutrional information table to find nutrional information.
        /// </summary>
        public long fdc_id { get; set; }
        /// <summary>
        /// Name of the food retrieved from food products database table.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// UPC code on food product's packaging label.
        /// </summary>
        public string UPC { get; set; }
        /// <summary>
        /// Manufacturer that processes and packages the food product.
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Food brand name.
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// Food sub-brand name.
        /// </summary>
        public string SubBrandName { get; set; }
        /// <summary>
        /// Date that this data was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Date that this food product's information became available.
        /// </summary>
        public DateTime AvailableDate { get; set; }
        /// <summary>
        /// Ingredients from the food product's packaging label.
        /// </summary>
        public string Ingredients { get; set; }
        /// <summary>
        /// Nutrients that the food product is not a source of.
        /// </summary>
        public string NotASourceOf { get; set; }
        /// <summary>
        /// Numeric value for the serving size of the food product.
        /// </summary>
        public decimal ServingSize { get; set; }
        /// <summary>
        /// Unit of measure of the serving size value of the food product.
        /// </summary>
        public string ServingSizeUOM { get; set; }
        /// <summary>
        /// Friendly serving size information that describe portion size of food.
        /// </summary>
        public string HouseholdServing { get; set; }
    }

    /// <summary>
    /// This class represents the results of querying the food nutrients database table. This information is from the USDA food database from https://fdc.nal.usda.gov/download-datasets.html.
    /// </summary>
    public partial class FoodNutrientsQueryResults
    {
        /// <summary>
        /// Foriegn key from the food products table used to lookup the nutrients contained in a given food product.
        /// </summary>
        public long fdc_id { get; set; }
        /// <summary>
        /// Name of the nutrient eg. Energy.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Amount of the nutrient per serving of the food product.
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// Unit of measure of the amount of nutrient present in a serving of the food product.
        /// </summary>
        public string unit_name { get; set; }
    }

    /// <summary>
    /// This class represents each individual food product's query results.
    /// </summary>
    public class foodResult
    {
        /// <summary>
        /// This food information is found by querying the food product database table.
        /// </summary>
        public FoodQueryResults foodInformation { get; set; }
        /// <summary>
        /// The nutrional information of the food product is found by querying the food nutrients table.
        /// </summary>
        public List<FoodNutrientsQueryResults> nutrionalInformation { get; set; }
    }

    public static class Foods
    {
        /// <summary>
        /// This API will get the list of foods with their ingredients and nutrional information matching your search criterion.
        /// </summary>
        /// <param name="EmailAddress">Client's email address given when they requested an API Key.</param>
        /// <param name="APIKey">Client's API Key check your email for this after your request of an API Key.</param>
        /// <param name="Count">Range 1 to 20, limits number of result records returned. The actual number of records may be less than this Count value depending upon result set for your search criterion.</param>
        /// <param name="searchCriterion">Name of the food product or the food product's UPC code. (The default search uses the UPC code for Planters Lightly Salted Mixed Nuts.)</param>
        /// <returns>This API returns apiResponseFoods which includes state information about the call and if successful a collection of food records. See the apiResponseFoods definition below.</returns>
        public static async Task<apiResponseFoods> GetFoodsAsync(string EmailAddress, string APIKey, int Count = 1, string searchCriterion = "029000016699")
        {
            try
            {
                string path = "api/Foods?EmailAddress=" + EmailAddress + "&APIKey=" + APIKey + "&Count=" + Count.ToString() + "&searchCriterion=" + HttpUtility.UrlEncode(searchCriterion);

                if (Client.client.BaseAddress == null)
                {
                    // Update port # in the following line.
                    Client.client.BaseAddress = new Uri("https://horvathsoftware.com/HSS_API_Store/");
                    Client.client.DefaultRequestHeaders.Accept.Clear();
                    Client.client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                apiResponseFoods foods = null;
                HttpResponseMessage response = await Client.client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    foods = await response.Content.ReadAsAsync<apiResponseFoods>();
                }
                return foods;
            }
            catch
            {
                apiResponseFoods foods = new apiResponseFoods();

                foods.responseStatus = apiResponseFoods.ResponseEnumFoods.UnknownAPIError;
                foods.foodsList = null;
                foods.allTimeNumberOfCallsToThisAPI = 1;

                return foods;
            }
        }
    }
}
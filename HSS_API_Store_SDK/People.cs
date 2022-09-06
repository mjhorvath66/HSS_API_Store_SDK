using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using static HSS_API_Store_SDK.apiResponsePeople;

namespace HSS_API_Store_SDK
{
    public class apiResponsePeople
    {
        /// <summary>
        /// This enum type defines the status results returned by PeopleGeneratorAPI GET api/People call.
        /// </summary>
        public enum ResponseEnumPeople
        {
            /// <summary>
            /// The people generator succeeded and the people list can be used.
            /// </summary>
            Success = 0,
            /// <summary>
            /// The Count parameter is not in range of 1 to 256.
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
        /// This enum type defines the state id enumeration that controls which state the fictional people records are from by PeopleGeneratorAPI GET api/People call.
        /// </summary>
        public enum StateIDEnum
        {
            /// <summary>
            /// The people generator will generate records of fictional people from any random state in the United States.
            /// </summary>
            Any,
            /// <summary>
            /// The people generator will generate records of fictional people from Alabama in the United States.
            /// </summary>
            AL,
            /// <summary>
            /// The people generator will generate records of fictional people from Alaska in the United States.
            /// </summary>
            AK,
            /// <summary>
            /// The people generator will generate records of fictional people from Arkansas in the United States.
            /// </summary>
            AR,
            /// <summary>
            /// The people generator will generate records of fictional people from Arizona in the United States.
            /// </summary>
            AZ,
            /// <summary>
            /// The people generator will generate records of fictional people from California in the United States.
            /// </summary>
            CA,
            /// <summary>
            /// The people generator will generate records of fictional people from Colorado in the United States.
            /// </summary>
            CO,
            /// <summary>
            /// The people generator will generate records of fictional people from Connecticut in the United States.
            /// </summary>
            CT,
            /// <summary>
            /// The people generator will generate records of fictional people from Washington D.C. in the United States.
            /// </summary>
            DC,
            /// <summary>
            /// The people generator will generate records of fictional people from Delaware in the United States.
            /// </summary>
            DE,
            /// <summary>
            /// The people generator will generate records of fictional people from Florida in the United States.
            /// </summary>
            FL,
            /// <summary>
            /// The people generator will generate records of fictional people from Georgia in the United States.
            /// </summary>
            GA,
            /// <summary>
            /// The people generator will generate records of fictional people from Hawaii in the United States.
            /// </summary>
            HI,
            /// <summary>
            /// The people generator will generate records of fictional people from Iowa in the United States.
            /// </summary>
            IA,
            /// <summary>
            /// The people generator will generate records of fictional people from Idaho in the United States.
            /// </summary>
            ID,
            /// <summary>
            /// The people generator will generate records of fictional people from Illinois in the United States.
            /// </summary>
            IL,
            /// <summary>
            /// The people generator will generate records of fictional people from Indiana in the United States.
            /// </summary>
            IN,
            /// <summary>
            /// The people generator will generate records of fictional people from Kansas in the United States.
            /// </summary>
            KS,
            /// <summary>
            /// The people generator will generate records of fictional people from Kentucky in the United States.
            /// </summary>
            KY,
            /// <summary>
            /// The people generator will generate records of fictional people from Louisiana in the United States.
            /// </summary>
            LA,
            /// <summary>
            /// The people generator will generate records of fictional people from Massachusetts in the United States.
            /// </summary>
            MA,
            /// <summary>
            /// The people generator will generate records of fictional people from Maryland in the United States.
            /// </summary>
            MD,
            /// <summary>
            /// The people generator will generate records of fictional people from Maine in the United States.
            /// </summary>
            ME,
            /// <summary>
            /// The people generator will generate records of fictional people from Michigan in the United States.
            /// </summary>
            MI,
            /// <summary>
            /// The people generator will generate records of fictional people from Minnesota in the United States.
            /// </summary>
            MN,
            /// <summary>
            /// The people generator will generate records of fictional people from Missouri in the United States.
            /// </summary>
            MO,
            /// <summary>
            /// The people generator will generate records of fictional people from Mississippi in the United States.
            /// </summary>
            MS,
            /// <summary>
            /// The people generator will generate records of fictional people from Montana in the United States.
            /// </summary>
            MT,
            /// <summary>
            /// The people generator will generate records of fictional people from North Carolina in the United States.
            /// </summary>
            NC,
            /// <summary>
            /// The people generator will generate records of fictional people from North Dakota in the United States.
            /// </summary>
            ND,
            /// <summary>
            /// The people generator will generate records of fictional people from Nebraska in the United States.
            /// </summary>
            NE,
            /// <summary>
            /// The people generator will generate records of fictional people from New Hampshire in the United States.
            /// </summary>
            NH,
            /// <summary>
            /// The people generator will generate records of fictional people from New Jersey in the United States.
            /// </summary>
            NJ,
            /// <summary>
            /// The people generator will generate records of fictional people from New Mexico in the United States.
            /// </summary>
            NM,
            /// <summary>
            /// The people generator will generate records of fictional people from Nevada in the United States.
            /// </summary>
            NV,
            /// <summary>
            /// The people generator will generate records of fictional people from New York in the United States.
            /// </summary>
            NY,
            /// <summary>
            /// The people generator will generate records of fictional people from Oklahoma in the United States.
            /// </summary>
            OK,
            /// <summary>
            /// The people generator will generate records of fictional people from Ohio in the United States.
            /// </summary>
            OH,
            /// <summary>
            /// The people generator will generate records of fictional people from Oregon in the United States.
            /// </summary>
            OR,
            /// <summary>
            /// The people generator will generate records of fictional people from Pennsylvania in the United States.
            /// </summary>
            PA,
            /// <summary>
            /// The people generator will generate records of fictional people from Rhode Island in the United States.
            /// </summary>
            RI,
            /// <summary>
            /// The people generator will generate records of fictional people from South Carolina in the United States.
            /// </summary>
            SC,
            /// <summary>
            /// The people generator will generate records of fictional people from South Dakota in the United States.
            /// </summary>
            SD,
            /// <summary>
            /// The people generator will generate records of fictional people from Tennessee in the United States.
            /// </summary>
            TN,
            /// <summary>
            /// The people generator will generate records of fictional people from Texas in the United States.
            /// </summary>
            TX,
            /// <summary>
            /// The people generator will generate records of fictional people from Utah in the United States.
            /// </summary>
            UT,
            /// <summary>
            /// The people generator will generate records of fictional people from Virginia in the United States.
            /// </summary>
            VA,
            /// <summary>
            /// The people generator will generate records of fictional people from Vermont in the United States.
            /// </summary>
            VT,
            /// <summary>
            /// The people generator will generate records of fictional people from Washington in the United States.
            /// </summary>
            WA,
            /// <summary>
            /// The people generator will generate records of fictional people from Wisconsin in the United States.
            /// </summary>
            WI,
            /// <summary>
            /// The people generator will generate records of fictional people from West Virginia in the United States.
            /// </summary>
            WV,
            /// <summary>
            /// The people generator will generate records of fictional people from Wyoming in the United States.
            /// </summary>
            WY
        }

        /// <summary>
        /// The status of PeopleGeneratorAPI GET api/Passwords call.
        /// </summary>
        public ResponseEnumPeople responseStatus { get; set; }
        /// <summary>
        /// People list containing the people generated if status == Success.
        /// </summary>
        public List<spGetCompletePeopleRecords> peopleList { get; set; }
        /// <summary>
        /// The all time number of calls made by this client to the PasswordGeneratorAPI. (Will always return 1 for failed calls to the API.)
        /// </summary>
        public int allTimeNumberOfCallsToThisAPI { get; set; }
    }

    public static class peopleExtensions
    {
        public static string FormatPhoneNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            value = new System.Text.RegularExpressions.Regex(@"\D")
                .Replace(value, string.Empty);
            value = value.TrimStart('1');
            if (value.Length == 7)
                return Convert.ToInt64(value).ToString("0##-####");
            if (value.Length == 10)
                return Convert.ToInt64(value).ToString("+1(0##)###-####");
            if (value.Length > 10)
                return Convert.ToInt64(value)
                    .ToString("+1(0##)###-#### " + new String('#', (value.Length - 10)));
            return value;
        }
    }

    /// <summary>
    /// This class represents the resulting records returned by spGetCompletePeopleRecords called by the PeopleGeneratorAPI.
    /// </summary>
    public class spGetCompletePeopleRecords
    {
        /// <summary>
        /// First name of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Middle name of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Last name of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Prefix of fictional person's name eg. Mr., Mrs., Dr., etc... returned by PeopleGeneratorAPI.
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// Suffix of fictional person's name eg. Jr., Sr., etc... returned by PeopleGeneratorAPI.
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// Birth date of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Gender of fictional person eg. M or F returned by PeopleGeneratorAPI.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Social Security Number of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string SocialSecurityNumber { get; set; }
        /// <summary>
        /// Maiden name of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string MaidenName { get; set; }
        /// <summary>
        /// Alias of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Address line1 of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// Address line2 of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string AddressLine2 { get; set; }
        /// <summary>
        /// City of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State code for state of address of fictional person, eg. AR,AZ,TX, etc... returned by PeopleGeneratorAPI.
        /// </summary>
        public string StateID { get; set; }
        /// <summary>
        /// Zip code of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Zip code plus 4 of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string Zip4Code { get; set; }
        /// <summary>
        /// County name of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// Latitude decimal value of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public Nullable<decimal> Latitude { get; set; }
        /// <summary>
        /// Longitude decimal value of address of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public Nullable<decimal> Longitude { get; set; }
        /// <summary>
        /// Phone number of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Extension of phone number of fictional person returned by PeopleGeneratorAPI.
        /// </summary>
        public string Extension { get; set; }
    }

    public static class People
    {
        /// <summary>
        /// This API will generate a list of random fictional people intended to be used as test data for test automation, but any other non-nefarious use would be fine with us.
        /// </summary>
        /// <param name="EmailAddress">Client's email address given when they requested an API Key.</param>
        /// <param name="APIKey">Client's API Key check your email for this after your request of an API Key.</param>
        /// <param name="Count">Range 1 to 256</param>
        /// <param name="StateSelection">Records from a particular state or any state in the United States.</param>
        /// <returns>This API returns apiResponsePeople which includes state information about the call and if successful a collection of people records. See the apiResponsePeople definition below.</returns>
        public static async Task<apiResponsePeople> GetPeopleAsync(string EmailAddress, string APIKey, int Count = 1, StateIDEnum StateSelection = StateIDEnum.Any)
        {
            try
            {
                string path = "api/People?EmailAddress=" + EmailAddress + "&APIKey=" + APIKey + "&Count=" + Count.ToString() + "&StateSelection=" + StateSelection.ToString();

                if (Client.client.BaseAddress == null)
                {
                    // Update port # in the following line.
                    Client.client.BaseAddress = new Uri("https://horvathsoftware.com/HSS_API_Store/");
                    Client.client.DefaultRequestHeaders.Accept.Clear();
                    Client.client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                apiResponsePeople people = null;
                HttpResponseMessage response = await Client.client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    people = await response.Content.ReadAsAsync<apiResponsePeople>();
                }
                return people;
            }
            catch
            {
                apiResponsePeople people = new apiResponsePeople();
                people.responseStatus = ResponseEnumPeople.UnknownAPIError;
                people.peopleList = null;
                people.allTimeNumberOfCallsToThisAPI = 1;

                return people;
            }
        }
    }
}
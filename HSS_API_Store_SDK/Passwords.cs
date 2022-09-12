using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using static HSS_API_Store_SDK.apiResponsePasswords;

namespace HSS_API_Store_SDK
{
    public class apiResponsePasswords
    {
        /// <summary>
        /// This enum type defines the status results returned by PasswordGeneratorAPI GET api/Passwords call.
        /// </summary>
        public enum ResponseEnumPasswords
        {
            /// <summary>
            /// The password generator succeeded and the passwordValue can be used.
            /// </summary>
            Success = 0,
            /// <summary>
            /// The Count parameter is not in range of 1 to 50 or WordCount is not in range of 3 to 10.
            /// </summary>
            InvalidCount = 1,
            /// <summary>
            /// One or both of the minLength and maxLength parameters is out of range (1 to 512) or MinWordLength is not in range of 3 to 10.
            /// </summary>
            LengthOutOfRange = 2,
            /// <summary>
            /// There was a problem finding the supplied EmailAddress and APIKey combination; therefore, access to api is denied.
            /// </summary>
            AccessDenied = 3,
            /// <summary>
            /// There was an unknown error processing the api call.
            /// </summary>
            UnknownAPIError = 4
        }

        /// <summary>
        /// Enum type defining values controlling what the password generator is required to include in passwords it generates.
        /// </summary>
        public enum RequiredSymbols
        {
            /// <summary>
            /// Only digits [0-9] will be included in password.
            /// </summary>
            Digits = 1,
            /// <summary>
            /// Only lower case characters [a-z] will be included in password.
            /// </summary>
            LowerCase = 2,
            /// <summary>
            /// Both digits [0-9] and lower case characters [a-z] will be included in password.
            /// </summary>
            DigitsAndLowerCase = 3,
            /// <summary>
            /// Only UPPER case characters [A-Z] will be included in password.
            /// </summary>
            UpperCase = 4,
            /// <summary>
            /// Both digits [0-9] and UPPER case characters [A-Z] will be included in password.
            /// </summary>
            DigitsAndUpperCase = 5,
            /// <summary>
            /// Both lower case characters [a-z] and UPPER case characters [A-Z] will be included in password.
            /// </summary>
            LowerCaseAndUpperCase = 6,
            /// <summary>
            /// All three digits [0-9], lower case characters [a-z] and UPPER case characters [A-Z] will be included in password.
            /// </summary>
            DigitsLowerCaseAndUpperCase = 7,
            /// <summary>
            /// Only non-digit [0-9] and non-character [a-zA-Z] will be included in password. (Any symbols excluded will also be omitted or if allowedInstead == True only the symbols allowed will be included.)
            /// </summary>
            Symbols = 8,
            /// <summary>
            /// Both digits [0-9] and non-character [a-zA-Z] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            DigitsAndSymbols = 9,
            /// <summary>
            /// Both lower case [a-z] and non-character [A-Z] and non-digit [0-9] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            LowerCaseAndSymbols = 10,
            /// <summary>
            /// All three digits [0-9], lower case [a-z] and non-character [A-Z] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            DigitsLowerCaseAndSymbols = 11,
            /// <summary>
            /// Both UPPER case [A-Z] and non-character [a-z] and non-digit [0-9] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            UpperCaseAndSymbols = 12,
            /// <summary>
            /// All three digits [0-9], UPPER case [A-Z] and non-character [a-z] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            DigitsUpperCaseAndSymbols = 13,
            /// <summary>
            /// All three lower case [a-z], UPPER case [A-Z] and non-digit [0-9] will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            LowerCaseUpperCaseAndSymbols = 14,
            /// <summary>
            /// All four lower case [a-z], UPPER case [A-Z], digits [0-9] and symbols will be included in password. (Rules for excluded/allowed symbols apply here too.)
            /// </summary>
            All = 15,
            /// <summary>
            /// Generate random passphrases instead of passwords. Uses optional parameters WordCount (range 3 to 10) and MinWordLength (range 3 to 10).
            /// </summary>
            Passphrase = 16
        }

        /// <summary>
        /// The status of PasswordGeneratorAPI GET api/Passwords call.
        /// </summary>
        public ResponseEnumPasswords responseStatus { get; set; }
        /// <summary>
        /// String array containing the passwords generated if status == Success.
        /// </summary>
        public string[] passwordValues { get; set; }
        /// <summary>
        /// The all time number of calls made by this client to the PasswordGeneratorAPI. (Will always return 1 for failed calls to the API.)
        /// </summary>
        public int allTimeNumberOfCallsToThisAPI { get; set; }
    }

    public static class Passwords
    {
        /// <summary>
        /// This API will generate an array of random passwords that selects random characters using parameters specified in the API call. The length is random should the user specify a range using minLength and maxLength parameters. When a PasswordCharacterSelection that includes symbols is selected the ExcludedOrAllowed parameter controls which symbols are included. (allowedInstead if True tells the API to pick only symbols from ExcludedOrAllowed parameter when a symbol is being added to the password; otherwise, the symbols from ExcludedOrAllowed are not picked when a symbol is being added to the password.) 
        /// </summary>
        /// <param name="EmailAddress">Client's email address given when they requested an API Key.</param>
        /// <param name="APIKey">Client's API Key check your email for this after your request of an API Key.</param>
        /// <param name="Count">Range 1 to 50</param>
        /// <param name="minLength">Range 1 to 512 if >= maxLength all passwords are fixed length == minLength</param>
        /// <param name="maxLength">Range 1 to 512</param>
        /// <param name="ExcludedOrAllowed">This string of characters defines list of symbols and characters not to include in any password generated with symbols in it. Note that if the allowedInstead == true then only the symbols and characters listed are picked to include in the password.</param>
        /// <param name="allowedInstead">When this is false the ExcludedOrAllowed parameter lists excluded symbols and characters.  If it is true then only those listed will be picked for symbols when the password generator is adding a symbol to the password it is generating.</param>
        /// <param name="PasswordCharacterSelection">This enum parameter controls the types of characters and symbols picked by the password generator.  For example: Digits will pick only numeric digits [0-9]. For more details on this see our website [HSS_API_Store]https://horvathsoftware.com/HSS_API_Store.</param>
        /// <param name="WordCount">Range 3 to 10 only used when PasswordCharacterSelection == Passphrase controls the number of words in the passphrase.</param>
        /// <param name="MinWordLength">Range 3 to 10 only used when PasswordCharacterSelection == Passphrase controls the minimum length of the individual words the passphrase contains.</param>
        /// <returns>This API returns apiResponsePasswords which includes state information about the call and if successful a collection of passwords. See the apiResponsePasswords definition below.</returns>
        public static async Task<apiResponsePasswords> GetPasswordsAsync(string EmailAddress, string APIKey, int Count = 1, int minLength = 9, int maxLength = 20, string ExcludedOrAllowed = "~!@#$%", bool allowedInstead = true, RequiredSymbols PasswordCharacterSelection = RequiredSymbols.All, int WordCount=3, int MinWordLength=6)
        {
            try
            {
                string path = "api/Passwords?EmailAddress=" + EmailAddress + "&APIKey=" + APIKey + "&Count=" + Count.ToString() + "&minLength=" + minLength.ToString() + "&maxLength=" + maxLength.ToString() + "&ExcludedOrAllowed=" + HttpUtility.UrlEncode(ExcludedOrAllowed) + "&allowedInstead=" + allowedInstead.ToString() + "&PasswordCharacterSelection=" + PasswordCharacterSelection.ToString() + "&WordCount=" + WordCount.ToString() + "&MinWordLength=" + MinWordLength.ToString();

                if (Client.client.BaseAddress == null)
                {
                    // Update port # in the following line.
                    Client.client.BaseAddress = new Uri("https://horvathsoftware.com/HSS_API_Store/");
                    Client.client.DefaultRequestHeaders.Accept.Clear();
                    Client.client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                apiResponsePasswords passwords = null;
                HttpResponseMessage response = await Client.client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    passwords = await response.Content.ReadAsAsync<apiResponsePasswords>();
                }
                return passwords;
            }
            catch
            {
                apiResponsePasswords passwords = new apiResponsePasswords();
                passwords.responseStatus = ResponseEnumPasswords.UnknownAPIError;
                passwords.passwordValues = null;
                passwords.allTimeNumberOfCallsToThisAPI = 1;

                return passwords;
            }
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using static HSS_API_Store_SDK.apiResponsePassphrase;

namespace HSS_API_Store_SDK
{
    public class apiResponsePassphrase
    {
        /// <summary>
        /// This enum type defines the status results returned by PassphraseGeneratorAPI GET api/Passphrase call.
        /// </summary>
        public enum ResponseEnumPassphrase
        {
            /// <summary>
            /// The passphrase generator succeeded and the passphraseValue can be used.
            /// </summary>
            Success = 0,
            /// <summary>
            /// The WordCount parameter is not in range of 3 to 10.
            /// </summary>
            InvalidWordCount = 1,
            /// <summary>
            /// The MinWordLength parameter is not in range of 3 to 10.
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
        /// The status of PassphraseGeneratorAPI GET api/Passphrase call.
        /// </summary>
        public ResponseEnumPassphrase responseStatus { get; set; }
        /// <summary>
        /// String containing the passphrase generated if status == Success.
        /// </summary>
        public string passphraseValue { get; set; }
        /// <summary>
        /// The all time number of calls made by this client to the PassphraseGeneratorAPI. (Will always return 1 for failed calls to the API.)
        /// </summary>
        public int allTimeNumberOfCallsToThisAPI { get; set; }
    }

    public static class Passphrase
    {
        /// <summary>
        /// This API will generate a random passphrase with WordCount number of English dictionary words of at least MinWordLength.
        /// </summary>
        /// <param name="EmailAddress">Client's email address given when they requested an API Key.</param>
        /// <param name="APIKey">Client's API Key check your email for this after your request of an API Key.</param>
        /// <param name="WordCount">Range 3 to 10, number of words in the passphrase.</param>
        /// <param name="MinWordLength">Range 3 to 10, minimum length of each word in the passphrase. </param>
        /// <returns>This API returns apiResponsePassphrase which includes state information about the call and if successful a passphrase string. See the apiResponsePassphrase definition below.</returns>
        public static async Task<apiResponsePassphrase> GetPassphraseAsync(string EmailAddress, string APIKey, int WordCount = 3, int MinWordLength = 3)
        {
            try
            {
                string path = "api/Passphrase?EmailAddress=" + EmailAddress + "&APIKey=" + APIKey + "&WordCount=" + WordCount.ToString() + "&MinWordLength=" + MinWordLength.ToString();

                if (Client.client.BaseAddress == null)
                {
                    // Update port # in the following line.
                    Client.client.BaseAddress = new Uri("https://horvathsoftware.com/HSS_API_Store/");
                    Client.client.DefaultRequestHeaders.Accept.Clear();
                    Client.client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }

                apiResponsePassphrase passphrase = null;
                HttpResponseMessage response = await Client.client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    passphrase = await response.Content.ReadAsAsync<apiResponsePassphrase>();
                }
                return passphrase;
            }
            catch
            {
                apiResponsePassphrase passphrase = new apiResponsePassphrase();
                passphrase.responseStatus = ResponseEnumPassphrase.UnknownAPIError;
                passphrase.passphraseValue = null;
                passphrase.allTimeNumberOfCallsToThisAPI = 1;

                return passphrase;
            }
        }
    }
}
using JMS_DAL.Firebase_Wrapper.RequestPayload;
using JMS_DAL.Firebase_Wrapper.ResponsePayload;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JMS_DAL
{
    public static class FirestoreHelper
    {
        static readonly HttpClient client = new HttpClient();
        const string SignInURL = @"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";

        static ResBody_SignInEmail SignInDetails;

        /// <summary>
        ///  Sends a Http POST request to the Firebase Authentication Server specified by the public facing Web API key set in the resources folder.
        /// </summary>
        /// <param name="Email">Firebase account email.</param>
        /// <param name="Password">Password of Firebase account.</param>
        /// <returns>Returns true if login is successful and token has been retrieved from the auth server.</returns>
        public static async Task<bool> SignIn(string Email, string Password)
        {
            Debug.WriteLine($"Sign In: {Email} - {Password}");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);4

            string test = JsonConvert.SerializeObject(
                new ReqBody_SignInEmail()
                {
                    email = Email,
                    password = Password,
                    returnSecureToken = true
                });
            var content = new StringContent(test, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(SignInURL + Properties.Resources.FirebaseAPIKey, content);
            
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful Sign In");
                string json = response.Content.ReadAsStringAsync().Result;
                SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful Sign In");
                SignInDetails = null;
                return false;
            }
        }

        /// <summary>
        /// Using given inputs, creates a http string query to be used for a http request.
        /// </summary>
        /// <param name="databaseID">Database ID as provided by firebase.</param>
        /// <param name="documentPath">.firebase.io.com/[documentPath] - document path without forwardslash at start.</param>
        /// <param name="fireIDtoken">Authentication token retireved from SignIn method.</param>
        /// <returns>Returns a fully concatenated string to be used for http request. </returns>
        private static string CreateQueryString(string databaseID,string documentPath)
        {
            return string.Concat("https://firestore.googleapis.com/v1/projects/",databaseID,"/databases/(default)/documents/",documentPath);
        }

    }
}

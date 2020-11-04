using JMS_DAL.Data_Objects;
using JMS_DAL.Firebase_Wrapper.RequestPayload;
using JMS_DAL.Firebase_Wrapper.ResponsePayload;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Runtime.CompilerServices;
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
            //Debug.WriteLine($"Sign In: {Email} - {Password}");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
        /// <param name="documentPath">/databases/(default)/documents/[documentPath] - document path without forwardslash at start.</param>
        /// <returns>Returns a fully concatenated string to be used for http request. </returns>
        private static string CreateQueryString(string databaseID,QueryType type,string documentPath = "",string documentID = "")
        {
            string query;

            switch (type)
            {
                case QueryType.Get:
                    query = string.Concat("https://firestore.googleapis.com/v1/projects/", databaseID, "/databases/(default)/documents/",documentPath);
                    break;

                case QueryType.Update:
                    query = string.Concat("https://firestore.googleapis.com/v1/projects/", databaseID, "/databases/(default)/documents/",documentPath,"/",documentID);
                    break;

                case QueryType.Delete:
                    query = string.Concat("https://firestore.googleapis.com/v1/projects/", databaseID, "/databases/(default)/documents/",documentPath,"/",documentID);
                    break;

                case QueryType.Commit:
                    query = string.Concat("https://firestore.googleapis.com/v1/projects/", databaseID, "/databases/(default)/documents:commit");
                    break;

                default:
                    query = "";
                    break;
            }
            
            return query;
        }

        public enum QueryType { Commit,Get,Update,Delete }


        #region CREATE DATA - HTTP Requests
        public static async Task<bool> PostData(ClientDTO dto,string DocumentID)
        {
            List<string> fields = new List<string>();
            string id = Guid.NewGuid().ToString();
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("ID",id));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("CompanyName",dto.CompanyName));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Address",dto.Address));
            string data = FirebasePostBuilder.ConvertToPostable("clients",id , string.Concat(fields[0], ",", fields[1], ",", fields[2]));
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Commit), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ClientDTO>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> PostData(EmployeeDTO dto)
        {
            List<string> fields = new List<string>();
            string id = Guid.NewGuid().ToString();
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("ID", id));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("FirstName", dto.FirstName));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("LastName", dto.LastName));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Role", dto.Role));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("UserUID", dto.UserUID));
            string data = FirebasePostBuilder.ConvertToPostable("employees", id, string.Concat(fields[0], ",", fields[1], ",", fields[2], ",", fields[3], ",", fields[4]));
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Commit), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> PostData(EquipmentDTO dto)
        {
            List<string> fields = new List<string>();
            string id = Guid.NewGuid().ToString();
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("ID", id));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Amount", dto.Amount));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Manufacturer", dto.Manufacturer));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Model", dto.Model));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Name", dto.Name));
            string data = FirebasePostBuilder.ConvertToPostable("equipment", id, string.Concat(fields[0], ",", fields[1], ",", fields[2], ",", fields[3], ",", fields[4]));
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Commit), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }


        public static async Task<bool> PostData(JobDTO dto)
        {
            List<string> fields = new List<string>();
            string id = Guid.NewGuid().ToString();
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("ID", id));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("ClientID", dto.ClientID));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("EquipmentList", dto.EquipmentList.ToArray()));
            fields.Add(FirebasePostBuilder.ConvertFieldToJSON("Name", dto.Name));
            string data = FirebasePostBuilder.ConvertToPostable("clients", id, string.Concat(fields[0], ",", fields[1], ",", fields[2], ",", fields[3]));
            var content = new StringContent(data, Encoding.UTF8, "application/json");


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Commit), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }
        #endregion

        #region READ DATA - HTTP Requests
        public static async Task<ClientDTO> GetAllData()
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Get,"clients"));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        #endregion

        #region UPDATE DATA - HTTP Requests
        public static async Task<bool> UpdateData()
        {
            string data = JsonConvert.SerializeObject(
                new ReqBody_SignInEmail()
                {

                });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(SignInURL + Properties.Resources.FirebaseAPIKey, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        #endregion

        #region DELETE DATA - HTTP Requests
        public static async Task<bool> DeleteData()
        {
            string data = JsonConvert.SerializeObject(
                new ReqBody_SignInEmail()
                {

                });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SignInDetails.idToken);
            HttpResponseMessage response = await client.PostAsync(SignInURL + Properties.Resources.FirebaseAPIKey, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                //string json = response.Content.ReadAsStringAsync().Result;
                //SignInDetails = JsonConvert.DeserializeObject<ResBody_SignInEmail>(json);
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        #endregion
    
    }

    public static class FirebasePostBuilder
    {
        public static string ConvertToPostable(string documentName, string entryID, string fields)
        {
            return string.Concat("{ \"writes\": [ { \"update\": { \"name\":\"projects/",
                Properties.Resources.ProjectID, "/databases/(default)/documents/", documentName, "/",
                entryID, "\",\"fields\": {", fields, "} } } ] }");
        }

        public static string ConvertFieldToJSON(string name, string value)
        {
            return string.Concat("\"", name, "\":{\"stringValue\":\"", value, "\"}");
        }
        public static string ConvertFieldToJSON(string name, int value)
        {
            return string.Concat("\"", name, "\":{\"integerValue\":\"", value, "\"}");
        }
        public static string ConvertFieldToJSON(string name, string[] value)
        {
            return string.Concat("\"", name, "\":{\"arrayValue\":\"", JsonConvert.SerializeObject(value), "\"}");
        }
    }

}

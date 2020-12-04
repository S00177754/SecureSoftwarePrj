// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using JMS_DAL.Data_Objects;
using JMS_DAL.Firebase_Wrapper.RequestPayload;
using JMS_DAL.Firebase_Wrapper.ResponsePayload;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public static class FirebaseHelper
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
                    query = string.Concat("https://", databaseID, ".firebaseio.com/", documentPath,"/",documentID, ".json?auth=", SignInDetails.idToken);
                    break;

                case QueryType.Update:
                    query = string.Concat("https://", databaseID, ".firebaseio.com/", documentPath, "/", documentID, ".json?auth=", SignInDetails.idToken);
                    break;

                case QueryType.Delete:
                    query = string.Concat("https://", databaseID, ".firebaseio.com/", documentPath, "/", documentID, ".json?auth=", SignInDetails.idToken);
                    break;

                case QueryType.GetAll:
                case QueryType.Commit:
                    query = string.Concat("https://", databaseID, ".firebaseio.com/", documentPath,".json?auth=", SignInDetails.idToken);
                    break;

                default:
                    query = "";
                    break;
            }
            
            return query;
        }

        public enum QueryType { Commit,Get,GetAll,Update,Delete }


        #region CREATE DATA - HTTP Requests
        public static async Task<string> PostData(ClientDTO dto)
        {
            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Update,"clients",dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dict.Values.FirstOrDefault();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return "";
            }
        }

        public static async Task<string> PostData(EmployeeDTO dto)
        {
            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Update,"employees",dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string,string> dict = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);
                return dict.Values.FirstOrDefault();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return "";
            }
        }

        public static async Task<string> PostData(EquipmentDTO dto)
        {
            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Update,"equipment",dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dict.Values.FirstOrDefault();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return "";
            }
        }


        public static async Task<string> PostData(JobDTO dto)
        {
            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.Update,"jobs",dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dict.Values.FirstOrDefault();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return "";
            }
        }
        #endregion

        #region READ ALL DATA - HTTP Requests

        [Serializable]
        public class FirebaseDoc<T>
        {
            public List<T> document;
        }

        public static async Task<List<ClientDTO>> GetAllClientData()
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID,QueryType.GetAll,"clients"));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string,ClientDTO> data = JsonConvert.DeserializeObject<Dictionary<string,ClientDTO>>(json);
                return data.Values.ToList();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<List<EquipmentDTO>> GetAllEquipmentData()
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.GetAll, "equipment"));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string,EquipmentDTO> data = JsonConvert.DeserializeObject<Dictionary<string,EquipmentDTO>>(json);
                return data.Values.ToList();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<List<JobDTO>> GetAllJobData()
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.GetAll, "jobs"));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string,JobDTO> data = JsonConvert.DeserializeObject<Dictionary<string,JobDTO>>(json);
                return data.Values.ToList();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<List<EmployeeDTO>> GetAllEmployeeData()
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.GetAll, "employees"));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string,EmployeeDTO> data = JsonConvert.DeserializeObject<Dictionary<string,EmployeeDTO>>(json);
                return data.Values.ToList();
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }


        #endregion

        #region READ DATA - HTTP Requests

        public static async Task<ClientDTO> GetClientData(string id)
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Get, "clients",id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                ClientDTO data = JsonConvert.DeserializeObject<ClientDTO>(json);
                return data;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<EquipmentDTO> GetEquipmentData(string id)
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Get, "equipment",id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                EquipmentDTO data = JsonConvert.DeserializeObject<EquipmentDTO>(json);
                return data;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<JobDTO> GetJobData(string id)
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Get, "jobs",id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                JobDTO data = JsonConvert.DeserializeObject<JobDTO>(json);
                return data;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }

        public static async Task<EmployeeDTO> GetEmployeeData(string id)
        {
            HttpResponseMessage response = await client.GetAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Get, "employees",id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                string json = response.Content.ReadAsStringAsync().Result;
                EmployeeDTO data = JsonConvert.DeserializeObject<EmployeeDTO>(json);
                return data;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return null;
            }
        }


        #endregion

        #region UPDATE DATA - HTTP Requests
        public static async Task<bool> UpdateData(ClientDTO dto)
        {

            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Update, "clients",dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> UpdateData(EmployeeDTO dto)
        {

            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Update, "employees", dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> UpdateData(EquipmentDTO dto)
        {

            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Update, "equipment", dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> UpdateData(JobDTO dto)
        {

            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Update, "jobs", dto.ID), content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
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
        public static async Task<bool> DeleteClientData(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Delete, "clients", id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> DeleteJobData(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Delete, "jobs", id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> DeleteEmployeeData(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Delete, "employees", id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
                return true;
            }
            else
            {
                Debug.WriteLine("Unsuccessful");
                return false;
            }
        }

        public static async Task<bool> DeleteEquipmentData(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(CreateQueryString(Properties.Resources.ProjectID, QueryType.Delete, "equipment", id));

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successful");
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
}

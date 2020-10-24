using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using Google.Cloud.Firestore;

namespace JMS_DAL
{
    public static class FirestoreHelper
    {

        public static void Initialize()
        {
            string ID = Properties.Resources.ResourceManager.GetString("ProjectID");
            FirestoreDb database = FirestoreDb.Create(ID);
            Console.WriteLine($"Created Cloud Firestore client with project ID: {ID}");
        }

        public static void AddData()
        {
            
        }

    }
}

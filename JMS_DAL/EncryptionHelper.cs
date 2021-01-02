// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace JMS_DAL
{
    static public class EncryptionHelper
    {

        public static string Encrypt(string plainText, RSAParameters publicKey)
        {
            using(RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
            {
                csp.ImportParameters(publicKey);

                byte[] plaintext_data = Encoding.ASCII.GetBytes(plainText);
                byte[] ciphertext_data = csp.Encrypt(plaintext_data,false);

                return Convert.ToBase64String(ciphertext_data);
            }
        }

        public static string Decrypt(string cipherText64, RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
            {
                csp.ImportParameters(privateKey);

                byte[] cipher = Convert.FromBase64String(cipherText64);
                
                //string cipherText = Encoding.ASCII.GetString(cipher);
                //byte[] cipherTextData = Encoding.ASCII.GetBytes(cipherText);
                byte[] plaintext_data = csp.Decrypt(cipher, false);
                string plainText = Encoding.ASCII.GetString(plaintext_data);
                return plainText;
            }
        }

        public static KeyPair CreateNewKeySet()
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
            {
                return new KeyPair(csp.ExportParameters(true),csp.ExportParameters(false));
            }
        }

        
    }

    public class RSA_DTO
    {
        public string D;
        public string DP;
        public string DQ;
        public string Exponent;
        public string InverseQ;
        public string Modulus;
        public string P;
        public string Q;

        public void AssignData(RSAParameters param,bool privateKey)
        {
            if (privateKey)
            {
                ConvertByteArray(out D,param.D);
                Debug.WriteLine("From Param: " + D);
                ConvertByteArray(out DP,param.DP);
                ConvertByteArray(out DQ,param.DQ);
                ConvertByteArray(out InverseQ, param.InverseQ);
                ConvertByteArray(out P, param.P);
                ConvertByteArray(out Q, param.Q);
            }

            ConvertByteArray(out Exponent, param.Exponent);
            ConvertByteArray(out Modulus, param.Modulus);
        }

        public RSAParameters GetParameters(bool privateKey)
        {
            RSAParameters param = new RSAParameters();
            if (privateKey)
            {
                Debug.WriteLine("To Param: " + D);
                param.D = Convert.FromBase64String(D);
                //ConvertString(out param.D,D);
                ConvertString(out param.DP, DP);
                ConvertString(out param.DQ, DQ);
                ConvertString(out param.InverseQ, InverseQ);
                ConvertString(out param.P, P);
                ConvertString(out param.Q, Q);
            }
            ConvertString(out param.Exponent,Exponent);
            ConvertString(out param.Modulus,Modulus);
            return param;
        }

        private void ConvertByteArray(out string x,byte[] array)
        {
            //x = Encoding.ASCII.GetString(array, 0, array.Length);
            x = Convert.ToBase64String(array);
        }

        private void ConvertString(out byte[] array, string x)
        {
            //array = Encoding.ASCII.GetBytes(x);
            array = Convert.FromBase64String(x);
        }
    }

    [Serializable]
    public class KeyPair
    {
        public RSAParameters PrivateKey;
        public RSAParameters PublicKey;
        public string UserID;

        public KeyPair() { }

        public KeyPair(RSAParameters privateKey, RSAParameters publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;

        }

        public KeyPair(KeyPairDTO dto)
        {
            PrivateKey = dto.PrivateKey.GetParameters(true);
            PublicKey = dto.PublicKey.GetParameters(false);
        }

        //public string GetPublicKeyString()
        //{
        //    var sw = new StringWriter();
        //    var xs = new XmlSerializer(typeof(RSAParameters));
        //    xs.Serialize(sw, PublicKey);
        //    return sw.ToString();
        //}
        //public string GetPrivateKeyString()
        //{
        //    var sw = new StringWriter();
        //    var xs = new XmlSerializer(typeof(RSAParameters));
        //    xs.Serialize(sw, PrivateKey);
        //    return sw.ToString();
        //}
    }

    public class KeyPairDTO
    {
        public RSA_DTO PrivateKey;
        public RSA_DTO PublicKey;
        public string UserID;

        public KeyPairDTO() { }

        public KeyPairDTO(KeyPair pair)
        {
            PrivateKey = new RSA_DTO();
            PublicKey = new RSA_DTO();
            PrivateKey.AssignData(pair.PrivateKey,true);
            PublicKey.AssignData(pair.PublicKey,false);
        }

        //public RSAParameters GetPublicKey()
        //{
        //    using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
        //    {
        //        string serialized = "< RSAKeyValue >" + PublicKey + "</ RSAKeyValue >";
        //        csp.FromXmlString(serialized);
        //        return csp.ExportParameters(false);
        //    }
        //}

        //public RSAParameters GetPrivateKey()
        //{
        //    using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048))
        //    {
        //        string serialized = "< RSAKeyValue >" + PrivateKey + "</ RSAKeyValue >";
        //        csp.FromXmlString(serialized);
        //        return csp.ExportParameters(true);
        //    }
        //}

    }
}

// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Firebase_Wrapper.RequestPayload
{
    [Serializable]
    public class ReqBody_SignInEmail
    {
        public string email;
        public string password;
        public bool returnSecureToken;
    }
}

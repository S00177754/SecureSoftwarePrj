// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Firebase_Wrapper.ResponsePayload
{
    public class ResBody_SignInEmail
    {
        public string idToken;
        public string email;
        public string refreshToken;
        public string expiresIn;
        public string localId;
        public bool registered;
    }
}

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

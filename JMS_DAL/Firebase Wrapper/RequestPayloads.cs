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

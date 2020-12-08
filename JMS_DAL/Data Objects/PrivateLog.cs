using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class PrivateLogDTO : DTO
    {
        private string userID;
        public string UserID { get { return userID; } set { userID = value; } }

        private string message;
        public string Message { get { return message; } set { message = value; } }

        public PrivateLogDTO()
        {

        }

        public PrivateLogDTO(PrivateLog log)
        {
            ID = log.ID.ToString();
            Message = log.Message;
            UserID = log.UserID;
            LastModifiedBy = log.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"");
        }

    }


    public class PrivateLog : DataObject
    {
        private string userID;
        public string UserID { get { return userID; } set { userID = value; } }

        private string message;
        public string Message { get { return message; } set { message = value; } }

        public PrivateLog()
        {
        }

        public PrivateLog(PrivateLogDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            Message = dto.Message;
            UserID = dto.UserID;
            LastModifiedBy = dto.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"");
        }
    }
}

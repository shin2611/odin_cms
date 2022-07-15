using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public static class Enums
    {
        public enum ErrorCode
        {
            NotAuthen = -1001,
            Exception = -99,
            Failed = -1,
            Success = 1
        }
        public enum CAPTCHATYPE
        {
            MATH = 0,
            STRING = 1,
            TOKEN = 2,
            SESSION = 3
        }
    }
}

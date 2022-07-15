using DataAccess.Models;
namespace ODIN_CMS.Models
{
    public class Response
    {
        public int ResponseCode { get; set; }
        public string ResponseDesc { get; set; }
        public dynamic Data { get; set; }
        public bool IsAdmin { get; set; }
        public Response()
        {
        }

        public Response(int code, string desc)
        {
            ResponseDesc = desc;
            ResponseCode = code;
        }
        public Response(int code, string desc, dynamic data)
        {
            ResponseDesc = desc;
            ResponseCode = code;
            Data = data;
        }
        public Response(dynamic data)
        {
            ResponseCode = (int)Enums.ErrorCode.Success;
            ResponseDesc = "Thành công";
            Data = data;
        }

        public Response(string desc)
        {
            ResponseDesc = desc;
            ResponseCode = (int)Enums.ErrorCode.Failed;
        }
        public Response(int code, string desc, bool isAdmin)
        {
            ResponseDesc = desc;
            ResponseCode = code;
            IsAdmin = isAdmin;
        }
    }
}
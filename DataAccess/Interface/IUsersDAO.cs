using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
namespace DataAccess.Interface
{
    public interface IUsersDAO
    {
        int Authentication(string username, string password);
        Users SelectByUserID(int userId);
        Users GetByEmail(string email);
        Users GetByUsername(string Username);
        List<Users> GetListUsers(string Keyword, int isActive, int groupId, int merchantId);
        List<Users> GetAllUserByStatus(int Status);
        int UpdateUsers(Users users);
        int DelleteUsers(int userId);
        int UpdateActiveUser(int Id);
        int ChangePassword(string UserName, string PasswordOld, string PasswordNew);

        Users GetAccountInfo(int userId);
        Users GetUserInfo(string username);
        int ChangeAvatar(int userId, string avatar);
        int GetAccountIdByTeleChatId(long chatId);
        void InsertLogSMSOTP(int userId, long chatId, string Otp, bool IsLogin);
        int AuthenOTP(int userId, string Otp);
        int GetAccountByPhone(string phone);
        void UpdateChatIdTeleBot(int userId, long chatId);
    }
}

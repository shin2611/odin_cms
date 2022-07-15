using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
namespace DataAccess.Interface
{
    public interface IUserRoleDAO
    {
        #region Quyền truy cập chức năng
        UserFunction CheckPermission(int UserID, string ActionName);
        int UserFunctionInsert(UserFunction RoleFunction);
        
        int UserFunctionInsertList(int UserID, string ListRole, int CreateUserID, bool IsAdmin); //Thêm ds quyền -> clear toàn bộ chức năng đang tồn tại
        
        //Thêm quyền chức năng cho danh sách user
        int UserFunctionInsertListByFunctionID(int FunctionID, string ListRole, int CreateUserID); 
        int UserFunctionDelete(int UserID, int FunctionID);
        int UserFunctionDeleteList(int UserID, string ListFunction);
        int UserFunctionDeleteAll(int UserID);
        List<UserFunction> UserFunction_GetByUserID(int UserID);
        List<UserFunction> GetListUser_ByRoleFunctionID(int FunctionID, int IsGrant);
        #endregion

        #region GROUP FUNCTION

        int GroupFunctionInsertList(int GroupID, string ListRole, int Type);

        int GroupFunctionInsertListByFunctionID(int FunctionID, string ListRole, int CreateGroupID);
        int GroupFunctionDelete(int GroupID, int FunctionID);
        int GroupFunctionDeleteList(int GroupID, string ListFunction);
        int GroupFunctionDeleteAll(int GroupID);
        List<GroupFunctions> GroupFunction_GetByGroupID(int GroupID);
        List<GroupFunctions> GetListGroup_ByRoleFunctionID(int FunctionID, int IsGrant);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using DataAccess.Models;

namespace DataAccess.Interface
{
    public interface IFucntionsDAO
    {
        int GetFunctionID(string functionname);
        Functions GetFunctionByFunctionID(int functionId);
        Functions GetFunctionByActionName(string ActionName);
        List<Functions> GetListFunctionByUserID(int userId, int grant);
        List<Functions> GetListFunctions(string Keyword, int isAcitve, int pageNumber, int pageSize, ref int TotalRecord);
        List<Functions> GetListFunctionsByFather(int FatherID);
        int InsertUpdateFunction(Functions functions);
        int UpdateOrder(int FunctionID, int ParentID, int Order);
        int DelleteFunction(int functionId);
        List<Functions> SelectAllFunctionID(int fatherID, string name, int isactive, int isdisplay);
        List<Groups> GetListGroups(int groupId);
        List<GroupFunctions> GroupFunctionGetList(int groupId, int type);
        int GroupEdit(GroupEdit groups);
        int GroupFunctionEdit(GroupFunctionEdit functions);

        GroupFunctionDetail GroupFunctionDetail(string action, int groupId);
        int DeleteGroup(int groupId);

        GroupEdit GetInfo(int groupId);
    }
}

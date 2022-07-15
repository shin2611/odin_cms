using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Groups
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int MerchantID { get; set; }
    }

    public class GroupEdit
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int ExeUserID { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GroupFunctions
    {
        public string FunctionName { get; set; }
        public int FunctionID { get; set; }
        public string Url { get; set; }
        public int ParentID { get; set; }
        public int Order { get; set; }
        public string CssIcon { get; set; }
        public bool IsGrant { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDisplay { get; set; }

    }

    public class GroupFunctionDetail
    {
        public int FunctionID { get; set; }
        public int GroupID { get; set; }
        public bool IsGrant { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }

    public class GroupFunctionEdit
    {
        public int ExeType { get; set; }
        public int ExeUserID { get; set; }
        public int FunctionID { get; set; }
        public int GroupID { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDisplay { get; set; }
    }
}

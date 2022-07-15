﻿using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using DataAccess.Models;
using System.Data;
using System.Reflection;
using Utils.Security;
using ODIN_CMS.Models;

namespace ODIN_CMS.Controllers.Common
{
    public class CommonLib
    {
        public UserFunction Permission()
        {
            var userfunction = new UserFunction();//(UserFunction)HttpContext.Session.GetString(SessionsManager.SESSION_PERMISSION);
            return userfunction;
        }
       
        //private UserFunction Permission { get { return userfunction; } }


        //public List<TreeData> GetListTreeService(bool selectable, int ParentsID, List<Service> roots)
        //{
        //    var tmp = new List<TreeData>();
        //    var levesub = roots.FindAll(c => c.ParentsID == ParentsID);
        //    levesub.Sort((f1, f2) => f1.ServiceID.CompareTo(f2.ServiceID));
        //    if (levesub.Count <= 0) return null;
        //    foreach (var t in levesub)
        //    {
        //        roots.Remove(t);
        //        var data = new TreeData
        //        {
        //            serviceId = t.ServiceID,
        //            parentServiceId = t.ParentsID,
        //            groupService = t.GroupService,
        //            text = t.ServiceName,
        //            status = t.Status,
        //            selectable = selectable
        //        };
        //        if (data.serviceId <= 0)
        //        {
        //            data.selectable = false;
        //        }
        //        tmp.Add(data);
        //        var childrens = GetListTreeService(selectable, t.ServiceID, roots);
        //        data.nodes = childrens;
        //    }
        //    return tmp;


        //}

        //public List<TreeData> GetListTreeSelected(int ServiceSelect, int ParentsID, List<Service> roots)
        //{
        //    var tmp = new List<TreeData>();
        //    var levesub = roots.FindAll(c => c.ParentsID == ParentsID);
        //    levesub.Sort((f1, f2) => f1.ServiceID.CompareTo(f2.ServiceID));
        //    if (levesub.Count <= 0) return null;
        //    foreach (var t in levesub)
        //    {
        //        roots.Remove(t);
        //        var data = new TreeData
        //        {
        //            serviceId = t.ServiceID,
        //            parentServiceId = t.ParentsID,
        //            groupService = t.GroupService,
        //            text = "[" + t.ServiceID + "]" + " - " + t.ServiceName,
        //            status = t.Status,
        //            selectable = true,
        //            state = new state()
        //        };
        //        if (t.ServiceID <= 0)
        //        {
        //            data.selectable = false;
        //        }
        //        if (ServiceSelect == t.ServiceID)
        //        {
        //            data.state.selected = true;
        //        }
        //        var childrens = GetListTreeSelected(ServiceSelect, t.ServiceID, roots);
        //        data.nodes = childrens;
        //        tmp.Add(data);
        //    }
        //    return tmp;
        //}

        public List<TreeFunction> GetListTreeFunction(int ParentsID, List<Functions> roots)
        {
            var tmp = new List<TreeFunction>();
            var levesub = roots.FindAll(c => c.ParentID == ParentsID);
            levesub.Sort((f1, f2) => f1.FunctionID.CompareTo(f2.FunctionID));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeFunction
                {
                    FuntionId = t.FunctionID,
                    ParentId = t.ParentID,
                    text = t.FunctionName,
                    icon = t.CssIcon,
                    IsBtnGrant = false
                };
                if (!string.IsNullOrEmpty(t.Url))
                {
                    data.IsBtnGrant = true;
                }
                tmp.Add(data);
                var childrens = GetListTreeFunction(t.FunctionID, roots);
                data.nodes = childrens;
            }
            return tmp;


        }

        public List<TreeCategory> GetListTreeCategory(int ParentsID, List<Categories> roots, int main_cateId)
        {
            var tmp = new List<TreeCategory>();

            var levesub = roots.FindAll(c => c.FatherID == ParentsID);
            levesub.Sort((f1, f2) => f1.CategoryID.CompareTo(f2.CategoryID));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeCategory
                {
                    Id = t.CategoryID,
                    ParentID = t.FatherID,
                    text = DBCommon.StripDiacriticsFile(t.CategoryName),
                    state = new CateState
                    {
                        selected = false,
                        expanded = false,
                        @checked = false,
                        disabled = false,
                    }
                };
                if (data.Id == main_cateId)
                    data.state.selected = true;
                tmp.Add(data);
                //var childrens = GetListTreeCategory(t.CategoryID, roots, main_cateId);
                //data.nodes = childrens;
            }
            return tmp;
        }
        public static string GenerateSign(string dataSign, string keySign)
        {
            var sign = string.Empty;
            try
            {
                var ticks = DateTime.Now.Ticks;
                var plaintextSign = string.Format("{0}|{1}|{2}", dataSign, keySign, ticks);
                sign = string.Format("{0}.{1}", ticks, Encrypt.MD5(plaintextSign));
                return sign;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                return sign;
            }
        }
 



        #region Chuyen list -> datatable
        public DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        #endregion
    }
    public static class RandomLetter
    {
        static Random _random = new Random();
        public static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
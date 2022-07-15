using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Models;

namespace DataAccess.Interface
{
    public interface IFileManagerDAO
    {
        int Insert(FileManager fileInfo);
        int Update(FileManager fileInfo);
        int Delete(int Id);
        FileManager GetByID(int Id);
        List<FileManager> GetPaged(DateTime fromDate, DateTime toDate);
        DataTable MakeDataPaper(int totalPages, int currPages, int recordPerPages);
        string GetUniqueFileName(string strDir, string strFileName);
    }
}

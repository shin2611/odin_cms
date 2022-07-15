using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class FileManager
    {
        public enum FileType : byte
        {
            Image = 1,
            Music = 2,
            Clip = 3,
            Document = 4,
            Flash
        }
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public string Length
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime PostTime
        {
            get;
            set;
        }

        public string AccountName
        {
            get;
            set;
        }

        public string LinkImages
        {
            get;
            set;
        }
    }
}

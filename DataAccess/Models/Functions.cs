using System;

namespace DataAccess.Models
{
	public class Functions
	{
		public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string Url { get; set; }
        public bool IsDisplay { get; set; }
        public bool NewStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public int ParentID { get; set; }
        public string FatherName { get; set; }
        public int Order { get; set; }
        public string CssIcon { get; set; }
        public int Counter { get; set; }
        public bool IsReport { get; set; }
        public string ActionName { get; set; } //Ten Action
	}
}

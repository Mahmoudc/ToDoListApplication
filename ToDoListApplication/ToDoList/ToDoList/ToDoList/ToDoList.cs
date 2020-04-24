using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ToDoList
    {
        public string date;
        public string details;
        public ToDoList() {
            date = "";
            details = "";
        }
        public ToDoList(string date, string details) {
            this.date = date;
            this.details=details;
        }
        public string Date {
            get {
                return date;
            }
            set {
                date = value;
            }
        }
        public string Details
        {
            get
            {
                return details;
            }
            set
            {
                details = value;
            }
        }
        public string displayText() {
            if(date.Length==9)
                return date + "\t" + details;
            else
                return date + "\t\t" + details;
        }
       
    }
}

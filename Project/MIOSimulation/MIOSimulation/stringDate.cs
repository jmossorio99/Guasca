using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class stringDate
    {
        private DateTime date;
        private string data;

        public stringDate(DateTime date, string data)
        {
            this.date = date;
            this.data = data;
        }

        public DateTime Date { get; set; }
        public string Data()
        {
            return data;
        }

        public int compareTo(stringDate other)
        {
            return date.CompareTo(other.date);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCHET.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Factory_number { get; set; }
        public int Id_category { get; set; }
        public Category Category { get; set; }
        public string Type { get; set; }
        public string Type_work { get; set; }
        public string Status { get; set; }
        public DateTime Date_last_serv { get; set; }
        public DateTime Scheduled_date { get; set; }
        
    }
}


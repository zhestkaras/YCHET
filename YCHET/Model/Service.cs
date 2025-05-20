using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCHET.Model
{
    internal class Service
    {
        public int Id { get; set; }
        public Equipment Equipment { get; set; }
        public Workers Workers  { get; set; }

        public int Equipment_id { get; set; }
        public string Description_works { get; set; }
        public int Employee_id { get; set; }
        public DateTime? Setting_date { get; set; }
        public DateTime? Date_service { get; set; }
    }
}

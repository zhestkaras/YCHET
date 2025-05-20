using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCHET.Model
{
   public class Vacation
    {
        public int Id { get; set; }

        public Workers Workers { get; set; }
        public int Employee_id { get; set; }
        public DateTime? Start_vacation { get; set; }
        public string How_vacation { get; set; }    
        public int Sick_vacation { get; set; }
        public DateTime? Vacation_end_date { get; set; }
       
    }
}

/*
id 
 employee_id 
 start_vacation 
 how_vacation 
 sick_vacation 
*/
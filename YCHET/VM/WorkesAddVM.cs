using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YCHET.Model;
 using System.Text.RegularExpressions;

namespace YCHET.VM
{
    
    internal class WorkesAddVM : BaseVM
    {
        private Workers newWorkers = new();
        public Workers NewWorkers
        {
            get => newWorkers;
            set
            {
                newWorkers = value;
                Signal();
            }
        }

         /*  
        static void Main()
        {
            string email = "chsc.christian+schou@christian-schou.dk";

            string pattern = @"^(?i)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            Regex regex = new Regex(pattern);

            bool isValid = regex.IsMatch(email);

            if (isValid)
            {
                Console.WriteLine("Valid email address!");
            }
            else
            {
                Console.WriteLine("Invalid email address!");
            }
        }
         */

        

        public CommandVM InsertWorkes { get; set; }
        public WorkesAddVM()
        {
            InsertWorkes = new CommandVM(() =>
            {
                WorkersDB.GetDb().Insert(NewWorkers);
                close?.Invoke();
            },
                        () =>
                    !string.IsNullOrEmpty(newWorkers.Last_name) &&
                    !string.IsNullOrEmpty(newWorkers.First_name));

            //!DateTime.Is(newService.Factory_number) &&


        }
        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}

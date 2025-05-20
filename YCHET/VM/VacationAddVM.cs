using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCHET.Model;

namespace YCHET.VM
{
     
    internal class VacationAddVM : BaseVM
    {
        private Vacation newVacation = new();
        public Vacation NewVacation
        {
            get => newVacation;
            set
            {
                newVacation = value;
                Signal();
            }
        }

        public CommandVM InsertVacation { get; set; }
        public VacationAddVM(Workers selectedWorkers)
        {
            InsertVacation = new CommandVM(() =>
            {
                NewVacation.Workers = selectedWorkers;
                NewVacation.Employee_id = selectedWorkers.Id;
                VacationDB.GetDb().Insert(NewVacation);
                close?.Invoke();
            },
                        () =>
                        !string.IsNullOrEmpty(newVacation.How_vacation));
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

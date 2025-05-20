using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YCHET.Model;

namespace YCHET.VM
{

    internal class ServiceAddVM : BaseVM
    {
        private Service newService = new();
        public Workers Workers { get; set; }
        public List<Workers> WorkersList { get => workersList; set { workersList = value; Signal(); } }
        public Service NewService
        {
            get => newService;
            set
            {
                newService = value;
                Signal();
            }
        }

        public CommandVM InsertService { get; set; }
        public ServiceAddVM(Equipment selectedEquipment)
        {
            WorkersList = WorkersDB.GetDb().SelectAll();
            InsertService = new CommandVM(() =>
                {
                    if (Workers == null)
                    {
                        MessageBox.Show("Выберите сотрудника");
                        return;
                    }
                    newService.Employee_id = Workers.Id;
                    newService.Equipment_id = selectedEquipment.Id;
                    newService.Equipment = selectedEquipment;
                    ServiceDB.GetDb().Insert(NewService);
                    close?.Invoke();
                },
                        () =>
                        !string.IsNullOrEmpty(newService.Description_works));
            //!DateTime.Is(newService.Factory_number) &&


        }
        Action close;
        private List<Workers> workersList;

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


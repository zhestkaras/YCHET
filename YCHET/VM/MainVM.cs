using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Spire.Xls;
using YCHET.Model;
using YCHET.View;
using static System.Reflection.Metadata.BlobBuilder;

namespace YCHET.VM
{



    internal class MainVM : BaseVM
    {

        private Equipment selectedEquipment;
        private ObservableCollection<Equipment> equipment = new();
        private List<Equipment> equipment1 = new();
        private ObservableCollection<Equipment> equ;
        private string search;

        ObservableCollection<Workers> ViewWorkers { get; set; } = new ObservableCollection<Workers>();
        ObservableCollection<Vacation> ViewVacations { get; set; } = new ObservableCollection<Vacation>();

        public CommandVM CreateReport { get; set; }
        public string Search
        {
            get => search; set
            {
                search = value;
                SearchAll();
            }
        }
        /*
        public int Id { get; set; }
        public string title { get; set; }
        public int author_id { get; set; }
        public int year_published { get; set; }
        public string genre { get; set; }
        public bool is_available { get; set; }
        */

        public List<Equipment> Equipment
        {
            get => equipment1;
            set
            {
                equipment1 = value;
                Signal();
            }
        }
        public ObservableCollection<Equipment> Equ

        {
            get => equ;
            set
            {
                equ = value;
                Signal();
            }
        }
        public Equipment SelectedEquipment
        {
            get => selectedEquipment;
            set
            {
                selectedEquipment = value;
                Signal();
            }
        }
        public CommandVM UpdateEquipment { get; set; }
        public CommandVM RemoveEquipment { get; set; }
        public CommandVM AddEquipment { get; set; }
        public CommandVM EquipmentViewAdd { get; set; }


        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>

        private Service selectedService;
        private ObservableCollection<Service> service = new();
        private List<Service> service1 = new();
        private ObservableCollection<Service> ser;

        public List<Service> Service
        {
            get => service1;
            set
            {
                service1 = value;
                Signal();
            }
        }
        public ObservableCollection<Service> Ser

        {
            get => ser;
            set
            {
                ser = value;
                Signal();
            }
        }
        public Service SelectedService
        {
            get => selectedService;
            set
            {
                selectedService = value;
                Signal();
            }
        }
        public CommandVM UpdateService { get; set; }
        public CommandVM RemoveService { get; set; }
        public CommandVM AddService { get; set; }
        public CommandVM ServiceViewAdd { get; set; }

        ///////////////////////////////////////////////////
        /// <summary>
        /// /////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private Category selectedCategory;
        private ObservableCollection<Category> category = new();
        private List<Category> category1 = new();
        private ObservableCollection<Category> cat;

        public List<Category> Category
        {
            get => category1;
            set
            {
                category1 = value;
                Signal();
            }
        }
        public ObservableCollection<Category> Cat

        {
            get => cat;
            set
            {
                cat = value;
                Signal();
            }
        }
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                Signal();
            }
        }
        public CommandVM UpdateCategory { get; set; }
        public CommandVM SelectedCategoryy { get; set; }
        public CommandVM RemoveCategory { get; set; }
        public CommandVM AddCategory { get; set; }
        public CommandVM CategoryViewAdd { get; set; }
        /// <summary>
        /// /////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private Vacation selectedVacation;
        private ObservableCollection<Vacation> vacation = new();
        private List<Vacation> vacation1 = new();
        private ObservableCollection<Vacation> vac;

        public List<Vacation> Vacation
        {
            get => vacation1;
            set
            {
                vacation1 = value;
                Signal();
            }
        }
        public ObservableCollection<Vacation> Vac

        {
            get => vac;
            set
            {
                vac = value;
                Signal();
            }
        }
        public Vacation SelectedVacation
        {
            get => selectedVacation;
            set
            {
                selectedVacation = value;
                Signal();
            }
        }
        public CommandVM UpdateVacation { get; set; }
        public CommandVM RemoveVacation { get; set; }
        public CommandVM AddVacation { get; set; }
        public CommandVM VacationViewAdd { get; set; }
        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private Workers selectedWorkers;
        private List<Workers> workers1 = new();
        private ObservableCollection<Workers> wor;

        public List<Workers> Workers
        {
            get => workers1;
            set
            {
                workers1 = value;
                Signal();
            }
        }
        public ObservableCollection<Workers> Wor

        {
            get => wor;
            set
            {
                wor = value;
                Signal();
            }
        }
        public Workers SelectedWorkers
        {
            get => selectedWorkers;
            set
            {
                selectedWorkers = value;
                Signal();
            }
        }
        public CommandVM UpdateWorkes { get; set; }
        public CommandVM RemoveWorkers { get; set; }
        public CommandVM AddWorkes { get; set; }
        public CommandVM WorkersViewAdd { get; set; }
        public CommandVM SelectedWorkes { get; set; }



        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>
        //public CommandVM CreateReport { get; set; }
        public MainVM()






        {




            CreateReport = new CommandVM(() =>
            {
                ////////////////!!!!!!!!!!!!!!!!!!!!!

                //    //1лист
                //    Worksheet list = workbook.Worksheets[0];
                //    list.Name = "Работники";
                //    Worksheet sheetTwo = workbook.Worksheets[1];



                Workbook workbook = new Workbook();

                Worksheet list = workbook.Worksheets[0];
                list.Name = "Работники";


                Worksheet sheetTwo = workbook.Worksheets[1];
                sheetTwo.Name = "Оборудование";


                Worksheet sheetThree = workbook.Worksheets[2];
                sheetThree.Name = "Отпуска";


                //Worksheet sheetFour = workbook.Worksheets[3];
                //sheetFour.Name = "Ремонт оборудования";


                //while (workbook.Worksheets.Count > 4)
                //{
                //    workbook.Worksheets[workbook.Worksheets.Count - 2].Remove();
                //}


                //Работники
                list.Range["A1"].Text = "Фамилия";
                list.Range["A1"].ColumnWidth = 18;
                list.Range["A1"].BorderAround();

                list.Range["B1"].Text = "Имя";
                list.Range["B1"].ColumnWidth = 15;
                list.Range["B1"].BorderAround();

                list.Range["C1"].Text = "Отчество";
                list.Range["C1"].ColumnWidth = 15;
                list.Range["C1"].BorderAround();

                list.Range["D1"].Text = "Должность";
                list.Range["D1"].ColumnWidth = 15;
                list.Range["D1"].BorderAround();

                list.Range["E1"].Text = "Почта";
                list.Range["E1"].ColumnWidth = 15;
                list.Range["E1"].BorderAround();

                list.Range["F1"].Text = "Номер телефона";
                list.Range["F1"].ColumnWidth = 15;
                list.Range["F1"].BorderAround();

                ObservableCollection<Workers> workers = new ObservableCollection<Workers>(WorkersDB.GetDb().SelectAll());

                for (int i = 0; i < workers.Count; i++)
                {
                    list.Range[$"A{i + 2}"].Text = workers[i].Last_name.ToString();
                    list.Range[$"B{i + 2}"].Text = workers[i].First_name.ToString();
                    list.Range[$"C{i + 2}"].Text = workers[i].Patronymic.ToString();
                    list.Range[$"D{i + 2}"].Text = workers[i].Post.ToString();
                    list.Range[$"E{i + 2}"].Text = workers[i].Email.ToString();
                    list.Range[$"F{i + 2}"].Text = workers[i].Phone_number.ToString();

                    list.Range[$"A{i + 2}"].BorderAround();
                    list.Range[$"B{i + 2}"].BorderAround();
                    list.Range[$"C{i + 2}"].BorderAround();
                    list.Range[$"D{i + 2}"].BorderAround();
                    list.Range[$"E{i + 2}"].BorderAround();
                    list.Range[$"F{i + 2}"].BorderAround();
                }

                // Оборудование
                sheetTwo.Range["A1"].Text = "Код";
                sheetTwo.Range["A1"].ColumnWidth = 15;
                sheetTwo.Range["A1"].BorderAround();

                sheetTwo.Range["B1"].Text = "Фактический номер";
                sheetTwo.Range["B1"].ColumnWidth = 20;
                sheetTwo.Range["B1"].BorderAround();

                sheetTwo.Range["C1"].Text = "Тип оборудования";
                sheetTwo.Range["C1"].ColumnWidth = 20;
                sheetTwo.Range["C1"].BorderAround();

                sheetTwo.Range["D1"].Text = "Тип работ";
                sheetTwo.Range["D1"].ColumnWidth = 20;
                sheetTwo.Range["D1"].BorderAround();

                sheetTwo.Range["E1"].Text = "Статус";
                sheetTwo.Range["E1"].ColumnWidth = 15;
                sheetTwo.Range["E1"].BorderAround();

                sheetTwo.Range["F1"].Text = "Дата последнего ремонта";
                sheetTwo.Range["F1"].ColumnWidth = 25;
                sheetTwo.Range["F1"].BorderAround();

                sheetTwo.Range["G1"].Text = "Дата запланированного ремонта";
                sheetTwo.Range["G1"].ColumnWidth = 30;
                sheetTwo.Range["G1"].BorderAround();

                ObservableCollection<Equipment> equipment = new ObservableCollection<Equipment>(EquipmentDB.GetDb().SelectAll());

                for (int i = 0; i < workers.Count; i++)
                {
                    sheetTwo.Range[$"A{i + 2}"].Text = equipment[i].Code.ToString();
                    sheetTwo.Range[$"B{i + 2}"].Text = equipment[i].Factory_number.ToString();
                    sheetTwo.Range[$"C{i + 2}"].Text = equipment[i].Type.ToString();
                    sheetTwo.Range[$"D{i + 2}"].Text = equipment[i].Type_work.ToString();
                    sheetTwo.Range[$"E{i + 2}"].Text = equipment[i].Date_last_serv.ToString();
                    sheetTwo.Range[$"F{i + 2}"].Text = equipment[i].Scheduled_date.ToString();

                    sheetTwo.Range[$"A{i + 2}"].BorderAround();
                    sheetTwo.Range[$"B{i + 2}"].BorderAround();
                    sheetTwo.Range[$"C{i + 2}"].BorderAround();
                    sheetTwo.Range[$"D{i + 2}"].BorderAround();
                    sheetTwo.Range[$"E{i + 2}"].BorderAround();
                    sheetTwo.Range[$"F{i + 2}"].BorderAround();
                }



                // Отпуска
                ///
                //sheetThree.Range["A1"].Text = "Начало отпуска";
                //sheetThree.Range["A1"].ColumnWidth = 20;
                //sheetThree.Range["A1"].BorderAround();

                //sheetThree.Range["B1"].Text = "Длительность (дней)";
                //sheetThree.Range["B1"].ColumnWidth = 20;
                //sheetThree.Range["B1"].BorderAround();

                //sheetThree.Range["C1"].Text = "Больничный отпуск";
                //sheetThree.Range["C1"].ColumnWidth = 20;
                //sheetThree.Range["C1"].BorderAround();

                //sheetThree.Range["D1"].Text = "Конец отпуска";
                //sheetThree.Range["D1"].ColumnWidth = 20;
                //sheetThree.Range["D1"].BorderAround();

                //ObservableCollection<Vacation> vacation = new ObservableCollection<Vacation>(VacationDB.GetDb().SelectAll());

                //for (int i = 0; i < workers.Count; i++)
                //{
                //    list.Range[$"A{i + 2}"].Text = vacation[i].Start_vacation.ToString();
                //    list.Range[$"B{i + 2}"].Text = vacation[i].How_vacation.ToString();
                //    list.Range[$"C{i + 2}"].Text = vacation[i].Sick_vacation.ToString();
                //    list.Range[$"D{i + 2}"].Text = vacation[i].Vacation_end_date.ToString();

                //    list.Range[$"A{i + 2}"].BorderAround();
                //    list.Range[$"B{i + 2}"].BorderAround();
                //    list.Range[$"C{i + 2}"].BorderAround();
                //    list.Range[$"D{i + 2}"].BorderAround();

                //}


                // Hемонт
                sheetThree.Range["A1"].Text = "Описание работы";
                sheetThree.Range["A1"].ColumnWidth = 40;
                sheetThree.Range["A1"].BorderAround();

                sheetThree.Range["B1"].Text = "Дата установки оборудования";
                sheetThree.Range["B1"].ColumnWidth = 25;
                sheetThree.Range["B1"].BorderAround();

                sheetThree.Range["C1"].Text = "Дата ремонта";
                sheetThree.Range["C1"].ColumnWidth = 20;
                sheetThree.Range["C1"].BorderAround();


                ObservableCollection<Service> services = new ObservableCollection<Service>(ServiceDB.GetDb().SelectAll());

                for (int i = 0; i < workers.Count; i++)
                {
                    sheetThree.Range[$"A{i + 2}"].Text = services[i].Description_works.ToString();
                    sheetThree.Range[$"B{i + 2}"].Text = services[i].Setting_date.ToString();
                    sheetThree.Range[$"C{i + 2}"].Text = services[i].Date_service.ToString();

                    sheetThree.Range[$"A{i + 2}"].BorderAround();
                    sheetThree.Range[$"B{i + 2}"].BorderAround();
                    sheetThree.Range[$"C{i + 2}"].BorderAround();


                }

                try
                {
                    var path = String.Format("NewReport.xls", AppDomain.CurrentDomain);
                    workbook.SaveToFile(path);
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                catch
                {
                    MessageBox.Show("Закройте excel и попробуйте еще раз");
                }

            }, () => true);



            Equipment = EquipmentDB.GetDb().SelectAll();


            UpdateEquipment = new CommandVM(() =>
            {
                if (EquipmentDB.GetDb().Update(SelectedEquipment))
                    MessageBox.Show("Успешно");


            }, () => SelectedEquipment != null);



            RemoveEquipment = new CommandVM(() =>
            {
                EquipmentDB.GetDb().Remove(SelectedEquipment);
                SelectAll();
            }, () => SelectedEquipment != null);

            AddEquipment = new CommandVM(() =>
            {
                EquipmentView add = new EquipmentView();
                add.ShowDialog();

                SelectAll();
            }, () => true);

            WorkersViewAdd = new CommandVM(() =>
            {
                WorkesView add = new WorkesView();
                add.ShowDialog();

                SelectAll();
            }, () => true);

            ServiceViewAdd = new CommandVM(() =>
            {
                if (selectedEquipment == null)
                {
                    MessageBox.Show("Выберите оборудование");
                    return;
                }
                ServiceView add = new ServiceView(selectedEquipment);
                add.ShowDialog();

                SelectAll();
            }, () => true);

            ///////////////////////////////////////////////////////////////
            ///
            Category = CategoryDB.GetDb().SelectAll();


            UpdateCategory = new CommandVM(() =>
            {
                if (CategoryDB.GetDb().Update(SelectedCategory))
                    MessageBox.Show("Успешно");


            }, () => SelectedCategory != null);



            RemoveCategory = new CommandVM(() =>
            {
                CategoryDB.GetDb().Remove(SelectedCategory);
                SelectAll();
            }, () => SelectedCategory != null);




            /////////////////////////////////////////////////////////////



            UpdateService = new CommandVM(() =>
            {
                if (ServiceDB.GetDb().Update(SelectedService))
                    MessageBox.Show("Успешно");


            }, () => SelectedService != null);



            RemoveService = new CommandVM(() =>
            {
                ServiceDB.GetDb().Remove(SelectedService);
                SelectAll();
            }, () => SelectedService != null);

            AddService = new CommandVM(() =>
            {

                ServiceAdd add = new ServiceAdd(SelectedEquipment);
                add.ShowDialog();

                LoadServiceForEquipment();
            }, () => true);

            ////////////////////////////////////////////////////////////////////
            ///

            Vacation = VacationDB.GetDb().SelectAll();


            UpdateVacation = new CommandVM(() =>
            {
                if (VacationDB.GetDb().Update(SelectedVacation))
                    MessageBox.Show("Успешно");


            }, () => SelectedVacation != null);



            RemoveVacation = new CommandVM(() =>
            {
                VacationDB.GetDb().Remove(SelectedVacation);
                SelectAll();
            }, () => SelectedVacation != null);





            AddVacation = new CommandVM(() =>
            {

                if (SelectedWorkers == null)
                {
                    MessageBox.Show("Выберите сотрудника");
                    return;
                }


                VacationAdd add = new VacationAdd(SelectedWorkers);
                add.ShowDialog();

                SelectAll();
            }, () => true);


            ///////////////////////////////////////////////
            /// 
            Workers = WorkersDB.GetDb().SelectAll();


            UpdateWorkes = new CommandVM(() =>
            {
                if (WorkersDB.GetDb().Update(SelectedWorkers))
                    MessageBox.Show("Успешно");


            }, () => SelectedWorkers != null);



            RemoveWorkers = new CommandVM(() =>
            {
                WorkersDB.GetDb().Remove(SelectedWorkers);
                SelectAll();
            }, () => SelectedWorkers != null);

            AddWorkes = new CommandVM(() =>
            {
                WorkesAdd add = new WorkesAdd();
                add.ShowDialog();

                SelectAll();
            }, () => true);

            VacationViewAdd = new CommandVM(() =>
            {
                SelectAll();
                VacationView add = new VacationView();

                add.ShowDialog();

                SelectAll();
            }, () => true);

           

        }


        public void LoadServiceForEquipment()
        {
            Service = ServiceDB.GetDb().SelectBy(SelectedEquipment);
        }

       
        private void SelectAll()
        {
            cat = new ObservableCollection<Category>(CategoryDB.GetDb().SelectAll());
            equ = new ObservableCollection<Equipment>(EquipmentDB.GetDb().SelectAll());
            ser = new ObservableCollection<Service>(ServiceDB.GetDb().SelectAll());
            vac = new ObservableCollection<Vacation>(VacationDB.GetDb().SelectAll());
            wor = new ObservableCollection<Workers>(WorkersDB.GetDb().SelectAll());

            Vacation = new List<Vacation>(VacationDB.GetDb().SelectAll());
        }
        private void SearchAll()
        {
            Category = new List<Category>(CategoryDB.GetDb().SelectBy(Search));
            Equipment = new List<Equipment>(EquipmentDB.GetDb().SelectBy(Search));
            Service = new List<Service>(ServiceDB.GetDb().SelectBy(Search));
            Vacation = new List<Vacation>(VacationDB.GetDb().SelectBy(Search));
            Workers = new List<Workers>(WorkersDB.GetDb().SelectBy(Search));
        }
        
       

    }
}





        

      


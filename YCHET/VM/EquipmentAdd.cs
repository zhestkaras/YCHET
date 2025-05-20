using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YCHET.Model;

namespace YCHET.VM
{


    internal class EquipmentAdd : BaseVM
    {
        public Equipment newEquipment = new();
        public Equipment Equipment { get; set; } = new();
        public Category Category { get; set; }

        public List<Category> Categories { get => categories; set { categories = value; Signal();  } }
        public Equipment NewEquipment
        {
            get => newEquipment;
            set
            {
                newEquipment = value;
                Signal();
            }
        }

        public CommandVM InsertEquipment { get; set; }
        public EquipmentAdd()
        {
            SelectCat();

            InsertEquipment = new CommandVM(() =>
            {
                if (Category == null)
                {
                    MessageBox.Show("Добавте категорию!!!");
                    return;
                }
                Equipment.Id_category = Category.Id;
                EquipmentDB.GetDb().Insert(Equipment);
                close?.Invoke();
            },
                    () => true);
                    //!string.IsNullOrEmpty(newEquipment.Code) &&
                    //!string.IsNullOrEmpty(newEquipment.Factory_number) &&
                    //!string.IsNullOrEmpty(newEquipment.Type) &&
                    //!string.IsNullOrEmpty(newEquipment.Type_work) &&
                    //!string.IsNullOrEmpty(newEquipment.Status));
        }
        Action close;
        private List<Category> categories = new List<Category>();

        internal void SetClose(Action close)
        {
            this.close = close;
        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
        
        void SelectCat()
        {
            Categories = CategoryDB.GetDb().SelectAll();
        }

    }
}


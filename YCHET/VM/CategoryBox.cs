using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCHET.Model;
using YCHET.VM;

namespace YCHET.VM
{
    public class CategoryBox
    {
        public string Name { get; set; }
        public ObservableCollection<CategoryBox> CategoryBoxx { get; set; }
        public CategoryBox(string name)
        {
            Name = name;
            CategoryBoxx = new ObservableCollection<CategoryBox>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
/*
internal class EquipmentAdd : BaseVM
{
    private Equipment newEquipment = new();

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
        InsertEquipment = new CommandVM(() =>
        {
            EquipmentDB.GetDb().Insert(NewEquipment);
            close?.Invoke();
        },
                () =>
                !string.IsNullOrEmpty(newEquipment.Code) &&
                !string.IsNullOrEmpty(newEquipment.Factory_number) &&
                !string.IsNullOrEmpty(newEquipment.Type) &&
                !string.IsNullOrEmpty(newEquipment.Type_work) &&
                !string.IsNullOrEmpty(newEquipment.Status));
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
    

*/
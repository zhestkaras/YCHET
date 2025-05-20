using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YCHET.VM;

namespace YCHET.View
{
    /// <summary>
    /// Логика взаимодействия для ServiceAdd.xaml
    /// </summary>
    public partial class ServiceAdd : Window
    {
        public ServiceAdd(Model.Equipment selectedEquipment)
        {
            InitializeComponent();
            DataContext = new ServiceAddVM(selectedEquipment);
        }
    }
}

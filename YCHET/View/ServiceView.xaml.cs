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
    /// Логика взаимодействия для ServiceView.xaml
    /// </summary>
    public partial class ServiceView : Window
    {
        public ServiceView(Model.Equipment selectedEquipment)
        {
            InitializeComponent();
            var vm  = new MainVM();
            vm.SelectedEquipment = selectedEquipment;
            vm.LoadServiceForEquipment();
            DataContext = vm;
        }
    }
}

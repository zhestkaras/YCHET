 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using YCHET.Model;
using YCHET.VM;


namespace YCHET.View
{
    /// <summary>
    /// Логика взаимодействия для EquipmentView.xaml
    /// </summary>
    public partial class EquipmentView : Window
    {
        public EquipmentView()
        {
            InitializeComponent();
            DataContext = new EquipmentAdd();
        }
    }
}




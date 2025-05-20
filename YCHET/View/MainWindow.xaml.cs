using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Spire.Xls;
using YCHET.Model;
using YCHET.VM;

namespace YCHET.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public readonly TelegramNotifier TTelegramNotifier;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();

            string botToken = Settings1.Default.TelegramBotToken;
            long chatId = long.Parse(Settings1.Default.TelegramChatId);

            TTelegramNotifier = new TelegramNotifier(botToken, chatId);

            _ = SendWelcomeMessage();
            _ = CheckEquipmentMaintenance();
        }
     
        private async Task SendWelcomeMessage()
        {
            try
            {
                await TTelegramNotifier.SendNotificationAsync("Здравствуйте! Рада Вас видеть!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке приветствия: {ex.Message}");
            }
        }

        private async void SendNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            await SendWelcomeMessage();
        }

        
        private async Task CheckEquipmentMaintenance()
        {
            try
            {
                var equipmentList = await Task.Run(() =>
                    new ObservableCollection<Equipment>(EquipmentDB.GetDb().SelectAll()));

                foreach (var equipment in equipmentList)
                {
                    if ((DateTime.Now - equipment.Date_last_serv).TotalDays > 180)
                    {
                        await TTelegramNotifier.SendNotificationAsync(
                            $"Требуется обслуживание: {equipment.Code}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке обслуживания: {ex.Message}");
            }
        }
    }

}




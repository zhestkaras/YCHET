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
using YCHET.Model;
using YCHET;
using YCHET.VM;
using SkiaSharp;
using MySqlConnector;

namespace YCHET.View
{
    /// <summary>
    /// Логика взаимодействия для VacationView.xaml
    /// </summary>
    public partial class VacationView : Window
    {


        public readonly TelegramNotifier TTelegramNotifier;
        private readonly Timer _notificationTimer;
        private readonly EquipmentDB _db;
        private readonly VacationDB __db;
        public VacationView()
        {
            InitializeComponent();
            DataContext = new MainVM();





            string botToken = Settings1.Default.TelegramBotToken;
            long chatId = long.Parse(Settings1.Default.TelegramChatId);

            TTelegramNotifier = new TelegramNotifier(botToken, chatId);
            _db = EquipmentDB.GetDb();
            __db = VacationDB.GetDb();

            // Инициализация таймера для проверки (каждые 6 часов)
            _notificationTimer = new Timer(async _ =>
            {
                //await CheckVacationNotifications();
               // await CheckSickLeaveNotifications();
            }, null, TimeSpan.Zero, TimeSpan.FromHours(6));

            //  _ = SendWelcomeMessage();
            //   _ = CheckEquipmentMaintenance();
        }

        // Проверка уведомлений об отпусках
        //private async Task CheckVacationNotifications()
        //{
        //    try
        //    {
        //        // Предполагая, что у вас есть метод для получения всех работников
        //        var workers = await Task.Run(() => _db.SelectAll());
        //        var now = DateTime.Now;

        //        foreach (var worker in workers)
        //        {
        //            // Предполагая, что отпуска хранятся в работнике
        //            if (worker.Vacation != null)
        //            {
        //                foreach (var vacation in worker.Vacation)
        //                {
        //                    var daysUntilEnd = (vacation.Vacation_end_date - now).TotalDays;

        //                    if (daysUntilEnd is > 0 and <= 2)
        //                    {
        //                        await SendVacationNotification(worker, vacation, daysUntilEnd);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка при проверке отпусков: {ex.Message}");
        //    }
        //}


        private async Task SendVacationNotification(Workers worker, Vacation vacation, double daysLeft)
        {
            string message = $"{worker.Last_name} {worker.First_name} {worker.Patronymic} ({worker.Post})\n" +
                           $"Заканчивается {vacation.How_vacation} через {Math.Ceiling(daysLeft)} дней\n" +
                           $"Дата окончания: {vacation.Vacation_end_date:dd.MM.yyyy}\n" +
                           $"Контактный телефон: {worker.Phone_number}";

            await TTelegramNotifier.SendNotificationAsync(message);
        }

       /*
        private async Task CheckSickLeaveNotifications()
        {
            try
            {
                var vacations = await Task.Run(() => _db.GetSickLeaves());
                var now = DateTime.Now;

                foreach (var vacation in vacations)
                {
                    
                    if ((now - vacation.Start_vacation).TotalDays <= 1)
                    {
                        var worker = await Task.Run(() => _db.GetWorkerById(vacation.Employee_id));
                        await SendSickLeaveNotification(worker, vacation, isStart: true);
                    }

                
                    if ((vacation.Vacation_end_date - now).TotalDays is > 0 and <= 1)
                    {
                        var worker = await Task.Run(() => _db.GetWorkerById(vacation.Employee_id));
                        await SendSickLeaveNotification(worker, vacation, isStart: false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке больничных: {ex.Message}");
            }
        }
       */
        private async Task SendSickLeaveNotification(Workers worker, Vacation vacation, bool isStart)
        {
            string status = isStart ? "начался" : "заканчивается";
            string action = isStart ? "Пожелайте скорейшего выздоровления!" : "Подготовьте рабочее место!";

            string message = $"Больничный {status} у сотрудника:\n" +
                           $"{worker.Last_name} {worker.First_name} {worker.Patronymic}\n" +
                           $"Должность: {worker.Post}\n" +
                           $"Период: {vacation.Start_vacation:dd.MM.yyyy} - {vacation.Vacation_end_date:dd.MM.yyyy}\n" +
                           $"Телефон: {worker.Phone_number}\n{action}";

            await TTelegramNotifier.SendNotificationAsync(message);
        }


        protected override void OnClosed(EventArgs e)
        {
            _notificationTimer?.Dispose();
            base.OnClosed(e);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
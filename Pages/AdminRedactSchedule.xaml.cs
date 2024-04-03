using PracticTaxi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticTaxi.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminRedactSchedule.xaml
    /// </summary>
    public partial class AdminRedactSchedule : Page
    {
        Schedule schedule = new Schedule();
        TaxiContext context = new TaxiContext();
        public AdminRedactSchedule(Schedule selected)
        {
            InitializeComponent();
            this.schedule = selected;
            GetData();
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            SetData();
            var validationResults = schedule.Validate(new ValidationContext(schedule));
            if (validationResults.Any())
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }
            else
            {
                context.Schedules.Update(schedule);
                context.SaveChanges();
                MessageBox.Show("Обновлено");
            }
            
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            context.Schedules.Remove(schedule);
            context.SaveChanges();
            MessageBox.Show("Удалено");
            NavigationService.GoBack();
        }
        private void tbdriver_TextChanged(object sender, TextChangedEventArgs e)
        {
            int? driverid = int.TryParse(tbdriver.Text, out int parsedId) ? parsedId : null;
            if (driverid == null)
            {
                tbdriver.Text = "";
                return;
            }
            if (driverid > 0 && driverid <= 42)
            {
                var driver = context.Drivers.FirstOrDefault(u => u.Iddriver == driverid);
                int userdriverid = driver.Usersid;
                var userdriver = context.Users.FirstOrDefault(u => u.Idusers == userdriverid);
                tbdrivername.Text = $"{userdriver.Surname} {userdriver.Name} {userdriver.Midname}";
            }
            else
                MessageBox.Show("Введите ид водителя от 1 до 42");
        }

        private void GetData()
        {
            tbdate.Text = schedule.Date.ToString("yyyy-MM-dd");
            tbstarttime.Text = schedule.Starttime.ToString("HH:mm");
            tbendtime.Text = schedule.Endtime.ToString("HH:mm");
            tbdriver.Text = schedule.Driverid.ToString();
        }

        private void SetData()
        {
            int driverid = int.Parse(tbdriver.Text);
            string starttimeText = tbstarttime.Text;
            TimeOnly timeOnly = new TimeOnly();
            if (TimeOnly.TryParse(starttimeText, out TimeOnly timeValue))
            {
                timeOnly = timeValue;
            }
            else
                MessageBox.Show("неправильное значение времени начала смены");

            string endtimeText = tbendtime.Text;
            TimeOnly timeOnly1 = new TimeOnly();
            if (TimeOnly.TryParse(endtimeText, out TimeOnly timeValue1))
            {
                timeOnly1 = timeValue1;
            }
            else
                MessageBox.Show("неправильное значение времени конца смены");

            string dateText = tbdate.Text;
            DateOnly orderDate;
            if (DateOnly.TryParseExact(dateText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
            {
                string desiredFormat = orderDate.ToString("yyyy-MM-dd");
            }
            else
                MessageBox.Show("неправильное значение даты");


            schedule.Driverid = driverid;
            schedule.Date = orderDate;
            schedule.Starttime = timeOnly;
            schedule.Endtime = timeOnly1;
        }
    }
}

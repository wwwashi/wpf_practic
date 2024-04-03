using PracticTaxi.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Default.xaml
    /// </summary>
    public partial class Default : Page
    {
        TaxiContext context = new TaxiContext();
        User user = new User();

        public Default(User selectedUser)
        {
            InitializeComponent();
            this.user = selectedUser;
            var ord = context.Orders.Where(o => o.Usersid == user.Idusers)
                                    .OrderByDescending(u => u.Idorders)
                                    .ToList();
            LViewOrders.ItemsSource = ord;
            DateTime dt = DateTime.Now;
            tbdate.Text = dt.ToString("yyyy-MM-dd");
            DateTime time = DateTime.Now;
            tbtime.Text = time.ToString("HH:mm");
        }

        private void btndone_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int pay_id = 0;
            if (cbpay.SelectedIndex == 0)
                pay_id = 1;
            if (cbpay.SelectedIndex == 1)
                pay_id = 2;

            string timeText = tbtime.Text;
            TimeOnly timeOnly = new TimeOnly();
            if (TimeOnly.TryParse(timeText, out TimeOnly timeValue))
            {
                timeOnly = timeValue;
            }
            else
                MessageBox.Show("значение неправильное");

            string dateText = tbdate.Text;
            DateOnly orderDate;
            if (DateOnly.TryParseExact(dateText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
            {
                string desiredFormat = orderDate.ToString("yyyy-MM-dd");
            }
            else
                MessageBox.Show("значение неправильное");

            var order = new Order
            {
                Date = orderDate,
                Time = timeOnly,
                Startaddress = tbstart.Text,
                Endaddress = tbend.Text,
                Cost = random.Next(50, 1000),
                Statusid = 1,
                Usersid = user.Idusers,
                Payid = pay_id
            };
            context.Orders.Add(order);
            context.SaveChanges();

            var ord = context.Orders.Where(o => o.Usersid == user.Idusers)
                                    .OrderByDescending(u => u.Idorders)
                                    .ToList();
            LViewOrders.ItemsSource = ord;
        }
        private void Selector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedOrders = (Order)LViewOrders.SelectedItem;

            if (selectedOrders != null)
            {
                NavigationService.Navigate(new Redact(selectedOrders, user));
            }
            else
            {
                MessageBox.Show("Выберите заказ");
            }
        }
    }
}

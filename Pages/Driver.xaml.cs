using PracticTaxi.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticTaxi.Pages
{
    public partial class Driver : Page
    {
        TaxiContext context = new TaxiContext();
        User user = new User();
        Model.Driver driver = new Model.Driver();
        public Driver(User selectedUser)
        {
            InitializeComponent();
            this.user = selectedUser;

            Listview();
        }
        private void Selector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedOrders = (Order)LViewOrders.SelectedItem;

            if (selectedOrders != null)
            {
                int userId = user.Idusers;
                driver = context.Drivers.FirstOrDefault(d => d.Usersid == userId);
                selectedOrders.Driverid = driver.Iddriver;
                selectedOrders.Statusid = 2;
                context.SaveChanges();
                    
                MessageBox.Show("Заказ принят");
                Listview();
            }
            else
            {
                MessageBox.Show("Выберите заказ");
            }
        }
        private void Listview()
        {
            var ord = context.Orders.Where(o => o.Driverid == null).OrderByDescending(u => u.Date).ToList();
            LViewOrders.ItemsSource = ord;
            var dord = context.Orders.Where(o => o.Driver.Users.Idusers == user.Idusers).OrderByDescending(u => u.Date).ToList();
            LViewDriverOrders.ItemsSource = dord;
        }
        private void LViewDriverOrders_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedOrders = (Order)LViewDriverOrders.SelectedItem;

            if (selectedOrders != null)
            {
                NavigationService.Navigate(new Redact(selectedOrders, user));
            }
            else
            {
                MessageBox.Show("Выберите заказ");
            }
        }

        private void LViewOrders_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}

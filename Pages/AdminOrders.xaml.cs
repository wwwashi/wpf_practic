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
    public partial class AdminOrders : Page
    {
        TaxiContext context = new TaxiContext();
        public AdminOrders()
        {
            InitializeComponent();
            Listview();
        }
        private void Selector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedOrders = (Order)LViewOrders.SelectedItem;
            if (selectedOrders != null)
                NavigationService.Navigate(new AdminRedact(selectedOrders));
            else
                MessageBox.Show("Выберите заказ");
        }
        private void Listview()
        {
            var ord = context.Orders.OrderBy(u => u.Idorders).ToList();
            LViewOrders.ItemsSource = ord;
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            Order neworser = new Order();
            NavigationService.Navigate(new AdminRedact(neworser));
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            Listview();
        }
    }
}

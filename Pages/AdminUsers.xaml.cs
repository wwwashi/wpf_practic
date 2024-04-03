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
    /// <summary>
    /// Логика взаимодействия для AdminUsers.xaml
    /// </summary>
    public partial class AdminUsers : Page
    {
        TaxiContext context = new TaxiContext();
        public AdminUsers()
        {
            InitializeComponent();
            Listview();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            User newuser = new User();
            NavigationService.Navigate(new Profile(newuser));
        }
        private void Selector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selecteduser = (User)LViewUsers.SelectedItem;
            if (selecteduser != null)
                NavigationService.Navigate(new Profile(selecteduser));
            else
                MessageBox.Show("Выберите заказ");
        }
        private void Listview()
        {
            var ord = context.Users.OrderBy(u => u.Idusers).ToList();
            LViewUsers.ItemsSource = ord;
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            Listview();
        }
    }
}

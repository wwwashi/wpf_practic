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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        User admin = new User();
        public Admin(User admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void btnorder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminOrders());
        }

        private void btnusers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminUsers());
        }

        private void btnschedule_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminSchedule());
        }
    }
}

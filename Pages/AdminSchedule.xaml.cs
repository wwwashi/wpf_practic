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
    /// Логика взаимодействия для AdminSchedule.xaml
    /// </summary>
    public partial class AdminSchedule : Page
    {
        TaxiContext context = new TaxiContext();
        public AdminSchedule()
        {
            InitializeComponent();
            Listview();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            Schedule newitem = new Schedule();
            NavigationService.Navigate(new AdminRedactSchedule(newitem));
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            Listview();
        }
        private void Selector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Schedule)LView.SelectedItem;
            if (selectedItem != null)
                NavigationService.Navigate(new AdminRedactSchedule(selectedItem));
            else
                MessageBox.Show("Выберите запись");
        }
        private void Listview()
        {
            var ord = context.Schedules.OrderBy(u => u.Idschedule).ToList();
            LView.ItemsSource = ord;
        }
    }
}

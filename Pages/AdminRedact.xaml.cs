using PracticTaxi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для AdminRedact.xaml
    /// </summary>
    public partial class AdminRedact : Page
    {
        TaxiContext context = new TaxiContext();
        Order order = new Order();
        User client = new User();
        public AdminRedact(Order selectedOrders)
        {
            InitializeComponent();
            this.order = selectedOrders;
            EnterData();//вставка данных из таблиц в текст боксы
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            ChangeData();

            // Проверка валидации
            var validationResults = order.Validate(new ValidationContext(order));
            var validationResultsuser = client.Validate(new ValidationContext(client));
            if (validationResults.Any())
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage)); MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }
            if (validationResultsuser.Any())
            {
                string errorMessage = string.Join("\n", validationResultsuser.Select(r => r.ErrorMessage)); MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }
            else
            {
                context.Orders.Update(order);
                context.SaveChanges();
                MessageBox.Show("Обновлено");
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
            MessageBox.Show("Удалено");
            NavigationService.GoBack();
        }

        private void EnterData()
        {
            int? userid = order.Usersid;
            client = context.Users.FirstOrDefault(u => u.Idusers == userid);

            //получение водителя из заказа
            if (order.Driverid != null)
            {
                int? driverid = order.Driverid;
                var driver = context.Drivers.FirstOrDefault(u => u.Iddriver == driverid);
                int userdriverid = driver.Usersid;
                var userdriver = context.Users.FirstOrDefault(u => u.Idusers == userdriverid);
                tbdriver.Text = $"{userdriver.Name}";
            }
            else
                tbdriver.Text = "Водитель еще не принял заказ";

            tbidorder.Text = order.Idorders.ToString();
            tbclientid.Text = order.Usersid.ToString();
            tbclient.Text = $"{client.Surname} {client.Name} {client.Midname}";
            tbdatetime.Text = $"{order.Date} {order.Time}";
            tbstart.Text = order.Startaddress.ToString();
            tbend.Text = order.Endaddress.ToString();

            if (order.Statusid == 1)
                tbstatus.SelectedIndex = 0;
            if (order.Statusid == 2)
                tbstatus.SelectedIndex = 1;
            if (order.Statusid == 3)
                tbstatus.SelectedIndex = 2;
            if (order.Statusid == 4)
                tbstatus.SelectedIndex = 2;

            if (order.Payid == 1)
                tbpay.SelectedIndex = 0;
            if (order.Payid == 2)
                tbpay.SelectedIndex = 1;

            tbcost.Text = order.Cost.ToString();
        }

        private void ChangeData()
        {
            if (tbpay.SelectedIndex == 0)
                order.Payid = 1;
            if (tbpay.SelectedIndex == 1)
                order.Payid = 2;

            if (tbstatus.SelectedIndex == 0)
                order.Statusid = 1;
            if (tbstatus.SelectedIndex == 1)
                order.Statusid = 2;
            if (tbstatus.SelectedIndex == 2)
                order.Statusid = 3;
            if (tbstatus.SelectedIndex == 3)
                order.Statusid = 4;

            int idclient = int.Parse(tbclientid.Text);

            order.Usersid = idclient;
            order.Startaddress = tbstart.Text;
            order.Endaddress = tbend.Text;
            order.Cost = Convert.ToDecimal(tbcost.Text);
        }

        private void tbclientid_TextChanged(object sender, TextChangedEventArgs e)
        {
            int? userid = int.TryParse(tbclientid.Text, out int parsedId) ? parsedId : null;
            if (tbclientid == null)
            {
                tbclientid.Text = "";
                return;
            }
            if (userid > 0)
            {
                var user = context.Users.FirstOrDefault(u => u.Idusers == userid);
                tbclient.Text = $"{user.Surname} {user.Name} {user.Midname}";
            }
            else
                MessageBox.Show("Введите ид пользователя");
        }
    }
}

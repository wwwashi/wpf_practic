using PracticTaxi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;

namespace PracticTaxi.Pages
{
    /// <summary>
    /// Логика взаимодействия для Redact.xaml
    /// </summary>
    public partial class Redact : Page
    {
        TaxiContext context = new TaxiContext();
        Order order = new Order();
        User user = new User();
        User currentuser = new User();
        public Redact(Order selectedOrders, User currentuser)
        {
            InitializeComponent();
            this.order = selectedOrders;
            this.currentuser = currentuser;

            if (currentuser.Roleid == 4)//клиент
            {
                tbidorder.IsReadOnly = true;
                tbdatetime.IsReadOnly = true;
                tbdriver.IsReadOnly = true;
                tbstatus.IsReadOnly = true;
                tbcost.IsReadOnly = true;
            }
            if (currentuser.Roleid == 3)//водила
            {
                tbidorder.IsReadOnly = true;
                tbclient.IsReadOnly = true;
                tbdatetime.IsReadOnly = true;
                tbdriver.IsReadOnly = true;
                tbpay.IsReadOnly = true;
                tbcost.IsReadOnly = true;
            }

            //получение клиента из заказа
            int? userid = order.Usersid;
            user = context.Users.FirstOrDefault(u => u.Idusers == userid);

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

            EnterData();//вставка данных из таблиц в текст боксы
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            ChangeData();

            // Проверка валидации
            var validationResults = order.Validate(new ValidationContext(order));
            if (validationResults.Any())
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage)); MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
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
            tbidorder.Text = order.Idorders.ToString();
            tbclient.Text = $"{user.Surname} {user.Name} {user.Midname}";
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

            string fio = tbclient.Text;

            order.Startaddress = tbstart.Text;
            order.Endaddress = tbend.Text;

            string[] parts = fio.Split(' ');
            string surname = parts[0];
            string name = parts[1];
            string midname = parts.Length > 2 ? parts[2] : "";

            user.Surname = surname;
            user.Name = name;
            user.Midname = midname;
        }
    }
}

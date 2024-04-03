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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticTaxi.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        TaxiContext context = new TaxiContext();
        User user = new User();
        public Profile(User user)
        {
            InitializeComponent();
            this.user = user;
            EnterData();
            if (user.Roleid == 1 || user.Roleid == null)
            {
                tblrole.Visibility = Visibility.Visible;
                tblrole.Visibility = Visibility.Visible;
            }
            else
            {
                tblrole.Visibility = Visibility.Hidden;
                tblrole.Visibility = Visibility.Hidden;
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            ChangeData();
            if (context.Users.Any(u => u.Login == tblogin.Text && u.Idusers != user.Idusers))
            {
                MessageBox.Show("Логин уже используется другим пользователем.");
                return;
            }
            var validationResultsuser = user.Validate(new ValidationContext(user));
            if (validationResultsuser.Any())
            {
                string errorMessage = string.Join("\n", validationResultsuser.Select(r => r.ErrorMessage)); MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }
            else
            {
                context.Users.Update(user);
                context.SaveChanges();
                MessageBox.Show("Обновлено");
            }
        }
        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            MessageBox.Show("Удалено");
            NavigationService.GoBack();
        }
        private void EnterData()
        {
            tbsurname.Text = user.Surname;
            tbname.Text = user.Name;
            tbmidname.Text = user.Midname;
            tblogin.Text = user.Login;
            tbpassword.Text = user.Password;
            tbmail.Text = user.Mail;
            tbphone.Text = user.Phone;
            if (user.Roleid == 1)
                tbrole.SelectedIndex = 0;
            if (user.Roleid == 3)
                tbrole.SelectedIndex = 1;
            if (user.Roleid == 4)
                tbrole.SelectedIndex = 2;
        }
        private void ChangeData()
        {
            user.Surname = tbsurname.Text;
            user.Name = tbname.Text;
            user.Midname = tbmidname.Text;
            user.Login = tblogin.Text;
            user.Password = tbpassword.Text;
            user.Mail = tbmail.Text;
            user.Phone = tbphone.Text;

            if (tbrole.SelectedIndex == 0)
                user.Roleid = 1;
            if (tbrole.SelectedIndex == 1)
                user.Roleid = 3;
            if (tbrole.SelectedIndex == 2)
                user.Roleid = 4;
        }
    }
}

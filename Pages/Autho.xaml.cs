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
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        TaxiContext context = new TaxiContext();
        User user = new User();
        public Autho()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLogin.Text) && !String.IsNullOrEmpty(tbPassword.Text))
                LoginUser();
            else
                MessageBox.Show("Пожалуйста, введите логин и пароль", "Предупреждение");
        }
        private void LoginUser()
        {
            string Login = tbLogin.Text.Trim();
            string Password = tbPassword.Text.Trim();

            user = context.Users.Where(p => p.Login == Login).FirstOrDefault();
            if (user != null)
            {
                if (user?.Password == Password)
                {
                    LoadForm(user.Roleid.ToString());
                    tbLogin.Text = "";
                    tbPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Предупреждение");
                }
            }
            else
            {
                MessageBox.Show("Пользователя с логином '" + Login + "' не существует", "Предупреждение");
            }
        }
        private void LoadForm(string _role)
        {
            switch (_role)
            {
                //клиент
                case "4":
                    NavigationService.Navigate(new Default(user));
                    break;
                //водила
                 case "3":
                    NavigationService.Navigate(new Driver(user));
                    break;
                 //админ
                 case "1":
                    NavigationService.Navigate(new Admin(user));
                    break;

            }
        }
    }
}

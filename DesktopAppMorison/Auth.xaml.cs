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
using System.Windows.Shapes;

namespace DesktopAppMorison
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        user187_dbEntities db;
        public Auth()
        {
            InitializeComponent();
            db = new user187_dbEntities();
        }

        private void Button_Click(object sender, object e)
        {
            var user = db.PO_Customer.FirstOrDefault(item => item.PhoneNumberCustomer == log.Text || item.EmailCustomer == log.Text &&  item.Password == pass.Text);

            if (log.Text == "" && pass.Text == "")
            {
                MessageBox.Show("Пустые поля", "Ошибка!");
                return;
            }
            else if (user != null)
            {

                int userId = user.id_customer;

                Kabinet physicalWindow = new Kabinet(userId);
                physicalWindow.Show();

            }
            else
            {
                MessageBox.Show("Не правильно введены данные", "Ошибка");
                return;
            }

        }

       
    }
}

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
    /// Логика взаимодействия для Polzovatel.xaml
    /// </summary>
    public partial class Polzovatel : Window
    {
        user187_dbEntities db = new user187_dbEntities();

        public Polzovatel()
        {
            InitializeComponent();
            DataContext = new PO_Customer();

        }
        public Polzovatel(PO_Customer user)
        {
            InitializeComponent();
            DataContext = user;

        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Fam.Text == "" || Imya.Text == ""  || Telphone.Text == "" || gmail.Text == "" || pass.Text == "")
            {
                MessageBox.Show("Пустые данные нельзя регистрировать!", "Ошибка!");
                return;
            }
            else if (Fam.Text.Length <= 5)
            {
                MessageBox.Show("Фамилия должна быть не меньше 5 символов", "Ошибка!");
                return;
            }
            else if (Imya.Text.Length <= 2)
            {
                MessageBox.Show("Имя должнобыть не меньше 2 символов", "Ошибка!");
                return;
            }
            else if (Telphone.Text.Length <= 1)
            {
                MessageBox.Show("Телефон должен быть не меньше 1 символов", "Ошибка!");
                return;
            }
            else if (gmail.Text.Length <= 1)
            {
                MessageBox.Show("Gmail должен быть не меньше 1 символов", "Ошибка!");
                return;
            }

            else if (db.PO_Customer.Select(item => item.PhoneNumberCustomer).Contains(Telphone.Text))
            {
                MessageBox.Show("Такой телефон существует в системе");
                return;
            }
            else if (db.PO_Customer.Select(item => item.EmailCustomer).Contains(gmail.Text))
            {
                MessageBox.Show("Такой G-mail существует в системе");
                return;
            }
            if (DataContext is PO_Customer user && user.id_customer == 0)
            {



                PO_Customer klient = new PO_Customer
                {
                    FirstNameCustomer = Fam.Text,
                    LastNameCustomer = Imya.Text,
                    MiddleNameCustomer = otc.Text,
                    PhoneNumberCustomer = Telphone.Text,
                    EmailCustomer = gmail.Text,
                    Password = pass.Text

                };


                // Добавляем объект PO_Customer в контекст базы данных
                db.PO_Customer.Add(klient);

                MessageBox.Show("Вы успешно зарегистрировались!", "Успех!");
                db.SaveChanges();


            }


        }

        private void auth_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Auth rw = new Auth();
            rw.Show();
            this.Close();
        }
    }
}


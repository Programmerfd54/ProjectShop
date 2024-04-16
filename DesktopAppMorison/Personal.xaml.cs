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
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Window
    {
        public Personal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(comb.Text == "Админ" && log.Text == "admin" && pass.Text == "1")
            {
                AdminPanel rw = new AdminPanel();
                rw.Show();
                this.Close();
            }
            else if (comb.Text == "Менеджер" && log.Text == "admin" && pass.Text == "1")
            {

            }
            else
            {
                MessageBox.Show("Ошибка лоина/пароля", "Ошибка!");
                return;
            }
        }
    }
}

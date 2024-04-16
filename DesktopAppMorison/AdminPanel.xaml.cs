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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            Load();
        }
        // Для оборудования
                private void BtnAdd_Click(object sender, object e)
                {
                    new AddorUpdateObr().ShowDialog();
                    Load();
                }

                private void BtnUpdate_Click(object sender, RoutedEventArgs e)
                {
                    if (UsersDataGrid.SelectedItem is PO_Equipment user)
                    {
                        new AddorUpdateObr(user).ShowDialog();
                        Load();
                    }
                }

                private void BtnRemove_Click(object sender, RoutedEventArgs e)
                {
                    try
                    {
                        if (UsersDataGrid.SelectedItem is DesktopAppMorison.PO_Equipment user)
                        {
                            Helper.context.PO_Equipment.Remove(user);
                            Helper.context.SaveChanges();
                            Load();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка");
                    }
                }

                private void Load()
                {
                    UsersDataGrid.ItemsSource = Helper.context.PO_Equipment.ToList();
                   
                    UsersDataGriddds.ItemsSource = Helper.context.PO_ListOfIncludedChannelsAndServices.ToList();


                 }
        // Пакеты
                 

       
    }
}

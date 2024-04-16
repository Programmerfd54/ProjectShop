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
    /// Логика взаимодействия для AddorUpdateObr.xaml
    /// </summary>
    public partial class AddorUpdatePaket : Window
    {
        public AddorUpdatePaket()
        {
            InitializeComponent();
        }

        public AddorUpdatePaket(PO_ServicePackages user)
        {
            InitializeComponent();
            Load();
            DataContext = user;
        }
       
        private void Load()
        {
            obr.ItemsSource = Helper.context.PO_ListOfIncludedChannelsAndServices.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PO_ServicePackages user && user.id_package == 0)
            {
                Helper.context.PO_ServicePackages.Add(user);
            }
            Helper.context.SaveChanges();
            Close();
        }
    }
}

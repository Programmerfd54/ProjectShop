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
    public partial class AddorUpdateObr : Window
    {
        public AddorUpdateObr()
        {
            InitializeComponent();
            DataContext = new PO_Equipment();
            Load();
        }
        public AddorUpdateObr(PO_Equipment user)
        {
            InitializeComponent();
            Load();
            DataContext = user;
        }
       
       
        private void Load()
        {
           obr.ItemsSource = Helper.context.PO_NameEquipmentType.ToList();

        }

        private void Button_Click(object sender, object e)
        {
            if (DataContext is PO_Equipment user && user.id_equipment == 0)
            {
                Helper.context.PO_Equipment.Add(user);
            }
            Helper.context.SaveChanges();
            Close();
        }
    }
}

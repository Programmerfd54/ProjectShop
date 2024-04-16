using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopAppMorison
{
    public partial class Kabinet : Window
    {
        private int userId;

        public Kabinet(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            Load();
            LoadOrderedEquipment();
        }

        public void Load()
        {
            datas.ItemsSource = Helper.context.PO_Equipment.ToList();
            datas1.ItemsSource = Helper.context.PO_ServicePackages.ToList();
        }

        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (parent is T correctlyTyped)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void UpdateEquipmentStatus(PO_Equipment selectedEquipment)
        {
            using (var db = new user187_dbEntities())
            {
                var equipment = db.PO_Equipment.FirstOrDefault(e => e.id_equipment == selectedEquipment.id_equipment);

                if (equipment != null)
                {
                    equipment.AvailableEquipmentInStock = "Отсутствует";
                    equipment.LastRepairDate = DateTime.Now;
                    equipment.id_customer = userId;

                    db.SaveChanges();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (adres.Text == "")
            {
                MessageBox.Show("Нельзя заказать товар, если нет адреса!", "Укажите адрес");
                return;
            }

            var selectedEquipment = datas.SelectedItem as PO_Equipment;

            if (selectedEquipment != null)
            {
                using (var db = new user187_dbEntities())
                {
                    // Проверка наличия заказа на выбранный товар текущим пользователем
                    var existingOrder = db.PO_Orders.FirstOrDefault(o => o.id_equipment == selectedEquipment.id_equipment && o.id_customer == userId);

                    if (existingOrder != null)
                    {
                        MessageBox.Show("Этот товар уже добавлен к вам.");
                        return;
                    }

                    // Проверка наличия заказа на выбранный товар другими пользователями
                    var otherUserOrder = db.PO_Orders.FirstOrDefault(o => o.id_equipment == selectedEquipment.id_equipment && o.id_customer != userId);

                    if (otherUserOrder != null)
                    {
                        MessageBox.Show("Извините, но этот товар уже заказан другим пользователем.");
                        DisableButton(sender as Button);
                        return;
                    }

                    int equipmentPrice = selectedEquipment.Price.HasValue ? selectedEquipment.Price.Value : 0;
                    var selectedPackage = datas1.SelectedItem as PO_ServicePackages;
                    int packagePrice = selectedPackage?.Price ?? 0;

                    int totalPrice = equipmentPrice + packagePrice;
                    UpdateEquipmentStatus(selectedEquipment);

                    PO_Orders newOrder = new PO_Orders
                    {
                        OrderDate = DateTime.Now,
                        id_customer = userId,
                        id_equipment = selectedEquipment.id_equipment,
                        id_package = selectedPackage?.id_package ?? 1,
                        Adress = adres.Text,
                        OrderTotal = totalPrice
                    };

                    db.PO_Orders.Add(newOrder);
                    db.SaveChanges();

                    Load(); // Перезагрузка данных после изменений
                }
            }
            else
            {
                var button = (Button)sender;
                if (selectedEquipment.AvailableEquipmentInStock != "Отсутствует")
                {
                    // Обработка кнопки, если товар доступен
                }
                else
                {
                    DisableButton(button);
                }
            }
        }

        private void DisableButton(Button button)
        {
            button.IsEnabled = false;
            button.Content = "Продано";

            var row = FindVisualParent<DataGridRow>(button);
            if (row != null)
            {
                row.Background = Brushes.Black;
            }
        }




        // Problems
        public class OrderedEquipment
        {
            public int EquipmentId { get; set; }
            public string NameEquipmentType { get; set; }
            // Другие свойства, если необходимо
        }
        public void LoadOrderedEquipment()
        {
            var orderedEquipment = Helper.context.PO_Orders
                    .Where(o => o.id_customer == userId && o.PO_Equipment != null)
                    .ToList() // Выполняем загрузку данных из базы данных в память
                    .Select(o => new OrderedEquipment
                    {
                        EquipmentId = o.PO_Equipment.id_equipment,
                        NameEquipmentType = $"{o.PO_Equipment.PO_NameEquipmentType.NameEquipmentType} - {o.PO_Equipment.SerialNumber}"
                    })
                    .ToList();

            obord.ItemsSource = orderedEquipment;
            obord.DisplayMemberPath = "NameEquipmentType";
            obord.SelectedValuePath = "EquipmentId";
        }
        // 11111111
        private void send_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


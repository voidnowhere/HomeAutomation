using HomeAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeAutomation
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        private void fillDataGridRooms()
        {
            using AppDbContext dbContext = new AppDbContext();
            dataGridRooms.ItemsSource = dbContext.Rooms.OrderByDescending(r => r.Id).ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillDataGridRooms();
        }

        private void dataGridRooms_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            using AppDbContext dbContext = new AppDbContext();
            Room room = e.Row.Item as Room;
            if (room.Name is null)
            {
                MessageBox.Show("The room name is required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            if (!Regex.IsMatch(room.Name, @"^\w+$"))
            {
                MessageBox.Show("The room name must only contain alphabetic characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            if (dbContext.Rooms.Where(r => r.Name == room.Name && r.Id != room.Id).Count() > 0)
            {
                MessageBox.Show("Room name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            if (room.Id == 0)
            {
                dbContext.Rooms.Add(new Room { Name = room.Name });
            }
            else
            {
                dbContext.Rooms.Update(room);
            }
            dbContext.SaveChanges();
            fillDataGridRooms();
        }

        private void dataGridRooms_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Do you really want to delete this room ?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    using AppDbContext dbContext = new AppDbContext();
                    dbContext.Rooms.Remove(dataGridRooms.SelectedItem as Room);
                    dbContext.SaveChanges();
                    fillDataGridRooms();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void dataGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = dataGridRooms.SelectedItem as Room;
            if (room is not null)
            {
                using AppDbContext dbContext = new AppDbContext();
                dbContext.Attach(room);
                dataGridRoomEquipments.ItemsSource = room.Equipments;
            }
            else
            {
                dataGridRoomEquipments.ItemsSource = null;
            }
        }
    }
}

using HomeAutomation.Models;
using Microsoft.EntityFrameworkCore;
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
        private AuthWindow authWindow;
        private Person person;

        public DashboardWindow(AuthWindow authWindow, Person person)
        {
            InitializeComponent();
            this.authWindow = authWindow;
            this.person = person;
        }

        private void fillDataGridRooms()
        {
            dataGridRooms.ItemsSource = new AppDbContext().Rooms.OrderByDescending(r => r.Id).ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillDataGridRooms();
            toggleButtonOnOff.IsEnabled = false;
            sliderTemperature.IsEnabled = false;
            sliderVolume.IsEnabled = false;
        }

        private void dataGridRooms_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Room room = e.Row.Item as Room;
            if (room.Name is null)
            {
                MessageBox.Show("The room name is required !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            if (!Regex.IsMatch(room.Name, @"^\w+(\s?\w+)+$"))
            {
                MessageBox.Show("The room name must only contain alphabetic characters !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            using AppDbContext dbContext = new AppDbContext();
            if (dbContext.Rooms.Count(r => r.Name == room.Name && r.Id != room.Id) > 0)
            {
                MessageBox.Show("Room name already exists !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    Room room = dataGridRooms.SelectedItem as Room;
                    if (dbContext.Equipments.Count(e => e.Room.Id == room.Id) > 0)
                    {
                        MessageBox.Show("You can't delete this room because it has linked equipments !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        e.Handled = true;
                        return;
                    }
                    dbContext.Rooms.Remove(room);
                    dbContext.SaveChanges();
                    fillDataGridRooms();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void fillDataGridRoomEquipments(Room room)
        {
            dataGridRoomEquipments.ItemsSource = new AppDbContext().Equipments.Where(e => e.Room.Id == room.Id).OrderByDescending(e => e.Id).ToList();
            toggleButtonOnOff.IsEnabled = false;
            sliderTemperature.Value = sliderTemperature.Minimum;
            sliderTemperature.IsEnabled = false;
            sliderVolume.Value = sliderVolume.Minimum;
            sliderVolume.IsEnabled = false;
        }

        private void dataGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = dataGridRooms.SelectedItem as Room;
            if (room is not null)
            {
                fillDataGridRoomEquipments(room);
            }
            else
            {
                dataGridRoomEquipments.ItemsSource = null;
            }
        }

        private void dataGridRoomEquipments_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Equipment equipment = e.Row.Item as Equipment;
            Room room = dataGridRooms.SelectedItem as Room;
            if (equipment.Name is null)
            {
                MessageBox.Show("The equipment name is required !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            if (!Regex.IsMatch(equipment.Name, @"^\w+(\s?\w+)+$"))
            {
                MessageBox.Show("The equipment name must only contain alphabetic characters !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            using AppDbContext dbContext = new AppDbContext();
            if (dbContext.Equipments.Count(e => e.Name == equipment.Name && e.Id != equipment.Id && e.Room.Id == room.Id) > 0)
            {
                MessageBox.Show("Equipment name already exists in the selected room !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }
            dbContext.Equipments.Update(equipment);
            dbContext.SaveChanges();
        }

        private void dataGridRoomEquipments_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBox.Show("Do you really want to delete this equipment ?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    using AppDbContext dbContext = new AppDbContext();
                    Equipment equipment = dataGridRoomEquipments.SelectedItem as Equipment;
                    if (dbContext.Logs.Count(l => l.Equipment.Id == equipment.Id) > 0)
                    {
                        MessageBox.Show("You can't delete this room equipment it has linked logs !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        e.Handled = true;
                        return;
                    }
                    dbContext.Equipments.Remove(equipment);
                    dbContext.SaveChanges();
                    fillDataGridRoomEquipments(dataGridRooms.SelectedItem as Room);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void dataGridRoomEquipments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Equipment equipment = dataGridRoomEquipments.SelectedItem as Equipment;
            if (equipment is not null)
            {
                using AppDbContext dbContext = new AppDbContext();
                dbContext.Entry(equipment).Reload();
                //
                sliderTemperature.Value = sliderTemperature.Minimum;
                sliderTemperature.IsEnabled = false;
                sliderVolume.Value = sliderVolume.Minimum;
                sliderVolume.IsEnabled = false;
                toggleButtonOnOff.IsEnabled = true;
                toggleButtonOnOff.IsChecked = equipment.Up;
                if (equipment is AC or Heater)
                {
                    sliderTemperature.Value = (double)((equipment is AC) ? ((AC)equipment).Temperature : ((Heater)equipment).Temperature);
                    sliderTemperature.IsEnabled = true;
                }
                else if (equipment is TV)
                {
                    sliderVolume.Value = (double)((TV)equipment).Volume;
                    sliderVolume.IsEnabled = true;
                }
            }
        }

        private void toggleButtonOnOff_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = dataGridRoomEquipments.SelectedItem as Equipment;
            if (equipment is not null)
            {
                using AppDbContext dbContext = new AppDbContext();
                equipment.Up = (bool)toggleButtonOnOff.IsChecked;
                dbContext.Update(equipment);
                dbContext.Attach(person);
                dbContext.Logs.Add(new Log
                {
                    Person = person,
                    Equipment = equipment,
                    Status = "Turned " + ((equipment.Up) ? "On" : "Off")
                });
                dbContext.SaveChanges();
            }
        }

        private void sliderTemperature_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Equipment equipment = dataGridRoomEquipments.SelectedItem as Equipment;
            if (equipment is not null && equipment is AC or Heater)
            {
                int temperature = (int)sliderTemperature.Value;
                using AppDbContext dbContext = new AppDbContext();
                if (equipment is AC)
                {
                    ((AC)equipment).Temperature = temperature;
                }
                else if (equipment is Heater)
                {
                    ((Heater)equipment).Temperature = temperature;
                }
                dbContext.Equipments.Update(equipment);
                dbContext.Attach(person);
                dbContext.Logs.Add(new Log
                {
                    Person = person,
                    Equipment = equipment,
                    Status = $"Set temperature to {temperature}"
                });
                dbContext.SaveChanges();
            }
        }

        private void sliderVolume_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Equipment equipment = dataGridRoomEquipments.SelectedItem as Equipment;
            if (equipment is not null && equipment is TV)
            {
                int volume = (int)sliderVolume.Value;
                using AppDbContext dbContext = new AppDbContext();
                ((TV)equipment).Volume = volume;
                dbContext.Equipments.Update(equipment);
                dbContext.Attach(person);
                dbContext.Logs.Add(new Log
                {
                    Person = person,
                    Equipment = equipment,
                    Status = $"Set volume to {volume}"
                });
                dbContext.SaveChanges();
            }
        }

        private void menuItemChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow(person).ShowDialog();
        }

        private void menuItemEquipmentsLogs_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new EquipmentsLogsWindow(authWindow, person).Show();
        }

        private void menuItemUsersManagement_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new UsersManagementWindow(authWindow, person).Show();
        }

        private void menuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            authWindow.Show();
        }
    }
}

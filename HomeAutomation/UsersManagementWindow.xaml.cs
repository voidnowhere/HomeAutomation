using HomeAutomation.Models;
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

namespace HomeAutomation
{
    /// <summary>
    /// Interaction logic for UsersManagementWindow.xaml
    /// </summary>
    public partial class UsersManagementWindow : Window
    {
        private AuthWindow authWindow;
        private Person person;
        private void fillUserListView()
        {
            UserListView.ItemsSource = new AppDbContext().Users.OrderByDescending(r => r.Id).ToList();
            using AppDbContext dbContext = new AppDbContext();
            UserListView.ItemsSource = dbContext.Users.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserListView.Items.Clear();
            fillUserListView();
        }


        public UsersManagementWindow(AuthWindow authWindow, Person person)
        {
            InitializeComponent();
            this.authWindow = authWindow;
            this.person = person;
        }

        private void menuItemDashboard_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new DashboardWindow(authWindow, person).Show();
        }

        private void menuItemEquipmentsLogs_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new EquipmentsLogsWindow(authWindow, person).Show();
        }

        private void menuItemChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow(person).ShowDialog();
        }

        private void menuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            authWindow.Show();
        }



        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListView.SelectedItems.Count > 0)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length == 0 || PasswordBox.Text.Length == 0 || FirstNameBox.Text.Length == 0 || LastNameBox.Text.Length == 0
                || CinBox.Text.Length == 0 || BirthDayBox.Text.Length == 0 || PhoneBox.Text.Length == 0)
            {
                MessageBox.Show("Pleas fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using AppDbContext dbContext = new AppDbContext();
            if (dbContext.Users.Count(c => c.NIC == CinBox.Text) > 0)
            {
                MessageBox.Show("this CIN already exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbContext.Users.Add(new User
            {
                Login = LoginBox.Text,
                Password = PasswordBox.Text,
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                NIC = CinBox.Text,
                PhoneNumber = PhoneBox.Text,
            });
            dbContext.SaveChanges();

            LoginBox.Clear();
            PasswordBox.Clear();
            CinBox.Clear();
            FirstNameBox.Clear();
            LastNameBox.Clear();
            PhoneBox.Clear();
            BirthDayBox.Clear();
            fillUserListView();


        }

        
    }
}

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
    /// Interaction logic for EquipmentsLogsWindow.xaml
    /// </summary>
    public partial class EquipmentsLogsWindow : Window
    {
        private AuthWindow authWindow;
        private Person person;

        public EquipmentsLogsWindow(AuthWindow authWindow, Person person)
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

        private void menuItemUsersManagement_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new UsersManagementWindow(authWindow, person).Show();
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
    }
}

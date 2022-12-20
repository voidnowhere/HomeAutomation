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
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void buttonAuth_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxLogin.Text.Length == 0 || passwordBox.Password.Length == 0)
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using AppDbContext dbContext = new AppDbContext();
            dbContext.SaveChanges();
            Person? person = dbContext.People.FirstOrDefault(u => u.Login == textBoxLogin.Text && u.Password == passwordBox.Password);
            if (person is null)
            {
                MessageBox.Show("Verify your login and password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (person is User && !person.IsAcive)
            {
                MessageBox.Show("Your account is deactivated, please contact the administrator!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            textBoxLogin.Clear();
            passwordBox.Clear();
            Hide();
            new DashboardWindow(this, person).Show();
        }
    }
}

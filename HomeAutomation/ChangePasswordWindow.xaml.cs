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
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private Person person;

        public ChangePasswordWindow(Person person)
        {
            InitializeComponent();
            this.person = person;
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBoxOld.Password.Length == 0 || passwordBoxNew.Password.Length == 0 || passwordBoxConfirm.Password.Length == 0)
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordBoxNew.Password != passwordBoxConfirm.Password)
            {
                MessageBox.Show("Confirm password does not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using AppDbContext dbContext = new AppDbContext();
            dbContext.Entry(person).Reload();
            if (person.Password != passwordBoxOld.Password)
            {
                MessageBox.Show("Your current password is incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            person.Password = passwordBoxConfirm.Password;
            dbContext.SaveChanges();
            MessageBox.Show("Your password is updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

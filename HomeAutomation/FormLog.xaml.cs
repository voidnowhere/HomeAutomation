using HomeAutomation.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour FormLog.xaml
    /// </summary>
    public partial class FormLog : Window
    {
        public FormLog()
        {
            InitializeComponent();
            AppDbContext dbcontexte = new AppDbContext();
            ListView items = new ListView();
           ListPerson.ItemsSource = new AppDbContext().People.OrderByDescending(r => r.Id).ToList();
         
        }

        
    }
}

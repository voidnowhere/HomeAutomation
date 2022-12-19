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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            List<Equipment> items = new List<Equipment>();
            items.Add(new Equipment() { Name = "AC"});
            items.Add(new Equipment() { Name = "TV" });
            items.Add(new Equipment() { Name = "Heater" });
            items.Add(new Equipment() { Name = "Door" });
            items.Add(new Equipment() { Name = "Lamp" });
            ListEquipment.ItemsSource = items;

            


        }

        //drag and drop Equipment
        private void TodoItem_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext),DragDropEffects.All);
            }
        }

        private void ListTarget_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects= DragDropEffects.All;
            e.Handled= true;
        }

      /* private void ListEquipment_DragOver(object sender, DragEventArgs e)
        { 
            
            List<Equipment> items = new List<Equipment>();
            e.Effects= DragDropEffects.All;
            e.Handled = true;
            items.Add(new Equipment() { Name = "test" });
            ListTarget.ItemsSource = items;

          
    }
*/
        private void ListTarget_Drop(object sender, DragEventArgs e)
        {
           
            object data = e.Data.GetData(DataFormats.Serializable);
            e.Effects = DragDropEffects.All;
            e.Handled = true;
         
               /* List<object> items = new List<object>();
                items.Add(data);*/
                ListTarget.Items.Add(data);
                //ListTarget.ItemsSource = items;
            


        }

       
    }
    }


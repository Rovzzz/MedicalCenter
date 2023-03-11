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

namespace MedicalCenter
{
    /// <summary>
    /// Логика взаимодействия для Page_Results.xaml
    /// </summary>
    public partial class Page_Results : Page
    {
        public Page_Results()
        {
            InitializeComponent();
            //DGridResults.ItemsSource = Entities.GetContext().Results.ToList();
        }

        private void BtnEdit_Service_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Service_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_AddEdit_Results());
        }

        private void BtnDelete_Service_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridResults.ItemsSource = Entities.GetContext().Results.ToList();
            }
        }
    }
}

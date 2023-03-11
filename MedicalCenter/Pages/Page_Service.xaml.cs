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
    /// Логика взаимодействия для Page_Service.xaml
    /// </summary>
    public partial class Page_Service : Page
    {
        public Page_Service()
        {
            InitializeComponent();
           //DGridService.ItemsSource = Entities.GetContext().Service.ToList();
        }

        private void BtnEdit_Service_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Service_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_AddEdit_Service());
        }

        private void BtnDelete_Service_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridService.ItemsSource = Entities.GetContext().Service.ToList();
            }
        }
    }
}

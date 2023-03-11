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
    /// Логика взаимодействия для Page_Users.xaml
    /// </summary>
    public partial class Page_Users : Page
    {
        public Page_Users()
        {
            InitializeComponent();
            //DGridUsers.ItemsSource = Entities.GetContext().Users.ToList();
        }

        private void BtnEdit_Users_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnAdd_Users_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_AddEdit_User());
        }

        private void BtnDelete_Users_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridUsers.ItemsSource = Entities.GetContext().Users.ToList();
            }
        }
    }
}

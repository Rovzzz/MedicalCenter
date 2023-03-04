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
    /// Логика взаимодействия для Page_NavigateMenu.xaml
    /// </summary>
    public partial class Page_NavigateMenu : Page
    {
        public Page_NavigateMenu()
        {
            InitializeComponent();
        }

        private void Button_Click_Workers(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_Workers());
        }

        private void Button_Click_Users(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_Users());
        }

        private void Button_Click_Results(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_Results());
        }

        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_Service());
        }
    }
}

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

namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_AddEdit_Results.xaml
    /// </summary>
    public partial class Page_AddEdit_Results : Page
    {
        public Page_AddEdit_Results()
        {
            InitializeComponent();
            ComboBox_service_Results.ItemsSource = Entities.GetContext().Service.ToList();
        }

        private void BtnSave_AddEdit_Results_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

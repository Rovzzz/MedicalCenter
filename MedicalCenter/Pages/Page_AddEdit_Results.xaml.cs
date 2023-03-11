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
        private Results _currentResults = new Results();
        public Page_AddEdit_Results()
        {
            InitializeComponent();
            DataContext = _currentResults;
            ComboBox_service_Results.ItemsSource = Entities.GetContext().Service.ToList();
            ComboBox_users_Results.ItemsSource = Entities.GetContext().Users.ToList();
            ComboBox_workers_Results.ItemsSource = Entities.GetContext().Workers.ToList();
        }

        private void BtnSave_AddEdit_Results_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrWhiteSpace(_currentResults.result))
                errors.AppendLine("Укажите результат");


            if (_currentResults.Users == null)
                errors.AppendLine("Укажите клиента");

            if (_currentResults.Workers == null)
                errors.AppendLine("Укажите врача");

            if (_currentResults.Service == null)
               errors.AppendLine("Укажите услугу");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentResults.id == 0)
                Entities.GetContext().Results.Add(_currentResults);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

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
    /// Логика взаимодействия для Page_AddEdit_Service.xaml
    /// </summary>
    public partial class Page_AddEdit_Service : Page
    {
        private Service _currentService = new Service();
        public Page_AddEdit_Service()
        {
            InitializeComponent();
            DataContext = _currentService;
        }

        private void BtnSave_AddEdit_Service_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.Service1))
                errors.AppendLine("Укажите название услуги");

            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentService.Price)))
                errors.AppendLine("Укажите цену услуги");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentService.id == 0)
                Entities.GetContext().Service.Add(_currentService);

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

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
    /// Логика взаимодействия для Page_AddEdit_Worker.xaml
    /// </summary>
    public partial class Page_AddEdit_Worker : Page
    {
        private Workers _currentWorkers = new Workers();
        public Page_AddEdit_Worker()
        {
            InitializeComponent();
            DataContext = _currentWorkers;
            ComboBox_dolgnosti.ItemsSource = Entities.GetContext().Dolgnosti.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentWorkers.name))
                errors.AppendLine("Укажите ФИО сотрудника");

            if (string.IsNullOrWhiteSpace(_currentWorkers.login))
                errors.AppendLine("Укажите логин сотрудника");

            if (string.IsNullOrWhiteSpace(_currentWorkers.password))
                errors.AppendLine("Укажите пароль сотрудника");

            if (_currentWorkers.Dolgnosti == null)
                errors.AppendLine("Укажите должность сотрудника");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentWorkers.id == 0)
                Entities.GetContext().Workers.Add(_currentWorkers);

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

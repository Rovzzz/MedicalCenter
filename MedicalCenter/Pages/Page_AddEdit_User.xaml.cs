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
    /// Логика взаимодействия для Page_AddEdit_User.xaml
    /// </summary>
    public partial class Page_AddEdit_User : Page
    {
        private Users _currentUsers = new Users();
        public Page_AddEdit_User(Users selectedUsers)
        {
            InitializeComponent();

            if(selectedUsers != null)
            {
                _currentUsers = selectedUsers;
            }

            DataContext = _currentUsers;
        }

        private void BtnSave_AddEdit_User_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUsers.name))
                errors.AppendLine("Укажите ФИО клиента");

            if (string.IsNullOrWhiteSpace(_currentUsers.login))
                errors.AppendLine("Укажите логин клиента");

            if (string.IsNullOrWhiteSpace(_currentUsers.password))
                errors.AppendLine("Укажите пароль клиента");

            if (string.IsNullOrWhiteSpace(_currentUsers.ip))
                errors.AppendLine("Укажите ip клиента");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentUsers.id == 0)
                Entities.GetContext().Users.Add(_currentUsers);

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

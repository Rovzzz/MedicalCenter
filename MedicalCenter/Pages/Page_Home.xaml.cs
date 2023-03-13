using MedicalCenter.Classes;
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
    /// Логика взаимодействия для Page_Home.xaml
    /// </summary>
    public partial class Page_Home : Page
    {
        public Page_Home(Workers worker)
        {
            InitializeComponent();
            GetInfoWorker(worker);
            if (worker.id_dolgnost != 1)
            {
                btn_history.Visibility = Visibility.Hidden;
            }
        }


        private void GetInfoWorker(Workers worker)
        {
            tbName.Text = worker.name;
            if (worker.id_dolgnost == 1)
                tbRole.Text = "Администратор";
            if (worker.id_dolgnost == 2)
                tbRole.Text = "Пользователь";
            if (worker.id_dolgnost == 3)
                tbRole.Text = "Лаборант";
        }

        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_History());
        }

        private void Btn_Navigation_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_NavigateMenu());
        }
    }
}

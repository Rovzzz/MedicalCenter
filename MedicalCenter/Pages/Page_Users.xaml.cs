using MedicalCenter.Classes;
using MedicalCenter.Pages;
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
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;

        List<Users> users = new List<Users>();

        public Page_Users(Workers worker)
        {
            InitializeComponent();
            //DGridUsers.ItemsSource = Entities.GetContext().Users.ToList();
            users = CurrentData.db.Users.ToList();
            maxpage = users.Count / pageSize;
        }

        private void BtnEdit_Users_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_AddEdit_User((sender as Button).DataContext as Users));
        }

        private void BtnAdd_Users_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_AddEdit_User(null));
        }

        private void BtnDelete_Users_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DGridUsers.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {usersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().Users.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены !");

                    DGridUsers.ItemsSource = Entities.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridUsers.ItemsSource = Entities.GetContext().Users.ToList();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && DGridUsers != null)
            {
               users = users.Where(n => n.name.ToLower().Contains(search.Text.ToLower())).ToList();
                DGridUsers.ItemsSource = users;
                maxpage = users.Count / pageSize;
            }
            else
            {
                if (DGridUsers != null)
                {
                    users = CurrentData.users;
                    maxpage = users.Count / pageSize;
                }

            }
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (search.Text == "Поиск")
                search.Text = "";
            else if (search.Text == "")
                search.Text = "Поиск";
        }
    }
}

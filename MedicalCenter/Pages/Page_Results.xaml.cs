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
    /// Логика взаимодействия для Page_Results.xaml
    /// </summary>
    public partial class Page_Results : Page
    {
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;

        List<Results> results = new List<Results>();

        public Page_Results(Workers worker)
        {
            InitializeComponent();
            //DGridResults.ItemsSource = Entities.GetContext().Results.ToList();
            results = CurrentData.db.Results.ToList();
            maxpage = results.Count / pageSize;
        }

        private void BtnEdit_Service_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_AddEdit_Results((sender as Button).DataContext as Results));
        }

        private void BtnAdd_Service_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.Page_AddEdit_Results(null));
        }

        private void BtnDelete_Service_Click(object sender, RoutedEventArgs e)
        {
            var resultsForRemoving = DGridResults.SelectedItems.Cast<Results>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {resultsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().Results.RemoveRange(resultsForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены !");

                    DGridResults.ItemsSource = Entities.GetContext().Results.ToList();
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
                DGridResults.ItemsSource = Entities.GetContext().Results.ToList();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && DGridResults != null)
            {
                results = results.Where(n => n.Users.name.ToLower().Contains(search.Text.ToLower())).ToList();
                DGridResults.ItemsSource = results;
                maxpage = results.Count / pageSize;
            }
            else
            {
                if (DGridResults != null)
                {
                    results = CurrentData.results;
                    maxpage = results.Count / pageSize;
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

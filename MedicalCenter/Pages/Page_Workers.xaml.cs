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

namespace MedicalCenter
{
    /// <summary>
    /// Логика взаимодействия для Page_Workers.xaml
    /// </summary>
    public partial class Page_Workers : Page
    {
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;

        List<Workers> workers = new List<Workers>();

        public Page_Workers(Workers worker)
        {
            InitializeComponent();
            //DGridWorkers.ItemsSource = Entities.GetContext().Workers.ToList();
            workers = CurrentData.db.Workers.ToList();
            maxpage = workers.Count / pageSize;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_AddEdit_Worker((sender as Button).DataContext as Workers));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var workersForRemoving = DGridWorkers.SelectedItems.Cast<Workers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {workersForRemoving.Count()} элементов?", "Внимание", 
                MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().Workers.RemoveRange(workersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены !");

                    DGridWorkers.ItemsSource = Entities.GetContext().Workers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_AddEdit_Worker(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridWorkers.ItemsSource = Entities.GetContext().Workers.ToList();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && DGridWorkers != null)
            {
                workers = workers.Where(n => n.name.ToLower().Contains(search.Text.ToLower())).ToList();
                DGridWorkers.ItemsSource = workers;
                maxpage = workers.Count / pageSize;
            }
            else
            {
                if (DGridWorkers != null)
                {
                    workers = CurrentData.workers;
                    maxpage = workers.Count / pageSize;
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

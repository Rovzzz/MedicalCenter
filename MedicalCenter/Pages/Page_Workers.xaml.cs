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
        public Page_Workers()
        {
            InitializeComponent();
            //DGridWorkers.ItemsSource = Entities.GetContext().Workers.ToList();
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
    }
}

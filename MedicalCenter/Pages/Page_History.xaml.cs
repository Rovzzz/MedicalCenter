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
    /// Логика взаимодействия для Page_History.xaml
    /// </summary>
    public partial class Page_History : Page
    {
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;

        List<HistoryHot> services = new List<HistoryHot>();

        public Page_History()
        {
            InitializeComponent();
            services = CurrentData.db.HistoryHot.ToList();
            maxpage = services.Count / pageSize;
            DisplayDataInGrid();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page_Home(CurrentData.worker));
        }

        private void DisplayDataInGrid()
        {
            var currentPageData = services.Skip(pageNumber * pageSize).Take(pageSize); // отображаем только данные для текущей страницы
            DataGridHistory.ItemsSource = currentPageData; // отображаем данные в DataGrid
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber < maxpage)
            {
                pageNumber++; // переход на следующую страницу
                DisplayDataInGrid(); // отображение данных
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber > 0)
            {
                pageNumber--; // переход на предыдущую страницу
                DisplayDataInGrid(); // отображение данных
            }
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (search.Text == "Поиск")
                search.Text = "";
            else if (search.Text == "")
                search.Text = "Поиск";
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && DataGridHistory != null)
            {
                services = services.Where(n => n.name.ToLower().Contains(search.Text.ToLower())).ToList();
                DataGridHistory.ItemsSource = services;
                maxpage = services.Count / pageSize;
            }
            else
            {
                if (DataGridHistory != null)
                {
                    services = CurrentData.historyHots;
                    maxpage = services.Count / pageSize;
                }

            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Service service = DataGridHistory.SelectedValue as Service;
            Manager.MainFrame.Navigate(new Page_AddEdit_Service(service));
        }
    }
}

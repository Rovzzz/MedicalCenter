using MedicalCenter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Page_Autorization.xaml
    /// </summary>
    public partial class Page_Autorization : Page
    {
        CaptchaWindow captchaWindow;
        private bool isFailedToSignIn;

        public Page_Autorization()
        {
            InitializeComponent();
            captchaWindow = new CaptchaWindow();
        }

        List<Workers> workers = new List<Workers>();


        private void cbShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (cbShowPassword.IsChecked == true)
            {
                tboxPassword.Text = pboxPassword.Password;

                pboxPassword.Visibility = Visibility.Hidden;
                tboxPassword.Visibility = Visibility.Visible;
            }
            else
            {
                pboxPassword.Visibility = Visibility.Visible;
                tboxPassword.Visibility = Visibility.Hidden;
            }
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pboxPassword.Password;
            bool isAuth = false;
            workers = CurrentData.db.Workers.ToList();
            foreach (Workers worker in workers)
            {
                if (worker.login == login && worker.password == password)
                {
                    CurrentData.worker = worker;
                    Manager.MainFrame.Navigate(new Page_Home(worker));
                    isAuth = true;
                    AddToHistory(worker);
                    break;
                }
            }
            if (!isAuth)
            {
                MessageBox.Show("Логин или пароль неверен");
                if (captchaWindow.IsPassedCaptcha)
                {
                    SignInButtonBlock();
                }
                else if (isFailedToSignIn)
                {
                    captchaWindow.Show();
                }
                isFailedToSignIn = true;
            }
        }

        private void AddToHistory(Workers worker)
        {

            HistoryHot historyHot = new HistoryHot();
            historyHot.login = worker.login;
            historyHot.name = worker.name;
            historyHot.role = worker.id_dolgnost;
            historyHot.data = DateTime.Now;
            historyHot.block = "Нет";
            Classes.CurrentData.db.HistoryHot.Add(historyHot);
            Classes.CurrentData.db.SaveChanges();
        }

        public async void SignInButtonBlock()
        {
            bLogin.IsEnabled = false;
            await Task.Run(() => Thread.Sleep(10000));
            bLogin.IsEnabled = true;
            captchaWindow.IsPassedCaptcha = false;
        }
    }
}

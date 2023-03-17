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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicalCenter
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        private static Random rand = new Random();
        private string captchaStr;

        public bool IsPassedCaptcha { get; set; }

        public CaptchaWindow()
        {
            InitializeComponent();

            GenerateCaptcha();
            IsPassedCaptcha = false;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (captchaStr.ToLower() == tbInput.Text.ToString().ToLower())
            {
                IsPassedCaptcha = true;
                Visibility = Visibility.Hidden;
            }
            ReGenerateCaptcha();
        }

        private void GenerateCaptcha() => GenerateGridColumns(GenerateCaptchaText());

        private void RepeatButton_click(object sender, RoutedEventArgs e) => ReGenerateCaptcha();

        private char[] GenerateCaptchaText()
        {
            int captchaLenght = rand.Next(4, 8);
            char[] captcha = new char[captchaLenght];

            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";

            for (int i = 0; i < captchaLenght; i++)
            {
                captcha[i] = allowedChars[rand.Next(0, allowedChars.Length)];
            }
            captchaStr = new string(captcha);
            return captcha;
        }

        private void GenerateGridColumns(char[] captcha)
        {
            ColumnDefinition[] colDefs = new ColumnDefinition[captcha.Length];

            for (int i = 0; i < captcha.Length; i++)
            {
                colDefs[i] = new ColumnDefinition();
                captchaGrid.ColumnDefinitions.Add(colDefs[i]);

                TextBlock captchaSymTextBlock = new TextBlock();
                captchaSymTextBlock.Text = captcha[i].ToString();
                Grid.SetColumn(captchaSymTextBlock, i);
                GenerateCaptchaNoise(captchaSymTextBlock);
                captchaGrid.Children.Add(captchaSymTextBlock);
            }
        }

        private void GenerateCaptchaNoise(TextBlock captchaSym)
        {
            captchaSym.FontSize = rand.Next(24, 43);
            captchaSym.RenderTransform = new RotateTransform(rand.Next(-45, 46));
            captchaSym.HorizontalAlignment = (HorizontalAlignment)rand.Next(0, 3);
            BlurEffect be = new BlurEffect();
            be.Radius = rand.Next(3, 5);
            captchaSym.Effect = be;
            captchaSym.Padding = new Thickness(6, 0, 6, 0);
            if (rand.Next(0, 3) == 0)
            {
                captchaSym.TextDecorations = TextDecorations.Strikethrough;
            }
        }

        private void ReGenerateCaptcha()
        {
            captchaGrid.Children.Clear();
            captchaGrid.ColumnDefinitions.Clear();
            tbInput.Text = string.Empty;
            GenerateCaptcha();
        }

    }
}

using Aspose.BarCode.Generation;
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
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MedicalCenter
{
    /// <summary>
    /// Логика взаимодействия для BarcodeGenerate.xaml
    /// </summary>
    public partial class BarcodeGenerate : Window
    {
        public BarcodeGenerate()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Установить по умолчанию как PNG
            var imageType = "Png";

            // Получить выбранный пользователем формат изображения
            if (rbPng.IsChecked == true)
            {
                imageType = rbPng.Content.ToString();
            }
            else if (rbBmp.IsChecked == true)
            {
                imageType = rbBmp.Content.ToString();
            }
            else if (rbJpg.IsChecked == true)
            {
                imageType = rbJpg.Content.ToString();
            }

            // Получить формат изображения из перечисления
            var imageFormat = (BarCodeImageFormat)Enum.Parse(typeof(BarCodeImageFormat), imageType.ToString());

            // Установить по умолчанию как Code128
            var encodeType = EncodeTypes.Code128;

            // Получить выбранный пользователем тип штрих-кода
            if (!string.IsNullOrEmpty(comboBarcodeType.Text))
            {
                switch (comboBarcodeType.Text)
                {
                    case "Code128":
                        encodeType = EncodeTypes.Code128;
                        break;

                    case "ITF14":
                        encodeType = EncodeTypes.ITF14;
                        break;

                    case "EAN13":
                        encodeType = EncodeTypes.EAN13;
                        break;

                    case "Datamatrix":
                        encodeType = EncodeTypes.DataMatrix;
                        break;

                    case "Code32":
                        encodeType = EncodeTypes.Code32;
                        break;

                    case "Code11":
                        encodeType = EncodeTypes.Code11;
                        break;

                    case "PDF417":
                        encodeType = EncodeTypes.Pdf417;
                        break;

                    case "EAN8":
                        encodeType = EncodeTypes.EAN8;
                        break;

                    case "QR":
                        encodeType = EncodeTypes.QR;
                        break;
                }
            }

            // Инициализировать объект штрих-кода
            Barcode barcode = new Barcode();
            barcode.Text = tbCodeText.Text;
            barcode.BarcodeType = encodeType;
            barcode.ImageType = imageFormat;

            try
            {
                string imagePath = "";

                if (cbGenerateWithOptions.IsChecked == true)
                {
                    // Сгенерируйте штрих-код с дополнительными параметрами и получите путь к изображению
                    imagePath = GenerateBarcodeWithOptions(barcode);
                }
                else
                {
                    // Сгенерировать штрих-код и получить путь к изображению
                    imagePath = GenerateBarcode(barcode);
                }

                // Показать изображение
                Uri fileUri = new Uri(Path.GetFullPath(imagePath));
                imgDynamic.Source = new BitmapImage(fileUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string GenerateBarcode(Barcode barcode)
        {
            // Путь к изображению
            string imagePath = comboBarcodeType.Text + "." + barcode.ImageType;

            // Инициализировать генератор штрих-кода
            BarcodeGenerator generator = new BarcodeGenerator(barcode.BarcodeType, barcode.Text);

            // Сохранить изображение
            generator.Save(imagePath, barcode.ImageType);

            return imagePath;
        }

        private string GenerateBarcodeWithOptions(Barcode barcode)
        {
            // Путь к изображению
            string imagePath = comboBarcodeType.Text + "." + barcode.ImageType;

            // Инициализировать генератор штрих-кода
            BarcodeGenerator generator = new BarcodeGenerator(barcode.BarcodeType, barcode.Text);

            if (barcode.BarcodeType == EncodeTypes.QR)
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                //установить автоматическую версию
                generator.Parameters.Barcode.QR.QrVersion = QRVersion.Auto;
                //Установите тип автоматического кодирования QR
                generator.Parameters.Barcode.QR.QrEncodeType = QREncodeType.Auto;
            }
            else if (barcode.BarcodeType == EncodeTypes.Pdf417)
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Parameters.Barcode.Pdf417.Columns = 3;
            }
            else if (barcode.BarcodeType == EncodeTypes.DataMatrix)
            {
                //установите DataMatrix ECC на 140
                generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;
            }
            else if (barcode.BarcodeType == EncodeTypes.Code32)
            {
                generator.Parameters.Barcode.XDimension.Millimeters = 1f;
            }
            else
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                //установить BarHeight 40
                generator.Parameters.Barcode.BarHeight.Pixels = 40;
            }

            // Сохранить изображение
            generator.Save(imagePath, barcode.ImageType);

            return imagePath;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MedicalCenter
{
    class Manager
    {
        public static Frame frame { get; set; }

        public static void StartTimer()
        {
            Thread thread = new Thread(TimerCallback);
            thread.Start();
        }

        static void TimerCallback()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Timer timer = new Timer((state) => {
                if (stopwatch.Elapsed.Minutes == 45)
                    MessageBox.Show("Время до завершения сессии: " + 15 + " минут");
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            while (stopwatch.Elapsed.Hours < 1) { }
            Classes.CurrentData.worker = null;
            Manager.frame.Navigate(new Pages.Page_Autorization());
        }

        public static Frame MainFrame { get; set; }
    }
}

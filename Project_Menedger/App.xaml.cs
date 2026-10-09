using Project_Menedger.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Menedger
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static is1_25_kokorinds_Kursovoy_ProjectEntities1 db = new is1_25_kokorinds_Kursovoy_ProjectEntities1();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var splashScreen = new Load();
            this.MainWindow = splashScreen;
            splashScreen.Show();

            Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(30);

                    splashScreen.Dispatcher.Invoke(() => splashScreen.Progress = i);
                }

                this.Dispatcher.Invoke(() =>
                {

                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }
    }
}

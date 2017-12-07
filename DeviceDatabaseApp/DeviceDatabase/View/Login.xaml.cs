using DeviceDatabase.Controller;
using DeviceDatabase.Model;
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

namespace DeviceDatabase.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //IT-Administrator
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Device d = new Device { Name = "Laptop 001", SerialCode = "01232ABCD", Status = 0, TypeId = 0, CalamitiesList = new List<Calamity>() };
            d.CalamitiesList.Add(new Calamity { About = "Lots of smoke", Date = DateTime.Now });
            DatabaseController.AddDevice(d);



        }
        //IT-Manager
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //List<Device> d = DatabaseController.GetDevices();
            //foreach (Device b in d)
            //{
            //    List<Calamity> c = new List<Calamity>();
            //    c.Add(new Calamity { About = "Lots of fire", Date = DateTime.Now });
            //    b.CalamitiesList = c;
            //}

            List<Device> d = DatabaseController.GetDevices();
            foreach (Device b in d)
            {
                Console.WriteLine(b.Id + " - " + b.Name);
                foreach (Calamity c in b.CalamitiesList)
                {
                    Console.WriteLine(c.Id + " - " + c.About);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseController.InitialSetup();
        }
    }
}

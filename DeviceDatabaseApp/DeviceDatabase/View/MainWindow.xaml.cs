using DeviceDatabase.Controller;
using DeviceDatabase.Model;
using DeviceDatabase.Model.Authentication;
using DeviceDatabase.View.CustomMessageBox;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceDatabase.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //https://lvcharts.net/App/examples/wpf/start

    [PrincipalPermission(SecurityAction.Demand)]
    public partial class MainWindow : Window, IView
    {
        private LiveChartsController liveChartsController;
        private BarcodeStickerController barcodeStickerController;

        public event EventHandler LoggingOut;

        public MainWindow()
        {
            InitializeComponent();

            liveChartsController = new LiveChartsController();
            barcodeStickerController = new BarcodeStickerController();

            // Bind data to the UI

            DataContext = liveChartsController;
        }

        public IViewModel ViewModel
        {
            get
            {
                return DataContext as IViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDeviceListView();
            UpdateDeviceTypeListView();
            UpdateCalamityListView();
            UpdateLogbookView();

            this.dg_Logbook.ItemsSource = DatabaseController.Logbook.LogList;
        }

        private void AddCalamity(object sender, RoutedEventArgs e)
        {
            Device d = (Device)this.dg_DevicesList.SelectedItem;

            AddCalamityView acv = new AddCalamityView(this, d.Name);

            if (acv.ShowDialog() == true)
            {
                DatabaseController.AddCalamity(d.DeviceId, acv.NewCalamity);
                UpdateDeviceListView();
                UpdateCalamityListView();
                UpdateLogbookView();
                liveChartsController.UpdateDistinctYears();
            }
        }

        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            Device d = (Device)this.dg_DevicesList.SelectedItem;

            if (MessageBox.Show(string.Format("Are you sure you want to delete {0}?", d.Name), string.Format("Deleting device {0}", d.Name), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseController.DeleteDevice(d.DeviceId);
                UpdateDeviceListView();
                UpdateCalamityListView();
                UpdateLogbookView();
                liveChartsController.UpdateDistinctYears();
            }
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            AddDeviceView adv = new AddDeviceView(this);

            if (adv.ShowDialog() == true)
            {
                DatabaseController.AddDevice(adv.NewDevice);
                UpdateDeviceListView();
                UpdateLogbookView();
            }
        }

        private void EditDevice(object sender, RoutedEventArgs e)
        {
            Device d = (Device)this.dg_DevicesList.SelectedItem;

            AddDeviceView adv = new AddDeviceView(this, d);

            if (adv.ShowDialog() == true)
            {
                DatabaseController.EditDevice(adv.NewDevice);
                UpdateDeviceListView();
                UpdateCalamityListView();
                UpdateLogbookView();
                liveChartsController.UpdateDistinctYears();
            }
        }

        private void AddDeviceType(object sender, RoutedEventArgs e)
        {
            AddDeviceTypeView advt = new AddDeviceTypeView(this);

            if (advt.ShowDialog() == true)
            {
                DatabaseController.AddDeviceType(advt.NewDeviceType);
                UpdateDeviceTypeListView();
                UpdateLogbookView();
            }
        }

        private void tb_SearchDevice_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDeviceListView();
        }

        private void UpdateDeviceListView()
        {
            if (tb_SearchDevice.Text.Trim() != "")
            {
                this.dg_DevicesList.ItemsSource = DatabaseController.SearchDevices(tb_SearchDevice.Text);
            }
            else
            {
                this.dg_DevicesList.ItemsSource = DatabaseController.GetDevices();
            }
        }

        private void tb_SearchDeviceType_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDeviceTypeListView();
        }

        private void UpdateCalamityListView()
        {
            if (tb_SearchCalamity.Text.Trim() != "")
            {
                this.dg_CalamityList.ItemsSource = DatabaseController.SearchCalamities(tb_SearchCalamity.Text);
            }
            else
            {
                this.dg_CalamityList.ItemsSource = DatabaseController.GetCalamities();
            }
        }

        private void UpdateLogbookView()
        {
            //this.dg_Logbook.ItemsSource = DatabaseController.Logbook.LogList;
        }

        private void cb_Years_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_YearsSelectedItemCheck();
        }

        private void cb_Years_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            this.cb_Years.SelectedIndex = 0;

            cb_YearsSelectedItemCheck();
        }

        private void cb_YearsSelectedItemCheck()
        {
            if (this.cb_Years.SelectedItem != null)
            {
                this.liveChartsController.SelectedYear = this.cb_Years.SelectedItem.ToString();
                this.liveChartsController.UpdateDistinctMonths();
            }
            else
            {
                this.liveChartsController.SelectedYear = null;
                liveChartsController.ClearLiveChart();
            }
        }

        private void cb_Months_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_MonthsSelectedItemCheck();
        }

        private void cb_Months_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            this.cb_Months.SelectedIndex = 0;
            cb_MonthsSelectedItemCheck();
        }

        private void cb_MonthsSelectedItemCheck()
        {
            if (this.cb_Months.SelectedItem != null)
            {
                string str = this.cb_Months.SelectedItem.ToString();
                liveChartsController.UpdateLiveChart(str);
            }
            else
            {
                liveChartsController.ClearLiveChart();
            }
        }



        private void UpdateDeviceTypeListView()
        {
            if (tb_SearchDeviceType.Text.Trim() != "")
            {
                this.dg_DeviceTypesList.ItemsSource = DatabaseController.SearchDeviceTypes(tb_SearchDeviceType.Text);
            }
            else
            {
                this.dg_DeviceTypesList.ItemsSource = DatabaseController.GetDeviceTypes();
            }
        }

        private void DeleteDeviceType(object sender, RoutedEventArgs e)
        {
            DeviceType d = (DeviceType)this.dg_DeviceTypesList.SelectedItem;

            if (MessageBox.Show(string.Format("Are you sure you want to delete {0}?", d.Name), string.Format("Deleting device {0}", d.Name), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseController.DeleteDeviceType(d.DeviceTypeId);
                UpdateDeviceListView();
                UpdateDeviceTypeListView();
                UpdateLogbookView();
            }
        }

        private void EditDeviceType(object sender, RoutedEventArgs e)
        {
            DeviceType d = (DeviceType)this.dg_DeviceTypesList.SelectedItem;

            AddDeviceTypeView adt = new AddDeviceTypeView(this, d);

            if (adt.ShowDialog() == true)
            {
                DatabaseController.EditDeviceType(adt.NewDeviceType);
                UpdateDeviceListView();
                UpdateDeviceTypeListView();
                UpdateLogbookView();
            }
        }

        private void EditCalamity(object sender, RoutedEventArgs e)
        {
            Calamity d = (Calamity)this.dg_CalamityList.SelectedItem;

            AddCalamityView acv = new AddCalamityView(this, d);

            if (acv.ShowDialog() == true)
            {
                DatabaseController.EditCalamity(acv.NewCalamity);
                UpdateDeviceListView();
                UpdateCalamityListView();
                UpdateLogbookView();
                liveChartsController.UpdateDistinctYears();
            }
        }
        private void DeleteCalamity(object sender, RoutedEventArgs e)
        {
            Calamity c = (Calamity)this.dg_CalamityList.SelectedItem;

            if (MessageBox.Show(string.Format("Are you sure you want to delete calamity from {0}?", c.Device.Name), string.Format("Deleting calamity {0}", c.About), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseController.DeleteCalamity(c.CalamityId);
                UpdateCalamityListView();
                UpdateDeviceListView();
                UpdateLogbookView();
                liveChartsController.UpdateDistinctYears();
            }
        }

        private void CalamityIsSolvedChanged(object sender, RoutedEventArgs e)
        {
            // Get clicked Calamity from DataGridList
            Calamity c = (Calamity)this.dg_CalamityList.SelectedItem;
            // Cast sender to checkbox
            CheckBox cb = (CheckBox)sender;

            c.IsSolved = cb.IsChecked.Value;

            if (c.IsSolved)
            {
                SolveCalamityView scv = new SolveCalamityView(this, c);

                if (scv.ShowDialog() == true)
                {
                    c.IsSolvedDate = scv.Calamity.IsSolvedDate;
                    c.IsSolvedSolution = scv.Calamity.IsSolvedSolution;
                } else
                {
                    return;
                }
            }
            else
            {
                c.IsSolvedDate = null;
                c.IsSolvedSolution = null;
            }

            DatabaseController.EditCalamity(c);
            // Update view
            UpdateCalamityListView();
        }

        private void tb_SearchCalamity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalamityListView();
        }

        private void ShowPrintBarcodePreview(object sender, RoutedEventArgs e)
        {
            Device d = (Device)this.dg_DevicesList.SelectedItem;

            System.Drawing.Image img = barcodeStickerController.GenerateSticker(d);

            BarcodeView bcv = new BarcodeView(this, barcodeStickerController.ConvertToBitmapImage(img), d.Name);

            if (bcv.ShowDialog() == true)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.FileName = d.Name;
                saveFileDialog.DefaultExt = ".jpg";
                saveFileDialog.Filter = "Images|*.png;*.bmp;*.jpg";

                if (saveFileDialog.ShowDialog() == true)
                {
                    img.Save(saveFileDialog.FileName);
                }
            }
        }

        private void InfoCalamity(object sender, RoutedEventArgs e)
        {
            // Get clicked Calamity from DataGridList
            Calamity c = (Calamity)this.dg_CalamityList.SelectedItem;

            CalamityView cv = new CalamityView(this, c);
            cv.ShowDialog();
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.LoggingOut?.Invoke(this, new EventArgs());
        }
    }
}

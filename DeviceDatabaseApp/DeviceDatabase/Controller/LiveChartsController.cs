using DeviceDatabase.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Controller
{
    public class LiveChartsController : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public string SelectedYear { get; set; }

        public List<string> DistinctYears { get; set; }
        public List<string> DistinctMonths { get; set; }

        private List<DateTime> dates;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LiveChartsController()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Solved",
                    Values = null
                },
                new ColumnSeries
                {
                    Title = "Not Solved",
                    Values = null
                }
            };

            Labels = new List<string>();

            OnPropertyChanged("SeriesCollection");
            OnPropertyChanged("Labels");

            Formatter = value => value.ToString();

            UpdateDistinctYears();
        }

        public void UpdateDistinctYears()
        {
            List<Calamity> cList = DatabaseController.GetCalamities();

            dates = cList
              .Select(c => new DateTime(c.Date.Year, c.Date.Month, 1))
              .ToList();

            this.DistinctYears = dates
                .Select(c => c.Date.Year.ToString())
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            OnPropertyChanged("DistinctYears");

            UpdateDistinctMonths();
        }

        public void UpdateDistinctMonths()
        {
            //DateTime.Month.ToString(“MMM”) isn't working
            //https://stackoverflow.com/a/18926064

            if (this.SelectedYear != null)
            {
                this.DistinctMonths = this.dates
                    .Where(e => e.Year.ToString() == this.SelectedYear)
                    .OrderBy(e => e.Month)
                    .Select(e => e.ToString("MMMM", CultureInfo.InvariantCulture))
                    .Distinct()
                    .ToList();
            }
            else
            {
                this.DistinctMonths = null;
            }

            OnPropertyChanged("DistinctMonths");
        }

        public void UpdateLiveChart(string _Month)
        {
            Labels.Clear();
            SeriesCollection[0].Values = new ChartValues<int>();
            SeriesCollection[1].Values = new ChartValues<int>();

            List<Device> dList = DatabaseController.GetDevices();

            foreach (Device d in dList)
            {
                List<Calamity> cList = d.CalamityCollection
                    .Where(e => e.Date.ToString("MMMM", CultureInfo.InvariantCulture) == _Month && e.Date.Year.ToString() == this.SelectedYear)
                    .ToList();

                if (cList.Count() != 0)
                {
                    Labels.Add(d.Name);
                    SeriesCollection[0].Values.Add(cList.Where(e => e.IsSolved).Count());
                    SeriesCollection[1].Values.Add(cList.Where(e => !e.IsSolved).Count());
                }
            }

            OnPropertyChanged("SeriesCollection");
            OnPropertyChanged("Labels");
        }

        public void ClearLiveChart()
        {
            Labels.Clear();
            SeriesCollection[0].Values = null;
            SeriesCollection[1].Values = null;

            OnPropertyChanged("SeriesCollection");
            OnPropertyChanged("Labels");
        }
    }
}

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

namespace TilleWPF.Domain
{
    using Model;
    /// <summary>
    /// Interaction logic for SearchDialog.xaml
    /// </summary>
    public partial class SearchDialog : UserControl
    {
        public bool IsCancelled;
        public MainController Controller { get; set; }
        public int year, month;

        public SearchDialog(MainController controller)
        {
            InitializeComponent();
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            this.Controller = controller;
            UpdateComboboxYear();
            UpdateComboBoxMonth();
        }

        private void MonthToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.monthCombobox.IsEnabled = true;
            this.AcceptButton.IsEnabled = true;
        }
        private void MonthToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            this.monthCombobox.IsEnabled = false;
            if (!yearComboBox.IsEnabled)
            {
                this.AcceptButton.IsEnabled = false;
            }
        }

        private void YearToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            yearComboBox.IsEnabled = true;
            AcceptButton.IsEnabled = true;
            UpdateComboBoxMonth();
        }
        private void YearToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            this.yearComboBox.IsEnabled = false;
            if (!monthCombobox.IsEnabled)
            {
                AcceptButton.IsEnabled = false;
            }
            UpdateComboBoxMonth();
        }

        public void UpdateComboboxYear()
        {
            var collectionYears = Controller.YearsAvailable();
            foreach (var item in collectionYears)
            {
                this.yearComboBox.Items.Add(new ComboBoxItem()
                {
                    Content = item.Key,
                    Tag = item.Key,
                    IsSelected = year == item.Key ? true:false
                });

            }
        }  
        public void UpdateComboBoxMonth()
        {
            if (yearComboBox.IsEnabled)
            {
                if (monthCombobox.Items.Count >= 0)
                {
                    monthCombobox.Items.Clear();
                }
                int selectedYear = (int)((ComboBoxItem)yearComboBox.SelectedItem).Tag;
                var collectionMonth = Controller.MonthByYears(selectedYear);
                DateTime dateAux;
                foreach (var item in collectionMonth)
                {
                    dateAux = new DateTime(1, item.Key, 1);
                    monthCombobox.Items.Add(new ComboBoxItem()
                    {
                        Content = dateAux.ToString("MMMM"),
                        Tag = item.Key,
                        IsSelected = month.Equals(item.Key) ? true : false
                    });
                }
                if (monthCombobox.SelectedIndex == -1)
                {
                    monthCombobox.SelectedIndex = 0;
                }
            }
            else
            {
                if (monthCombobox.Items.Count >= 0)
                {
                    monthCombobox.Items.Clear();
                }
                var collectionMonth = Controller.MonthAvailables();
                DateTime dateAux;
                foreach (var item in collectionMonth)
                {
                    dateAux = new DateTime(1, item.Key, 1);
                    monthCombobox.Items.Add(new ComboBoxItem()
                    {
                        Content = dateAux.ToString("MMMM"),
                        Tag = item.Key,
                        IsSelected = month.Equals(item.Key) ? true : false
                    });
                }
                if (monthCombobox.SelectedIndex == -1)
                {
                    monthCombobox.SelectedIndex = 0;
                }
            }
            
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComboBoxMonth();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            if (yearComboBox.IsEnabled)
            {
                year = (int)((ComboBoxItem)yearComboBox.SelectedItem).Tag;
            }
            else
            {
                year = -1;
            }
            if (monthCombobox.IsEnabled)
            {
                month = (int)((ComboBoxItem)monthCombobox.SelectedItem).Tag;
            }
            else
            {
                month = -1;
            }
            IsCancelled = false;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
        }
    }
}

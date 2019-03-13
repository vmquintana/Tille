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
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        MainController controller;
        int year, month;
        public SearchView(MainController controller, int month, int year)
        {
            InitializeComponent();
            this.month = month;
            this.year = year;
            this.controller = controller;
            Search();
            UpdateFooter();
            CalculateFootersValues();
        }
        //Search
        public void Search()
        {
            if (month != -1 || year != -1)
            {
                FillSearchDataGrid(true, month, year);
                FillMonthHeader(month, year);
                CalculateFootersValues();
            }
        }
        //Fill datagrids with search information
        private void FillSearchDataGrid(bool refill, int month, int year)
        {
            if (refill)
            {
                //Expenses
                if (dataGridExpensesSearch.ItemsSource != null)
                {
                    dataGridExpensesSearch.ItemsSource = null;
                }

                //Profit
                if (dataGridProfitsSearch.ItemsSource != null)
                {
                    dataGridProfitsSearch.ItemsSource = null;
                }

                //Stats
                if (dataGridStatsSearch.ItemsSource != null)
                {
                    dataGridStatsSearch.ItemsSource = null;
                }

                //Book
                if (dataGridBookSearch.ItemsSource != null)
                {
                    dataGridBookSearch.ItemsSource = null;
                }

                //Services
                if (dataGridServicesSearch.ItemsSource != null)
                {
                    dataGridServicesSearch.ItemsSource = null;
                }
            }

            //Book
            this.dataGridBookSearch.ItemsSource = controller.SearchBook(month, year);

            //Expenses
            this.dataGridExpensesSearch.ItemsSource = controller.SearchDataExpense(month, year);

            //Profit
            this.dataGridProfitsSearch.ItemsSource = controller.SearchDataProfit(month, year);

            //Stats
            this.dataGridStatsSearch.ItemsSource = controller.SearchStats(month, year);

            //Services
            this.dataGridServicesSearch.ItemsSource = controller.SearchServices(month, year);
        }
       
        //Fill headers
        private void FillMonthHeader(int month, int year)
        {
            string dateToWrite;
            DateTime date;
            if (month != -1 && year != -1)
            {
                date = new DateTime(year, month, 1);
                dateToWrite = date.ToString("MMMM/yyyy");
            }
            else if (month != -1 && year == -1)
            {
                date = new DateTime(1, month, 1);
                dateToWrite = date.ToString("MMMM");
            }
            else
            {
                date = new DateTime(year, 1, 1);
                dateToWrite = date.ToString("yyyy");
            }


            this.monthYearCurrentStatsSearch.Text = dateToWrite;
            this.monthYearCurrentBookSearch.Text = dateToWrite;
            this.monthYearCurrentServicesSearch.Text = dateToWrite;
        }

        //Calc footer's values
        private void CalculateFootersValues()
        {
            double valuesSum = 0;
            //Expenses
            var expensesCollection = this.dataGridExpensesSearch.ItemsSource;
            foreach (MovementItem item in expensesCollection)
            {
                valuesSum += item.Cost;
            }
            this.datagridFooterExpensesSearch.Columns[2].Header = valuesSum.ToString("C2");

            //Profit
            valuesSum = 0;
            var profitCollection = this.dataGridProfitsSearch.ItemsSource;
            foreach (MovementItem item in profitCollection)
            {
                valuesSum += item.Cost;
            }
            this.datagridFooterProfitsSearch.Columns[2].Header = valuesSum.ToString("C2");

            //Stats
            valuesSum = 0;
            var statsCollection = this.dataGridStatsSearch.ItemsSource;
            foreach (StatisticsItem item in statsCollection)
            {
                valuesSum += item.Total;
            }
            this.datagridFooterStatsSearch.Columns[1].Header = valuesSum.ToString("C2");

            //Book
            valuesSum = 0;
            double valuesSumStimated = 0;
            var bookCollection = this.dataGridBookSearch.ItemsSource;
            foreach (BookItem item in bookCollection)
            {
                valuesSumStimated += item.EstimatedPrice;
                valuesSum += item.Price;
            }
            this.datagridFooterBookSearch.Columns[2].Header = valuesSumStimated.ToString("C2");
            this.datagridFooterBookSearch.Columns[3].Header = valuesSum.ToString("C2");
        }
        private void UpdateFooterStats(bool updateExpense, bool updateProfit)
        {
            double offsetWidth = 0;
            if (updateExpense)
            {
                //Expenses
                offsetWidth = this.dataGridExpensesSearch.Columns[0].Width.DisplayValue;
                if (this.dataGridExpensesSearch.Columns[1].Visibility == Visibility.Visible)
                {
                    offsetWidth += this.dataGridExpensesSearch.Columns[1].Width.DisplayValue;
                }

                this.datagridFooterExpensesSearch.Columns[0].Width = offsetWidth;
                this.datagridFooterExpensesSearch.Columns[1].Width = this.dataGridExpensesSearch.Columns[2].Width;

            }
            if (updateProfit)
            {
                //Profits
                offsetWidth = 0;
                offsetWidth = this.dataGridProfitsSearch.Columns[0].Width.DisplayValue;

                if (this.dataGridProfitsSearch.Columns[1].Visibility == Visibility.Visible)
                {
                    offsetWidth += this.dataGridProfitsSearch.Columns[1].Width.DisplayValue;
                }

                this.datagridFooterProfitsSearch.Columns[0].Width = offsetWidth;
                this.datagridFooterProfitsSearch.Columns[1].Width = this.dataGridProfitsSearch.Columns[2].Width;
            }
        }
        private void UpdateFooter()
        {
            double offsetWidth = 0;
            UpdateFooterStats(true, true);

            //Book
            offsetWidth = 0;
            offsetWidth = this.dataGridBookSearch.Columns[0].Width.DisplayValue +
                          this.dataGridBookSearch.Columns[1].Width.DisplayValue +
                          this.dataGridBookSearch.Columns[2].Width.DisplayValue;

            this.datagridFooterBookSearch.Columns[0].Width = offsetWidth;
            this.datagridFooterBookSearch.Columns[1].Width = this.dataGridBookSearch.Columns[3].Width;
            this.datagridFooterBookSearch.Columns[2].Width = this.dataGridBookSearch.Columns[4].Width;
            this.datagridFooterBookSearch.Columns[3].Width = this.dataGridBookSearch.Columns[5].Width;

        }

        private void ShowDescriptionExpenses(object sender, RoutedEventArgs e)
        {
            if (this.dataGridExpensesSearch.Columns[1].Visibility == Visibility.Hidden)
            {
                this.dataGridExpensesSearch.Columns[1].Visibility = Visibility.Visible;
                this.expensesToggleDescriptionVisibility.Content = "Ocultar desripción";
                UpdateFooterStats(true, false);
            }
            else
            {
                this.dataGridExpensesSearch.Columns[1].Visibility = Visibility.Hidden;
                this.expensesToggleDescriptionVisibility.Content = "Mostrar desripción";
                UpdateFooterStats(true, false);
            }

        }

        private void ShowDescriptionProfits(object sender, RoutedEventArgs e)
        {
            if (this.dataGridProfitsSearch.Columns[1].Visibility == Visibility.Hidden)
            {
                this.dataGridProfitsSearch.Columns[1].Visibility = Visibility.Visible;
                this.profitToggleDescriptionVisibility.Content = "Ocultar desripción";
                UpdateFooterStats(false, true);
            }
            else
            {
                this.dataGridProfitsSearch.Columns[1].Visibility = Visibility.Hidden;
                this.profitToggleDescriptionVisibility.Content = "Mostrar desripción";
                UpdateFooterStats(false, true);
            }

        }
    }
}

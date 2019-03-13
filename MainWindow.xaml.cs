using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TilleWPF
{
    using MaterialDesignThemes.Wpf;
    using Model;
    using System.Collections;
    using Domain;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        MainController controller;

        public bool cancelDialog = false;

        public MainWindow()
        {
            InitializeComponent();
            controller = new Model.MainController();

            FillMonthHeader();
            FillDataGrid(false);
            FillDataProductComboBox(false);
        }

        //Refresh
        private async void Refresh(object sender, RoutedEventArgs e)
        {

            var dialogWaiting =  new SampleProgressDialog();
           
            var result = this.mainDialogHost.ShowDialog(dialogWaiting);
            await DoRefresh();
            
            this.mainDialogHost.IsOpen = false;
            
            
        }
        async Task DoRefresh()
        {
            FillMonthHeader();
            FillDataGrid(true);
            FillDataProductComboBox(false);
            await Task.Delay(1000);
        }

        /// <summary>
        /// Call and show the view to execute a search in the db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Search(object sender, RoutedEventArgs e)
        {
            var searchControls = new SearchDialog(controller);
            await mainDialogHost.ShowDialog(searchControls);
            if (!searchControls.IsCancelled)
            {
                var searchView = new SearchView(controller, searchControls.month, searchControls.year);
                await mainDialogHost.ShowDialog(searchView);
            }
        }

        /// <summary>
        /// Show the restaurant modal for include a new breakfast service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RestaurantModal(object sender, RoutedEventArgs e)
        {
            mainDialogHost.ShowDialog(BreakfastModal);
        }

        //Validate on writing
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void NumberValidationIntegerTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        //Fill Data methods
        private void FillDataGrid(bool refill)
        {
            if (refill)
            {
                //Wrehouse
                if (dataGridWarehouse.ItemsSource!= null)
                {
                    //  dataGridWarehouse.
                    dataGridWarehouse.ItemsSource = null;
                }

                //Book
                if (dataGridBook.ItemsSource != null)
                {
                    dataGridBook.ItemsSource = null;
                }

                //Expenses
                if (dataGridExpenses.ItemsSource != null)
                {
                    dataGridExpenses.ItemsSource = null;
                }

                //Profit
                if (dataGridProfits.ItemsSource != null)
                {
                    dataGridProfits.ItemsSource = null;
                }

                //Stats
                if (dataGridStats.ItemsSource != null)
                {
                    dataGridStats.ItemsSource = null;
                }

                //Services
                if (dataGridServices.ItemsSource != null)
                {
                    dataGridServices.ItemsSource = null;
                }
            }
            //Wrehouse
            this.dataGridWarehouse.ItemsSource = controller.FillDataWarehouse();

            //Book
            this.dataGridBook.ItemsSource = controller.FillBook();

            //Expenses
            this.dataGridExpenses.ItemsSource = controller.FillDataExpense();

            //Profit
            this.dataGridProfits.ItemsSource = controller.FillDataProfit();

            //Stats
            this.dataGridStats.ItemsSource = controller.FillStats();

            //Services
            this.dataGridServices.ItemsSource = controller.FillServices();
        }
        private void FillDataProductComboBox(bool refill)
        {
            if (refill)
            {
                this.ComboBoxProducts.Items.Clear();
                this.comboBoxSellProductName.Items.Clear();
            }
            ComboBoxItem comboBoxItem;
            foreach (var item in controller.AvailablesProduts())
            {
                comboBoxItem = new ComboBoxItem()
                {
                    Content=item.Name,
                    Tag = item.Id
                };
               
                this.comboBoxSellProductName.Items.Add(comboBoxItem);
            }
            foreach (var item in controller.Produts())
            {
                comboBoxItem = new ComboBoxItem()
                {
                    Content = item.Name,
                    Tag = item.Id
                };

                this.ComboBoxProducts.Items.Add(comboBoxItem);
            }


            this.ComboBoxProducts.SelectedIndex = 0;
            this.comboBoxSellProductName.SelectedIndex = 0;
        }
        private void RefreshTable(DataGrid grid, ICollection items)
        {
            grid.ItemsSource = items;
        }
        private void FillMonthHeader()
        {
            string current = DateTime.Now.ToString("MMMM/yyyy");
            this.monthYearCurrentStats.Text = current;
            this.monthYearCurrentWarehouse.Text = current;
            this.monthYearCurrentBook.Text = current;
            this.monthYearCurrentServices.Text = current;
        }

        //Update Footers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFooter();
            CalculateFootersValues();
        }

        private void UpdateFooterStats(bool updateExpense, bool updateProfit)
        {
            double offsetWidth = 0;
            if (updateExpense)
            {
                //Expenses
                offsetWidth = this.dataGridExpenses.Columns[0].Width.DisplayValue;
                if (this.dataGridExpenses.Columns[1].Visibility == Visibility.Visible)
                {
                    offsetWidth += this.dataGridExpenses.Columns[1].Width.DisplayValue;
                }

                this.datagridFooterExpenses.Columns[0].Width = offsetWidth;
                this.datagridFooterExpenses.Columns[1].Width = this.dataGridExpenses.Columns[2].Width;

            }
            if (updateProfit)
            {
                //Profits
                offsetWidth = 0;
                offsetWidth = this.dataGridProfits.Columns[0].Width.DisplayValue;

                if (this.dataGridProfits.Columns[1].Visibility == Visibility.Visible)
                {
                    offsetWidth += this.dataGridProfits.Columns[1].Width.DisplayValue;
                }

                this.datagridFooterProfits.Columns[0].Width = offsetWidth;
                this.datagridFooterProfits.Columns[1].Width = this.dataGridProfits.Columns[2].Width;
            }
        }

        private void UpdateFooter()
        {
            double offsetWidth = 0;
            UpdateFooterStats(true, true);
            //Warehouse
            offsetWidth = this.dataGridWarehouse.Columns[0].Width.DisplayValue + this.dataGridWarehouse.Columns[1].Width.DisplayValue;
            this.datagridFooter.Columns[0].Width = offsetWidth;
            this.datagridFooter.Columns[1].Width = this.dataGridWarehouse.Columns[2].Width;

            //Book
            offsetWidth = 0;
            offsetWidth = this.dataGridBook.Columns[0].Width.DisplayValue +
                          this.dataGridBook.Columns[1].Width.DisplayValue +
                          this.dataGridBook.Columns[2].Width.DisplayValue;

            this.datagridFooterBook.Columns[0].Width = offsetWidth;
            this.datagridFooterBook.Columns[1].Width = this.dataGridBook.Columns[3].Width;
            this.datagridFooterBook.Columns[2].Width = this.dataGridBook.Columns[4].Width;
            this.datagridFooterBook.Columns[3].Width = this.dataGridBook.Columns[5].Width;

        }
        //Calc footer's values
        private void CalculateFootersValues()
        {
            double valuesSum = 0;
            //Expenses
            var expensesCollection = this.dataGridExpenses.ItemsSource;
            foreach (MovementItem item in expensesCollection)
            {
                valuesSum += item.Cost;
            }
            this.datagridFooterExpenses.Columns[2].Header = valuesSum.ToString("C2");

            //Profit
            valuesSum = 0;
            var profitCollection = this.dataGridProfits.ItemsSource;
            foreach (MovementItem item in profitCollection)
            {
                valuesSum += item.Cost;
            }
            this.datagridFooterProfits.Columns[2].Header = valuesSum.ToString("C2");

            //Stats
            valuesSum = 0;
            var statsCollection = this.dataGridStats.ItemsSource;
            foreach (StatisticsItem item in statsCollection)
            {
                valuesSum += item.Total;
            }
            this.datagridFooterStats.Columns[1].Header = valuesSum.ToString("C2");

            //Warehouse
            valuesSum = 0;
            var warehouseCollection = this.dataGridWarehouse.ItemsSource;
            foreach (WarehouseItem item in warehouseCollection)
            {
                valuesSum += item.Cost;
            }
            this.datagridFooter.Columns[2].Header = valuesSum.ToString("C2");

            //Book
            valuesSum = 0;
            double valuesSumStimated = 0;
            var bookCollection = this.dataGridBook.ItemsSource;
            foreach (BookItem item in bookCollection)
            {
                valuesSumStimated += item.EstimatedPrice;
                valuesSum += item.Price;
            }
            this.datagridFooterBook.Columns[2].Header = valuesSumStimated.ToString("C2");
            this.datagridFooterBook.Columns[3].Header = valuesSum.ToString("C2");
        }

        //Link to diferents tables
        private void MenuItem_Click_Statistics(object sender, RoutedEventArgs e)
        {
            ToHideAll();
            SetButtonStyles(sender);
            this.statistics.Visibility = Visibility.Visible;
            //FillDataGridBook();
        }

        private void MenuItem_Warehouse(object sender, RoutedEventArgs e)
        {
            ToHideAll();
            SetButtonStyles(sender);
            this.warehouse.Visibility = Visibility.Visible;
            //FillDataGridAlmacen();
        }

        private void MenuItem_Click_Book(object sender, RoutedEventArgs e)
        {
            ToHideAll();
            SetButtonStyles(sender);
            this.book.Visibility = Visibility.Visible;
            //FillDataGridAlmacen();
        }

        private void MenuItem_Click_Services(object sender, RoutedEventArgs e)
        {
            ToHideAll();
            SetButtonStyles(sender);
            this.services.Visibility = Visibility.Visible;
            //FillDataGridAlmacen();
        }

        private void ToHideAll()
        {
            this.statistics.Visibility = Visibility.Hidden;
            this.warehouse.Visibility = Visibility.Hidden;
            this.book.Visibility = Visibility.Hidden;
            this.services.Visibility = Visibility.Hidden;
        }

        private void SetButtonStyles(object sender)
        {
            this.ButtonStats.Style= (Style)FindResource("MaterialDesignFlatButton");
            this.ButtonWarehouse.Style= (Style)FindResource("MaterialDesignFlatButton");
            this.ButtonBook.Style= (Style)FindResource("MaterialDesignFlatButton");
            this.ButtonServices.Style= (Style)FindResource("MaterialDesignFlatButton");
            //Change button type to indicate selected button
            ((Button)sender).Style = (Style)FindResource("MaterialDesignRaisedDarkButton");
        }

        //Another actions
        private void CheckBox_NewProduct_Checked(object sender, RoutedEventArgs e)
        {
            this.ComboBoxProducts.Visibility = Visibility.Hidden;
            this.newProduct.Visibility = Visibility.Visible;
        }

        private void CheckBox_NewProduct_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ComboBoxProducts.Visibility = Visibility.Visible;
            this.newProduct.Visibility = Visibility.Hidden;
        }
        
        private void ShowDescriptionExpenses(object sender, RoutedEventArgs e)
        {
            if (this.dataGridExpenses.Columns[1].Visibility == Visibility.Hidden)
            {
                this.dataGridExpenses.Columns[1].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar desripción";
                UpdateFooterStats(true, false);
            }
            else
            {
                this.dataGridExpenses.Columns[1].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar desripción";
                UpdateFooterStats(true, false);
            }
            
        }
        private void ShowDescriptionProfits(object sender, RoutedEventArgs e)
        {
            if (this.dataGridProfits.Columns[1].Visibility == Visibility.Hidden)
            {
                this.dataGridProfits.Columns[1].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar desripción";
                UpdateFooterStats(false, true);
            }
            else
            {
                this.dataGridProfits.Columns[1].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar desripción";
                UpdateFooterStats(false, true);
            }

        }

        private void ShowActionsExpenses(object sender, RoutedEventArgs e)
        {
            if (this.dataGridExpenses.Columns[4].Visibility == Visibility.Hidden)
            {
                this.dataGridExpenses.Columns[4].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar acciones";
                UpdateFooterStats(true, false);
            }
            else
            {
                this.dataGridExpenses.Columns[4].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar acciones";
                UpdateFooterStats(true, false);
            }
            
        }

        /// <summary>
        /// Show or hide the action edit and delete on the profits table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowActionsProfit(object sender, RoutedEventArgs e)
        {
            if (this.dataGridProfits.Columns[4].Visibility == Visibility.Hidden)
            {
                this.dataGridProfits.Columns[4].Visibility = Visibility.Visible;
               ((Button)sender).Content = "Ocultar acciones";
                UpdateFooterStats(true, false);
            }
            else
            {
                this.dataGridProfits.Columns[4].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar acciones";
                UpdateFooterStats(true, false);
            }
            
        }

        /// <summary>
        /// Show or hide the action edit and delete on the warehouse table warehouse
        /// </summary>
        /// <param name="sender"> sender is a button</param>
        /// <param name="e"></param>
        private void ShowActionsWareHouse(object sender, RoutedEventArgs e)
        {
            if (this.dataGridWarehouse.Columns[4].Visibility == Visibility.Hidden)
            {
                this.dataGridWarehouse.Columns[4].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar acciones";
                UpdateFooter();
            }
            else
            {
                this.dataGridWarehouse.Columns[4].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar acciones";
                UpdateFooter();
            }

        } 
        
        /// <summary>
        /// Show or hide the action edit and delete on the warehouse table Books
        /// </summary>
        /// <param name="sender"> sender is a button</param>
        /// <param name="e"></param>
        private void ShowActionsBook(object sender, RoutedEventArgs e)
        {
            if (this.dataGridBook.Columns[6].Visibility == Visibility.Hidden)
            {
                this.dataGridBook.Columns[6].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar acciones";
                UpdateFooter();
            }
            else
            {
                this.dataGridBook.Columns[6].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar acciones";
                UpdateFooter();
            }

        }

        /// <summary>
        /// Show or hide the action edit and delete on the warehouse table Books
        /// </summary>
        /// <param name="sender"> sender is a button</param>
        /// <param name="e"></param>
        private void ShowActionsServices(object sender, RoutedEventArgs e)
        {
            if (this.dataGridServices.Columns[4].Visibility == Visibility.Hidden)
            {
                this.dataGridServices.Columns[4].Visibility = Visibility.Visible;
                ((Button)sender).Content = "Ocultar acciones";
                UpdateFooter();
            }
            else
            {
                this.dataGridServices.Columns[4].Visibility = Visibility.Hidden;
                ((Button)sender).Content = "Mostrar acciones";
                UpdateFooter();
            }

        }


        //Binding textfields
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            BindingExpression be = textbox.GetBindingExpression(TextBox.TextProperty);
            be.ValidateWithoutUpdate();
        }
        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker textbox = (DatePicker)sender;
            BindingExpression be = textbox.GetBindingExpression(DatePicker.TextProperty);
            be.ValidateWithoutUpdate();
        }

        //For closing dialoghostevent
        private void CloseDialog(object sender, DialogClosingEventArgs eventArgs)
        {
            if (cancelDialog)
            {
                eventArgs.Cancel();
                cancelDialog = false;
            }
            else
            {
                var collection = ((StackPanel)((DialogHost)sender).DialogContent).Children;
                foreach (var item in collection)
                {
                    if (item.GetType().Name.Equals("Grid"))
                    {
                        CleanAllData((Grid)item);
                    }
                }
            }

            
        }

        public bool HasInputErrors(Grid container)
        {
            foreach (var itemGrid in container.Children)
            {
                if (itemGrid.GetType().Name.Equals("TextBox"))
                {
                    if (((TextBox)itemGrid).IsVisible)
                    {
                        BindingExpression be = ((TextBox)itemGrid).GetBindingExpression(TextBox.TextProperty);
                        if (be != null)
                        {
                            if (be.HasError)
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (itemGrid.GetType().Name.Equals("DatePicker"))
                {
                    if (((DatePicker)itemGrid).IsVisible)
                    {
                        BindingExpression be = ((DatePicker)itemGrid).GetBindingExpression(DatePicker.TextProperty);
                        if (be != null)
                        {
                            if (be.HasError)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
         
        public void UpdateValidation(Grid container)
        {
            foreach (var itemGrid in container.Children)
            {
                
                if (itemGrid.GetType().Name.Equals("TextBox"))
                {
                       
                    BindingExpression be = ((TextBox)itemGrid).GetBindingExpression(TextBox.TextProperty);
                    if (be != null)
                    {
                        be.ValidateWithoutUpdate();
                    }
                }
                else if (itemGrid.GetType().Name.Equals("DatePicker"))
                {
                    BindingExpression be = ((DatePicker)itemGrid).GetBindingExpression(DatePicker.TextProperty);
                    if (be != null)
                    {
                        be.ValidateWithoutUpdate();
                    }
                }
            }
        }
        
        public void CleanAllData(Grid container)
        {
           
            foreach (var itemGrid in container.Children)
            {
               
                if (itemGrid.GetType().Name.Equals("TextBox"))
                {

                    ((TextBox)itemGrid).Text="";
                  
                }
                else if (itemGrid.GetType().Name.Equals("DatePicker"))
                {
                    ((DatePicker)itemGrid).Text = "";
                }
            }
        }

        //Action Zone
        private void AddExpenseMovement(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime dateTime = (DateTime)this.expenseDate.SelectedDate;
            double amount = -double.Parse(this.expensePrice.Text);
            string description = this.expenseDescription.Text;

            controller.AddMovement(dateTime, description, amount, MovementType.Other);

            RefreshTable(this.dataGridExpenses,controller.FillDataExpense());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }
        private void AddProfitMovement(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime dateTime = (DateTime)this.profitDate.SelectedDate;
            double amount = double.Parse(this.profitPrice.Text);
            string description = this.profitDescription.Text;

            controller.AddMovement(dateTime, description, amount, MovementType.Other);

            RefreshTable(this.dataGridProfits, controller.FillDataProfit());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }
        private void AddProductMovement(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }
            string productName, description;
            int productId;
            int amount = int.Parse(this.productAmount.Text);
            //Negative beacuse is a expense
            double cost = -double.Parse(this.productCost.Text);

            DateTime dateTime = (DateTime)this.productDate.SelectedDate;

            if ((bool)this.checkBoxNewProduct.IsChecked)
            {
                productName = this.newProduct.Text;
                description = $"Compra: {productName}({amount})";
                controller.AddProduct(productName,amount,dateTime,description,cost,MovementType.Warehouse);
            }
            else
            {
                productId = (int)((ComboBoxItem)this.ComboBoxProducts.SelectedItem).Tag;
                string name = (string)((ComboBoxItem)this.ComboBoxProducts.SelectedItem).Content;
                description = $"Compra:{name}({amount})";
                controller.UpdateProduct(productId, amount, dateTime, description, cost, MovementType.Warehouse);
            }

            RefreshTable(this.dataGridExpenses, controller.FillDataExpense());
            RefreshTable(this.dataGridWarehouse, controller.FillDataWarehouse());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
            FillDataProductComboBox(true);
        }
        private void SellProductMovement(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }
            string productName = (string)((ComboBoxItem)this.comboBoxSellProductName.SelectedItem).Content;
            //Negative because is a sell action and lost products 
            int amount = -(int)sliderAmountSell.Value;
            string description = $"Venta: {productName}({-amount})";

            //Positive because is a profit
            double cost = double.Parse(this.sellPrice.Text);
            DateTime dateTime = (DateTime)this.sellDate.SelectedDate;
            int productId = (int)((ComboBoxItem)this.comboBoxSellProductName.SelectedItem).Tag;
                
            controller.UpdateProduct(productId, amount, dateTime, description, cost, MovementType.Warehouse);
            
            RefreshTable(this.dataGridProfits, controller.FillDataProfit());
            RefreshTable(this.dataGridWarehouse, controller.FillDataWarehouse());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
            FillDataProductComboBox(true);
        }
        private void AddServiceBreakfast(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime date = (DateTime)this.dateBreakfast.SelectedDate;
            string description = this.breakfastDescription.Text;
            double price = double.Parse(this.breakfastPrice.Text);
            controller.AddService(date,description,price,ServiceType.Breakfast);

            RefreshTable(this.dataGridServices, controller.FillServices());
            RefreshTable(this.dataGridProfits, controller.FillDataProfit());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }
        private void AddServiceTaxi(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime date = (DateTime)this.taxiDate.SelectedDate;
            string description = this.taxiDescription.Text;
            double price = double.Parse(this.taxiPrice.Text);
            controller.AddService(date, description, price, ServiceType.Taxi);

            RefreshTable(this.dataGridServices, controller.FillServices());
            RefreshTable(this.dataGridProfits, controller.FillDataProfit());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }
        private void AddServiceClean(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime date = (DateTime)this.cleanDate.SelectedDate;
            string description = this.cleanDescription.Text;

            //Negative because is expense
            double price = -double.Parse(this.cleanPrice.Text);
            controller.AddService(date, description, price, ServiceType.Clean);

            RefreshTable(this.dataGridServices, controller.FillServices());
            RefreshTable(this.dataGridExpenses, controller.FillDataExpense());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }
        private void AddBookIn(object sender, RoutedEventArgs e)
        {
            StackPanel stack = (StackPanel)((StackPanel)((Button)sender).Parent).Parent;
            foreach (var item in stack.Children)
            {
                if (item.GetType().Name.Equals("Grid"))
                {
                    UpdateValidation((Grid)item);

                    if (HasInputErrors((Grid)item))
                    {
                        cancelDialog = true;
                        return;
                    }
                }
            }

            DateTime dateIn = (DateTime)this.bookDateIn.SelectedDate;
            DateTime dateOut = (DateTime)this.bookDateOut.SelectedDate;
            int clients = int.Parse(this.bookClients.Text);
            string country = this.bookCountry.Text;
            double estimatedPrice = double.Parse(this.bookEstimatedPrice.Text);
            double price = double.Parse(this.bookRealPrice.Text);

            controller.AddBookIn(clients, dateIn, dateOut, country, estimatedPrice, price);

            RefreshTable(this.dataGridBook, controller.FillBook());
            RefreshTable(this.dataGridProfits, controller.FillDataProfit());
            RefreshTable(this.dataGridStats, controller.FillStats());
            CalculateFootersValues();
        }

        //Slider changevalue event
        private void SliderAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var collection = ((Grid)((Slider)sender).Parent).Children;
            foreach (var item in collection)
            {
                if (item.GetType().Name.Equals("TextBox"))
                {
                    ((TextBox)item).Text = ((Slider)sender).Value.ToString();
                }
            }
        }
        private void TextBoxSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox= (TextBox)sender;
            Slider slider = new Slider();

            string text = textBox.Text;
            var collection = ((Grid)textBox.Parent).Children;

            if (!text.Equals(""))
            {
                foreach (var item in collection)
                {
                    if (item.GetType().Name.Equals("Slider"))
                    {
                       slider=(Slider)item;
                    }
                }

                if (!int.TryParse(text, out int value))
                {
                   textBox.Text = slider.Value.ToString();
                }
                else
                {
                    if (value > slider.Maximum || value < slider.Minimum)
                    {
                        textBox.Text = slider.Value.ToString();
                    }
                    else
                    {
                        slider.Value = value;
                    }
                  
                }
            }
            
        }

        //ComboBox change maximum and minimum selected item warehouse
        private void ComboBoxProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                Grid gridContainer = (Grid)comboBox.Parent;

                var childrenCollection = gridContainer.Children;

                foreach (var child in childrenCollection)
                {
                    if (child.GetType().Name.Equals("StackPanel"))
                    {
                        foreach (var grid in ((StackPanel)child).Children)
                        {
                            if (grid.GetType().Name.Equals("Grid"))
                            {
                                var collection = ((Grid)grid).Children;
                                foreach (var item in collection)
                                {
                                    if (item.GetType().Name.Equals("Slider"))
                                    {
                                        int id = (int)((ComboBoxItem)comboBox.SelectedItem).Tag;
                                        ((Slider)item).Maximum = controller.AmountAvailble(id);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}

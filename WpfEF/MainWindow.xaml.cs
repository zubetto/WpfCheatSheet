using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

namespace DataPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<WpfEF.Customer> DataSource { get; set; } = new ObservableCollection<WpfEF.Customer>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private int startInd = 0;
        private int rowsNum = 20;
        private int totalRows = int.MaxValue;

        public int StartIndex { get { return startInd; } set {startInd = value; OnPropertyChanged("StartIndex"); } }
        public int RowsNumber { get { return rowsNum; } set { rowsNum = value; OnPropertyChanged("RowsNumber"); } }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadCustomersData(int startInd, int number, Collection<WpfEF.Customer> customers, ref int totalRows)
        {
            using (var contoso = new WpfEF.ContosoData())
            {
                var data = contoso.Customers.OrderBy(c => c.CustomerID).Skip(StartIndex).Take(RowsNumber);

                foreach (var row in data)
                    customers.Add(row);

                totalRows = contoso.Customers.Count();
            }
        }

        // --- Load Command --------------------------------------------------------------------------------------
        public static RoutedCommand LoadCmd = new RoutedCommand();
        
        private void LoadCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DataSource.Clear();
            LoadCustomersData(StartIndex, RowsNumber, DataSource, ref totalRows);
        }

        private void LoadCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(Validation.GetHasError(txtStartIndex) || Validation.GetHasError(txtRowsNumber));
        }

        // --- Load Previous Command -------------------------------------------------------------------------------
        public static RoutedCommand LoadPrevCmd = new RoutedCommand();

        private void LoadPrevCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            StartIndex -= RowsNumber;

            DataSource.Clear();
            LoadCustomersData(StartIndex, RowsNumber, DataSource, ref totalRows);
        }

        private void LoadPrevCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(Validation.GetHasError(txtStartIndex) || Validation.GetHasError(txtRowsNumber)) && 
                            StartIndex - RowsNumber >= 0;
        }

        // --- Load Next Command -----------------------------------------------------------------------------------
        public static RoutedCommand LoadNextCmd = new RoutedCommand();

        private void LoadNextCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            StartIndex += RowsNumber;

            DataSource.Clear();
            LoadCustomersData(StartIndex, RowsNumber, DataSource, ref totalRows);
        }

        private void LoadNextCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(Validation.GetHasError(txtStartIndex) || Validation.GetHasError(txtRowsNumber)) &&
                            StartIndex + RowsNumber < totalRows;
        }
    }
}

using System.Windows;
using CustomersApp.View;
using CustomersApp.ViewModel;

namespace CustomersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            CustomerListViewModel viewModel = (CustomerListViewModel)MainGrid.DataContext;
            AddPersonWindow window = new AddPersonWindow(viewModel);
            window.Show();
        }
    }
}
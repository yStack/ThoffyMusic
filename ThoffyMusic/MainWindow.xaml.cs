using System.Windows;
using ThoffyMusic.ViewModel;


namespace ThoffyMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserViewModel _userViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
            DataContext = _userViewModel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _userViewModel.User.Password = PasswordBox.Password;
            _userViewModel.LoginCommand.Execute(null);
        }
    }
}

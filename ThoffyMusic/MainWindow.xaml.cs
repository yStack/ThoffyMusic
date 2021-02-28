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



    }
}

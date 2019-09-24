using RPSLS.Classes;

namespace RPSLS.Views
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            GameState.MainWindow = this;
        }

        #endregion Window-Manipulation Methods
    }
}
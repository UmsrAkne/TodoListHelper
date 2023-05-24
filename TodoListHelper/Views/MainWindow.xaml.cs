using System.Windows;

namespace TodoListHelper.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            NameScope.SetNameScope(ContextMenu, NameScope.GetNameScope(this));
        }
    }
}
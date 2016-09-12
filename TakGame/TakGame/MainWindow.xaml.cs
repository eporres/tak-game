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

namespace TakGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardView mBoardView;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (mBoardView == null) return;
            mBoardView.Render(BoardCanvas.ActualHeight, BoardCanvas.ActualWidth);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var board = new Board(6);
            mBoardView = new BoardView(board, BoardCanvas);
            mBoardView.Render(BoardCanvas.ActualHeight, BoardCanvas.ActualWidth);
        }

    }
}

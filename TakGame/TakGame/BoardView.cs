using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TakGame
{
    class BoardView
    {
        Board mBoard;
        List<Rectangle> mCells;
        List<TextBlock> mLetterMarkers;
        List<TextBlock> mNumberMarkers;

        public BoardView(Board board, Canvas canvas)
        {
            mBoard = board;
            SetupCells(canvas);
            SetupMarkers(canvas);
        }

        private void SetupCells(Canvas canvas)
        {
            mCells = new List<Rectangle>();
            for (int i = 0; i < mBoard.Dimension; i++)
            {
                bool isRowEven = i % 2 == 0;
                var primaryBrush = isRowEven ? Brushes.Black : Brushes.White;
                var seconaryBrush = isRowEven ? Brushes.White : Brushes.Black;

                for (int j = 0; j < mBoard.Dimension; j++)
                {
                    var isColEven = j % 2 == 0;
                    var cellColor = isColEven ? primaryBrush : seconaryBrush;

                    var cell = new Rectangle();
                    cell.Fill = cellColor;
                    mCells.Add(cell);

                    canvas.Children.Add(cell);
                }
            }
        }

        private void SetupMarkers(Canvas canvas)
        {
            mLetterMarkers = new List<TextBlock>();
            for (int i = 0; i < mBoard.Dimension; i++)
            {
                var tb = new TextBlock();
                tb.Height = Double.NaN;
                tb.Width = 25;

                tb.Foreground = Brushes.White;
                //tb.Background = Brushes.DodgerBlue;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontWeight = FontWeights.Bold;
                char letter = (char)(64 + i + 1);
                tb.Text = letter.ToString();
                mLetterMarkers.Add(tb);
                canvas.Children.Add(tb);
            }

            mNumberMarkers = new List<TextBlock>();

            for (int i = 0; i < mBoard.Dimension; i++)
            {
                var tb = new TextBlock();
                tb.Height = double.NaN;
                tb.Width = 25;

                tb.Foreground = Brushes.White;
                //tb.Background = Brushes.DodgerBlue;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontWeight = FontWeights.Bold;

                tb.Text = String.Format("{0}", i + 1);
                //tb.InvalidateMeasure();
                //tb.InvalidateArrange();
                //tb.InvalidateVisual();

                mNumberMarkers.Add(tb);
                canvas.Children.Add(tb);
            }
        }

        public void Render(double height, double width)
        {
            double minBoardSize = Math.Min(height, width);

            var cellHeight = minBoardSize / mBoard.Dimension;
            var cellWidth = minBoardSize / mBoard.Dimension;

            for (int i = 0; i < mBoard.Dimension; i++)
            {
                var rowTopLeftPixelY = i * cellHeight;

                for (int j = 0; j < mBoard.Dimension; j++)
                {
                    var colTopLeftPixelX = j * cellWidth;
                    UpdateCell(cellHeight, cellWidth, i, rowTopLeftPixelY, j, colTopLeftPixelX);

                    if (i == mBoard.Dimension - 1)
                    {
                        UpdateNumberMarker(cellHeight, cellWidth, j,
                            rowTopLeftPixelY, colTopLeftPixelX);
                    }
                }

                UpdateLetterMarker(cellHeight, cellWidth, i, rowTopLeftPixelY, 0);
                
            }
        }

        private void UpdateCell(double cellHeight, double cellWidth,
            int i, double rowTopLeftPixelY, int j, double colTopLeftPixelX)
        {
            var cell = mCells[i * mBoard.Dimension + j];
            cell.Width = cellWidth;
            cell.Height = cellHeight;

            Canvas.SetTop(cell, rowTopLeftPixelY);
            Canvas.SetLeft(cell, colTopLeftPixelX);
        }

        private void UpdateLetterMarker(double cellHeight, double cellWidth,
            int i, double rowTopLeftPixelY, double colTopLeftPixelX)
        {
            var marker = mLetterMarkers[i];
            var x_pos = colTopLeftPixelX - marker.ActualWidth - 5;
            var y_pos = rowTopLeftPixelY + cellHeight / 2.0 - marker.ActualHeight / 2.0;

            Canvas.SetLeft(marker, x_pos);
            Canvas.SetTop(marker, y_pos);
        }

        private void UpdateNumberMarker(double cellHeight, double cellWidth,
            int i, double rowTopLeftPixelY, double colTopLeftPixelX)
        {
            var marker = mNumberMarkers[i];
            var x_pos = colTopLeftPixelX + cellWidth / 2.0 - marker.ActualWidth / 2.0;
            var y_pos = rowTopLeftPixelY + cellHeight + 5;

            Canvas.SetLeft(marker, x_pos);
            Canvas.SetTop(marker, y_pos);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace PilotBrothersSafe
{
    internal class Game
    {
        private Button[][] _desk;

        public Button[][] Desk { get => _desk; }

        public Game(int deskSize, double deskSizeInWindow,RoutedEventHandler deskButtonClick)
        {
            _desk = new Button[deskSize][];
            double size = deskSizeInWindow / deskSize;

            for (int i = 0; i < deskSize; i++)
            {
                _desk[i] = new Button[deskSize];

                for (int j = 0; j < deskSize; j++)
                {
                    var button = new Button();
                    button.Width    = size;
                    button.Height   = size;
                    button.FontSize = size;
                    button.Content = "-";
                    button.Click += deskButtonClick;
                    _desk[i][j] = button;
                }
            }
        }

        public void Step(int x, int y)
        {
            for (int id = 0; id < _desk.Length; id++)
            {
                _desk[id][y].Content = _desk[id][y].Content.ToString() == "-" ? "|" : "-";

                if (id != y)
                    _desk[x][id].Content = _desk[x][id].Content.ToString() == "-" ? "|" : "-";
            }
        }

        public void Refresh(int steps)
        {
            int x;
            int y;

            Random rand = new Random();

            do
            {
                for (int i = 0; i < steps; i++)
                {
                    x = rand.Next(_desk.Length);
                    y = rand.Next(_desk.Length);

                    Step(x, y);
                }
            }
            while (Win());
        }

        public bool Win()
        {
            string sumbol = (string)_desk[0][0].Content;

            for (int i = 0; i < _desk.Length; i++)
                foreach (var el in _desk[i])
                    if ((string)el.Content != sumbol)
                    {
                        return false;
                    }

            return true;
        }
    }
}

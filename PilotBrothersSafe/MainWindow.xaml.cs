using System;
using System.Windows;
using System.Windows.Controls;

namespace PilotBrothersSafe
{
    public partial class MainWindow : Window
    {
        private Game _game;

        public MainWindow()
        {
            InitializeComponent();
            InitDesk(5);
        }
        
        private int GetDeskSize()
        {
            int count = -1;
            try
            {
                count = int.Parse(textBox.Text);
            }
            catch
            {
                MessageBox.Show("Размер поля должен быть числом!");
            }

            return count;
        }

        private void СreateButton_Click(object sender, RoutedEventArgs e)
        {
            int size = GetDeskSize();

            if (size >= 2 && size <= 20)
                InitDesk(size);
            else
                MessageBox.Show("Размер поля должен быть в пределах [ 2; 20 ]");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _game.Refresh(100);
        }

        private void InitDesk(int size)
        {
            if (wrapPanel.Children.Count > 0)
                wrapPanel.Children.Clear();

            _game = new Game(size, wrapPanel.Width, FieldButton_Click);
            
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    wrapPanel.Children.Add(_game.Desk[i][j]);

            _game.Refresh(100);

            wrapPanel.UpdateLayout();
        }

        public void FieldButton_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < _game.Desk.Length; i++)
                for (int j = 0; j < _game.Desk.Length; j++)
                    if ((Button)sender == _game.Desk[i][j])
                    {
                        x = i;
                        y = j;
                    }

            _game.Step(x, y);

            wrapPanel.UpdateLayout();

            if (_game.Win())
            {
                MessageBox.Show("Вы выиграли!!!");
            }
        }

    }
}

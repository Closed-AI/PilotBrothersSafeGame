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

namespace PilotBrothersSafe
{
    public partial class MainWindow : Window
    {
        private Button[][] _buttons;

        public MainWindow()
        {
            InitializeComponent();
            InitDesk(5);
        }

        private Button[] CreateButtons(int quantity)
        {
            double size = 600d / quantity;
            Button[] buttons = new Button[quantity];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Width = size;
                buttons[i].Height = size;
                buttons[i].Content = "-";
                buttons[i].FontSize = size;
                buttons[i].Click += FieldButton_Click;
            }
            return buttons;
        }

        private void AddToWrapPanel(Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
                wrapPanel.Children.Add(buttons[i]);
        }

        private int GetQuantityButtons()
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
            int size = GetQuantityButtons();

            if (size >= 2 && size <= 20)
                InitDesk(size);
            else
                MessageBox.Show("Размер поля должен быть в пределах [ 2; 20 ]");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh(100);
        }

        private void InitDesk(int size)
        {
            if (wrapPanel.Children.Count > 0)
                wrapPanel.Children.Clear();


            _buttons = new Button[size][];

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i] = CreateButtons(size);
                AddToWrapPanel(_buttons[i]);
            }

            Refresh(100);

            wrapPanel.UpdateLayout();
        }

        private void Refresh(int steps)
        {
            int x;
            int y;

            Random rand = new Random();

            do
            {
                for (int i = 0; i < steps; i++)
                {
                    x = rand.Next(_buttons.Length);
                    y = rand.Next(_buttons.Length);

                    Step(x, y);
                }
            }
            while (Win());
        }

        private void FieldButton_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < _buttons.Length; i++)
                for (int j = 0; j < _buttons.Length; j++)
                    if ((Button)sender == _buttons[i][j])
                    {
                        x = i;
                        y = j;
                    }

            Step(x, y);

            wrapPanel.UpdateLayout();

            if (Win())
            {
                MessageBox.Show("Вы выиграли!!!");
            }
        }

        private void Step(int x, int y)
        {
            for (int id = 0; id < _buttons.Length; id++)
            {
                _buttons[id][y].Content = _buttons[id][y].Content.ToString() == "-" ? "|" : "-";

                if (id != y)
                    _buttons[x][id].Content = _buttons[x][id].Content.ToString() == "-" ? "|" : "-";
            }
        }

        private bool Win()
        {
            string sumbol = (string)_buttons[0][0].Content;

            for (int i = 0; i < _buttons.Length; i++)
                foreach (var el in _buttons[i])
                    if ((string)el.Content != sumbol)
                    {
                        return false;
                    }

            return true;
        }
    }
}

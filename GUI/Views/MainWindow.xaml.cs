using GUI.Models;
using Physics;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Context.Load();

            Load();
        }

        private void Load()
        {
            List<UniverseViewModel> universesModels = new List<UniverseViewModel>();
            foreach (Universe universe in Context.Universes)
                universesModels.Add(new UniverseViewModel(universe));

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = universesModels;

            dataGrid.IsReadOnly = true;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Universe universe = new Universe();

            UniverseWindow window = new UniverseWindow(universe);
            window.ShowDialog();

            Context.Universes.Add(universe);

            Context.Save();
            Load();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);

                UniverseWindow window = new UniverseWindow(universe);
                window.ShowDialog();

                Context.Save();
                Load();
            }
            else
            {
                MessageBox.Show("Выберите вселенную", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);
                Context.Universes.Remove(universe);

                Context.Save();
                Load();
            }
            else
            {
                MessageBox.Show("Выберите вселенную", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);

                DrawingWindow window = new DrawingWindow(universe);
                window.ShowDialog();
            }
            else
            {
                //MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                Random rnd = new Random();

                Func<string> color = () =>
                {
                    string str = "#";
                    string abc = "0123456789abcdef";
                    for (int i = 0; i < 6; i++)
                        str += abc[rnd.Next(abc.Length)];
                    return str;
                };

                Universe universe = new Universe { G = rnd.NextDouble() * 20 };
                for (int i = 0; i < rnd.Next(5, 10); i++)
                {
                    uint size = (uint)rnd.Next(1, 500);
                    universe.Bodies.Add(new Body
                    {
                        X = rnd.NextDouble() * 4000 - 2000,
                        Y = rnd.NextDouble() * 4000 - 2000,
                        D = size,
                        Mass = size,
                        Velocity = new Physics.Vector { Vx = rnd.NextDouble() * 50 - 25, Vy = rnd.NextDouble() * 50 - 25 },
                        ColorHex = color()
                    });
                }

                DrawingWindow window = new DrawingWindow(universe);
                window.ShowDialog();
            }
        }
    }
}

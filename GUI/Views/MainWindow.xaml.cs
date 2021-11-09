using GUI.Models;
using Physics;
using System.Collections.Generic;
using System.Windows;

namespace GUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            List<UniverseViewModel> universes = new List<UniverseViewModel>();
            universes.Add(new UniverseViewModel(new Universe(1, "Вселенная 1")));
            universes.Add(new UniverseViewModel(new Universe(0.001, "Вселенная 2")));
            universes.Add(new UniverseViewModel(new Universe(2.5, "Вселенная 3")));

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = universes;

            dataGrid.IsReadOnly = true;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

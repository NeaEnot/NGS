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
            try
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);

                UniverseWindow window = new UniverseWindow(universe);
                window.ShowDialog();

                Context.Save();
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);
                Context.Universes.Remove(universe);

                Context.Save();
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Universe universe = Context.Universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);

                DrawingWindow window = new DrawingWindow(universe);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

﻿using GUI.Models;
using Physics;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GUI.Views
{
    public partial class MainWindow : Window
    {
        private List<Universe> universes;

        public MainWindow()
        {
            InitializeComponent();

            universes = new List<Universe>();

            Load();
        }

        private void Load()
        {
            List<UniverseViewModel> universesModels = new List<UniverseViewModel>();
            foreach (Universe universe in universes)
                universesModels.Add(new UniverseViewModel(universe));

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = universesModels;

            dataGrid.IsReadOnly = true;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Universe universe = new Universe("Новая Вселенная");

            UniverseWindow window = new UniverseWindow(universe);
            window.ShowDialog();

            universes.Add(universe);

            Load();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Universe universe = universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);

                UniverseWindow window = new UniverseWindow(universe);
                window.ShowDialog();

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
                Universe universe = universes.Find(req => req.Id == (dataGrid.SelectedItem as UniverseViewModel).Id);
                universes.Remove(universe);

                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
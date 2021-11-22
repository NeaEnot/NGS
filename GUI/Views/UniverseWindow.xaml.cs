using Physics;
using System;
using System.Windows;

namespace GUI.Views
{
    public partial class UniverseWindow : Window
    {
        private Universe universe;

        public UniverseWindow(Universe universe)
        {
            InitializeComponent();

            this.universe = universe;

            Load();
        }

        private void Load()
        {
            tbG.Text = universe.G.ToString();
            tbName.Text = universe.Name;

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = universe.Bodies;

            dataGrid.IsReadOnly = true;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Body body = new Body();
            BodyWindow window = new BodyWindow(body);

            if (window.ShowDialog() == true)
            {
                universe.Bodies.Add(body);
                Load();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Body body = dataGrid.SelectedItem as Body;
                BodyWindow window = new BodyWindow(body);

                if (window.ShowDialog() == true)
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
                Body body = dataGrid.SelectedItem as Body;
                universe.Bodies.Remove(body);
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbG_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                double g = double.Parse(tbG.Text);
                universe.G = g;
            }
            catch
            { }
        }

        private void tbName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            universe.Name = tbName.Text;
        }
    }
}

using GUI.Models;
using Ookii.Dialogs.Wpf;
using Physics;
using System;
using System.Windows;

namespace GUI.Views
{
    public partial class MainWindow : Window
    {
        private string universePath;
        private Context context;
        private Universe universe;

        public MainWindow(string universePath)
        {
            InitializeComponent();

            this.universePath = universePath;
            if (universePath != "")
                OpenUniverse();

            Load();
        }

        private void OpenUniverse()
        {
            context = new Context(universePath);
            universe = context.LoadUniverse();
        }

        private void Load()
        {
            if (universe != null)
            {
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = universe.Bodies;

                dataGrid.IsReadOnly = true;

                tbG.Text = universe.G.ToString();

                Title = "Newton Galaxy Simulation - " + universe.Name;

                miSave.IsEnabled = true;
                miUniverse.IsEnabled = true;
                miHistory.IsEnabled = true;
                miRender.IsEnabled = true;
                tbG.IsEnabled = true;
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                dataGrid.ItemsSource = null;
                tbG.Text = "";

                Title = "Newton Galaxy Simulation";

                miSave.IsEnabled = false;
                miUniverse.IsEnabled = false;
                miHistory.IsEnabled = false;
                miRender.IsEnabled = false;
                tbG.IsEnabled = false;
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        private void miNew_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow window = new CreateWindow();
            if (window.ShowDialog() == true)
            {
                universe = window.Universe;
                context = null;
            }
        }

        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

            if (dlg.ShowDialog() == true)
            {
                universePath = dlg.SelectedPath;
                OpenUniverse();
                Load();
            }
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            if (context == null)
            {
                VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

                if (dlg.ShowDialog() == true)
                {
                    universePath = dlg.SelectedPath;
                    context = new Context(universePath);
                    context.SaveUniverse(universe);
                }
            }
            else
            {
                context.SaveUniverse(universe);
            }
        }

        private void miHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miRender_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Body body = new Body();
            BodyWindow window = new BodyWindow(body);

            if (window.ShowDialog() == true)
            {
                universe.Bodies.Add(body);
                Load();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
    }
}
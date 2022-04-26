using Drawing.Centers;
using Physics;
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
using System.Windows.Shapes;

namespace GUI.Views
{
    public partial class CenterWindow : Window
    {
        private Universe universe;

        internal ICenter Center { get; private set; }

        public CenterWindow(Universe universe)
        {
            InitializeComponent();

            this.universe = universe;

            cbBodies.ItemsSource = universe.Bodies;
            lbBodies.ItemsSource = universe.Bodies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (tabs.SelectedIndex)
                {
                    case 0:
                        double x = double.Parse(tbX.Text);
                        double y = double.Parse(tbY.Text);

                        Center = new CoordCenter(x, y);

                        DialogResult = true;

                        break;
                    case 1:
                        Body body = cbBodies.SelectedItem as Body;

                        Center = new BodyCenter(body);

                        DialogResult = true;

                        break;
                    case 2:
                        var bodies = lbBodies.SelectedItems;

                        Center = new MassCenter(bodies.Cast<Body>().ToList());

                        DialogResult = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

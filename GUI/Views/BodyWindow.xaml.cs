using Physics;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Vector = Physics.Vector;

namespace GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для BodyWindow.xaml
    /// </summary>
    public partial class BodyWindow : Window
    {
        private Body body;

        public BodyWindow(Body body)
        {
            InitializeComponent();

            this.body = body;

            Load();
        }

        private void Load()
        {
            tbX.Text = body.X.ToString();
            tbY.Text = body.Y.ToString();
            tbD.Text = body.D.ToString();
            tbMass.Text = body.Mass.ToString();
            tbVx.Text = body.Velocity.Vx.ToString();
            tbVy.Text = body.Velocity.Vy.ToString();
            tbColor.Text = body.ColorHex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(tbX.Text);
                double y = double.Parse(tbY.Text);
                uint d = uint.Parse(tbD.Text);
                uint mass = uint.Parse(tbMass.Text);
                int vx = int.Parse(tbVx.Text);
                int vy = int.Parse(tbVy.Text);
                string color = tbColor.Text;

                Regex reg = new Regex("^(#[0-9a-f]{6})$");
                if (!reg.IsMatch(color))
                    throw new Exception("Цвет задан некорректно.");

                body.X = x;
                body.Y = y;
                body.D = d;
                body.Mass = mass;
                body.Velocity = new Vector { Vx = vx, Vy = vy };
                body.ColorHex = color;

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

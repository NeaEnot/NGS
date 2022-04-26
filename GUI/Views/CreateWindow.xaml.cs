using Physics;
using System;
using System.Windows;
using Vector = Physics.Vector;

namespace GUI.Views
{
    public partial class CreateWindow : Window
    {
        private static Random rnd = new Random();
        private static string abc = "0123456789abcdef";

        public Universe Universe { get; private set; }

        public CreateWindow()
        {
            InitializeComponent();
        }

        private void chbEmpty_Checked(object sender, RoutedEventArgs e)
        {
            if (chbEmpty.IsChecked.Value)
            {
                tbCount.IsEnabled = false;
                tbCoordsRangeMin.IsEnabled = false;
                tbCoordsRangeMax.IsEnabled = false;
                tbDMin.IsEnabled = false;
                tbDMax.IsEnabled = false;
                tbMassMin.IsEnabled = false;
                tbDependence.IsEnabled = false;
                tbBrightnessMin.IsEnabled = false;
                tbBrightnessMax.IsEnabled = false;
                tbVMin.IsEnabled = false;
                tbVMax.IsEnabled = false;

                chbDependence.IsEnabled = false;
            }
            else
            {
                tbCount.IsEnabled = true;
                tbCoordsRangeMin.IsEnabled = true;
                tbCoordsRangeMax.IsEnabled = true;
                tbDMin.IsEnabled = true;
                tbDMax.IsEnabled = true;
                tbMassMin.IsEnabled = true;
                tbDependence.IsEnabled = chbDependence.IsChecked.Value;
                tbBrightnessMin.IsEnabled = true;
                tbBrightnessMax.IsEnabled = true;
                tbVMin.IsEnabled = true;
                tbVMax.IsEnabled = true;

                chbDependence.IsEnabled = true;
            }
        }

        private void chbDependence_Checked(object sender, RoutedEventArgs e)
        {
            tbDependence.IsEnabled = chbDependence.IsChecked.Value;
        }

        private void chbG_Checked(object sender, RoutedEventArgs e)
        {
            tbG.IsEnabled = !chbG.IsChecked.Value;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text))
                    throw new Exception("Вселенная не имеет названия.");

                double g = !chbG.IsChecked.Value ? double.Parse(tbG.Text) : rnd.NextDouble() * 100;

                Universe = new Universe { Name = tbName.Text, G = g };
                IdHelper idHelper = new IdHelper(Universe);

                if (!chbEmpty.IsChecked.Value)
                {
                    uint count = uint.Parse(tbCount.Text);

                    double coordsRangeMin = double.Parse(tbCoordsRangeMin.Text);
                    double coordsRangeMax = double.Parse(tbCoordsRangeMax.Text);
                    uint dMin = uint.Parse(tbDMin.Text);
                    uint dMax = uint.Parse(tbDMax.Text);
                    uint massMin = uint.Parse(tbMassMin.Text);
                    double dependence = chbDependence.IsChecked.Value ? double.Parse(tbDependence.Text) : 0;
                    ushort brightnessMin = ushort.Parse(tbBrightnessMin.Text);
                    ushort brightnessMax = ushort.Parse(tbBrightnessMax.Text);
                    double vMin = double.Parse(tbVMin.Text);
                    double vMax = double.Parse(tbVMax.Text);

                    if (coordsRangeMin > coordsRangeMax || dMin > dMax || brightnessMin > brightnessMax || vMin > vMax)
                        throw new Exception("Минимальное значение не может быть больше максимального.");

                    for (uint i = 0; i < count; i++)
                    {
                        double x = coordsRangeMin + rnd.NextDouble() * (coordsRangeMax - coordsRangeMin);
                        double y = coordsRangeMin + rnd.NextDouble() * (coordsRangeMax - coordsRangeMin);
                        double vx = vMin + rnd.NextDouble() * (vMax - vMin);
                        double vy = vMin + rnd.NextDouble() * (vMax - vMin);
                        uint d = GenerateUint(dMin, dMax);
                        uint mass =
                            dependence != 0 ?
                                dependence > 0 ?
                                (uint)(d * dependence)
                                :
                                (uint)(uint.MaxValue - d * dependence) > massMin ? (uint)(uint.MaxValue - d * dependence) : massMin
                            :
                                GenerateUint(uint.MinValue, uint.MaxValue);
                        string color = GenerateColorHex(brightnessMin, brightnessMax);

                        Body body = new Body
                        {
                            Id = idHelper.GetId(),
                            X = x,
                            Y = y,
                            Velocity = new Vector { Vx = vx, Vy = vy },
                            D = d,
                            Mass = mass,
                            ColorHex = color
                        };

                        Universe.Bodies.Add(body);
                    }
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private uint GenerateUint(uint min, uint max)
        {
            while (true)
            {
                uint ui = (uint)new Random().Next(-int.MaxValue, int.MaxValue) % (max - min) + min;
                if (ui >= min && ui < max)
                    return ui;
            }
        }

        private string GenerateColorHex(ushort brightnessMin, ushort brightnessMax)
        {
            while (true)
            {
                string str = "#";
                for (int i = 0; i < 6; i++)
                    str += abc[rnd.Next(abc.Length)];

                ushort brightness = BrightnessCalc.Calc(str);
                if (brightness >= brightnessMin && brightness <= brightnessMax)
                    return str;
            }
        }
    }
}

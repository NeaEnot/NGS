using Drawing;
using Drawing.Centers;
using Physics;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для DrawingWindow.xaml
    /// </summary>
    public partial class DrawingWindow : Window
    {
        private Universe universeOriginal;
        private Universe universe;
        private DrawingLogic logic;

        private CancellationTokenSource cts;

        public DrawingWindow(Universe universe)
        {
            InitializeComponent();

            universeOriginal = universe;

            CopyUniverse();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (cts != null)
                cts.Cancel();
        }

        private void CopyUniverse()
        {
            universe =
                new Universe()
                {
                    Name = universeOriginal.Name,
                    G = universeOriginal.G
                };

            foreach(Body bodyO in universeOriginal.Bodies)
            {
                Body body = new Body
                {
                    X = bodyO.X,
                    Y = bodyO.Y,
                    D = bodyO.D,
                    Mass = bodyO.Mass,
                    Velocity = bodyO.Velocity,
                    ColorHex = bodyO.ColorHex
                };

                universe.Bodies.Add(body);
            }

            logic = new DrawingLogic(universe);

            cbObject.ItemsSource = universe.Bodies;

            Bitmap bmp = logic.GetCurrentFrame(Width - 200, Height, new CoordCenter(0, 0));
            img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnPause.IsEnabled = true;
            btnStop.IsEnabled = true;

            Movie();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();

            btnStart.IsEnabled = true;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();

            btnStart.IsEnabled = true;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;

            CopyUniverse();
        }

        private async void Movie()
        {
            int speed = (int)sliderSpeed.Value;
            int maxSpeed = (int)sliderSpeed.Maximum;
            double distance = Math.Pow(sliderScale.Value / 100, Math.E);

            ICenter center = new CoordCenter(0, 0);

            switch (tabs.SelectedIndex)
            {
                case 0:
                    try
                    {
                        double x = double.Parse(tbX.Text);
                        double y = double.Parse(tbY.Text);
                        center = new CoordCenter(x, y);
                    }
                    catch
                    { }
                    break;
                case 1:
                    try
                    {
                        Body b = cbObject.SelectedItem as Body;
                        center = new BodyCenter(b);
                    }
                    catch
                    { }
                    break;
            }

            cts = new CancellationTokenSource();
            await Task.Run(() =>
            {
                while (true)
                {
                    universe.Update();

                    Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                    {
                        Bitmap bmp = logic.GetCurrentFrame(Width - 200, Height, center, distance);
                        img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }));

                    Thread.Sleep(maxSpeed - speed + 1);

                    if (cts.Token.IsCancellationRequested)
                        return;
                }
            });
        }

        private void btnNextFrame_Click(object sender, RoutedEventArgs e)
        {
            int speed = (int)sliderSpeed.Value;
            int maxSpeed = (int)sliderSpeed.Maximum;
            double distance = Math.Pow(sliderScale.Value / 100, Math.E);

            ICenter center = new CoordCenter(0, 0);

            switch (tabs.SelectedIndex)
            {
                case 0:
                    try
                    {
                        double x = double.Parse(tbX.Text);
                        double y = double.Parse(tbY.Text);
                        center = new CoordCenter(x, y);
                    }
                    catch
                    { }
                    break;
                case 1:
                    try
                    {
                        Body b = cbObject.SelectedItem as Body;
                        center = new BodyCenter(b);
                    }
                    catch
                    { }
                    break;
            }

            universe.Update();

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                Bitmap bmp = logic.GetCurrentFrame(Width - 200, Height, center, distance);
                img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }));
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            CopyUniverse();
        }
    }
}

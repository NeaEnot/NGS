using Drawing;
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

            Bitmap bmp = logic.GetCurrentFrame(Width - 200, Height);
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
            int scale = (int)sliderScale.Value;

            // Считываем центрирование

            cts = new CancellationTokenSource();
            await Task.Run(() =>
            {
                while (true)
                {
                    universe.Update();

                    Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                    {
                        Bitmap bmp = logic.GetCurrentFrame(Width - 200, Height);
                        img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }));

                    Thread.Sleep(maxSpeed - speed);

                    if (cts.Token.IsCancellationRequested)
                        return;
                }
            });
        }
    }
}

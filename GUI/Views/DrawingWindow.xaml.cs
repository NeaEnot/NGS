using Drawing;
using Drawing.Centers;
using GUI.Models.Savers;
using Ookii.Dialogs.Wpf;
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
        private ISaver saver;

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

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                Bitmap bmp = logic.GetCurrentFrame(new DrawingParams { W = Width - 200, H = Height, Center = new CoordCenter(0, 0) });
                img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnPause.IsEnabled = true;
            btnStop.IsEnabled = true;

            cbSaveType.IsEnabled = false;
            tbPathToSave.IsEnabled = false;
            btnSelectPath.IsEnabled = false;

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

            cbSaveType.IsEnabled = true;
            tbPathToSave.IsEnabled = true;
            btnSelectPath.IsEnabled = true;

            CopyUniverse();
        }

        private async void Movie()
        {
            int speed = (int)sliderSpeed.Value;
            int maxSpeed = (int)sliderSpeed.Maximum;
            double distance = Math.Pow(sliderScale.Value / 100, Math.E);

            ICenter center = GetCenter();
            SetSaver();

            cts = new CancellationTokenSource();
            await Task.Run(() =>
            {
                while (true)
                {
                    universe.Update();

                    Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    {
                        Bitmap bmp = logic.GetCurrentFrame(new DrawingParams { W = Width - 200, H = Height, Center = center, Distance = distance });
                        img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        SaveFrame(bmp);
                    }));

                    Thread.Sleep(maxSpeed - speed + 1);

                    if (cts.Token.IsCancellationRequested)
                    {
                        if (saver != null)
                            saver.Save();
                        return;
                    }
                }
            });
        }

        private void btnNextFrame_Click(object sender, RoutedEventArgs e)
        {
            int speed = (int)sliderSpeed.Value;
            int maxSpeed = (int)sliderSpeed.Maximum;
            double distance = Math.Pow(sliderScale.Value / 100, Math.E);

            ICenter center = GetCenter();

            universe.Update();

            Bitmap bmp = logic.GetCurrentFrame(new DrawingParams { W = Width - 200, H = Height, Center = center, Distance = distance });
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                img.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            ));
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            CopyUniverse();
        }

        private void btnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

            if (dlg.ShowDialog() == true)
            {
                string folder = dlg.SelectedPath;
                tbPathToSave.Text = folder;
            }
        }

        private ICenter GetCenter()
        {
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

            return center;
        }

        private void SetSaver()
        {
            if (!string.IsNullOrWhiteSpace(tbPathToSave.Text))
            {
                string path = tbPathToSave.Text;
                try
                {
                    switch (cbSaveType.SelectedItem)
                    {
                        case "jpegs":
                            saver = new JpegsSaver(path);
                            break;
                        case "gif":
                            saver = new GifSaver(path);
                            break;
                    }
                }
                catch
                {
                    saver = null;
                }
            }
        }

        private void SaveFrame(Bitmap bmp)
        {
            if (saver != null)
                saver.AddFrame(bmp);
        }
    }
}

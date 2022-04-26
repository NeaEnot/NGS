using Drawing;
using Drawing.Centers;
using Drawing.Sortings;
using GUI.Models;
using Physics;
using Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public partial class RenderWindow : Window
    {
        private Context context;
        private Universe universe;

        private ICenter center;
        private Date maxDate;

        private List<ISorting> sortings = new List<ISorting>
        {
            new BrightnessSorting(Order.Ascending),
            new BrightnessSorting(Order.Descending),
            new DiametrSorting(Order.Ascending),
            new DiametrSorting(Order.Descending),
            new MassSorting(Order.Ascending),
            new MassSorting(Order.Descending)
        };

        internal RenderWindow(Context context, Universe universe)
        {
            InitializeComponent();

            this.universe = universe;
            this.context = context;

            HistorySummary history = context.LoadHistorySummary();
            maxDate = Date.Parse(history.Date);

            tbDateEnd.ToolTip = maxDate;

            cbSorting.ItemsSource = sortings;
            cbFormat.ItemsSource = RendersFactory.GetRendersNames();
        }

        private void btnChoseCenter_Click(object sender, RoutedEventArgs e)
        {
            CenterWindow window = new CenterWindow(universe);
            if (window.ShowDialog() == true)
            {
                center = window.Center;

                if (center is CoordCenter)
                    lblCenter.Content = "Координаты";
                else if (center is BodyCenter)
                    lblCenter.Content = "Тело";
                else if (center is MassCenter)
                    lblCenter.Content = "Центр масс";
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int width = int.Parse(tbWidth.Text);
                int height = int.Parse(tbHeight.Text);
                double distance = double.Parse(tbDistance.Text);
                Date start = Date.Parse(tbDateStart.Text);
                Date end = Date.Parse(tbDateEnd.Text);
                int delay = int.Parse(tbFrameDelay.Text);
                ISorting sorting = cbSorting.SelectedItem as ISorting;
                string format = cbFormat.SelectedItem as string;

                if (end > maxDate)
                    throw new Exception("Выход за пределы сгенерированной истории");

                DrawingLogic drawingLogic = new DrawingLogic(universe);
                IRender render = RendersFactory.GetRender(format);
                RenderConfig config = new RenderConfig
                {
                    Path = CreateDir(format).FullName,
                    Delay = delay,
                    StartDate = start
                };
                render.Configure(config);

                for (Date current = start; current < end; current = current.NextDay())
                {
                    UniverseState state = context.LoadUniverseState(new HistorySummary { Date = current.ToString() });
                    center.BodyStates = state.BodyStates;

                    DrawingParams p = new DrawingParams
                    {
                        W = width,
                        H = height,
                        UniverseState = state,
                        Center = center,
                        Distance = distance,
                        Sorting = sorting
                    };

                    Bitmap bmp = drawingLogic.GetCurrentFrame(p);
                    render.AddFrame(bmp);
                }

                render.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DirectoryInfo CreateDir(string format)
        {
            DirectoryInfo formatDir = new DirectoryInfo(@$"{context.Path}\Renders\{format}");
            if (!formatDir.Exists)
                formatDir.Create();

            DirectoryInfo dir = new DirectoryInfo(@$"{context.Path}\Renders\{format}\{DateTime.Now.ToString("yyyy.MM.HH.mm.ss.ff")}");
            if (!dir.Exists)
                dir.Create();

            return dir;
        }
    }
}

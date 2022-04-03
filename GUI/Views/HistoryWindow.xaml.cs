using GUI.Models;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI.Views
{
    public partial class HistoryWindow : Window
    {
        private Context context;
        private Universe universe;

        private Date currentDate = new Date();

        private CancellationTokenSource cts;

        internal HistoryWindow(Context context, Universe universe)
        {
            InitializeComponent();

            this.context = context;
            CopyUniverse(universe);
        }

        private void CopyUniverse(Universe universe)
        {
            this.universe =
                new Universe()
                {
                    Name = universe.Name,
                    G = universe.G
                };

            foreach (Body bodyO in universe.Bodies)
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

                this.universe.Bodies.Add(body);
            }
        }

        private void LoadData()
        {
            HistorySummary sumary = context.LoadHistorySummary();
            currentDate = Date.Parse(sumary.Date);
            lblDate.Content = currentDate;

            if (currentDate != new Date(0, 0, 0, 0))
            {
                UniverseState currentState = context.LoadUniverseState(sumary);
                universe.ToState(currentState);
            }
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Date purpose = GetPurpose();

            cts = new CancellationTokenSource();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            await Task.Run(() =>
            {
                while (currentDate < purpose)
                {
                    currentDate = currentDate.NextDay();
                    universe.Update();

                    UniverseState state = new UniverseState(universe);
                    HistorySummary sumary = new HistorySummary { Date = currentDate.ToString() };

                    context.SaveUniverseState(state, currentDate);
                    context.SaveHistorySummary(sumary);

                    Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    {
                        lblDate.Content = currentDate;
                    }));

                    if (cts.Token.IsCancellationRequested)
                        break;
                }
            });
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        private Date GetPurpose()
        {
            Date purpose = new Date(0, 0, 0, 0);

            try
            {
                if (tbDistance.Text.Contains('.'))
                {
                    Date add = Date.Parse(tbDistance.Text);
                    purpose = currentDate + add;
                }
                else
                {
                    ulong days = ulong.Parse(tbDistance.Text);
                    purpose = currentDate;

                    for (ulong i = 0; i < days; i++)
                        purpose = purpose.NextDay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return purpose;
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Timelines
{
    /// <summary>
    /// Logique d'interaction pour Timeline.xaml
    /// </summary>
    public partial class TimelineControl : UserControl
    {
        public Timeline Timeline { get; set; }

        public TimelineControl()
        {
            InitializeComponent();
        }

        public void Update(Timeline tl)
        {
            Timeline = tl;

            DateTime now = DateTime.Now;

            lblTimelineName.Content = tl.Name;

            foreach (TimelineEvent evt in tl.Events)
            {
                if (evt.Date > now && evt.Date <= now.AddMonths(1))
                {
                    double x = bar.Margin.Left + ((evt.Date - now).Days * bar.Width / 31);
                    Color col;
                    switch (evt.TypeOfEvent)
                    {
                        case TimelineEvent.EventType.Task:
                            col = Colors.Red;
                            break;

                        case TimelineEvent.EventType.Appointment:
                            col = Colors.Blue;
                            break;

                        case TimelineEvent.EventType.Milestone:
                            col = Colors.Green;
                            break;

                        default:
                            col = Colors.Black;
                            break;
                    }

                    Rectangle rect = new Rectangle();
                    rect.Margin = new Thickness(x, 95, 0, 95);
                    rect.Fill = new SolidColorBrush() { Color = col };
                    rect.Width = 5;
                    rect.Height = 40;
                    rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                    Label lbl = new Label();
                    lbl.Content = evt.ShortName;

                    lbl.Margin = new Thickness(x, 135, 0, 0);
                    lbl.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    lbl.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                    mainGrid.Children.Add(lbl);
                    mainGrid.Children.Add(rect);
                }
            }

        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            if (Timeline != null)
            {
                AddEventWindow wd = new AddEventWindow(Timeline);
                wd.ShowDialog();

            }
        }
    }
}

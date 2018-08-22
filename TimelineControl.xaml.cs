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
        public event EventHandler TimeLineEventsChanged;

        private List<Grid> EventMarks;

        protected virtual void OnTimeLineEventsChanged(EventArgs e)
        {
            EventHandler handler = TimeLineEventsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public Timeline Timeline { get; set; }

        public bool Visible { get; set; }

        public TimelineControl(Timeline tl)
        {
            InitializeComponent();
            Timeline = tl;
            EventMarks = new List<Grid>();
            Visible = true;
        }

        public void Update()
        {
            foreach (var ctl in EventMarks)
            {
                mainGrid.Children.Remove(ctl);
            }
            

            DateTime now = DateTime.Now;

            lblTimelineName.Content = Timeline.Name;

            foreach (TimelineEvent evt in Timeline.Events)
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

                    TextBlock lbl1 = new TextBlock();
                    lbl1.FontSize = 12;
                    lbl1.TextTrimming = TextTrimming.CharacterEllipsis;
                    lbl1.Text = string.Concat(evt.Date.ToString("MMM").Substring(0, 3), " ", evt.Date.ToString("dd"));
                    lbl1.Width = 40;
                    //lbl.Margin = new Thickness(x, 135, 0, 0);
                    lbl1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    lbl1.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                    Rectangle rect = new Rectangle();
                    //rect.Margin = new Thickness(x, 95, 0, 95);
                    rect.Fill = new SolidColorBrush() { Color = col };
                    rect.Width = 5;
                    rect.Height = 40;
                    rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    rect.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                 
                    TextBlock lbl2 = new TextBlock();
                    lbl2.FontSize = 10;
                    lbl2.TextTrimming = TextTrimming.CharacterEllipsis;
                    lbl2.Text = evt.ShortName;
                    lbl2.Width = 40;
                    //lbl.Margin = new Thickness(x, 135, 0, 0);
                    lbl2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    lbl2.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                    Grid grd = new Grid();
                    grd.RowDefinitions.Add(new RowDefinition());
                    grd.RowDefinitions.Add(new RowDefinition());
                    grd.RowDefinitions.Add(new RowDefinition());
                    grd.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    grd.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    grd.Margin = new Thickness(x - lbl2.Width / 2, 60, 0, 0);

                    grd.Width = 40;
                    grd.Height = 80;
                    //grd.Background = new SolidColorBrush() { Color = Colors.Green };
                    grd.Children.Add(lbl1);
                    grd.Children.Add(rect);
                    grd.Children.Add(lbl2);
                    Grid.SetRow(lbl1, 0);
                    Grid.SetRow(rect, 1);
                    Grid.SetRow(lbl2, 2);

                    //todo move in event class
                    string evttooltip = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine,
                                                                evt.ShortName,
                                                                evt.Date.ToShortDateString(),
                                                                evt.Description);

                    ToolTip tooltip = new ToolTip { Content = evttooltip };
                    grd.ToolTip = tooltip;

                    mainGrid.Children.Add(grd);
                    EventMarks.Add(grd);
                }
            }

        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            if (Timeline != null)
            {
                AddEventWindow wd = new AddEventWindow(Timeline);
                wd.ShowDialog();

                OnTimeLineEventsChanged(new EventArgs());
            }
        }

        private void btnVisible_Click(object sender, RoutedEventArgs e)
        {
            Visible = !Visible;
            Opacity = Visible ? 1 : .5;

            OnTimeLineEventsChanged(new EventArgs());
        }
    }
}

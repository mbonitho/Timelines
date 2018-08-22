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
using System.IO;

namespace Timelines
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Timeline> Timelines { get; set; }
        public List<TimelineControl> TimelineControls { get; set; }
        TimelineControl MasterTLC { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Timeline masterTL = new Timeline() { Name = "Master"};

            Timelines = LoadTimelines();

            MasterTLC = new TimelineControl(masterTL);
            MasterTLC.btnVisible.Visibility = System.Windows.Visibility.Hidden;
            MasterTLC.btnEvents.Visibility = System.Windows.Visibility.Hidden;
            //MasterTLC.btnDelete.Visibility = System.Windows.Visibility.Hidden;
            MasterTLC.Update();
            timelinesPanel.Children.Add(MasterTLC);

            bool even = true;
            TimelineControls = new List<TimelineControl>();
            foreach (Timeline tl in Timelines)
            {
                TimelineControl tlc = new TimelineControl(tl);
                tlc.Update();
                timelinesPanel.Children.Add(tlc);
                tlc.TimeLineEventsChanged += tlc_TimeLineEventsChanged;
                TimelineControls.Add(tlc);

                tlc.Background = even ? new SolidColorBrush() { Color = Colors.Gray } : new SolidColorBrush() { Color = Colors.LightGray };

                even = !even;
            }

            UpdateMasterTimeLine();
        }

        private void tlc_TimeLineEventsChanged(object sender, EventArgs e)
        {
            UpdateMasterTimeLine();
        }


        public List<Timeline> LoadTimelines()
        {
            List<Timeline> result = new List<Timeline>();

            //TODO
            //if (File.Exists())
            //{
 
            //}

            //Hack
            Timeline devTL = new Timeline() { Name = "Dev" };
            TimelineEvent evt1 = new TimelineEvent("Tests unitaires", DateTime.Today.AddDays(1), TimelineEvent.EventType.Task, "Réaliser les tests");
            TimelineEvent evt2 = new TimelineEvent("Documentation", DateTime.Today.AddDays(5), TimelineEvent.EventType.Task, "Rédiger la documentation");
            TimelineEvent evt3 = new TimelineEvent("Livraison", DateTime.Today.AddDays(30), TimelineEvent.EventType.Milestone, "Livrer");
            
            devTL.Events.Add(evt1);
            devTL.Events.Add(evt2);
            devTL.Events.Add(evt3);

            result.Add(devTL);

            return result;
        }

        public void UpdateMasterTimeLine()
        {
            DateTime now = DateTime.Now;
            MasterTLC.Timeline.Events.Clear();

            foreach (TimelineControl tlc in TimelineControls.Where((x) => x.Visible))
            {
                var events = tlc.Timeline.Events.Where((evt) => evt.Date > now && evt.Date <= now.AddMonths(1));

                MasterTLC.Timeline.Events.AddRange(events);
            }

            MasterTLC.Update();
        }
    }
}

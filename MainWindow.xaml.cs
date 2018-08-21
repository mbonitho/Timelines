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

        public MainWindow()
        {
            InitializeComponent();

            Timeline masterTL = new Timeline() { Name = "Master"};

            Timelines = LoadTimelines();

            TimelineControl masterTLC = new TimelineControl();
            masterTLC.Update(masterTL);
            timelinesPanel.Children.Add(masterTLC);

            foreach (Timeline tl in Timelines)
            {
                TimelineControl tlc = new TimelineControl();
                tlc.Update(tl);
                timelinesPanel.Children.Add(tlc);
            }
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
    }
}

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
using System.Windows.Shapes;

namespace Timelines
{
    /// <summary>
    /// Logique d'interaction pour AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public Timeline Timeline { get; set; }

        public AddEventWindow(Timeline tl)
        {
            InitializeComponent();

            this.Title = tl.Name;

            foreach (TimelineEvent evt in tl.Events)
            {
                lvEvents.Items.Add(evt);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvEvents.SelectedItems.Count >= 1)
            {
                //TimelineEvent evt = Timeline.Events.FirstOrDefault((x) => x.ShortName == lvEvents.SelectedItems[0].Shortname;


            }
        }
    }
}

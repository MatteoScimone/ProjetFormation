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

namespace ProjetFormationWPF
{
    /// <summary>
    /// Logique d'interaction pour FrmPresence.xaml
    /// </summary>
    public partial class FrmPresence : Window
    {
        sncfEntities gst1;
        public FrmPresence()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst1 = new sncfEntities();
            listFormation.ItemsSource = gst1.formation.ToList();
        }

        private void listFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            listAgent.ItemsSource = gst1.agent.ToList();
        }

        private void listAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cboAgent_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnPresent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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

namespace ProjetFormationWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sncfEntities gst;
        sncfEntities gst1;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnPresence_Click(object sender, RoutedEventArgs e)
        {
            FrmPresence frm1 = new FrmPresence();
            frm1.Show();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            GestionFraisKilometre frm = new GestionFraisKilometre();
            frm.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new sncfEntities();
            gst1 = new sncfEntities();
           
            
        }
    }
}

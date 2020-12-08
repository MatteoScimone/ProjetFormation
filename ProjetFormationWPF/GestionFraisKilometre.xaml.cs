using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Logique d'interaction pour GestionFraisKilometre.xaml
    /// </summary>
    public partial class GestionFraisKilometre : Window
    {
        sncfEntities gst;
        double montant;
        int idFormation;
        int idAgent;
        public GestionFraisKilometre()
        {
            InitializeComponent();
        }

        private void lstFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tousLesAgents = gst.agent.ToList();
            var lesInscriptionsDeLaFormation = gst.inscription.ToList().FindAll(i => i.numFormation == (lstFormation.SelectedItem as formation).idFormation);

            lstAgent.ItemsSource = from lAgent in tousLesAgents
                            // Any : test si ça contient les éléments;
                        where !lesInscriptionsDeLaFormation.Any(ins => ins.numAgent == lAgent.idAgent)
                        select lAgent;
                       
            
           

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new sncfEntities();
            lstFormation.ItemsSource = gst.formation.ToList();
        }

        private async void  lstAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HttpClient ws = new HttpClient();
            string reponse = await ws.GetStringAsync("https://fr.distance24.org/route.json?stops=" + (lstFormation.SelectedItem as formation).lieuFormation + "|" + (lstAgent.SelectedItem as agent).villeAgent);
            var json = JsonConvert.DeserializeObject<dynamic>(reponse);
            txtKilometre.Text = json.distance;
            double d = Convert.ToInt16(json.distance);
            montant =((lstFormation.SelectedItem as formation).dureeFormation * (lstFormation.SelectedItem as formation).prixFormation) + (d * 1.36);
            txtTotal.Text =  montant.ToString();
        }

        private void btnInscrire_Click(object sender, RoutedEventArgs e)
        {
            if (lstFormation.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner formation ", "Votre choix", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if(lstAgent.SelectedItem==null)
            {
                MessageBox.Show("Veuillez sélectionner agent ", "Votre choix", MessageBoxButton.OK, MessageBoxImage.Hand);

            }
            else
            {
                //MessageBox.Show("L'inscription de votre agent a bien été effectuée ", "Votre choix", MessageBoxButton.OK, MessageBoxImage.Information);
                //inscription i = new inscription()
                //{

                //}
                inscription insc = new inscription()
                {
                    numFormation = idFormation,
                    numAgent = idAgent,
                    presence = 0,
                    nbKm = Convert.ToInt16(txtKilometre.Text)
                };
                gst.inscription.Add(insc);
                gst.SaveChanges();
                MessageBox.Show("L'inscription de votre agent \na bien été effectuée", "Insertion", MessageBoxButton.OK, MessageBoxImage.Information);
                //lstAgent.ItemsSource = (from agt in gst.agent
                //                         where !gst.inscription.Any(inscript => inscript.numAgent == agt.idAgent && inscript.numFormation == idFormation)
                //                         select agt).ToList<agent>();
                var tousLesAgentss = gst.agent.ToList();
                var lesInscriptionsDeLaFormations = gst.inscription.ToList().FindAll(i => i.numFormation == (lstFormation.SelectedItem as formation).idFormation);

                lstAgent.ItemsSource = from lAgents in tousLesAgentss
                                           // Any : test si ça contient les éléments;
                                       where !lesInscriptionsDeLaFormations.Any(ins => ins.numAgent == lAgents.idAgent)
                                       select lAgents;

            }
        }
    }
}

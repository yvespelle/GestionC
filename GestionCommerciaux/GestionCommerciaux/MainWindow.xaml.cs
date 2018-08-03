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

namespace GestionCommerciaux
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Ajouter(champNomAjouter.Text, champPrenomAjouter.Text, champDeptAjouter.Text, champMailAjouter.Text, champTelephoneAjouter.Text, champClientsAjouter.Text, champCollaborateurs1.Text, champBureauAjouter.Text);
            AjouterPhoto(champPhotoAjouter.Text);
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            Supprimer(champNomSupprimer.Text, champPrenomSupprimer.Text);
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            AfficherDetail();
        }
    }
}

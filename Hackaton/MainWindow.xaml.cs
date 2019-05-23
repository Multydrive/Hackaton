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
using System.Collections.ObjectModel;

namespace Hackaton
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = listing;
        }

        //Listes des Combos Box
        private List<string> _region = new List<string>() { "Bandle", "Bilgewater", "Demacia", "Freljord", "Ionia", "Mont Targon", "Noxus", "Néant", "Piltover", "Shurima", "Zaun", "Iles Obscures", "Runeterra" };
        private List<string> _classe = new List<string>() { "Tank", "Combattant", "Tueur", "Mage", "Controleur", "Tireur" };
        private List<string> _tank = new List<string>() { "Initiateur", "Gardien" };
        private List<string> _combattant = new List<string>() { "Colosse", "Blitzer" };
        private List<string> _tueur = new List<string>() { "Assassin", "Escarmoucheur" };
        private List<string> _mage = new List<string>() { "à Burst", "de Combat", "Artilleur" };
        private List<string> _controleur = new List<string>() { "Enchanteur", "Piégeur" };
        private List<string> _tireur = new List<string>() { "Tireur" };

        public List<string> Region { get => _region; }
        public List<string> Classe { get => _classe; }
        public List<string> Tank { get => _tank; }
        public List<string> Combattant { get => _combattant; }
        public List<string> Tueur { get => _tueur; }
        public List<string> Mage { get => _mage; }
        public List<string> Controleur { get => _controleur; }
        public List<string> Tireur { get => _tireur; }

        ObservableCollection<Champion> listing = new ObservableCollection<Champion>();
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        string nomdefichier = "/database.txt";

        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            FormulaireAjoutEdition fenetre2 = new FormulaireAjoutEdition();
            if (fenetre2.ShowDialog() == true) //on confirme l'opération
            {
                listing.Add(fenetre2.RecupDonneesSaisies());
                fenetre2.Close();
            }
            else //on annule l'opération
            {
                MessageBox.Show("Opération annulée");
            }
            fenetre2.Close();
        }
        private void Btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (Dtg_datagrid01.SelectedItems.Count == 1)
            {
                Champion tmp = Dtg_datagrid01.Items[Dtg_datagrid01.SelectedIndex] as Champion;
                FormulaireAjoutEdition fenetre2 = new FormulaireAjoutEdition(tmp);

                if (fenetre2.ShowDialog() == true) //on confirme l'opération de modification
                {
                    listing.Remove(tmp);
                    listing.Add(fenetre2.RecupDonneesSaisies());
                }
                else //on annule l'opération de modification
                {
                    MessageBox.Show("Opération annulée");
                }
                fenetre2.Close();
            }
        }
        private void Btn_Suppprimer_Click(object sender, RoutedEventArgs e)
        {
            if (Dtg_datagrid01.SelectedItems.Count == 1)
            {
                Champion tmp = Dtg_datagrid01.Items[Dtg_datagrid01.SelectedIndex] as Champion;
                listing.Remove(tmp);
            }
            else
            {
                MessageBox.Show("Selection vide ou trop nombreuse (max 1 à la fois)");
            }
        }


        private void Btn_Lecture_Click(object sender, RoutedEventArgs e)
        {
            listing.Clear();
            StreamReader lecteur = new StreamReader(path + nomdefichier);
            while (!lecteur.EndOfStream)
            {
                string tmp = lecteur.ReadLine();
                string[] donnees_eclatees = tmp.Split('#');
                listing.Add(new Champion(donnees_eclatees[0], donnees_eclatees[1], donnees_eclatees[2], donnees_eclatees[3]));
            }
            lecteur.Close();
        }
            else
            {
                MessageBox.Show("Fichier non trouvé");
            }
}
        private void Btn_Sauvergarder_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter ecriveur = new StreamWriter(path + nomdefichier, false);
            foreach (Champion xxx in listing)
            {
                ecriveur.WriteLine(xxx.ToString());
            }
            ecriveur.Close();
        }
    }
}

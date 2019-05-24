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
using Microsoft.Win32;
using System.Collections.ObjectModel; // Collection observable 
using System.IO; // Accès fichiers lecture-ecriture
using System.Text.RegularExpressions;

namespace Hackaton
{

    public partial class FormulaireAjoutEdition : Window
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        string chemin;
        int pas_bon;
        string res;

        public FormulaireAjoutEdition()
        {
            InitializeComponent();
            this.Title = "Ajout champion";
            ComboBox_Classe.ItemsSource = Classe;
            ComboBox_Region.ItemsSource = Region;
            ComboBox_Sous_Classe.IsEnabled = false;
        }

        public FormulaireAjoutEdition(Champion ttt)
        {
            InitializeComponent();
            this.Title = "Modification champion";
            ComboBox_Classe.ItemsSource = Classe;
            ComboBox_Region.ItemsSource = Region;

            if (!File.Exists(ttt.Image)) ttt.Image = null;
            if (ttt.Image != null) Img_Champion.Source = new BitmapImage(new Uri(ttt.Image));

            chemin = ttt.Image;
            Txtbox_Nom.Text = ttt.Nom;
            ComboBox_Classe.SelectedIndex = _classe.IndexOf(ttt.Classe);
            ComboBox_Region.SelectedIndex = _region.IndexOf(ttt.Region);
            Choix_SousClasse(ComboBox_Classe.SelectedIndex, ttt);
            Date_Apparition.SelectedDate = ttt.Apparition;

        }

        public Champion RecupDonneesSaisies()
        {
            return new Champion(chemin, Txtbox_Nom.Text, ComboBox_Region.Text, ComboBox_Classe.Text, ComboBox_Sous_Classe.Text, Date_Apparition.SelectedDate.Value);
        }

        //Listes des Combos Box
        private List<string> _region = new List<string>() { "Bandle", "Bilgewater", "Demacia", "Freljord", "Ionia", "Mont Targon", "Noxus", "Neant", "Piltover", "Shurima", "Zaun", "Iles Obscures", "Runeterra" };
        private List<string> _classe = new List<string>() { "Tank", "Combattant", "Tueur", "Mage", "Controleur", "Tireur" };
        private List<string> _tank = new List<string>() { "Initiateur", "Gardien" };
        private List<string> _combattant = new List<string>() { "Colosse", "Blitzer" };
        private List<string> _tueur = new List<string>() { "Assassin", "Escarmoucheur" };
        private List<string> _mage = new List<string>() { "a Burst", "de Combat", "Artilleur" };
        private List<string> _controleur = new List<string>() { "Enchanteur", "Piegeur" };
        private List<string> _tireur = new List<string>() { "Tireur" };

        public List<string> Region { get => _region; }
        public List<string> Classe { get => _classe; }
        public List<string> Tank { get => _tank; }
        public List<string> Combattant { get => _combattant; }
        public List<string> Tueur { get => _tueur; }
        public List<string> Mage { get => _mage; }
        public List<string> Controleur { get => _controleur; }
        public List<string> Tireur { get => _tireur; }



        public void Btn_Open_Click(object sender, RoutedEventArgs e)
        {

            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Title = "Choisir une image";
            op.Filter = "Fichiers images|*.png";

            if (op.ShowDialog() == true)
            {
                Img_Champion.Source = new BitmapImage(new Uri(op.FileName));

            }
            chemin = op.FileName;


        }


        private void ComboBox_Classe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Choix_SousClasse(ComboBox_Classe.SelectedIndex, null);
        }

        private void Choix_SousClasse(int Index, Champion ttt)
        {
            if (ttt == null) ttt = new Champion(null, null, null, null, null, DateTime.Now);

            switch (Index)
            {
                case 0: //Tank
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tank;
                    ComboBox_Sous_Classe.SelectedIndex = _tank.IndexOf(ttt.Sous_classe);
                    break;

                case 1: //Combattant
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Combattant;
                    ComboBox_Sous_Classe.SelectedIndex = _combattant.IndexOf(ttt.Sous_classe);
                    break;

                case 2: //Tueur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tueur;
                    ComboBox_Sous_Classe.SelectedIndex = _tueur.IndexOf(ttt.Sous_classe);
                    break;

                case 3: //Mage
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Mage;
                    ComboBox_Sous_Classe.SelectedIndex = _mage.IndexOf(ttt.Sous_classe);
                    break;

                case 4: //Controleur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Controleur;
                    ComboBox_Sous_Classe.SelectedIndex = _controleur.IndexOf(ttt.Sous_classe);
                    break;

                case 5: //Tireur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tireur;
                    ComboBox_Sous_Classe.SelectedIndex = _tireur.IndexOf(ttt.Sous_classe);
                    break;
            }
        }



        private void Btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            /* if (File.Exists (path + nomdedossier))
            {

            }
            else
            {
                // File.Copy(chemin, path + nomdedossier);

            }*/
            if (Txtbox_Nom.Text == null || ComboBox_Classe.SelectedIndex == -1 || ComboBox_Sous_Classe.SelectedIndex == -1 || ComboBox_Region.SelectedIndex == -1 || Date_Apparition.SelectedDate == null)
            {
                MessageBox.Show("Un champ n'est pas rempli");
            }
            else
            {
                this.DialogResult = true;
            }

            
            

        }

        private void Btn_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Txtbox_Nom_KeyUp(object sender, KeyEventArgs e)
        {
            
            Regex regexItem = new Regex("^[a-zA-Z\\'\\-]*$");

            if (!(regexItem.IsMatch(Txtbox_Nom.Text)))
            {
                //res += "le titre " + Txtbox_Nom.Text +  "n est pas correct car cela ne ejefhehfjdfhgjdhgdjfhg";
                Txtbox_Nom.Text = Txtbox_Nom.Text.Remove(Txtbox_Nom.Text.Count() - 1, 1);
                Txtbox_Nom.CaretIndex = Txtbox_Nom.Text.Count();
            }
           
        
        }
    }

}

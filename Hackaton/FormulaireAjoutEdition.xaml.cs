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

namespace Hackaton
{
    /// <summary>
    /// Logique d'interaction pour FormulaireAjoutEdition.xaml
    /// </summary>
    public partial class FormulaireAjoutEdition : Window
    {
        public FormulaireAjoutEdition()
        {
            InitializeComponent();
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

        private void Btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Png Files |*. log|Textfiles|*.txt |All file| ";
                fileDialog.DefaultExt = ".log";
            Nullable<bool> dialogOK = fileDialog.ShowDialog();

            if (dialogOK == true)
            {
                string sFilenames = "";
                // < @loop: Filenames > 
                foreach (string sFilename in fileDialog.FileNames)
                {
                    //collect string sFilenames += ";" + sFilename; 
                }
                sFilenames = sFilenames.Substring(1); //delete first ; 
                // </ @Loop: Filenames > 
                Txtb1.Text = sFilenames;

            }
        }

        private void WindowFormulaire_Loaded(object sender, RoutedEventArgs e)
        {
            //initialisation des listes de classes
            ComboBox_Classe.ItemsSource = Classe;
            ComboBox_Region.ItemsSource = Region;
            ComboBox_Sous_Classe.IsEnabled = false;

        }

        private void ComboBox_Classe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_Classe.SelectedIndex)
            {
                case 0: //Tank
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tank;
                    break;

                case 1: //Combattant
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tank;
                    break;

                case 2: //Tueur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tueur;
                    break;

                case 3: //Mage
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Mage;
                    break;

                case 4: //Controleur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Controleur;
                    break;

                case 5: //Tireur
                    ComboBox_Sous_Classe.IsEnabled = true;
                    ComboBox_Sous_Classe.ItemsSource = Tireur;
                    break;
            }
        }
    }

}

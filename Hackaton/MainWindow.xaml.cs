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
            ComboBox_List.ItemsSource = Choix; //link de la combobox choix
            ComboBox_List.SelectedIndex = 0;
        }

        ObservableCollection<Champion> listing = new ObservableCollection<Champion>();
        string path = System.AppDomain.CurrentDomain.BaseDirectory; //chemin d acces de l executable
        string nomdefichier = "/database.txt";

        //liste combo_box recherche
        private List<string> Choix = new List<string>() { "Nom", "Region", "Classe", "Sous-Classe" };

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
        private void Btn_Suppprimer_Click(object sender, RoutedEventArgs e) //supprime un objet de la liste
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


        private void Btn_Lecture_Click(object sender, RoutedEventArgs e) //import de la database dans l application
        {
            if (File.Exists(path + nomdefichier) == true)
            {
                listing.Clear();
                StreamReader lecteur = new StreamReader(path + nomdefichier);
                while (!lecteur.EndOfStream)
                {
                    string tmp = lecteur.ReadLine();
                    string[] donnees_eclatees = tmp.Split('#');

                    short Y, M, D;  //recuperation de la date de parution
                    Int16.TryParse(donnees_eclatees[7], out Y);
                    Int16.TryParse(donnees_eclatees[6], out M);
                    Int16.TryParse(donnees_eclatees[5], out D);

                    DateTime date = new DateTime(Y, M, D);
                    listing.Add(new Champion(donnees_eclatees[0], donnees_eclatees[1], donnees_eclatees[2], donnees_eclatees[3], donnees_eclatees[4], date)); //remplissage de database
                }
                lecteur.Close();
                MessageBox.Show("L'importation a été réussite avec succés");
            }
            else
            {
                MessageBox.Show("Fichier non trouvé");
            }
        }
        private void Btn_Sauvegarder_Click(object sender, RoutedEventArgs e) //sauvergarde les donnees de l application dans database
        {
            StreamWriter ecriveur = new StreamWriter(path + nomdefichier, false);
            foreach (Champion xxx in listing)
            {
                ecriveur.WriteLine(xxx.ToString());
            }
            ecriveur.Close();
            MessageBox.Show("La sauvegarde a été effectué");
        }
        private void Btn_Rechercher_Click(object sender, RoutedEventArgs e) //recherche l'element de la textbox en ciblant la colonne par rapport a la combobox
        {
            int cpt = 0;
            Dtg_datagrid01.UnselectAllCells();
            switch (ComboBox_List.SelectedIndex)
            {
                case 0: //Nom
                    foreach (Champion xxx in Dtg_datagrid01.Items)
                    {
                        if (xxx.Nom == TxtB_recherche.Text)
                        {
                            cpt++;
                            Dtg_datagrid01.SelectedItems.Add(xxx);
                        }
                    }
                    break;

                case 1: //Region
                    foreach (Champion xxx in Dtg_datagrid01.Items)
                    {
                        if (xxx.Region == TxtB_recherche.Text)
                        {
                            cpt++;
                            Dtg_datagrid01.SelectedItems.Add(xxx);
                        } 
                    }
                    break;

                case 2: //Classe
                    foreach (Champion xxx in Dtg_datagrid01.Items)
                    {
                        if (xxx.Classe == TxtB_recherche.Text)
                        {
                            cpt++;
                            Dtg_datagrid01.SelectedItems.Add(xxx);
                        }
                    }
                    break;

                case 3: //Sous-classe
                    foreach (Champion xxx in Dtg_datagrid01.Items)
                    {
                        if (xxx.Sous_classe == TxtB_recherche.Text)
                        {
                            cpt++;
                            Dtg_datagrid01.SelectedItems.Add(xxx);
                        }
                    }
                break;
            }
            
            MessageBox.Show("Nombre d'occurences :" + Convert.ToString(cpt)); //nombre d element contenant la recherche
        }

    }

    
}

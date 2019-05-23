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

        private void Btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Log Files |*. log|Textfiles|*.txt |All file| ";
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
    }

}

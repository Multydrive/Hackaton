using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton
{
    class Champion
    {
        //attributs
        private string _nom;
        private string _region;
        private string _classe;
        private string _sous_classe;

        //accesseur  & mutateur
        public string Nom { get => _nom; set => _nom = value; }
        public string Region { get => _region; set => _region = value; }
        public string Classe { get => _classe; set => _classe = value; }
        public string Sous_classe { get => _sous_classe; set => _sous_classe = value; }

        //constructeur
        public Champion(string pnom, string pregion, string pclasse, string psous_classe)
        {
            this._nom = pnom;
            this._region = pregion;
            this._classe = pclasse;
            this._sous_classe = psous_classe;
        }
        //Override de ToString
        public override string ToString()
        {

            return this.Nom + "#" + this.Region + "#" + this.Classe + "#" + this.Sous_classe + "#";
        }
    }
}

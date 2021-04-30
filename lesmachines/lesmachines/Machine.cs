using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesMachines
{   
    /// <summary>
    /// Cette classe abstraite est la classe mère de chaque machine
    /// </summary>
    abstract class Machine
    {
        // temps= temps pris par la machine lors de la réalisation d'une pièce
        // id=  numéro de la machine
        // tempsMax= temps maximal de réalisation d'une pièce
        // tempsMin = temps minimal de réalisation d'une pièce
        // rnd=Random permettant d'avoir un temps pour chaque pièce réalisée
        protected int temps, id , tempsMax, tempsMin;
        protected Random rnd;

        //Constructeur de la machine
        protected Machine(int id,int tempsMax,int tempsMin )
        {
            this.rnd = new Random();
            this.id = id;

            // Multiplication par 1000 car la fonction sleep compte en millisecondes
            this.tempsMax = (tempsMax*1000)+1;
            this.tempsMin = tempsMin*1000;
        }

        // Méthode travail pour la production de ressource de la machine
        public abstract void Travail();

    }
}

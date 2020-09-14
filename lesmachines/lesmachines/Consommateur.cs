using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    /// <summary>
    /// Cette classe est pour le fonctionnement d'une machine nécessitant une ressource en entrée 
    /// et ne rendant rien 
    /// elle hérite de la classe machine
    /// </summary>
    class Consommateur :Machine
    {
        // panierEntrant=Panier d'où la machine prends des ressources
        private Panier panierEntrant;

        // Constructeur de la machine, qui utilise le constructeur de la classe mère
        public Consommateur(int id, int tempsMax, int tempsMin, Panier panierEntrant) : base(id, tempsMax, tempsMin)
        {
           
            this.panierEntrant = panierEntrant;
        }

        // méthode pour le fonctionnement de la machine
        public override void Travail()
        {

            // Fonctionnement continu de la machine jusqu'à arrêt du programme
            while (true)
            {
                // On prend une ressource dans le panier entrant
                PrisePanier(panierEntrant);
                // On fabrique la pièce
                Fabrication();

                // on recommence
            }
        }
    }
}

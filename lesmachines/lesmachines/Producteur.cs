using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    /// <summary>
    /// Producteur est une classe machine nécessitant aucune ressource en entrée,
    /// et créant une ressource en sortie, 
    /// elle hérite de la classe Machine
    /// </summary>
    class Producteur : Machine
    {
        // panierSortant=Panier où la machine met des ressources
        private Panier panierSortant;

        // Constructeur de la machine, qui utilise le constructeur de la classe mère
        public Producteur(int id, int tempsMax, int tempsMin, Panier panier1) : base(id, tempsMax, tempsMin)
        {
            this.panierSortant = panier1;
        }

        // méthode pour le fonctionnement de la machine
        public override void Travail()
        {
            // reponse=reponse de la fonction d'ajout ou de prise de ressource
            bool reponse;

            // Fonctionnement continu de la machine jusqu'à arrêt du programme
            while (true)
            {
                // Calcul du temps de fabrication entre les limites
                temps = rnd.Next(tempsMin, tempsMax);
                // Le thread dort pendant le temps de fabrication
                Thread.Sleep(temps);

                // On bloque l'accès aux modification du panier sortant pour y ajouter une ressource
                lock (panierSortant)
                {
                    // On essaye d'ajouter une pièce
                    reponse = panierSortant.AjouterRessource();

                    // Si échec et tant qu'échec
                    while (!reponse)
                    {
                        // On affiche que le panier est plein
                        System.Console.WriteLine("Machine {0}: panier P{1} plein", id, panierSortant.Id);
                        // On attend pulse indiquant que le panier n'est plus plein
                        Monitor.Wait(panierSortant);
                        // On retente
                        reponse = panierSortant.AjouterRessource();
                    }
                    // Si l'ajout est réussi, on l'affiche
                    System.Console.WriteLine("Machine {0}: dépôt pièce P{1} ({2})", id, panierSortant.Id, panierSortant.Ressource);
                    // Si le panierSortant est passé de 0 à 1, on réveille les machines attendant que le panier soit vide
                    if (panierSortant.IsRefurnished)
                    {
                        Monitor.Pulse(panierSortant);
                    }

                    //on recommence
                }

            }
        }
    }
}

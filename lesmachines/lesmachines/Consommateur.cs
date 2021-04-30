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
            // reponse=reponse de la fonction d'ajout ou de prise de ressource
            bool reponse;

            // Fonctionnement continu de la machine jusqu'à arrêt du programme
            while (true)
            {

                // on bloque le panierEntrant, afin de changer les ressources sans créer d'erreurs de synchronisation
                lock (panierEntrant)
                {
                    //On prend la ressource du panier d'entrée
                    reponse = panierEntrant.PrendreRessource();

                    //Tant qu'on a pas eu de réussite
                    while (!reponse)
                    {
                        // On envoie un message indiquant que le panier est vide
                        System.Console.WriteLine("Machine {0}: panier P{1} vide", id, panierEntrant.Id);
                        // on met le thread en pause en attente jusqu'à recevoir un pulse
                        Monitor.Wait(panierEntrant);

                        // Si pulse reçu, on retente de retirer une ressource
                        reponse = panierEntrant.PrendreRessource();
                    }
                    // Si on a réussi a prendre une ressource, on l'affiche
                    System.Console.WriteLine("Machine {0}: prise pièce P{1} ({2})", id, panierEntrant.Id, panierEntrant.Ressource);

                    // Si on est passé de Max à Max-1, on envoit un pulse aux machines attendant que le panier soit vidé
                    if (panierEntrant.IsNotFull)
                    {
                        Monitor.Pulse(panierEntrant);
                    }
                   
                }

                // Calcul du temps de fabrication entre les limites
                temps = rnd.Next(tempsMin, tempsMax);
                // Le thread dort pendant le temps de fabrication
                Thread.Sleep(temps);

                //on recommence
            }
        }
    }
}

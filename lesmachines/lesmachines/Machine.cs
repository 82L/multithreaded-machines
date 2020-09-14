using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        // Méthode pour la prise d'une pièce
        protected void PrisePanier(Panier panierEntrant)
        {
            // reponse=reponse de la fonction de prise de ressource
            bool reponse;
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
                if (!panierEntrant.IsFull)
                {
                    Monitor.Pulse(panierEntrant);
                }
            }
        }

        // Méthode pour l'ajout d'une pièce au panier
        protected void AjoutPanier(Panier panierSortant)
        {
            // reponse=reponse de la fonction d'ajout de ressource
            bool reponse;
            // On bloque l'accès aux modification du panierSortant pour y ajouter une ressource
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

            }
        }

        // Méthode pour la fabrication de la pièce, 
        // Virtual au cas où la fonction nécessite un changement
        protected virtual void Fabrication()
        {
            // Calcul du temps de fabrication entre les limites
            temps = rnd.Next(tempsMin, tempsMax);
            // Le thread dort pendant le temps de fabrication
            Thread.Sleep(temps);
        }
    }
}

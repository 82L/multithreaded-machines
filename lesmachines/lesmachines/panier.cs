using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    /// <summary>
    /// La classe Panier permet de stocker les ressources entre deux machines. Elle dispose d'une limite de stockage.
    /// </summary>
    class Panier
    {
        // ressource = indique le nombre de ressource dans le panier
        // id = numéro du panier
        // max = nombre de ressource maximum stockable dans le panier
        // isRefurnished = Variable servant à indiquer si le nombre de ressource du panier passe de 0 à 1
        // isNotFull = Variable servant à indiquer si le nombre de ressource du panier passe de 5 à 4
        private int ressource, id, max;
        private bool isRefurnished, isNotFull;

        //Constructeur de la classe
        public Panier(int max, int id)
        {
           
            ressource = 0;
            this.max = max;
            this.id = id;


        }

        // Getter nécessaires à la classe
        public int Ressource { get => ressource; }
        public int Id { get => id; }
        public bool IsRefurnished { get => isRefurnished; }
        public bool IsNotFull { get => isNotFull; }

        // Fonction pour retirer une ressource du panier
        public bool PrendreRessource()
        {
            // Si le panier est vide, aucune ressource n'est retiré, fonction renvoit échec
            if (Ressource == 0)
            {
                return false;
            }
            // Sinon on enlève une ressource
            else
            {
                ressource--;
                // Si on pas de Max à Max-1, on change isNotFull en true, sinon false
                if (ressource == (max-1))
                {
                    isNotFull = true;
                }
                else
                {
                    isNotFull = false;
                }
                // On a bien retiré une ressource, fonction renvoie réussite
                return true;
            }
        }

        // Fonction pour retirer une ressource du panier
        public bool AjouterRessource()
        {
            //Si le panier est plein, échec
            if (Ressource == max)
            {
                return false;
            }
            // sinon, on ajoute la ressource au panier
            else
            {
                ressource++;

                // si on passe de 0 à 1, on change isRefurnished à true, sinon false
                if(ressource==1)
                {
                    isRefurnished = true;
                }
                else
                {
                    isRefurnished = false;
                }
                // on a bien retiré une ressource, fonction renvoie réussite
                return true;
            }
        }

    }
}

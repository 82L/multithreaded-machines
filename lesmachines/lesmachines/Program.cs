using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LesMachines
{
    ///<summary>
    /// Auteur : Pierre-Baptiste lefloch
    /// 
    /// Ce Programme vise à faire fonctionner en multithreading des machines de 3 types différents ayant besoins de ressources communes
    ///</summary>

    class Program
    {
        // Lancement du programme
        private static void Main(string[] args)
        {
            // Déclaration des paniers, avec leur limite et leur ID
            Panier panier1=new Panier(5,1);
            Panier panier2 = new Panier(7,2);

            // Déclaration les 3 machines dont on aura besoin, en leur assignant leurs paniers nécessaire lors de la création, ils passent par référence
            Producteur machine1 = new Producteur(1, 6, 3, panier1);
            ProducteurConsommateur machine2 = new ProducteurConsommateur(2,9,5, panier1, panier2);
            Consommateur machine3 = new Consommateur(3,6,3,panier2);

            // Déclaration des 3 threads, auquels on assigne les processus des machines
            Thread th1 = new Thread(machine1.Travail);
            Thread th2 = new Thread(machine2.Travail);
            Thread th3 = new Thread(machine3.Travail);

            // Démarrage des 3 threads
            th1.Start();
            th2.Start();
            th3.Start();
        }

    }
}

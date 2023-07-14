using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LesMachines
{
    ///<summary>
    /// Author : 82L
    /// Last Edit : July 2023
    /// 
    /// This program was an end year DUT assignment in OO languages, made in 2018
    /// The goal was to make a multithreaded functionning system using machines and systems.
    ///
    /// The project was entirely refactored as a test, to see my progress, in 2023
    ///</summary>

    class Program
    {
        /// <summary>
        ///  Main thread, used to start the machine system.
        /// </summary>
        /// <param name="args"> arguments too pass to the program, not used</param>
        private static void Main(string[] args)
        {
          

            List <Machine>  machines = new List<Machine>();
            List <Thread>  threads = new List<Thread>();

            // basket declaration
            Basket basket1=new Basket(1,5);
            Basket basket2 = new Basket(2,7);
            
            //Machine declaration
            machines.Add(new Producer(1, 3, 6, basket1));
            machines.Add(new ProducerConsumer(2,5,9, basket1, basket2));
            machines.Add(new Consumer(3,3,6,basket2));


            foreach (var machine in machines)
            {
                threads.Add(new Thread(machine.ProcessResource));
            }
    
            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

    }
}

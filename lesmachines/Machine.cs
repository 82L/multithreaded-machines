using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{   
    /// <summary>
    /// Abstract class necessary for the creation of different kinds of machines
    /// </summary>
    internal abstract class Machine
    {

        /// <summary>
        /// Max time limit for the machine to process a resource
        /// </summary>
        private readonly int _maxMakingDurationForResource;
        
        /// <summary>
        /// Max time limit for the machine to process a resource
        /// </summary>
        private readonly int _minMakingDurationForResource;
        
        /// <summary>
        /// Used to set <paramref name="DurationOfResourceMaking"/>,
        /// between <paramref name="MinMakingDurationForResource"/> and <paramref name="MaxMakingDurationForResource"/>
        /// </summary>
        private readonly Random _randomNumberGenerator;
        
        /// <summary>
        /// ID of the machine
        /// </summary>
        protected readonly int Id;


        /// <summary>
        /// Constructor of the Machine
        /// </summary>
        /// <param name="id">Future identifier of the Machine</param>
        /// <param name="maxMakingDurationForResource">Max duration allowed to machine to create a resource, in seconds</param>
        /// <param name="minMakingDurationForResource">Min duration allowed to machine to create a resource, in seconds</param>
        /// <param name="basket">basket used by the machine</param>
        protected Machine(int id, int minMakingDurationForResource,int maxMakingDurationForResource )
        {
            this._randomNumberGenerator = new Random();
            this.Id = id;
            
            // Multiplied by 1000 because sleep is in millis
            this._maxMakingDurationForResource = (maxMakingDurationForResource*1000)+1;
            this._minMakingDurationForResource = minMakingDurationForResource*1000;
        }

        /// <summary>
        ///  Get if we need to pulse the machine, depending of basket state
        /// </summary>
        /// <returns> Whether other machines may need a pulse </returns>
        protected abstract bool DoesOtherMachinesMayNeedAPulse();

        /// <summary>
        /// Set duration for current piece 
        /// </summary>
        protected int GetDurationForCurrentResource()
        {
            return _randomNumberGenerator.Next(_minMakingDurationForResource, _maxMakingDurationForResource);
        }

        /// <summary>
        /// The method applied by the machine to process or create a resource
        /// </summary>
        public abstract void ProcessResource();

        /// <summary>
        ///  Interact with the basket to take or give resource
        /// </summary>
        /// <param name="basket">Basket to interact with</param>
        /// <param name="basketFunction">Basket function to use in process</param>
        protected virtual void BasketInteraction(Basket basket, Func<int, bool> basketFunction)
        {
            lock (basket)
            {
                bool success = basketFunction(Id);

                while (!success)
                {
                    Monitor.Wait(basket);
                    success = basketFunction(Id);
                }
                
                if (DoesOtherMachinesMayNeedAPulse())
                {
                    Monitor.Pulse(basket);
                }
            }
        }
    }
}

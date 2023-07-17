using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    
    /// <summary>
    /// Producer and consumer, from Machine. Get resource from basket1, transform it and put it inside basket2.
    /// </summary>
    class ConsumerProducer : Machine
    {
        /// <summary>
        /// Basket to take resources from
        /// </summary>
        private readonly Basket _basketToTake;
        /// <summary>
        /// Basket to give resources to after processing
        /// </summary>
        private readonly Basket _basketToGive;

        /// <summary>
        ///  Constructor of class
        /// </summary>
        /// <param name="id">Id of the machine, needed for clarification</param>
        /// <param name="minMakingDurationForResource">Min time to use resource</param>
        /// <param name="maxMakingDurationForResource">Max time to use resource</param>
        /// <param name="basketToTake">Basket to get resources from</param>
        /// <param name="basketToGive">Basket to give resources to</param>
        public ConsumerProducer(int id, int minMakingDurationForResource, int maxMakingDurationForResource, Basket basketToTake, Basket basketToGive)
            : base(id, minMakingDurationForResource, maxMakingDurationForResource)
        {
            this._basketToTake = basketToTake;
            this._basketToGive = basketToGive;
           
        }

        protected override bool DoesOtherMachinesMayNeedAPulse()
        {
            return _basketToTake.IsFullMinusOne || _basketToGive.IsAtOne;
        }
        
        public override void ProcessResource()
        {
            // Continuous process until end of program
            while (true) 
            {
                BasketInteraction(_basketToTake, _basketToTake.TakeResource);
                Thread.Sleep(GetDurationForCurrentResource());
                BasketInteraction(_basketToGive, _basketToGive.AddResource);
            }
          
        }
    }
}

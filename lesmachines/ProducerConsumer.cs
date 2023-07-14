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
    class ProducerConsumer : Machine
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
        public ProducerConsumer(int id, int minMakingDurationForResource, int maxMakingDurationForResource, Basket basketToTake, Basket basketToGive)
            : base(id, minMakingDurationForResource, maxMakingDurationForResource)
        {
            this._basketToTake = basketToTake;
            this._basketToGive = basketToGive;
           
        }
        
        protected override void BasketInteractionFail(Basket basket)
        {
            System.Console.WriteLine(
                basket == _basketToTake ? "Machine {0}: basket P{1} empty" : "Machine {0}: basket P{1} full", Id,
                basket.Id);
        }
        protected override void BasketUsed(Basket basket)
        {
            System.Console.WriteLine(
                basket == _basketToTake ? "Machine {0}: take resource from basket P{1} ({2})" 
                    : "Machine {0}: put resource in basket P{1} ({2})",
                Id, basket.Id, basket.ResourceNumber);
        }
        protected override bool DoesOtherMachinesMayNeedAPulse()
        {
            return !_basketToTake.IsFull || !_basketToGive.IsEmpty;
        }
        
        public override void ProcessResource()
        {
            // Continuous process until end of program
            while (true) 
            {
                BasketInteraction(_basketToTake, _basketToTake.TakeResource);
                Thread.Sleep(GetDurationForCurrentPiece());
                BasketInteraction(_basketToGive, _basketToGive.AddResource);
            }
          
        }
    }
}

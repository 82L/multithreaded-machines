﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    /// <summary>
    /// Consumer, from Machine. Get resource from basket and process it.
    /// </summary>
    class Consumer :Machine
    {
        /// <summary>
        /// Basket used by the machine
        /// </summary>
        private readonly Basket _basket;
        
        /// <summary>
        ///  Constructor of class
        /// </summary>
        /// <param name="id">Id of the machine, needed for clarification</param>
        /// <param name="minMakingDurationForResource">Min time to use resource</param>
        /// <param name="maxMakingDurationForResource">Max time to use resource</param>
        /// <param name="basket">Basket to get resources from</param>
        public Consumer(int id, int minMakingDurationForResource, int maxMakingDurationForResource, Basket basket)
            : base(id, minMakingDurationForResource, maxMakingDurationForResource)
        {
            this._basket = basket;
        }
        
        
        protected override void BasketInteractionFail(Basket basket)
        {
            System.Console.WriteLine("Machine {0}: basket P{1} empty", Id, basket.Id);
        }
        
        protected override void BasketUsed(Basket basket)
        {
            System.Console.WriteLine("Machine {0}: take resource from basket P{1} ({2})", Id, basket.Id, basket.ResourceNumber);
        }
        
        protected override bool DoesOtherMachinesMayNeedAPulse()
        {
            return !_basket.IsFull;
        }
        
        public override void ProcessResource()
        {
            // Continuous process until end of program
            while (true) 
            {
                BasketInteraction(_basket, _basket.TakeResource);
                Thread.Sleep(GetDurationForCurrentPiece());
            }
        }
    }
}

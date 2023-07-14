using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesMachines
{
    /// <summary>
    /// Basket class goal is to stock and gives resources to different machines
    /// </summary>
    class Basket
    {
        
        /// <summary>
        ///  Maximum number of resources permitted inside the basket
        /// </summary>
        private readonly int _maxLimit;
        
        /// <summary>
        /// Number of resources currently inside the basket
        /// </summary>
        public int ResourceNumber { get; private set; }
        
        /// <summary>
        /// id of the basket
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Whether the basket is empty
        /// </summary>
        public bool IsEmpty { get; private set; }
        
        /// <summary>
        /// Whether the basket is full
        /// </summary>
        public bool IsFull { get; private set; }
        
        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="maxLimit">Future max limit of the basket</param>
        /// <param name="id">Id of the machine, needed for referencing</param>
        public Basket(int id, int maxLimit )
        {
            ResourceNumber = 0;
            this._maxLimit = maxLimit;
            this.Id = id;
            this.IsFull = false;
            this.IsEmpty = true;
        }
        
        /// <summary>
        /// Takes and remove resource of the basket
        /// </summary>
        /// <returns> If it could get a resource or not</returns>
        public bool TakeResource()
        {
            //Can't take resource if basket is empty
            if (IsEmpty)
            {
                return false;
            }
           
            ResourceNumber--; 
            UpdateBasketStatus();

            return true;
        }

        /// <summary>
        /// Adds a resource to the basket
        /// </summary>
        /// <returns> If it could put in a resource or not</returns>
        public bool AddResource()
        {
            //Can't add resource if basket is full
            if (IsFull)
            {
                return false;
            }
            ResourceNumber++;
            UpdateBasketStatus();
            return true;
        }
        
        /// <summary>
        /// Updates the status of the basket, whether it is full or empty
        /// </summary>
        private void UpdateBasketStatus()
        {
            IsEmpty = ResourceNumber == 0;
            IsFull = ResourceNumber == _maxLimit;
        }

    }
}

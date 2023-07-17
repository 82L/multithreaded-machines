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
        private int _resourceNumber;

        /// <summary>
        /// id of the basket
        /// </summary>
        private readonly int _id;
        
        /// <summary>
        /// Whether the basket is empty
        /// </summary>
        public bool IsEmpty { get; private set; }
        
        /// <summary>
        /// Whether the basket is full
        /// </summary>
        public bool IsFull { get; private set; }
        
        /// <summary>
        /// Send if basket resource number = max limit minus 1
        /// </summary>
        public bool IsFullMinusOne => (_maxLimit - 1) == _resourceNumber;

        /// <summary>
        /// Send if basket resource number = max limit minus 1
        /// </summary>
        public bool IsAtOne => 1 == _resourceNumber;
        
        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="maxLimit">Future max limit of the basket</param>
        /// <param name="id">Id of the machine, needed for referencing</param>
        public Basket(int id, int maxLimit )
        {
            this._resourceNumber = 0;
            this._maxLimit = maxLimit;
            this._id = id;
            this.IsFull = false;
            this.IsEmpty = true;
        }
        
        /// <summary>
        /// Takes and remove resource of the basket
        /// </summary>
        /// <returns> If it could get a resource or not</returns>
        public bool TakeResource(int machineId)
        {
            //Can't take resource if basket is empty
            if (IsEmpty)
            {
                System.Console.WriteLine("Basket P{1} empty, machine {0} can't take anything", machineId, _id);
                return false;
            }
           
            _resourceNumber--; 
            System.Console.WriteLine("Machine {0} has taken resource from basket P{1} ({2})", machineId, _id, _resourceNumber);
            UpdateBasketStatus();
            return true;
        }

        /// <summary>
        /// Adds a resource to the basket
        /// </summary>
        /// <returns> If it could put in a resource or not</returns>
        public bool AddResource(int machineId)
        {
            //Can't add resource if basket is full
            if (IsFull)
            {
                System.Console.WriteLine("Basket P{1} full Machine {0} can't put anything in", machineId, _id);
                return false;
            }
            _resourceNumber++;
            System.Console.WriteLine("Machine {0} has put resource on basket P{1} ({2})", machineId, _id, _resourceNumber);
            UpdateBasketStatus();
            return true;
        }
        
        /// <summary>
        /// Updates the status of the basket, whether it is full or empty
        /// </summary>
        private void UpdateBasketStatus()
        {
            IsEmpty = _resourceNumber == 0;
            IsFull = _resourceNumber == _maxLimit;
        }

    }
}

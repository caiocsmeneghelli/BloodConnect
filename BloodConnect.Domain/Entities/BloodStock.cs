using BloodConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Entities
{
    public class BloodStock : BaseEntity
    {
        public BloodStock(BloodType bloodType, RhFactor rhFactor)
        {
            BloodType = bloodType;
            RhFactor = rhFactor;
        }

        public BloodType BloodType { get; private set; }
        public RhFactor RhFactor { get; private set; }
        public int QuantityMl { get; private set; }

        public void AddMl(int quantityMl)
        {
            QuantityMl = +quantityMl;
        }

        public void Withdraw(int quantityMl)
        {
            QuantityMl = -quantityMl;
        }
    }
}

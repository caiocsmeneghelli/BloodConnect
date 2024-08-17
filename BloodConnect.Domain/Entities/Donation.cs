using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Entities
{
    public class Donation : BaseEntity
    {
        public Donation(int donorId, int quantityMl) : base()
        {
            DonorId = donorId;
            QuantityMl = quantityMl;
        }

        public Donation(Donor donor, int quantityMl) : base()
        {
            Donor = donor;
            QuantityMl = quantityMl;
        }

        public int DonorId { get; private set; }
        public Donor Donor { get; private set; }
        public int QuantityMl { get; private set; }
    }
}

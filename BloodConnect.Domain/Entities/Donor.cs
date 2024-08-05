using BloodConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Entities
{
    public class Donor : BaseEntity
    {
        public Donor(string fullName, string email, DateTime birthDate, Genre genre,
            double weight, BloodType bloodType, RhFactor rhFactor) : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Genre = genre;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
            Donations = new List<Donation>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Genre Genre { get; private set; }
        public double Weight { get; set; }
        public BloodType BloodType { get; private set; }
        public RhFactor RhFactor { get; private set; }
        public int AddressId { get; private set; }
        public Address Address { get; private set; }
        public List<Donation> Donations { get; private set; }


        public void AddAddress(Address address)
        {
            Address = address;
        }
    }
}

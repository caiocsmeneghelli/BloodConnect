using System;

namespace BloodConnect.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address()
        {
            
        }
        public Address(string street, string city, string state, string cep) : base()
        {
            Street = street;
            City = city;
            State = state;
            CEP = cep;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string CEP { get; private set; }
    }
}

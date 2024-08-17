
using MediatR;

namespace BloodConnect.Application.Commands.CreateDonor
{
    public class CreateDonorCommand : IRequest<Result>
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Genre { get; set; }
        public double Weight { get; set; }
        public string? BloodType { get; set; }
        public string? RhFactor { get; set; }

        // Address // use CEP
        public string? Cep { get; set; }
    }
}

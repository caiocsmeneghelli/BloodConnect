using BloodConnect.Application.ViewModel;
using BloodConnect.Domain.Enums;
using MediatR;

namespace BloodConnect.Application.Queries.GetBloodStockPerBloodTypeRhFactor
{
    public class GetBloodStockPerBloodTypeRhFactorQuery : IRequest<BloodStockViewModel?> {
        public GetBloodStockPerBloodTypeRhFactorQuery(BloodType bloodType, RhFactor rhFactor)
        {
            BloodType = bloodType;
            RhFactor = rhFactor;
        }

        public BloodType BloodType { get; private set; }
        public RhFactor RhFactor { get; private set; }
    }
}
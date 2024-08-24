using System.Reflection.Metadata.Ecma335;
using BloodConnect.Application.ViewModel;
using BloodConnect.Domain.UnitOfWork;
using MediatR;

namespace BloodConnect.Application.Queries.GetBloodStockPerBloodTypeRhFactor
{
    public class GetBloodStockPerBloodTypeRhFactorQueryHandler :
        IRequestHandler<GetBloodStockPerBloodTypeRhFactorQuery, BloodStockViewModel?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBloodStockPerBloodTypeRhFactorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BloodStockViewModel?> Handle(GetBloodStockPerBloodTypeRhFactorQuery request, CancellationToken cancellationToken)
        {
            var bloodStock = await _unitOfWork.BloodStocks
                .GetByTypeAndRhFactorAsync(request.BloodType, request.RhFactor);
            if(bloodStock is null)
                return null;

            return new BloodStockViewModel(bloodStock); 
        }
    }
}
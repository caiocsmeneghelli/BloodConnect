using BloodConnect.Application.ViewModel;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllBloodStock
{
    public class GetAllBloodStockQueryHandler : IRequestHandler<GetAllBloodStockQuery, List<BloodStockViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBloodStockQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BloodStockViewModel>> Handle(GetAllBloodStockQuery request, CancellationToken cancellationToken)
        {
            List<BloodStock> listModel = await _unitOfWork.BloodStocks.GetAllAsync();
            return listModel.Select(reg => new BloodStockViewModel(reg)).ToList();
        }
    }
}

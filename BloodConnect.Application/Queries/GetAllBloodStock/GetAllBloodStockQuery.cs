using BloodConnect.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllBloodStock
{
    public class GetAllBloodStockQuery : IRequest<List<BloodStockViewModel>>
    {
    }
}

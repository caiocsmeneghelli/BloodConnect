using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.ViewModel
{
    public class BloodStockViewModel
    {
        public BloodStockViewModel(BloodStock model)
        {
            BloodType = model.BloodType;
            RhFactor = model.RhFactor;
            BloodTypeString = model.BloodType.ToString();
            RhFactorString = model.RhFactor.ToString();
            QuantityMl = model.QuantityMl;
        }

        public string BloodTypeString { get; private set; }
        public BloodType BloodType { get; private set; }
        public string RhFactorString { get; private set; }
        public RhFactor RhFactor { get; private set; }
        public int QuantityMl { get; private set; }
    }
}

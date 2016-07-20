using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Reports
{
    public class GetActiveRentToOwn : IGetActiveRentToOwn
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetActiveRentToOwn(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public virtual ResultModel Execute()
        {
            var vehicles = _autoRenterDatabaseContext.Vehicles.Where(i => i.RentToOwn).Include("Location").ToList();

            return new ResultModel
            {
                Data = vehicles.Select(VehicleModelConverter.ConvertDatabaseVehicleModelToApiVehicleModel).ToList(),
                Success = true
            };
        }
    }
}
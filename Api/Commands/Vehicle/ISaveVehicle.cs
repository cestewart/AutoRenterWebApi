﻿using Api.Models;

namespace Api.Commands.Vehicle
{
    public interface ISaveVehicle
    {
        ResultModel Execute(VehicleModel vehicleModel);
    }
}

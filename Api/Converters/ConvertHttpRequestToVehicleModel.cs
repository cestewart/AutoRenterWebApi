using System.Collections.Specialized;
using System.Web;
using Api.Models;

namespace Api.Converters
{
    public class ConvertHttpRequestToVehicleModel : IConvertHttpRequestToVehicleModel
    {
        public VehicleModel Execute(HttpRequest httpRequest)
        {
            var vehicleModel = GetVehicleModelData(httpRequest.Form);
            vehicleModel.Image = MediaModelConverter.ConvertHttpPostedFileToMediaModel(httpRequest.Files["Image"], vehicleModel.MediaId);
            return vehicleModel;
        }

        public virtual VehicleModel GetVehicleModelData(NameValueCollection form)
        {
            return new VehicleModel
            {
                VehicleId = DataTypeConverter.ToInt(form["VehicleId"]),
                Vin = form["Vin"],
                Model = form["Model"],
                Year = DataTypeConverter.ToInt(form["Year"]),
                Miles = DataTypeConverter.ToInt(form["Miles"]),
                Color = form["Color"],
                LocationId = DataTypeConverter.ToInt(form["LocationId"]),
                RentToOwn = DataTypeConverter.ToBool(form["RentToOwn"]),
                Make = form["Make"],
            };
        }
    }
}
using System;
using System.Collections.Generic;

namespace QGS.Entities.BusinessEntities
{
    public class CarVM
    {
        #region Vehicle Fields
        public int CarId { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string HorsePower_HP_ { get; set; }
        public string Consumption_L_100Km_ { get; set; }
        public string Co2Emission_g_Km_ { get; set; }
        public string FuelType { get; set; }
        public Nullable<int> Seating { get; set; }
        public Nullable<bool> Abs { get; set; }
        public Nullable<int> Airbag { get; set; }
        public string Transmission { get; set; }
        public string Colour { get; set; }
        public Nullable<int> Price { get; set; }
        #endregion

        #region Equipments Fields
        public string MainEquipment { get; set; }
        public string StandardEquipment { get; set; }

        #endregion

        #region IncludedService Fields
        public string ServiceName { get; set; }
        #endregion

        public int final_price { get; set; }
        public int final_time { get; set; }
        public int final_mileage { get; set; }

        public int final_insage { get; set; }
        public int finalIns { get; set; }

        public string isMainDriver { get; set; }
        public string email { get; set; }

        //public List<string> InsId { get; set; }
        //public List<string> InsMonth { get; set; }

        public IList<Insurance> insObj;

        public IList<Mileage> milObj;

        public IList<PayBackTime> payObj;



    }

    public class Insurance
    {
        public int InsuranceId { get; set; }
        public Nullable<int> Term_Month_ { get; set; }
    }
    public class Mileage
    {
        public int MileageId { get; set; }
        public Nullable<int> Mileage_K_ { get; set; }
    }

    public class PayBackTime
    {
        public int PaybackTimeId { get; set; }
        public int PaybackTime_Month_ { get; set; }
    }
}

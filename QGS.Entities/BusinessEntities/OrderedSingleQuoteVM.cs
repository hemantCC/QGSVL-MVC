using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QGS.Entities.BusinessEntities
{
    public class OrderedSingleQuoteVM
    {

        public int CarId { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        [Display(Name = "Horsepower")]
        public string HorsePower_HP_ { get; set; }
        [Display(Name = "Consumption")]
        public string Consumption_L_100Km_ { get; set; }
        [Display(Name = "Co2Emission")]
        public string Co2Emission_g_Km_ { get; set; }
        public string FuelType { get; set; }
        public Nullable<int> Seating { get; set; }
        public Nullable<bool> Abs { get; set; }
        public Nullable<int> Airbag { get; set; }
        public string Transmission { get; set; }
        public string Colour { get; set; }
        public Nullable<int> Price { get; set; }
        public string MainEquipment { get; set; }
        public string StandardEquipment { get; set; }
       
        

        
        public string ServiceName { get; set; }

        [Display(Name = "Total Price")]
        public int final_price { get; set; }
        [Display(Name = "Total Time Period")]
        public int final_time { get; set; }
        [Display(Name = "Insurance Term")]
        public int final_ins { get; set;}

        public string status { get; set; }
    }

}

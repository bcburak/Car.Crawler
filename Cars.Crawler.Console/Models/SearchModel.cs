using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Crawler.Main.Models
{
    public class SearchModel
    {
        public string MileAge { get; set; }
        public string Model { get; set; }
        public string StockType { get; set; }
        public string Price { get; set; }
        public string Dealer { get; set; }
        public string MilesFrom { get; set; }
    }
}

//foreach (var asdadsa in asd)
//{
//    var toto = asdadsa.SelectSingleNode("//div[@class='mileage']").InnerText;
//}
//var mileAge = item.SelectSingleNode("//div[@class='mileage']").InnerText;

////var asd = item.SelectSingleNode("//div[@class='mileage']").InnerText;
//var stockType = item.SelectSingleNode("//p[@class='stock-type']").InnerText;
//var model = item.SelectSingleNode("//h2[@class='title']").InnerText;
//var primaryPrice = item.SelectSingleNode("//span[@class='primary-price']").InnerText;
//var vehicleDealer = item.SelectSingleNode("//div[@class='vehicle-dealer']").InnerText;

//https://www.cars.com/shopping/results/?page=2&page_size=20&dealer_id=&keyword=&list_price_max=100000&list_price_min=&makes[]=tesla&maximum_distance=all&mileage_max=&models[]=tesla-model_s&sort=best_match_desc&stock_type=used&year_max=&year_min=&zip=94596
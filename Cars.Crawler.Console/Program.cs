using Cars.Crawler.Main.Models;
using Cars.Crawler.Services;
using CefSharp;
using CefSharp.OffScreen;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cars.Crawler.Main
{
    class Program
    {
        //private static ChromiumWebBrowser browser;
      
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        private static async Task MainAsync()
        {
            //var urlList = new List<string>();
            Dictionary<string, string> urlList = new Dictionary<string, string>();

            var pagetwoFormodelS = ReplaceUrl(Constants.Constants.searchUrl, "page=1", "page=2");
            var pageoneModelX = ReplaceUrl(Constants.Constants.searchUrl, "tesla-model_s", "tesla-model_x");
            var pagetwoModelX = ReplaceUrl(pageoneModelX, "page=1", "page=2");

            urlList.Add("modelSPage1",Constants.Constants.searchUrl);
            urlList.Add("modelSPage2",pagetwoFormodelS);
            urlList.Add("modelXPage1",pageoneModelX);
            urlList.Add("modelXPage2",pagetwoModelX);

            var browser = new Browser();

            foreach (var item in urlList)
            {
                browser.OpenUrl(item.Value);
                string source = await browser.Page.GetSourceAsync();

                if (source != null)
                {
                    ParseHtml(source,item.Key);
                }
            }           
        }

        private static void ParseHtml(string html,string fileName)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<SearchModel> searchModelList = new List<SearchModel>();
            
            //Filter the content
            htmlDoc.DocumentNode.Descendants()
                            .Where(n => n.Name == "script")
                            .ToList()
                            .ForEach(n => n.Remove());

            const string classValue = "vehicle-details";
            var node = htmlDoc.DocumentNode.SelectNodes($"//*[@class='{classValue}']").ToList();
            //var node = htmlDoc.DocumentNode.Descendants("div")
            //.Where(i => i.GetAttributeValue("class", "").Contains("vehicle-details")).ToList();
            foreach (var item in node)
            {
                var searchModel = new SearchModel();

                var childs = item.ChildNodes;

                foreach (var nodeItem in childs)
                {
                    if (nodeItem.OuterHtml.Contains("mileage"))
                    {
                        searchModel.MileAge = nodeItem.InnerText.Trim();
                    }
                    if (nodeItem.OuterHtml.Contains("stock-type"))
                    {
                        searchModel.StockType = nodeItem.InnerText.Trim();
                    }
                    if (nodeItem.OuterHtml.Contains("title"))
                    {
                        searchModel.Model = nodeItem.InnerText.Trim();
                    }
                    if (nodeItem.OuterHtml.Contains("primary-price"))
                    {
                        searchModel.Price = nodeItem.InnerText.Trim();
                    } 
                    if (nodeItem.OuterHtml.Contains("vehicle-dealer"))
                    {
                        searchModel.Dealer = sWhitespace.Replace(nodeItem.InnerText, "").Split('(')[0]; 
                    }
                    if (nodeItem.OuterHtml.Contains("miles-from"))
                    {
                        searchModel.MilesFrom= nodeItem.InnerText.Trim();
                    }
                  
                }
                searchModelList.Add(searchModel);


            }
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON",string.Concat(fileName,".json"));
            string json = JsonConvert.SerializeObject(searchModelList);
            File.WriteAllText(filePath , json);

        }

        public static string ReplaceUrl(string url, string oldValue, string newValue)
        {
            return url = url.Replace(oldValue, newValue);
        }
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();     
            
        }   

    }
}

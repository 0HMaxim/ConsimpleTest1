using ConsimpleTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsimpleTest
{
    class Program
    {
        static List<Product> products = new List<Product>();
        static List<Category> category = new List<Category>();


        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            List<JToken> JTokenproducts = null;
            List<JToken> JTokencategories = null;

        
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString($"https://tester.consimple.pro");


                JTokenproducts = JObject.Parse(json)["Products"].ToList();
                JTokencategories = JObject.Parse(json)["Categories"].ToList();



                for (int i = 0; i < JTokenproducts.Count; i++)
                    products.Add(JTokenproducts[i].ToObject<Product>());

                for (int i = 0; i < JTokencategories.Count; i++)
                    category.Add(JTokencategories[i].ToObject<Category>());

            }


            Console.WriteLine($"{"Product name",30}\t\t\t {"Category name",20}");
            Console.WriteLine();


            foreach (var item in products)
            {
                Console.WriteLine($"{item.Name,30}\t\t\t {category.FirstOrDefault(x => x.Id == item.CategoryId).Name,20}");
            }

         

        }
    }
}

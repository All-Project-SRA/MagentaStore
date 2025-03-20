using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Magenta_S.Models
{
    public class my_buy
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        public int idu { get; set; }
        public int idbill { get; set; }
        public int idseesion { get; set; }

        public List<Buy> GetBuys
        {
            get
            {
                return _magentaDataContext.Buys.Where(x => x.Idu == idu && x.bill == idbill)
                                               .ToList();

            }

        }

        public string TextAPI { get; set; }

        public async Task<string> GetProductSuggestionsForSeller()
        {
            var soldProducts = (from b in _magentaDataContext.Buys
                                join p in _magentaDataContext.Products on b.Idproduct equals p.Idproduct
                                where b.Ok == true // جميع المبيعات المسموح بها
                                select new
                                {
                                    Idproduct = b.Idproduct,
                                    Count = b.Count,
                                    ColorP = b.ColorP,
                                    Sizee = b.Sizee,
                                    Bill = b.bill,
                                    ProductDetails = new
                                    {
                                        p.Type,
                                        p.Genderoftype,
                                        p.Quality,
                                        p.Season,
                                        p.Company,
                                        p.Meadin,
                                        p.Price,
                                        p.count,
                                        p.Description
                                    }
                                })
                                .ToList();

            if (!soldProducts.Any())
                return "No sales data available."; 

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string soldProductsJson = JsonConvert.SerializeObject(soldProducts, Formatting.None, settings);

            string prompt = $"Here is a JSON list of all sold products with their attributes:\n{soldProductsJson}\n\n" +
                           "Analyze the data and provide concise suggestions to improve sales, inventory, and product variety. " +
                           "Focus on actionable steps based on the purchasing trends of customers. Avoid using introductions or numbering. " +
                           "Return the suggestions directly in plain text format.";

            string apiKey = "AIzaSyDbWf4QoGGQwq9IFBiTyINOcFPEyY4hwh8";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var payloadObject = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
            };

            string jsonPayload = JsonConvert.SerializeObject(payloadObject);
            using (HttpClient client = new HttpClient())
            {
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, httpContent);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                        if (apiResponse?.Candidates != null && apiResponse.Candidates.Count > 0)
                        {
                            string resultText = apiResponse.Candidates[0].Content.Parts[0].Text.Trim();
                            Console.WriteLine("AI Suggestions: " + resultText);
                            return resultText; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            return "Failed to get suggestions from AI.";
        }

        public List<Bill> GetBills
        {
            get
            {
                if (idseesion == 1)
                {
                    return _magentaDataContext.Bills.ToList();

                }
                else
                    return _magentaDataContext.Bills.Where(x => x.Idu == idu).ToList();

            }
            set { }

        }

        public List<Buy> GetBuysForAdmin
        {
            get
            {
                return _magentaDataContext.Buys.Where(x => x.bill == idbill).ToList();

            }

        }

        public List<Bill> GetBillsForAdmin
        {
            get
            {
                return _magentaDataContext.Bills.ToList();

            }

        }

    }
}
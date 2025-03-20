using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Magenta_S.Models
{

    public class ContentPart
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Content
    {
        [JsonProperty("parts")]
        public List<ContentPart> Parts { get; set; }
    }

    public class Candidate
    {
        [JsonProperty("content")]
        public Content Content { get; set; }
    }

    public class ApiResponse
    {
        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }
    }

    public class ProductDTO
    {
        public int Idproduct { get; set; }
        public string Type { get; set; }
        public string Genderoftype { get; set; }
        public string Quality { get; set; }
        public string Season { get; set; }
        public string Company { get; set; }
        public string Meadin { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
    }

    public class BuyDTO
    {
        public int Idu { get; set; }
        public int Idproduct { get; set; }
        public string ColorP { get; set; }
        public string Sizee { get; set; }
    }


    public class ProductA
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        private Product GetProductByID(int ID)
        {
            return _magentaDataContext.Products.FirstOrDefault(x=>x.Idproduct== ID);
        }

        [Required]
        public List<SelectListItem> Selectcolor { get; set; }
        [Required]
        public List<SelectListItem> Selectsize { get; set; }

        public List<int> ProductFavirote { get; set; }

        public int apis { get; set; }

        public async Task<List<int>> GetSuggestedProducts()
        {
            var Mybus = _magentaDataContext.Buys
                .Where(x => x.Idu == Idu)
                .Select(p => new
                {
                    Idproduct = p.Idproduct,
                    ColorP = p.ColorP,
                    Sizee = p.Sizee

                })
                .ToList();

            var products = productallshow
                .Select(p => new
                {
                    Idproduct = p.Idproduct,
                    Type = p.Type,
                    Genderoftype = p.Genderoftype,
                    Quality = p.Quality,
                    Season = p.Season,
                    Company = p.Company,
                    Meadin = p.Meadin,
                    Sizes = p.Sizes.Select(s => new { s.idproduct, s.size1 }).ToList(), 
                    Colors = p.Colors.Select(c => new { c.Idprodact, c.ColorP }).ToList()

                })
                .ToList();

            if (!Mybus.Any())
                return new List<int>(); 

            string mybusJson = JsonConvert.SerializeObject(Mybus, Formatting.None);
            string productsJson = JsonConvert.SerializeObject(products, Formatting.None);

            string prompt = $"Here is a JSON list of products purchased by a user:\n{mybusJson}\n\n" +
                            $"Here is a JSON list of all available products:\n{productsJson}\n\n" +
                            "Based on the products the user has purchased, analyze their attributes (such as type, COMPANY, quality, and season,GendetoTYPE,madein,price , color, size) and recommend other similar or related products from the available list.\n" +
                            "Return  only only 2 lenths of an array of 'Idproduct' integers in JSON format, like this: [101, 203, 305].";

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
                            Console.WriteLine("Raw AI Response: " + resultText);

                            resultText = resultText.Trim('`').Replace("json\n", "").Trim();

                            try
                            {
                                return JsonConvert.DeserializeObject<List<int>>(resultText) ?? new List<int>();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("JSON Parsing Error: " + ex.Message);
                                return new List<int>();
                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }

            return new List<int>();
        }

        public void dataforSelectsize()
        {

            SelectListItem s0 = new SelectListItem()
            {
                Text = "35",
                Value = "35"

            };
            SelectListItem s1 = new SelectListItem()
            {
                Text = "36",
                Value = "36"
            };
            SelectListItem s2 = new SelectListItem()
            {
                Text = "37",
                Value = "37"
            };
            SelectListItem s3 = new SelectListItem()
            {

                Text = "38",
                Value = "38"
            };
            SelectListItem s4 = new SelectListItem()
            {
                Text = "39",
                Value = "39"
            };
            SelectListItem s5 = new SelectListItem()
            {
                Text = "40",
                Value = "40"
            };
            SelectListItem s6 = new SelectListItem()
            {
                Text = "41",
                Value = "41"
            };
            SelectListItem s7 = new SelectListItem()
            {
                Text = "42",
                Value = "42"
            };
            SelectListItem s8 = new SelectListItem()
            {
                Text = "43",
                Value = "43"
            };

            SelectListItem s9 = new SelectListItem()
            {
                Text = "44",
                Value = "44"
            };
            SelectListItem s10 = new SelectListItem()
            {
                Text = "45",
                Value = "45"
            };
            SelectListItem s11 = new SelectListItem()
            {
                Text = "46",
                Value = "46"
            };
            SelectListItem s12 = new SelectListItem()
            {
                Text = "small",
                Value = "small"
            };
            SelectListItem s13 = new SelectListItem()
            {
                Text = "medium",
                Value = "medium"
            };
            SelectListItem s14 = new SelectListItem()
            {
                Text = "large",
                Value = "large"
            };
            SelectListItem s15 = new SelectListItem()
            {
                Text = "X large",
                Value = "X large"
            };
            SelectListItem s16 = new SelectListItem()
            {
                Text = "XX large",
                Value = "XX large"
            };
            List<SelectListItem> l = new List<SelectListItem>() { s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16 };
            Selectsize = l;


        }

        public void dataforSelectcolor()
        {

            SelectListItem s0 = new SelectListItem()
            {
                Text = "Red",
                Value = "Red"

            };
            SelectListItem s1 = new SelectListItem()
            {
                Text = "Green",
                Value = "Green"
            };
            SelectListItem s2 = new SelectListItem()
            {
                Text = "Yallow",
                Value = "Yallow"
            };
            SelectListItem s3 = new SelectListItem()
            {

                Text = "Silver",
                Value = "Silver"
            };
            SelectListItem s4 = new SelectListItem()
            {
                Text = "Vinous",
                Value = "Vinous"
            };
            SelectListItem s5 = new SelectListItem()
            {
                Text = "Grey",
                Value = "Grey"
            };
            SelectListItem s6 = new SelectListItem()
            {
                Text = "White",
                Value = "White"
            };
            SelectListItem s7 = new SelectListItem()
            {
                Text = "Pink",
                Value = "Pink"
            };
            SelectListItem s8 = new SelectListItem()
            {
                Text = "Blue",
                Value = "Blue"
            };

            SelectListItem s9 = new SelectListItem()
            {
                Text = "Black",
                Value = "Black"
            };
            SelectListItem s10 = new SelectListItem()
            {
                Text = "Iron",
                Value = "Iron"
            };
            SelectListItem s11 = new SelectListItem()
            {
                Text = "Orange",
                Value = "Orange"
            };
            SelectListItem s12 = new SelectListItem()
            {
                Text = "Purple",
                Value = "Purple"
            };
            SelectListItem s13 = new SelectListItem()
            {
                Text = "Magenta",
                Value = "Magenta"
            };
            List<SelectListItem> l = new List<SelectListItem>() { s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13 };
            Selectcolor = l;


        }

        public int Idproduct { get; set; }

        public int Idu { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Genderoftype { get; set; }

        [Required]
        public string Quality { get; set; }

        [Required]
        public string Season { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Description { get; set; }

        public string image { get; set; }

        [Required]
        public string Meadin { get; set; }

        [Required]
        public int Price
        {
            get;

            set;
        }

        [Required]
        public int count { get; set; }

        public bool Ok { get; set; }

        public System.DateTime date { get; set; }

        public virtual ICollection<Color> Colors { get; set; }

        public virtual ICollection<Size> Sizes { get; set; }

        public List<Product> getallforuserAPI()
        {
            return _magentaDataContext.Products
                 .Where(p => ProductFavirote.Contains(p.Idproduct))
                 .ToList();

        }

        public List<Product> getallproduct
        {
            get
            {
                var products = _magentaDataContext.Products.Where(x => x.Idu == Idu && x.Ok == true)
                                                           .ToList();

                var c = from item in products
                        orderby item.date descending
                        select item;

                return c.ToList();



            }
            set { }
        }

        public List<Product> Show_Fivorite
        {
            get
            {
                var products = _magentaDataContext.Products.Where(x => x.Idu == Idu && x.Ok == true)
                                                           .ToList();
                var c = from item in products
                        orderby item.date descending
                        select item;
                return c.ToList();



            }
            set { }
        }

        public List<Product> productallshow
        {
            get
            {
                var products = _magentaDataContext.Products.Where(x => x.Ok == true && x.count > 0);

                var c = from item in products
                        orderby item.date descending
                        select item;

                return c.ToList();
            }
            set { }
        }

        public Product getproduct(ProductA productA)
        {
            Product product = new Product();

            product.Type = productA.Type;
            product.Genderoftype = productA.Genderoftype;
            product.Quality = productA.Quality;
            product.Season = productA.Season;
            product.Company = productA.Company;
            product.Description = productA.Description;
            product.Meadin = productA.Meadin;
            product.Price = productA.Price;
            product.count = productA.count;
            product.date = DateTime.UtcNow;
            product.Ok = false;

            return product;

        }

        public Product UpdateProduct(ProductA A)
        {
            Product Productdb = GetProductByID(A.Idproduct);

            Product product = new Product();

            product.Idproduct = Productdb.Idproduct;
            product.Type = Productdb.Type;
            product.Idu = Productdb.Idu;
            product.Genderoftype = Productdb.Genderoftype;
            product.Quality = Productdb.Quality;
            product.Season = Productdb.Season;
            product.Company = Productdb.Company;
            product.Description = A.Description;
            product.Meadin = Productdb.Meadin;
            product.image = Productdb.image;
            product.Price = A.Price;
            product.count = A.count;
            product.date = Productdb.date;
            product.Ok = Productdb.Ok;

            return product;

        }

        public void UpdateProduct(int id)
        {
            Product product = GetProductByID(id);

            Type = product.Type;
            Genderoftype = product.Genderoftype;
            Quality = product.Quality;
            Season = product.Season;
            Company = product.Company;
            Description = product.Description;
            image = product.image;
            Meadin = product.Meadin;
            Price = product.Price;
            count = product.count;
            date = product.date;
            Idu = product.Idu;
            Idproduct = product.Idproduct;
            Ok = product.Ok;

        }

    }
}
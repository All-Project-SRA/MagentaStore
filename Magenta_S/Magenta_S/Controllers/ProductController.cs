using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using Magenta_S.Models;
using System.Data.Entity;
using System.Net.Http;
using Newtonsoft.Json;

namespace Magenta_S.Controllers
{
    public class ProductController : Controller
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        private string CheckPhoto(HttpPostedFileBase upload)
        {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            byte[] imageBytes = new byte[upload.ContentLength];
            upload.InputStream.Read(imageBytes, 0, upload.ContentLength);
            string base64Image = Convert.ToBase64String(imageBytes);

            var parts = new List<object>{new
                     {
                     text = "If the image contains inappropriate scenes, nudity, or pictures of people on the beach in inappropriate clothing, answer me with \"yes.\" Otherwise, answer me with \"no\" only, nothing else."
                    },
                 new
                    {
                 inline_data = new
                     {
                    mime_type = upload.ContentType,
                     data = base64Image
                 }
                    }
                 };

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = parts
                }
            }
            };

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    if (responseContent.Contains("yes"))
                    {
                        return "yes";
                    }
                }
                else
                {
                    return "no";
                }
            }
            return "Error";
        }

        public ActionResult Add_Product()
        {

            ProductA product = new ProductA();

            product.dataforSelectsize();
            product.dataforSelectcolor();

            return View(product);
        }

        [HttpPost]
        public ActionResult Add_Product(ProductA a, HttpPostedFileBase Upload)
        {
            if (Upload == null)
            {
                ModelState.AddModelError("imageu", " Please Enter image ");
                return View();
            }

            if (CheckPhoto(Upload) == "yes")
            {
                ModelState.AddModelError("imageu", "الصورة تحتوي على مشاهد غير لائقة.");
                return View();
            }

            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/imageproduct"), Upload.FileName);
                Upload.SaveAs(path);

                ProductA productA = new ProductA();
                Product product = productA.getproduct(a);

                product.image = Upload.FileName;

                bool checkforsize = false;
                bool checkforcolor = false;

                foreach (var item in a.Selectcolor)
                {
                    if (item.Selected)
                    {
                        checkforcolor = true;

                        Color color = new Color();
                        color.ColorP = item.Value;

                        product.Colors.Add(color);
                    }
                }
                foreach (var item in a.Selectsize)
                {
                    if (item.Selected)
                    {
                        checkforsize = true;

                        Size size = new Size();
                        size.size1 = item.Value;

                        product.Sizes.Add(size);

                    }
                }
                if (checkforsize == false)
                {
                    ModelState.AddModelError("checks", " Please Enter Size ");

                    productA.dataforSelectsize();

                    productA.dataforSelectcolor();

                    return View(productA);

                }
                if (checkforcolor == false)
                {

                    ModelState.AddModelError("checkc", " Please Enter Color  ");
                    productA.dataforSelectsize();

                    productA.dataforSelectcolor();

                    return View(productA);

                }
                product.Idu = (int)(Session["iduser"]);

                _magentaDataContext.Products.Add(product);
                _magentaDataContext.SaveChanges();

                return RedirectToAction("HomeDealer", "Home");
            }
            return View();
        }

        public ActionResult My_Product()
        {
            ProductA product = new ProductA();
            product.Idu = (int)Session["iduser"];

            return View(product);
        }

        [HttpPost]
        [ActionName("My_Product")]
        public ActionResult My_Products(int Idproduct)
        {
            Product product = _magentaDataContext.Products.FirstOrDefault(x => x.Idproduct == Idproduct);

            try
            {
                _magentaDataContext.Products.Remove(product);
                _magentaDataContext.SaveChanges();
            }
            catch
            {
                product.Ok = false;
                _magentaDataContext.Entry(product).State = EntityState.Modified;
                _magentaDataContext.SaveChanges();
            }

            return RedirectToAction("My_Product");

        }

        [HttpGet]
        public ActionResult Modification(int Idproduct)
        {
            int idu = (int)Session["iduser"];

            ProductA product = new ProductA();
            product.UpdateProduct(Idproduct);

            if (product.Idu != idu)
            {
                throw new HttpException(404, "Bad Request");

            }

            product.dataforSelectsize();
            product.dataforSelectcolor();

            return View(product);
        }

        [HttpPost]
        public ActionResult Modification(ProductA p)
        {
            bool checkforsize = false;
            bool checkforcolor = false;

            ProductA productA = new ProductA();
            Product product = productA.UpdateProduct(p);

            foreach (var item in p.Selectcolor)
            {
                if (item.Selected)
                {
                    checkforsize = true;

                    Color color = new Color();

                    color.ColorP = item.Value;
                    color.Product = product;
                    color.Idprodact = product.Idproduct;

                    product.Colors.Add(color);
                }
            }

            foreach (var item in p.Selectsize)
            {
                if (item.Selected)
                {
                    checkforcolor = true;

                    Size size = new Size();

                    size.size1 = item.Value;
                    size.idproduct = product.Idproduct;
                    size.Product = product;

                    product.Sizes.Add(size);

                }
            }

            if (checkforsize == false)
            {
                ModelState.AddModelError("checks", " Please Enter Size ");

                productA.UpdateProduct(p.Idproduct);

                productA.dataforSelectsize();
                productA.dataforSelectcolor();

                return View(productA);

            }
            if (checkforcolor == false)
            {
                ModelState.AddModelError("checkc", " Please Enter Color  ");

                productA.UpdateProduct(p.Idproduct);

                productA.dataforSelectsize();
                productA.dataforSelectcolor();

                return View(productA);
            }

            _magentaDataContext.Entry(product).State = EntityState.Modified;
            _magentaDataContext.SaveChanges();

            _magentaDataContext.Sizes.RemoveRange(_magentaDataContext.Sizes.Where(c => c.idproduct == product.Idproduct));
            _magentaDataContext.SaveChanges();

            _magentaDataContext.Sizes.AddRange(product.Sizes).Where(x => x.idproduct == product.Idproduct);
            _magentaDataContext.SaveChanges();

            _magentaDataContext.Colors.RemoveRange(_magentaDataContext.Colors.Where(c => c.Idprodact == product.Idproduct));
            _magentaDataContext.SaveChanges();

            _magentaDataContext.Colors.AddRange(product.Colors).Where(x => x.Idprodact == product.Idproduct);
            _magentaDataContext.SaveChanges();


            return RedirectToAction("HomeDealer", "Home");
        }

        [HttpGet]
        public ActionResult Offer(int Idproduct)
        {
            Product p = _magentaDataContext.Products.FirstOrDefault(x => x.Idproduct == Idproduct);

            return View(p);
        }

        [HttpPost]
        public ActionResult Offer()
        {
            return View();
        }

        public ActionResult All_Product()
        {
            return View();
        }

        public ActionResult Products_For_Admin()
        {
            return View();
        }

    }
}
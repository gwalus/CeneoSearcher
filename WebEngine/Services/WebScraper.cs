using HtmlAgilityPack;
using Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebEngine.Interfaces;
using WebEngine.Model;

namespace WebEngine.Services
{
  
    public class WebScraper : IWebScraper
    {
        private readonly HtmlWeb _web;
        private List<Product> _products;
        public WebScraper()
        {
            _web = new HtmlWeb();
            _products = new List<Product>();
        }
        /// <summary>
        /// Method that allow us to scrap the information from the page 
        /// <paramref name="html">Link to list of products </paramref>
        /// </summary>
        /// <returns>List of Products</returns>
        public List<Product> GetListOfProducts(string html)
        {
            var htmlDoc = _web.Load(html);
            try
            {
                foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//div/div")
                    .Where(x => x.GetAttributeValue("class", string.Empty)
                    .Equals("cat-prod-row__body"))
                    .ToList())
                {

                    Product element = new Product();
                    element.Link = link.SelectSingleNode("div/div/div/div/strong/a")
                                        .GetAttributeValue("href", string.Empty).Remove(0,1);
                    element.Name = link.SelectSingleNode("div/div/div/div/strong/a")
                                        .InnerText;

                    if (link.SelectSingleNode("div/a/img")
                            .GetAttributeValue("src", string.Empty)
                            .Equals("/content/img/icons/pix-empty.png"))
                    {
                        element.Image = link.SelectSingleNode("div/a/img")
                                            .GetAttributeValue("data-original", string.Empty).Insert(0,"http:");
                    }
                    else
                    {
                        element.Image = link.SelectSingleNode("div/a/img")
                                            .GetAttributeValue("src", string.Empty).Insert(0, "http:");
                    }

                    try
                    {
                        element.Rate = link.SelectNodes("div//span")
                                            .Where(x => x.GetAttributeValue("class", string.Empty)
                                            .Equals("product-score"))
                                            .First()
                                            .InnerText
                                            .Trim()
                                            .Remove(3);
                    }
                    catch (Exception)
                    {
                        element.Rate = string.Empty;
                    }
                    element.Price = link.SelectNodes("div/div")
                                        .Where(x => x.GetAttributeValue("class", string.Empty)
                                        .Equals("cat-prod-row__price"))
                                        .First()
                                        .SelectSingleNode("a//span/span")
                                        .InnerText;

                    _products.Add(element);
                }
                return _products;
            }
            catch (Exception)
            {
                return _products;
            }
            
        }
        /// <summary>
        /// This method enter the link from the parameter and take the lowest price of the product if possible
        /// </summary>
        /// <param name="link">Link to the specified product</param>
        /// <returns>Single product price</returns>
        public double GetProductPrice(string link)
        {
            if (link.Contains("Click"))
            {
                return 0;
            }
            var htmlDoc = _web.Load("https://www.ceneo.pl/"+$"{link}");
            var node = htmlDoc.DocumentNode.SelectSingleNode($"//script");           

            try
            {
                var productDetail = JsonSerializer.Deserialize<ProductInteriorPrice>(node.InnerText);

                return productDetail.offers.lowPrice;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

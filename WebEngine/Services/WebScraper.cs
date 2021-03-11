using HtmlAgilityPack;
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
                                        .GetAttributeValue("href", string.Empty);
                    element.Name = link.SelectSingleNode("div/div/div/div/strong/a")
                                        .InnerText;

                    if (link.SelectSingleNode("div/a/img")
                            .GetAttributeValue("src", string.Empty)
                            .Equals("/content/img/icons/pix-empty.png"))
                    {
                        element.Image = link.SelectSingleNode("div/a/img")
                                            .GetAttributeValue("data-original", string.Empty);
                    }
                    else
                    {
                        element.Image = link.SelectSingleNode("div/a/img")
                                            .GetAttributeValue("src", string.Empty);
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
                        element.Rate = "Brak danych";
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

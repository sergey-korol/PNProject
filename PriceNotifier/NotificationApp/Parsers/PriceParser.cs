﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace NotificationApp.Parsers
{
    public class PriceParser : IPriceParser
    {
        public double? Parse(string html)
        {
            string price = null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            if (doc.DocumentNode.SelectNodes("//a[@class='b-offers-desc__info-price-value b-offers-desc__info-price-value_primary']") != null)
            {
                foreach (var htmlNode in doc.DocumentNode.SelectNodes("//a[@class='b-offers-desc__info-price-value b-offers-desc__info-price-value_primary']"))
                {
                    var node = htmlNode.InnerText.Trim('-');
                    string[] numbers = Regex.Split(node, @"\D+");
                    price = numbers[0] + "," + numbers[1];

                    if (numbers[1].Length == 3)
                    {
                        if (numbers[2].Length == 3)
                        {
                            price = numbers[0] + numbers[1] + numbers[2] + "," + numbers[3];
                        }
                        price = numbers[0] + numbers[1] + "," + numbers[2];
                    }
                }

                double finalPrice;
                if (double.TryParse(price, out finalPrice))
                {
                    return finalPrice;
                }
            }

            var nodes = doc.QuerySelectorAll(".price span [itemprop=lowPrice]").FirstOrDefault();
            if (!string.IsNullOrEmpty(nodes?.InnerText))
            {
                var p = nodes.InnerText;
                double finalPrice;
                if (double.TryParse(p, out finalPrice))
                {
                    return finalPrice;
                }
                p = p.Replace('.', ',');
                return double.Parse(p);
            }

            return null;
        }
    }
}

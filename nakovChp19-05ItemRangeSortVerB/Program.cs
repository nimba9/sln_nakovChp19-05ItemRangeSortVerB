using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;
using System.IO;

namespace CompanyArticles
{
    class Article : IComparable<Article>
    {
        private string barcode;
        private string vendor;
        private string title;
        private decimal price;
        public Article(string barcode, string vendor, string title, decimal price)
        {
            this.barcode = barcode;
            this.vendor = vendor;
            this.Title = title;
            this.price = price;
        }

        public string Barcode
        {
            get
            {
                return this.barcode;
            }

            set
            {
                this.barcode = value;
            }
        }

        public string Vendor
        {
            get
            {
                return this.vendor;
            }

            set
            {
                this.vendor = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
            }
        }

        public override string ToString()
        {
            return "Title: " + this.Title + " Price: " + this.price + " Barcode: " + this.barcode + " Vendor: " + this.vendor;
        }

        public int CompareTo(Article obj)
        {
            int compare = this.price.CompareTo(obj.price);
            if (compare == 0)
            {
                compare = this.title.CompareTo(obj.title);
                if (compare == 0)
                {
                    compare = this.vendor.CompareTo(obj.vendor);
                    if (compare == 0)
                    {
                        return this.barcode.CompareTo(obj.barcode);
                    }

                    return this.vendor.CompareTo(obj.vendor);
                }

                return this.title.CompareTo(obj.title);
            }

            return this.price.CompareTo(obj.price);
        }
    }
 
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            OrderedMultiDictionary<decimal, Article> firmArticles = new OrderedMultiDictionary<decimal, Article>(true);
            
            decimal startRange;
            decimal endRange;
            
            Console.WriteLine("Please input the price range separated by coma:");
            string[] priceRange = Console.ReadLine().Split(',');
            startRange = decimal.Parse(priceRange[0]);
            endRange = decimal.Parse(priceRange[1]);

            Console.WriteLine("Please input 4 items: barcode number, vendor name, item title and price number: ");
            int merchandiseCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < merchandiseCount; i++)
            {
                string[] articleInfo = Console.ReadLine().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                decimal articlePrice = decimal.Parse(articleInfo[3]);
                Article currentArticle = new Article(articleInfo[0], articleInfo[1], articleInfo[2], articlePrice);
                firmArticles.Add(articlePrice, currentArticle);
            }

            var searchedArticles = firmArticles.Range(startRange, true, endRange, true);
            if (searchedArticles.Count == 0)
            {
                Console.WriteLine("No results found!");
                return;
            }

            StringBuilder finaldata = new StringBuilder();
            foreach (var article in searchedArticles)
            {
                foreach (var item in article.Value)
                {
                    finaldata.AppendLine(item.ToString());
                }
            }

            finaldata = finaldata.Remove(finaldata.Length - 2, 2);
            Console.WriteLine(finaldata);
        }
    }
}
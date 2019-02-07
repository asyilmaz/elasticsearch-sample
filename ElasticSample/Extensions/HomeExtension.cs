using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample.Extensions
{
    public static class HomeExtension
    {
        public static List<string> GetKeywords(string name, string lastName)
        {
            string[] names = name.Split(" ");
            string[] lastNames = lastName.Split(" ");
            List<string> keywords = new List<string>();

            keywords.AddRange(names);
            keywords.AddRange(lastNames);
            keywords.Add($"{name} {lastName}");
            return keywords;
        }
    }
}

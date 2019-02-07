using ElasticSample.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ElasticTest
{
    [TestClass]
    public class HomeTest
    {

        [TestMethod]
        public void GetKeywords_OneName_()
        {
            List<string> expected = new List<string>(new[] { "Ahmet", "Sabri", "Yýlmaz", "Ahmet Sabri Yýlmaz" });

            var actual = HomeExtension.GetKeywords("Ahmet Sabri", "Yýlmaz");
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}

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
            List<string> expected = new List<string>(new[] { "Ahmet", "Sabri", "Y�lmaz", "Ahmet Sabri Y�lmaz" });

            var actual = HomeExtension.GetKeywords("Ahmet Sabri", "Y�lmaz");
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}

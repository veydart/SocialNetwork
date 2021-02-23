using Newtonsoft.Json;
using NUnit.Framework;

namespace SocialNetwork.Tests.Extensions
{
    public static class AssertExtension
    {
        public static void IsEqual<T>(T expectedModel, T actualModel)
        {
            var actualModelText = JsonConvert.SerializeObject(actualModel, Formatting.Indented);
            var expectedModelText = JsonConvert.SerializeObject(expectedModel, Formatting.Indented);

            Assert.AreEqual(expectedModelText, actualModelText);
        }
    }
}
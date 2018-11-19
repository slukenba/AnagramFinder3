using AnagramFinder2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestAnagramFinder2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSortAnagramCharacters()
        {
            var program = new Program();
            string anagramToSort = "stop";
            string sortedAnagram = program.SortAnagramCharacters(anagramToSort);

            Assert.AreEqual("opst", sortedAnagram);
        }

        [TestMethod]
        public void TestCreateDictValue()
        {
            var program = new Program();
            string currentValue = "stop";
            string newValue = "tops";

            string resultString = program.CreateDictValue(currentValue, newValue);

            Assert.AreEqual("stop,tops", resultString);
        }
    }
}

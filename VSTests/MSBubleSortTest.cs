using System.Diagnostics;

using Algorithms;

namespace VSTests
{
    [TestClass]
    public class MSBubleSortTest
    {
        private static TestContext _testContext;
        private List<int> _testData;
        private BubleSorting alg;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testContext = context;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _testData = new List<int> { 5, 3, 8, 1, 2 };
            alg = new BubleSorting();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Debug.WriteLine($"Test '{_testContext.TestName}' completed");
        }


        [TestMethod]
        public void Sort_AlreadySortedArray_ReturnsSameArray()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };
            var expected = new int[] { 1, 2, 3, 4, 5 };

            alg.Sort(input);
            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            var input = Array.Empty<int>();
            alg.Sort(input);
            Assert.IsTrue(input.Length == 0);
        }

        [TestMethod]
        public void Sort_NullInput_ThrowsException()
        {
            alg.Sort(null);
        }


        [DataTestMethod]
        [DataRow(new int[] { 5, 3, 8, 1, 2 }, new int[] { 1, 2, 3, 5, 8 }, DisplayName = "Normal case")]
        [DataRow(new int[] { -1, -5, -3 }, new int[] { -5, -3, -1 }, DisplayName = "Negative numbers")]
        [DataRow(new int[] { 10 }, new int[] { 10 }, DisplayName = "Single element")]
        [DataRow(new int[] { 7, 7, 7 }, new int[] { 7, 7, 7 }, DisplayName = "Same element")]
        public void Sort_DataRowTests_ReturnsSortedArray(int[] input, int[] expected)
        {
            alg.Sort(input);
            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        [DynamicData(nameof(GetSortTestData), DynamicDataSourceType.Method)]
        public void Sort_DynamicDataTests_ReturnsSortedArray(int[] input, int[] expected)
        {
            alg.Sort(input);
            CollectionAssert.AreEqual(expected, input);
        }

        public static IEnumerable<object[]> GetSortTestData()
        {
            yield return new object[] { new int[] { 9, 0, 5 }, new int[] { 0, 5, 9 } };
            yield return new object[] { new int[] { 100, 50, 25 }, new int[] { 25, 50, 100 } };
        }

        [TestMethod]
        [Timeout(1000)]
        public void Sort_LargeArray_CompletesInTime()
        {
            var largeArray = new int[10000];
            var random = new Random();
            for (int i = 0; i < largeArray.Length; i++)
            {
                largeArray[i] = random.Next();
            }

            alg.Sort(largeArray);
            Assert.IsTrue(IsSorted(largeArray));
        }

        [TestMethod]
        [Ignore("Demonstrating how to skip a test")]
        public void Sort_SkippedTest_NotRun()
        {
            Assert.Fail("This test should be skipped");
        }


        [TestMethod]
        [DoNotParallelize]
        public void Sort_NonParallelTest()
        {
            var input = new int[_testData.Count];
            _testData.CopyTo(input);
            alg.Sort(input);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 5, 8 }, input);
        }


        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }
    }
}

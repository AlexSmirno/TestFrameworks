using Algorithms;

namespace NTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class NBubleSortTest
    {
        private BubleSorting alg;
        private List<int> _testData;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestContext.Progress.WriteLine("Test class initialized");
        }

        [SetUp]
        public void Setup()
        {
            alg = new BubleSorting();
            _testData = new List<int> { 5, 3, 8, 1, 2 };
            TestContext.Progress.WriteLine($"Starting test: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void Teardown()
        {
            TestContext.Progress.WriteLine($"Completed test: {TestContext.CurrentContext.Test.Name}");
        }





        [Test]
        public void Sort_AlreadySortedArray_ReturnsSameArray()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };
            var expected = new int[] { 1, 2, 3, 4, 5 };

            alg.Sort(input);
            Assert.AreEqual(expected, input);
        }

        [Test]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            var input = Array.Empty<int>();
            alg.Sort(input);
            Assert.IsEmpty(input);
        }

        [Test]
        public void Sort_NullInput_ThrowsArgumentNullException()
        {
            alg.Sort(null);
        }


        [Test]
        [TestCase(new int[] { 5, 3, 8, 1, 2 }, new int[] { 1, 2, 3, 5, 8 }, Description = "Normal case")]
        [TestCase(new int[] { -1, -5, -3 }, new int[] { -5, -3, -1 }, Description = "Negative numbers")]
        [TestCase(new int[] { 10 }, new int[] { 10 }, Description = "Single element")]
        [TestCase(new int[] { 7, 7, 7 }, new int[] { 7, 7, 7 }, Description = "Same element")]
        public void Sort_TestCases_ReturnsSortedArray(int[] input, int[] expected)
        {
            alg.Sort(input);
            Assert.AreEqual(expected, input);
        }

        [Test]
        [TestCaseSource(nameof(SortTestCases))]
        public void Sort_TestCaseSource_ReturnsSortedArray(int[] input, int[] expected)
        {
            alg.Sort(input);
            Assert.AreEqual(expected, input);
        }

        public static IEnumerable<object[]> SortTestCases()
        {
            yield return new object[] { new int[] { 9, 0, 5 }, new int[] { 0, 5, 9 } };
            yield return new object[] { new int[] { 100, 50, 25 }, new int[] { 25, 50, 100 } };
        }

        [Test]
        [MaxTime(1000)]  // Milliseconds
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

        [Test]
        [Ignore("Demonstrating how to skip a test")]
        public void Sort_SkippedTest_NotRun()
        {
            Assert.Fail("This test should be skipped");
        }



        [Test]
        [NonParallelizable]
        public void Sort_NonParallelTest()
        {
            var input = _testData.ToArray();
            alg.Sort(input);
            Assert.AreEqual(new int[] { 1, 2, 3, 5, 8 }, input);
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

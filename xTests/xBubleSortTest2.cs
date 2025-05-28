using Algorithms;

using Xunit.Abstractions;

namespace xTests
{
    public class xBubleSortTest2
    {
        private readonly ITestOutputHelper _output;
        private readonly BubleSorting _sorter;
        private List<int> _testData;

        public xBubleSortTest2(ITestOutputHelper output)
        {
            _output = output;
            _sorter = new BubleSorting();
            _testData = new List<int> { 5, 3, 8, 1, 2 };

            _output.WriteLine("Test initialized");
        }

        public void Dispose()
        {
            _output.WriteLine("Test cleanup");
        }

        [Theory]
        [InlineData(new int[] { 5, 3, 8, 1, 2 }, new int[] { 1, 2, 3, 5, 8 })]
        [InlineData(new int[] { -1, -5, -3 }, new int[] { -5, -3, -1 })]
        [InlineData(new int[] { 10 }, new int[] { 10 })]
        [InlineData(new int[] { 7, 7, 7 }, new int[] { 7, 7, 7 })]
        public void Sort_TheoryTests_ReturnsSortedArray(int[] input, int[] expected)
        {
            _sorter.Sort(input);
            Assert.Equal(expected, input);
        }

        [Theory]
        [MemberData(nameof(SortTestData))]
        public void Sort_MemberDataTests_ReturnsSortedArray(int[] input, int[] expected)
        {
            _sorter.Sort(input);
            Assert.Equal(expected, input);
        }

        public static IEnumerable<object[]> SortTestData()
        {
            yield return new object[] { new int[] { 9, 0, 5 }, new int[] { 0, 5, 9 } };
            yield return new object[] { new int[] { 100, 50, 25 }, new int[] { 25, 50, 100 } };
        }

        [Fact]
        public void Sort_AlreadySortedArray_ReturnsSameArray()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };
            var expected = new int[] { 1, 2, 3, 4, 5 };

            _sorter.Sort(input);
            Assert.Equal(expected, input);
        }

        [Fact]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            var input = Array.Empty<int>();
            _sorter.Sort(input);
            Assert.Empty(input);
        }

        [Fact]
        public void Sort_NullInput_ThrowsArgumentNullException()
        {
            _sorter.Sort(null);
        }

        [Fact(Timeout = 1000)]
        public void Sort_LargeArray_CompletesInTime()
        {
            var largeArray = new int[10000];
            var random = new Random();
            for (int i = 0; i < largeArray.Length; i++)
            {
                largeArray[i] = random.Next();
            }

            _sorter.Sort(largeArray);
            Assert.True(IsSorted(largeArray));
        }

        [Fact(Skip = "Demonstrating how to skip a test")]
        public void Sort_SkippedTest_NotRun()
        {
            Assert.True(false, "This test should be skipped");
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

using System;
using Xunit;

namespace KataRange.Tests
{
    public class UnitTest1
    {

        /*
         * Test Cases:
         * 
         * Creating the range
         * - [2, 9] ✓
         * - {2, 9] ✗
         * - (2, 9} ✗
         * - [2, 9) ✓
         * - (2, 9) ✓
         * - [4, -1] ✗
         * - [a, 4] ✗
         * 
         */


        [Fact]
        public void CreatingRange()
        {
            string rangeStr = "[2,9]";
            KataRange range = new KataRange(rangeStr);
        }

        [Fact]
        public void CheckLimits_left()
        {
            string rangeStr = "{2,9]";
            Assert.Throws<FormatException>(() => new KataRange(rangeStr));
        }

        [Fact]
        public void CheckLimits_right()
        {
            string rangeStr = "(2,9}";
            Assert.Throws<FormatException>(() => new KataRange(rangeStr));
        }

        [Fact]
        public void GetFirstNumber()
        {
            string rangeStr = "[2,9]";
            Assert.Equal(2, new KataRange(rangeStr).GetLower());
        }

        [Fact]
        public void GetSecondNumber()
        {
            string rangeStr = "(2,9)";
            Assert.Equal(8, new KataRange(rangeStr).GetUpper());
        }

        [Fact]
        public void GetValidNumber()
        {
            string rangeStr = "[a,4]";
            Assert.Throws<FormatException>(() => new KataRange(rangeStr));
        }

        [Fact]
        public void RangeCrossing()
        {
            string rangeStr = "[4,-1)";
            Assert.Throws<FormatException>(() => new KataRange(rangeStr));
        }

        /*
         * Test Cases:
         * 
         * Executing the methods
         *
         * CONTAINS
         * - [2,9] contains 2,4,5,6 ✓
         * - (2,9) contains 2,3,9 ✗
         * - (2,9) contains 1,3,8 ✗
         * 
         * ALLPOINTS
         * - (2,9)
         * - [2,9]
         * - (-1,9]
         * 
         * CONTAINSRANGE
         * - [2,9] constains [3,4] ✓
         * - (2,9) conatins [2,4] ✗
         * - [2,9) conatins [2,10] ✗
         * 
         * ENDPOINTS
         * - [2,9] endpoints 2, 9 ✓
         * - (2,9] endpoints 3, 9 ✓
         * - [2,9) endpoints 2, 8 ✓
         * - (2,9) endpoints 3, 8 ✓
         * 
         * OVERLAPS
         * - (2,9) overlaps (-1,3) ✓
         * - (2,9) overlaps (4,15) ✓
         * - (2,9) overlaps (4,7) ✗
         * - (2,9) overlaps (15,20) ✗
         * 
         * EQULAS
         * - (2,9) equals (2,9)
         * - [2,8) !equals (2,9)
         * 
         */

        [Fact]
        public void ValidContains()
        {
            int[] integers = new int[] { 2, 4, 5, 6 };
            string rangeStr = "[2, 9]";
            KataRange range = new KataRange(rangeStr);
            Assert.True(range.Contains(integers));
        }

        [Fact]
        public void InvalidContainsLimit()
        {
            int[] integers = new int[] { 2, 3, 9 };
            string rangeStr = "(2, 9)";
            KataRange range = new KataRange(rangeStr);
            Assert.False(range.Contains(integers));
        }

        [Fact]
        public void InvalidContainsOutside()
        {
            int[] integers = new int[] { 1, 3, 8 };
            string rangeStr = "(2, 9)";
            KataRange range = new KataRange(rangeStr);
            Assert.False(range.Contains(integers));
        }

        [Fact]
        public void GetPointsOpen()
        {
            int[] expectedIntegers = new int[] { 3, 4, 5, 6, 7, 8 };
            string rangeStr = "(2, 9)";
            KataRange range = new KataRange(rangeStr);
            Assert.Equal(expectedIntegers, range.getAllPoints());
        }

        [Fact]
        public void GetPointsClosed()
        {
            int[] expectedIntegers = new int[] { 2, 3, 4, 5, 6, 7, 8, 9  };
            string rangeStr = "[2, 9]";
            KataRange range = new KataRange(rangeStr);
            Assert.Equal(expectedIntegers, range.getAllPoints());
        }

        [Fact]
        public void GetPointsMixed()
        {
            int[] expectedIntegers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9  };
            string rangeStr = "(-1, 9]";
            KataRange range = new KataRange(rangeStr);
            Assert.Equal(expectedIntegers, range.getAllPoints());
        }

        [Fact]
        public void ContainsRangeNormal()
        {
            KataRange innerRange = new KataRange("[3,4]");
            KataRange outterRange = new KataRange("[2,9]");
            Assert.True(outterRange.Contains(innerRange));
        }

        [Fact]
        public void ContainsRangeLimits()
        {
            KataRange innerRange = new KataRange("[2,4]");
            KataRange outterRange = new KataRange("(2,9)");
            Assert.False(outterRange.Contains(innerRange));
        }

        [Fact]
        public void ContainsRangeMixed()
        {
            KataRange innerRange = new KataRange("[2,10]");
            KataRange outterRange = new KataRange("[2,9)");
            Assert.False(outterRange.Contains(innerRange));
        }

        [Fact]
        public void ContainsRangeClosed()
        {
            KataRange range = new KataRange("[2,9]");
            Assert.Equal(new int[] { 2, 9 }, range.EndPoints());
        }

        [Fact]
        public void ContainsRangeMixed1()
        {
            KataRange range = new KataRange("(2,9]");
            Assert.Equal(new int[] { 3, 9 }, range.EndPoints());
        }

        [Fact]
        public void ContainsRangeMixed2()
        {
            KataRange range = new KataRange("[2,9)");
            Assert.Equal(new int[] { 2, 8 }, range.EndPoints());
        }

        [Fact]
        public void ContainsRangeOpen()
        {
            KataRange range = new KataRange("(2,9)");
            Assert.Equal(new int[] { 3, 8 }, range.EndPoints());
        }

        [Fact]
        public void OverlapLeft()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange overRange = new KataRange("(-1,4)");
            Assert.True(overRange.Overlaps(mainRange));
        }

        [Fact]
        public void OverlapRigth()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange overRange = new KataRange("(4,15)");
            Assert.True(overRange.Overlaps(mainRange));
        }

        [Fact]
        public void OverlapBad1()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange overRange = new KataRange("(4,7)");
            Assert.False(overRange.Overlaps(mainRange));
        }

        [Fact]
        public void OverlapBad2()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange overRange = new KataRange("(15,20)");
            Assert.False(overRange.Overlaps(mainRange));
        }

        [Fact]
        public void Equlas()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange secondRange = new KataRange("(2,9)");
            Assert.True(mainRange.IsEqualTo(secondRange));
        }

        [Fact]
        public void NotEqulas()
        {
            KataRange mainRange = new KataRange("(2,9)");
            KataRange secondRange = new KataRange("(15,20)");
            Assert.False(mainRange.IsEqualTo(secondRange));
        }



    }
}

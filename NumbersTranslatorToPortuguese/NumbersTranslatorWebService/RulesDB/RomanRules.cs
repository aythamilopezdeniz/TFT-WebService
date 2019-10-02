using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class RomanRules
    {
        private SortedList<int, string> SortedListRomanNumbers;

        public RomanRules()
        {
            SortedListRomanNumbers = new SortedList<int, string>();
        }

        public void Initialize()
        {
            SortedRomanNumbers();
        }

        private void SortedRomanNumbers()
        {
            SortedListRomanNumbers.Add(1000, "M");
            SortedListRomanNumbers.Add(900, "CM");
            SortedListRomanNumbers.Add(500, "D");
            SortedListRomanNumbers.Add(400, "CD");
            SortedListRomanNumbers.Add(100, "C");
            SortedListRomanNumbers.Add(90, "XC");
            SortedListRomanNumbers.Add(50, "L");
            SortedListRomanNumbers.Add(40, "XL");
            SortedListRomanNumbers.Add(10, "X");
            SortedListRomanNumbers.Add(9, "IX");
            SortedListRomanNumbers.Add(5, "V");
            SortedListRomanNumbers.Add(4, "IV");
            SortedListRomanNumbers.Add(1, "I");
        }

        public SortedList<int, string> GetSortedListRomanNumbers()
        {
            return SortedListRomanNumbers;
        }
    }
}
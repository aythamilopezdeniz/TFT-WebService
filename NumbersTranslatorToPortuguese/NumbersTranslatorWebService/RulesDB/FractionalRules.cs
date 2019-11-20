using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class FractionalRules
    {
        private SortedList<string, string> SortedListSpecialNumbers { get; }

        public FractionalRules()
        {
            SortedListSpecialNumbers = new SortedList<string, string>();
        }

        public void Initialize()
        {
            SortedSpecialNumbers();
        }

        private void SortedSpecialNumbers()
        {
            SortedListSpecialNumbers.Add("2", "meio");
            SortedListSpecialNumbers.Add("3", "terço");
        }

        public SortedList<string, string> GetSortedListSpecialNumbers()
        {
            return SortedListSpecialNumbers;
        }
    }
}
using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class MultiplicativeRules
    {
        private SortedList<string, string> SortedListSpecialNumbers { get; set; }
        private SortedList<string, string> AlternativeSortedListSpecialNumbers { get; set; }

        public MultiplicativeRules()
        {
            SortedListSpecialNumbers = new SortedList<string, string>();
            AlternativeSortedListSpecialNumbers = new SortedList<string, string>();
        }

        public void Initialize()
        {
            SortedSpecialNumbers();
            SortedAlternativeSpecialNumbers();
        }

        private void SortedSpecialNumbers()
        {
            SortedListSpecialNumbers.Add("2", "dobro");
            SortedListSpecialNumbers.Add("3", "triplo");
            SortedListSpecialNumbers.Add("4", "quádruplo");
            SortedListSpecialNumbers.Add("5", "quíntuplo");
            SortedListSpecialNumbers.Add("6", "sêxtuplo");
            SortedListSpecialNumbers.Add("7", "sétuplo");
            SortedListSpecialNumbers.Add("8", "óctuplo");
            SortedListSpecialNumbers.Add("9", "nônuplo");
            SortedListSpecialNumbers.Add("10", "décuplo");
            SortedListSpecialNumbers.Add("11", "undécuplo");
            SortedListSpecialNumbers.Add("12", "duodécuplo");
            SortedListSpecialNumbers.Add("100", "cêntuplo");
        }

        private void SortedAlternativeSpecialNumbers()
        {
            AlternativeSortedListSpecialNumbers.Add("2", "duplo");
            AlternativeSortedListSpecialNumbers.Add("3", "tríplice");
        }

        public SortedList<string, string> GetSortedListSpecialNumbers()
        {
            return SortedListSpecialNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeSpecialNumbers()
        {
            return AlternativeSortedListSpecialNumbers;
        }
    }
}
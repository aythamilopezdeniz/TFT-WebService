using System.Collections;

namespace NumbersTranslatorWebService.Rules
{
    public class OrdinalRules
    {
        private SortedList SortedListUnitsNumbers { get; set; }

        public OrdinalRules()
        {
            SortedListUnitsNumbers = new SortedList();
        }

        public void SortedUnitsNumbers()
        {
            SortedListUnitsNumbers.Add(1, "primeiro");
            SortedListUnitsNumbers.Add(2, "segundo");
            SortedListUnitsNumbers.Add(3, "terceiro");
            SortedListUnitsNumbers.Add(4, "quarto");
            SortedListUnitsNumbers.Add(5, "quinto");
            SortedListUnitsNumbers.Add(6, "sexto");
            SortedListUnitsNumbers.Add(7, "sétimo");
            SortedListUnitsNumbers.Add(8, "oitavo");
            SortedListUnitsNumbers.Add(9, "nono");
            SortedListUnitsNumbers.Add(10, "décimo");
        }

        public SortedList GetSortedListNumbers()
        {
            return SortedListUnitsNumbers;
        }
    }
}
using System.Collections;

namespace NumbersTranslatorWebService.Rules
{
    public class OrdinalRules
    {
        private SortedList SortedListUnitsNumbers { get; }
        private SortedList SortedListTensNumbers { get; }
        private SortedList SortedListHundredsNumbers { get; }
        private SortedList SortedListBigNumbers { get; }

        public OrdinalRules()
        {
            SortedListUnitsNumbers = new SortedList();
            SortedListTensNumbers = new SortedList();
            SortedListHundredsNumbers = new SortedList();
            SortedListBigNumbers = new SortedList();
        }

        public void SortedUnitsNumbers()
        {
            SortedListUnitsNumbers.Add("1", "primeiro");
            SortedListUnitsNumbers.Add("2", "segundo");
            SortedListUnitsNumbers.Add("3", "terceiro");
            SortedListUnitsNumbers.Add("4", "quarto");
            SortedListUnitsNumbers.Add("5", "quinto");
            SortedListUnitsNumbers.Add("6", "sexto");
            SortedListUnitsNumbers.Add("7", "sétimo");
            SortedListUnitsNumbers.Add("8", "oitavo");
            SortedListUnitsNumbers.Add("9", "nono");
        }

        public void SortedTensNumbers()
        {
            SortedListTensNumbers.Add("10", "décimo");
            SortedListTensNumbers.Add("20", "vigésimo");
            SortedListTensNumbers.Add("30", "trigésimo");
            SortedListTensNumbers.Add("40", "quadragésimo");
            SortedListTensNumbers.Add("50", "quinquagésimo");
            SortedListTensNumbers.Add("60", "sexagésimo");
            SortedListTensNumbers.Add("70", "septuagésimo");
            SortedListTensNumbers.Add("80", "octogésimo");
            SortedListTensNumbers.Add("90", "nonagésimo");
        }

        public void SortedHundredsNumbers()
        {
            SortedListHundredsNumbers.Add("100", "centésimo");
            SortedListHundredsNumbers.Add("200", "ducentésimo");
            SortedListHundredsNumbers.Add("300", "trecentésimo");
            SortedListHundredsNumbers.Add("400", "quadringentésimo");
            SortedListHundredsNumbers.Add("500", "qüingentésimo");
            SortedListHundredsNumbers.Add("600", "sexcentésimo");
            SortedListHundredsNumbers.Add("700", "septingentésimo");
            SortedListHundredsNumbers.Add("800", "octingentésimo");
            SortedListHundredsNumbers.Add("900", "nongentésimo");
        }

        public void SortedThousandsAndMillonsNumbers()
        {
            SortedListBigNumbers.Add("1000", "milésimo");
            SortedListBigNumbers.Add("1000000", "milionésimo"); //10^6 Millón
            SortedListBigNumbers.Add("1000000000000", "bilionésimo"); //10^12 Millón
            SortedListBigNumbers.Add("1000000000000000000", "trilionésimo"); //10^18 Millón
        }

        public SortedList GetSortedListUnitsNumbers()
        {
            return SortedListUnitsNumbers;
        }

        public SortedList GetSortedListTensNumbers()
        {
            return SortedListTensNumbers;
        }

        public SortedList GetSortedListHundredsNumbers()
        {
            return SortedListHundredsNumbers;
        }
    }
}
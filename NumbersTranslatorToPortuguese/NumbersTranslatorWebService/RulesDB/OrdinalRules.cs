using System.Collections;
using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class OrdinalRules
    {
        private SortedList<string, string> SortedListUnitsNumbers { get; }
        private SortedList<string, string> SortedListTensNumbers { get; }
        private SortedList<string, string> SortedListHundredsNumbers { get; }
        private SortedList<string, string> SortedListMillonsNumbers { get; }
        private SortedList<string, string> AlternativeSortedListSpecialsNumbers { get; }
        private SortedList<string, string> AlternativeSortedListTensNumbers { get; }
        private SortedList<string, string> AlternativeSortedListHundredsNumbers { get; }

        public OrdinalRules()
        {
            SortedListUnitsNumbers = new SortedList<string, string>();
            SortedListTensNumbers = new SortedList<string, string>();
            SortedListHundredsNumbers = new SortedList<string, string>();
            SortedListMillonsNumbers = new SortedList<string, string>();
            AlternativeSortedListTensNumbers = new SortedList<string, string>();
            AlternativeSortedListSpecialsNumbers = new SortedList<string, string>();
            AlternativeSortedListHundredsNumbers = new SortedList<string, string>();
        }

        public void Initialize()
        {
            SortedUnitsNumbers();
            SortedTensNumbers();
            SortedHundredsNumbers();
            SortedMillonsNumbers();
            AlternativeSortedTensNumbers();
            AlternativeSortedSpecialNumbers();
            AlternativeSortedHundredsNumbers();
        }

        private void SortedUnitsNumbers()
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

        private void SortedTensNumbers()
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

        private void SortedHundredsNumbers()
        {
            SortedListHundredsNumbers.Add("100", "centésimo");
            SortedListHundredsNumbers.Add("200", "ducentésimo");
            SortedListHundredsNumbers.Add("300", "tricentésimo");
            SortedListHundredsNumbers.Add("400", "quadringentésimo");
            SortedListHundredsNumbers.Add("500", "quingentésimo");
            SortedListHundredsNumbers.Add("600", "sexcentésimo");
            SortedListHundredsNumbers.Add("700", "septingentésimo");
            SortedListHundredsNumbers.Add("800", "octingentésimo");
            SortedListHundredsNumbers.Add("900", "nongentésimo");
        }

        private void SortedMillonsNumbers()
        {
            SortedListMillonsNumbers.Add("1000000", "milionésimo"); //Millón 10^6
            SortedListMillonsNumbers.Add("1000000000000", "bilionésimo"); //Un billón 10^12
            SortedListMillonsNumbers.Add("1000000000000000000", "trilionésimo"); //Un trillón 10^18
            SortedListMillonsNumbers.Add("1000000000000000000000000", "quatrilionésimo"); //Un cuatrillón 10^24
            SortedListMillonsNumbers.Add("1000000000000000000000000000000", "quintilionésimo"); //Un quintillón 10^30
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000", "sextilionésimo"); //Un sextillón 10^36
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000", "septilionésimo"); //Un septillón 10^42
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000", "octilionésimo"); //Un optillón 10^48
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000", "nonilionésimo"); //Un nonillón 10^54
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000", "decilionésimo"); //Un decillón 10^60
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000", "undecilionésimo"); //Un undecillón 10^66
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000", "duodecilionésimo"); //Un duodecillón 10^72
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000", "tredecilionésimo"); //Un tredecillón 10^78
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quatordecilionésimo"); //Un cuatordecillón 10^84
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quindecilionésimo"); //10^48 Optillón
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "sedecilionésimo"); //10^51 Mil Optillones
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "septendecilionésimo"); //10^54 Nonillón
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "optodecilionésimo"); //10^57 Mil Nonillones
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "nonidecilionésimo"); //10^60 Decillón
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "vintidecilionésimo"); //10^63 Mil Decillones
        }

        private void AlternativeSortedTensNumbers()
        {
            AlternativeSortedListTensNumbers.Add("70", "setuagésimo");
        }

        private void AlternativeSortedSpecialNumbers()
        {
            AlternativeSortedListSpecialsNumbers.Add("11", "undécimo");
            AlternativeSortedListSpecialsNumbers.Add("12", "duodécimo");
        }

        private void AlternativeSortedHundredsNumbers()
        {
            AlternativeSortedListHundredsNumbers.Add("300", "trecentésimo");
            AlternativeSortedListHundredsNumbers.Add("400", "quadragésimo");
            AlternativeSortedListHundredsNumbers.Add("600", "seiscentésimo");
            AlternativeSortedListHundredsNumbers.Add("700", "setigentésimo");
            AlternativeSortedListHundredsNumbers.Add("800", "octogentésimo");
            AlternativeSortedListHundredsNumbers.Add("900", "noningentésimo");
        }

        public SortedList<string, string> GetSortedListUnitsNumbers()
        {
            return SortedListUnitsNumbers;
        }

        public SortedList<string, string> GetSortedListTensNumbers()
        {
            return SortedListTensNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeTensNumbers()
        {
            return AlternativeSortedListTensNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeSpecialsNumbers()
        {
            return AlternativeSortedListSpecialsNumbers;
        }

        public SortedList<string, string> GetSortedListHundredsNumbers()
        {
            return SortedListHundredsNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeHundredsNumbers()
        {
            return AlternativeSortedListHundredsNumbers;
        }

        public SortedList<string, string> GetSortedListMillonsNumbers()
        {
            return SortedListMillonsNumbers;
        }
    }
}
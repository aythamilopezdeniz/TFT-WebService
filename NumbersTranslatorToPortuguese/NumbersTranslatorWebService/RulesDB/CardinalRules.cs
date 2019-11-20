using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class CardinalRules
    {
        private SortedList<string, string> SortedListUnitsNumbers { get; }
        private SortedList<string, string> SortedListTensNumbers { get; }
        private SortedList<string, string> SortedListSpecialsNumbers { get; }
        private SortedList<string, string> SortedListHundredsNumbers { get; }
        private SortedList<string, string> SortedListMillonsNumbers { get; }
        private SortedList<string, string> AlternativeSortedListSpecialsNumbers { get; }
        private SortedList<string, string> AlternativeSortedListMillonsNumbers { get; }

        public CardinalRules()
        {
            SortedListUnitsNumbers = new SortedList<string, string>();
            SortedListTensNumbers = new SortedList<string, string>();
            SortedListHundredsNumbers = new SortedList<string, string>();
            SortedListSpecialsNumbers = new SortedList<string, string>();
            SortedListMillonsNumbers = new SortedList<string, string>();
            AlternativeSortedListSpecialsNumbers = new SortedList<string, string>();
            AlternativeSortedListMillonsNumbers = new SortedList<string, string>();
        }

        public void Initialize()
        {
            SortedUnitsNumbers();
            SortedTensNumbers();
            SortedSpecialNumbers();
            SortedHundredsNumbers();
            SortedMillonsNumbers();
            AlternativeSortedSpecialNumbers();
            AlternativeSortedMillonsNumbers();
        }

        private void SortedUnitsNumbers()
        {
            SortedListUnitsNumbers.Add("1", "um");
            SortedListUnitsNumbers.Add("2", "dois");
            SortedListUnitsNumbers.Add("3", "três");
            SortedListUnitsNumbers.Add("4", "quatro");
            SortedListUnitsNumbers.Add("5", "cinco");
            SortedListUnitsNumbers.Add("6", "seis");
            SortedListUnitsNumbers.Add("7", "sete");
            SortedListUnitsNumbers.Add("8", "oito");
            SortedListUnitsNumbers.Add("9", "nove");
        }

        private void SortedTensNumbers()
        {
            SortedListTensNumbers.Add("10", "dez");
            SortedListTensNumbers.Add("20", "vinte");
            SortedListTensNumbers.Add("30", "trinta");
            SortedListTensNumbers.Add("40", "quarenta");
            SortedListTensNumbers.Add("50", "cinquenta");
            SortedListTensNumbers.Add("60", "sessenta");
            SortedListTensNumbers.Add("70", "setenta");
            SortedListTensNumbers.Add("80", "oitenta");
            SortedListTensNumbers.Add("90", "noventa");
        }

        private void SortedSpecialNumbers()
        {
            SortedListSpecialsNumbers.Add("11", "onze");
            SortedListSpecialsNumbers.Add("12", "doze");
            SortedListSpecialsNumbers.Add("13", "treze");
            SortedListSpecialsNumbers.Add("14", "catorze");
            SortedListSpecialsNumbers.Add("15", "quinze");
            SortedListSpecialsNumbers.Add("16", "dezasseis");
            SortedListSpecialsNumbers.Add("17", "dezassete");
            SortedListSpecialsNumbers.Add("18", "dezoito");
            SortedListSpecialsNumbers.Add("19", "dezenove");
        }

        private void SortedHundredsNumbers()
        {
            SortedListHundredsNumbers.Add("100", "cem");
            SortedListHundredsNumbers.Add("200", "duzentos");
            SortedListHundredsNumbers.Add("300", "trezentos");
            SortedListHundredsNumbers.Add("400", "quatrocentos");
            SortedListHundredsNumbers.Add("500", "quinhentos");
            SortedListHundredsNumbers.Add("600", "seiscentos");
            SortedListHundredsNumbers.Add("700", "setecentos");
            SortedListHundredsNumbers.Add("800", "oitocentos");
            SortedListHundredsNumbers.Add("900", "novecentos");
        }

        private void SortedMillonsNumbers()
        {
            SortedListMillonsNumbers.Add("1000000", "milião"); // Un millón 10^6
            SortedListMillonsNumbers.Add("1000000000000", "bilião"); // Un billón 10^12
            SortedListMillonsNumbers.Add("1000000000000000000", "trilião"); // Un trillón 10^18
            SortedListMillonsNumbers.Add("1000000000000000000000000", "quadrilião"); // Un cuatrillón 10^24
            SortedListMillonsNumbers.Add("1000000000000000000000000000000", "quintilião"); // Un quintillón 10^30
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000", "sextilião"); // Un sextillón 10^36
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000", "septilião"); // Un septillón 10^42
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000", "octilião"); // Un optillón 10^48
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000", "nonilião"); // Un nonillón 10^54
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000", "decilião"); // Un decillón 10^60
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000", "undecilião"); // Un undecillón 10^66
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000", "doudecilião"); // Un duodecillón 10^72
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000", "tredecilião"); // Un tredecillón 10^78
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quatuordecilião"); // Un cuatordecillón 10^84
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quindecilião"); // Un quindecillón 10^90
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "sedecilião"); // Un sexdecillón 10^96
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "septendecilião"); // Un septendecillón 10^102
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "optodecilião"); // Un optodecillón 10^108
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "nonidecilião"); // Un novendecillón 10^114
            SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "vintilião"); // Un vigintillón 10^120
        }

        private void AlternativeSortedSpecialNumbers()
        {
            AlternativeSortedListSpecialsNumbers.Add("14", "quatorze");
        }

        private void AlternativeSortedMillonsNumbers()
        {
            AlternativeSortedListMillonsNumbers.Add("1000000", "milhão"); // Un millón 10^6
            AlternativeSortedListMillonsNumbers.Add("1000000000000", "bilhão"); // Un billón 10^12
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000", "trilhão"); // Un trillón 10^18
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000", "quatrilhão"); // Un cuatrillón 10^24
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000", "quintilhão"); // Un quintillón 10^30
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000", "sextilhão"); // Un sextillón 10^36
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000", "septilhão"); // Un septillón 10^42
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000", "octilhão"); // Un optillón 10^48
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000", "nonilhão"); // Un nonillón 10^54
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000", "decilhão"); // Un decillón 10^60
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000", "undecilhão"); // Un undecillón 10^66
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000", "dodecilhão"); // Un duodecillón 10^72
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000", "tredecilhão"); // Un tredecillón 10^78
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quatuordecilhão"); // Un cuatordecillón 10^84
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quindecilhão"); // Un quindecillón 10^90
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "sedecilhão"); // Un sexdecillón 10^96
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "septendecilhão"); // Un septendecillón 10^102
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "octodecilhão"); // Un optodecillón 10^108
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "novendecilhão"); // Un novendecillón 10^114
            AlternativeSortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "vigintilhão"); // Un vigintillón 10^120
        }

        public SortedList<string, string> GetSortedListUnitsNumbers()
        {
            return SortedListUnitsNumbers;
        }

        public SortedList<string, string> GetSortedListTensNumbers()
        {
            return SortedListTensNumbers;
        }

        public SortedList<string, string> GetSortedListHundredsNumbers()
        {
            return SortedListHundredsNumbers;
        }

        public SortedList<string, string> GetSortedListSpecialNumbers()
        {
            return SortedListSpecialsNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeSpecialNumbers()
        {
            return AlternativeSortedListSpecialsNumbers;
        }

        public SortedList<string, string> GetSortedListMillonsNumbers()
        {
            return SortedListMillonsNumbers;
        }

        public SortedList<string, string> GetSortedListAlternativeMillonsNumbers()
        {
            return AlternativeSortedListMillonsNumbers;
        }
    }
}
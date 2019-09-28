using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class FractionalRules
    {
        //private SortedList<string, string> SortedListSpecialUnitsNumbers { get; }
        //private SortedList<string, string> SortedListTensNumbers { get; }
        //private SortedList<string, string> SortedListHundredsNumbers { get; }
        //private SortedList<string, string> SortedListMillonsNumbers { get; }
        private SortedList<string, string> SortedListSpecialNumbers { get; }
        //private SortedList<string, string> AlternativeSortedListSpecialNumbers { get; }
        //private SortedList<string, string> AlternativeSortedListTensNumbers { get; }
        //private SortedList<string, string> AlternativeSortedListHundredsNumbers { get; }

        public FractionalRules()
        {
            //SortedListSpecialUnitsNumbers = new SortedList<string, string>();
            //SortedListTensNumbers = new SortedList<string, string>();
            //SortedListHundredsNumbers = new SortedList<string, string>();
            //SortedListMillonsNumbers = new SortedList<string, string>();
            SortedListSpecialNumbers = new SortedList<string, string>();
            //AlternativeSortedListSpecialNumbers = new SortedList<string, string>();
            //AlternativeSortedListTensNumbers = new SortedList<string, string>();
            //AlternativeSortedListHundredsNumbers = new SortedList<string, string>();
        }

        public void Initialize()
        {
            //SortedSpecialsUnitsNumbers();
            SortedSpecialNumbers();
            //SortedTensNumbers();
            //SortedHundredsNumbers();
            //AlternativeSortedTensNumbers();
            //AlternativeSortedHundredsNumbers();
        }

        private void SortedSpecialNumbers()
        {
            SortedListSpecialNumbers.Add("2", "meio");
            SortedListSpecialNumbers.Add("3", "terço");
            //SortedListSpecialNumbers.Add("11", "undécimo");
            //SortedListSpecialNumbers.Add("12", "duodécimo");
        }

        //private void SortedSpecialsUnitsNumbers()
        //{
        //    SortedListSpecialUnitsNumbers.Add("2", "meio");
        //    SortedListSpecialUnitsNumbers.Add("3", "terço");
        //    SortedListSpecialUnitsNumbers.Add("4", "quarto");
        //    SortedListSpecialUnitsNumbers.Add("5", "quinto");
        //    SortedListSpecialUnitsNumbers.Add("6", "sexto");
        //    SortedListSpecialUnitsNumbers.Add("7", "sétimo");
        //    SortedListSpecialUnitsNumbers.Add("8", "oitavo");
        //    SortedListSpecialUnitsNumbers.Add("9", "nono");
        //}

        //private void SortedSpecialNumbers()
        //{
        //    SortedListSpecialNumbers.Add("11", "undécimo");
        //    SortedListSpecialNumbers.Add("12", "duodécimo");
        //}

        //private void SortedTensNumbers()
        //{
        //    SortedListTensNumbers.Add("10", "décimo");
        //    SortedListTensNumbers.Add("20", "vigésimo");
        //    SortedListTensNumbers.Add("30", "trigésimo");
        //    SortedListTensNumbers.Add("40", "quadragésimo");
        //    SortedListTensNumbers.Add("50", "quinquagésimo");
        //    SortedListTensNumbers.Add("60", "sexagésimo");
        //    SortedListTensNumbers.Add("70", "septuagésimo");
        //    SortedListTensNumbers.Add("80", "octogésimo");
        //    SortedListTensNumbers.Add("90", "nonagésimo");
        //}

        //private void SortedHundredsNumbers()
        //{
        //    SortedListHundredsNumbers.Add("100", "centésimo");
        //    SortedListHundredsNumbers.Add("200", "ducentésimo");
        //    SortedListHundredsNumbers.Add("300", "trecentésimo");
        //    SortedListHundredsNumbers.Add("400", "quadringentésimo");
        //    SortedListHundredsNumbers.Add("500", "quingentésimo");
        //    SortedListHundredsNumbers.Add("600", "sexcentésimo");
        //    SortedListHundredsNumbers.Add("700", "septingentésimo");
        //    SortedListHundredsNumbers.Add("800", "octingentésimo");
        //    SortedListHundredsNumbers.Add("900", "nongentésimo");
        //}

        //private void SortedMillonsNumbers()
        //{
        //    SortedListMillonsNumbers.Add("1000000", "milionésimo");
        //    SortedListMillonsNumbers.Add("1000000000000", "billonésimo"/*"bilião"*/); // Un billón 10^12
        //    SortedListMillonsNumbers.Add("1000000000000000000", "trillonésimo"/*"trilião"*/); // Un trillón 10^18
        //    SortedListMillonsNumbers.Add("1000000000000000000000000", "quatrillonésimo"/*"quadrilião"*/); // Un cuatrillón 10^24
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000", "quintillonésimo"/*"quintilião"*/); // Un quintillón 10^30
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000", "sextillonésimo"/*"sextilião"*/); // Un sextillón 10^36
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000", "septillonésimo"/*"septilião"*/); // Un septillón 10^42
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000", "octillonésimo"/*"octilião"*/); // Un optillón 10^48
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000", "nonillonésimo"/*"nonilião"*/); // Un nonillón 10^54
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000", "decillonésimo"/*"decilião"*/); // Un decillón 10^60
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000", "undecillonésimo"/*"undelião"*/); // Un undecillón 10^66
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000", "ducodecillonésimo"/*"doudecilião"*/); // Un duodecillón 10^72
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000", "tredecillonésimo"/*"tredecilião"*/); // Un tredecillón 10^78
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "cuatordecillonésimo"/*"quatuordecilião"*/); // Un cuatordecillón 10^84
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quindecillonésimo"/*"quindecilião"*/); // Un quindecillón 10^90
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "sexdecillonésimo"/*"sedecilião"*/); // Un sexdecillón 10^96
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "septendecillonésimo"/*"septendecilião"*/); // Un septendecillón 10^102
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "optodecillonésimo"/*"optodecilião"*/); // Un optodecillón 10^108
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "novendecillonésimo"/*"nonidecilião"*/); // Un novendecillón 10^114
        //    SortedListMillonsNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "vigintillonésimo"/*"vintilião"*/); // Un vigintillón 10^120
        //}

        //private void AlternativeSortedTensNumbers()
        //{
        //    AlternativeSortedListTensNumbers.Add("70", "setuagésimo");
        //}

        //private void AlternativeSortedHundredsNumbers()
        //{
        //    AlternativeSortedListHundredsNumbers.Add("300", "trecentésimo");
        //    AlternativeSortedListHundredsNumbers.Add("400", "quadragésimo");
        //    AlternativeSortedListHundredsNumbers.Add("600", "seiscentésimo");
        //    AlternativeSortedListHundredsNumbers.Add("700", "setigentésimo");
        //    AlternativeSortedListHundredsNumbers.Add("800", "octogentésimo");
        //    AlternativeSortedListHundredsNumbers.Add("900", "noningentésimo");
        //}

        //public SortedList<string, string> GetSortedListSpecialUnitsNumbers()
        //{
        //    return SortedListSpecialUnitsNumbers;
        //}

        public SortedList<string, string> GetSortedListSpecialNumbers()
        {
            return SortedListSpecialNumbers;
        }

        //public SortedList<string, string> GetSortedListTensNumbers()
        //{
        //    return SortedListTensNumbers;
        //}

        //public SortedList<string, string> GetSortedListAlternativeTensNumbers()
        //{
        //    return AlternativeSortedListTensNumbers;
        //}

        //public SortedList<string, string> GetSortedListHundredsNumbers()
        //{
        //    return SortedListHundredsNumbers;
        //}

        //public SortedList<string, string> GetSortedListAlternativeHundredsNumbers()
        //{
        //    return AlternativeSortedListHundredsNumbers;
        //}
    }
}
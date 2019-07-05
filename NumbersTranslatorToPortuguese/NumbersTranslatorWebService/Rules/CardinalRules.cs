using System.Collections;
using System.Collections.Generic;

namespace NumbersTranslatorWebService.Rules
{
    public class CardinalRules
    {
        private SortedList SortedListUnitsNumbers { get; }
        private SortedList SortedListTensNumbers { get; }
        private SortedList SortedListHundredsNumbers { get; }
        private SortedList<string, string> SortedListShortScaleNumbers { get; }
        private SortedList<string, string> SortedListLongScaleNumbers { get; }
        private SortedList SortedListSpecialNumbers { get; }

        public CardinalRules()
        {
            SortedListUnitsNumbers = new SortedList();
            SortedListTensNumbers = new SortedList();
            SortedListHundredsNumbers = new SortedList();
            SortedListSpecialNumbers = new SortedList();
            SortedListShortScaleNumbers = new SortedList<string, string>();
            SortedListLongScaleNumbers = new SortedList<string, string>();
        }

        public void SortedUnitsNumbers()
        {
            SortedListUnitsNumbers.Add(1, "um");
            SortedListUnitsNumbers.Add(2, "dois");
            SortedListUnitsNumbers.Add(3, "três");
            SortedListUnitsNumbers.Add(4, "quatro");
            SortedListUnitsNumbers.Add(5, "cinco");
            SortedListUnitsNumbers.Add(6, "seis");
            SortedListUnitsNumbers.Add(7, "sete");
            SortedListUnitsNumbers.Add(8, "oito");
            SortedListUnitsNumbers.Add(9, "nove");
        }

        public void SortedTensNumbers()
        {
            SortedListTensNumbers.Add(10, "dez");
            SortedListTensNumbers.Add(20, "vinte");
            SortedListTensNumbers.Add(30, "trinta");
            SortedListTensNumbers.Add(40, "quarenta");
            SortedListTensNumbers.Add(50, "cinquenta");
            SortedListTensNumbers.Add(60, "sessenta");
            SortedListTensNumbers.Add(70, "setenta");
            SortedListTensNumbers.Add(80, "oitenta");
            SortedListTensNumbers.Add(90, "noventa");
        }

        public void SortedSpecialNumbers()
        {
            SortedListSpecialNumbers.Add(11, "onze");
            SortedListSpecialNumbers.Add(12, "doze");
            SortedListSpecialNumbers.Add(13, "treze");
            SortedListSpecialNumbers.Add(14, "catorze");
            SortedListSpecialNumbers.Add(15, "quinze");
            SortedListSpecialNumbers.Add(16, "dezasseis");
            SortedListSpecialNumbers.Add(17, "dezassete");
            SortedListSpecialNumbers.Add(18, "dezoito");
            SortedListSpecialNumbers.Add(19, "dezenove");
        }

        public void SortedHundredsNumbers()
        {
            SortedListHundredsNumbers.Add(100, "cem");
            SortedListHundredsNumbers.Add(200, "duzentos");
            SortedListHundredsNumbers.Add(300, "trezentos");
            SortedListHundredsNumbers.Add(400, "quatrocentos");
            SortedListHundredsNumbers.Add(500, "quinhentos");
            SortedListHundredsNumbers.Add(600, "seisentos");
            SortedListHundredsNumbers.Add(700, "setecentos");
            SortedListHundredsNumbers.Add(800, "oitocentos");
            SortedListHundredsNumbers.Add(900, "novecentos");
        }

        public void SortedShortScaleNumbers()
        {
            SortedListShortScaleNumbers.Add("1000000", "milhão");
            SortedListShortScaleNumbers.Add("1000000000", "bilhâo");
        }

        public void SortedLongScaleNumbers()
        {
            SortedListLongScaleNumbers.Add("1000000", "milhão"); // Un millón 10^6
            SortedListLongScaleNumbers.Add("1000000000000", "bilião"); // Un billón 10^12
            SortedListLongScaleNumbers.Add("1000000000000000000", "trilião"); // Un trillón 10^18
            SortedListLongScaleNumbers.Add("1000000000000000000000000", "quadrilião"); // Un cuatrillón 10^24
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000", "quintilião"); // Un quintillón 10^30
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000", "sextilião"); // Un sextillón 10^36
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000", "septilião"); // Un septillón 10^42
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000", "octilião"); // Un optillón 10^48
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000", "nonilião"); // Un nonillón 10^54
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000", "decilião"); // Un decillón 10^60
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000", "undelião"); // Un undecillón 10^66
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000", "doudecilião"); // Un duodecillón 10^72
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000", "tredecilião"); // Un tredecillón 10^78
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quatuordecilião"); // Un cuatordecillón 10^84
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "quindecilião"); // Un quindecillón 10^90
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "sedecilião"); // Un sexdecillón 10^96
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "septendecilião"); // Un septendecillón 10^102
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "optodecilião"); // Un optodecillón 10^108
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "nonidecilião"); // Un novendecillón 10^114
            SortedListLongScaleNumbers.Add("1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", "vintilião"); // Un vigintillón 10^120
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

        public SortedList GetSortedListSpecialNumbers()
        {
            return SortedListSpecialNumbers;
        }

        public SortedList<string, string> GetSortedListShortScaleNumbers()
        {
            return SortedListShortScaleNumbers;
        }

        public SortedList<string, string> GetSortedListLongScaleNumbers()
        {
            return SortedListLongScaleNumbers;
        }
    }
}
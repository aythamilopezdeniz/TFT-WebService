using System.Collections;

namespace NumbersTranslatorWebService.Rules
{
    public class CardinalRules
    {
        //private ArrayList UnitsNumbers { get; }
        private SortedList SortedListUnitsNumbers { get; }

        public CardinalRules()
        {
            //UnitsNumbers = new ArrayList();
            SortedListUnitsNumbers = new SortedList();
        }

        //public void ListUnitsNumbers()
        //{
        //    UnitsNumbers.Add("um");
        //    UnitsNumbers.Add("dois");
        //    UnitsNumbers.Add("três");
        //    UnitsNumbers.Add("quatro");
        //    UnitsNumbers.Add("cinco");
        //    UnitsNumbers.Add("seis");
        //    UnitsNumbers.Add("sete");
        //    UnitsNumbers.Add("oito");
        //    UnitsNumbers.Add("nove");
        //    UnitsNumbers.Add("dez");
        //}

        //public ArrayList GetUnitsNumbers()
        //{
        //    return UnitsNumbers;
        //}

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
            SortedListUnitsNumbers.Add(10, "dez");
        }

        public SortedList GetSortedListUnitsNumbers()
        {
            return SortedListUnitsNumbers;
        }
    }
}
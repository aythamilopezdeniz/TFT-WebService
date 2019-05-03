using NumbersTranslatorWebService.Rules;
using System;
using System.Collections;

namespace NumbersTranslatorWebService.Entities
{
    public class Cardinal : Number
    {
        private string Sign { get; set; }

        public Cardinal(string dato)
        {
            Sign = "Menos";
            Initialize(dato);
        }

        public override string Translate(string number)
        {
            CardinalRules cardinalRules = new CardinalRules();
            //cardinalRules.ListUnitsNumbers();
            //ArrayList digit = cardinalRules.GetUnitsNumbers();
            //return (string) digit[Convert.ToInt32(number) - 1];
            cardinalRules.SortedUnitsNumbers();
            SortedList digit = cardinalRules.GetSortedListUnitsNumbers();
            return (string) digit[Convert.ToInt32(number)];
        }
    }
}
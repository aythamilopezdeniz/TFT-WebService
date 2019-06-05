using Entities;
using NumbersTranslatorWebService.Rules;
using System;
using System.Collections;

namespace NumbersTranslatorWebService.Entities
{
    public class Ordinal : Number
    {
        private string Sign { get; set; }

        public Ordinal(string dato)
        {
            Sign = "Menos";
            Initialize(dato);
        }

        public override string Translate(Treatment treatment)
        {
            OrdinalRules ordinalRules = new OrdinalRules();
            ordinalRules.SortedUnitsNumbers();
            SortedList list = ordinalRules.GetSortedListNumbers();
            return (string) list[Convert.ToInt32(treatment.getText())];
        }
    }
}
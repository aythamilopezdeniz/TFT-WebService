using Entities;
using NumbersTranslatorWebService.Rules;
using System.Collections;

namespace NumbersTranslatorWebService.Entities
{
    public class Ordinal : Number
    {
        private SortedList Units { get; set; }
        private SortedList Tens { get; set; }
        private SortedList Hundreds { get; set; }
        private ArrayList Digits { get; set; }

        private ArrayList sentence;

        public Ordinal(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            Digits = new ArrayList();
        }

        public override void Translate(Treatment treatment)
        //public override void Translate(string number)
        {
            GeneratedListNumbers();
            DescomposeNumber(treatment);
            //DescomposeOrdinalNumber(treatment.getText());
            //DescomposeOrdinalNumber(number);
            //return GetOrdinalNumberSentence();
        }

        private void GeneratedListNumbers()
        {
            OrdinalRules ordinalRules = new OrdinalRules();
            ordinalRules.SortedUnitsNumbers();
            ordinalRules.SortedTensNumbers();
            ordinalRules.SortedHundredsNumbers();
            Units = ordinalRules.GetSortedListUnitsNumbers();
            Tens = ordinalRules.GetSortedListTensNumbers();
            Hundreds = ordinalRules.GetSortedListHundredsNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.getIntegerNumber().Equals(true))
                DescomposeOrdinalNumber(treatment.getText());
        }

        private void DescomposeOrdinalNumber(string dato)
        {
            if (!IsMinusContains(dato))
                TransforNumber(dato);
        }

        private bool IsMinusContains(string text)
        {
            if (text.StartsWith("-")) return true;
            return false;
        }

        private void TransforNumber(string number)
        {
            if (number.Length >= 1 && !number.Equals("0"))
            {
                while (number.Length > 0)
                {
                    if (number.Length > 2)
                    {
                        WriteNumber(number.Substring(number.Length - 3));
                        number = number.Substring(0, number.Length - 3);
                    }
                    else
                    {
                        WriteNumber(number.Substring(0, number.Length));
                        number = "";
                    }
                }
            }
        }

        private void WriteNumber(string number)
        {
            string phrase = "";
            Digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                if (!phrase.Equals("")) phrase += " ";
                if ((number.Length - 1 - i) > 1)
                    phrase += Hundreds[number[i].ToString() + "00"];
                else if ((number.Length - 1 - i) == 1)
                    phrase += Tens[number[i].ToString() + "0"];
                else
                    phrase += Units[number[i].ToString()];
            }
            CheckText(phrase);
            InsertSentence(phrase);
        }

        private void CheckText(string phrase)
        {
            phrase = CheckTextThousands(phrase);
        }

        private string CheckTextThousands(string phrase)
        {
            return phrase;
        }

        private void InsertSentence(string phrase)
        {
            sentence.Insert(0, phrase);
        }

        public override string GetSentence()
        {
            string phrase = "";
            foreach (var item in sentence)
            {
                if (!item.Equals(""))
                    phrase += item + " ";
            }
            if (phrase.Equals("")) return "";
            return (char.ToUpper(phrase[0]) + phrase.Substring(1)).Trim();
        }
    }
}
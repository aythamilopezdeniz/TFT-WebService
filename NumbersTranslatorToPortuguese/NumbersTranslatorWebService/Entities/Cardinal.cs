using Entities;
using NumbersTranslatorWebService.Rules;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NumbersTranslatorWebService.Entities
{
    public class Cardinal : Number
    {
        private SortedList units;
        private SortedList tens;
        private SortedList hundreds;
        private SortedList specials;
        private SortedList<string, string> shortScale;
        private SortedList<string, string> longScale;
        private ArrayList sentence;
        private ArrayList number;
        private int Param { get; set; }

        public Cardinal(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            number = new ArrayList();
        }

        public override string Translate(Treatment treatment)
        {
            GenerateListsNumbers();
            DescomposeNumber(treatment);
            //GetSentence();
            //return (string) units[Convert.ToInt32(treatment.getText())];
            //return (string) tens[Int32.Parse(treatment.getText())];
            //return (string)sentence[0];
            return GetCardinalNumberSentence();
        }

        private void GenerateListsNumbers()
        {
            CardinalRules cardinalRules = new CardinalRules();
            //cardinalRules.ListUnitsNumbers();
            //ArrayList digit = cardinalRules.GetUnitsNumbers();
            //return (string) digit[Convert.ToInt32(number) - 1];
            cardinalRules.SortedUnitsNumbers();
            cardinalRules.SortedTensNumbers();
            cardinalRules.SortedHundredsNumbers();
            cardinalRules.SortedSpecialNumbers();
            cardinalRules.SortedShortScaleNumbers();
            cardinalRules.SortedLongScaleNumbers();
            units = cardinalRules.GetSortedListUnitsNumbers();
            tens = cardinalRules.GetSortedListTensNumbers();
            hundreds = cardinalRules.GetSortedListHundredsNumbers();
            specials = cardinalRules.GetSortedListSpecialNumbers();
            shortScale = cardinalRules.GetSortedListShortScaleNumbers();
            longScale = cardinalRules.GetSortedListLongScaleNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.getIntegerNumber().Equals(true))
                DescomposeIntegerNumber(treatment.getText());
            else if (treatment.getDecimalNumber().Equals(true))
                return;
            else if (treatment.getFractionalNumber().Equals(true))
                return;
            else if (treatment.getExponentialNumber().Equals(true))
                return;
        }

        private void DescomposeIntegerNumber(string dato)
        {
            ValidateNegativeNumber(dato, "entero");
            TransformNumber(number[0].ToString());
        }

        private void ValidateNegativeNumber(string text, string tipo)
        {
            if (text.StartsWith("-"))
            {
                if (sentence.Contains("Menos")) sentence.Remove("Menos");
                else sentence.Add("Menos");
                number.Add(Int32.Parse(text.Substring(text.IndexOf("-") + 1)));
            } else
                number.Add(Int32.Parse(text));
        }

        private void TransformNumber(string number)
        {
            int value = Int32.Parse(number);
            if (number.Length == 1 && number.Equals("0")) sentence.Add("Zero");
            else
            {
                while (value > 0)
                {
                    Translate((value % 1000).ToString());
                    value = value / 1000;
                }
            }
        }

        private void Translate(string number)
        {
            string phrase = "";
            for (int i = 0; i < number.Length; i++)
            {
                if (!phrase.Equals("") && !number[i].ToString().Equals("0")) phrase += " e ";
                if ((number.Length - 1 - i) > 1)
                {
                    if (number[i].ToString().Equals("1") &&
                        (!number[i + 1].ToString().Equals("0") || !number[i + 2].ToString().Equals("0")))
                        phrase += "cento";
                    else phrase += hundreds[Int32.Parse(number[i].ToString() + "00")];
                }
                else if ((number.Length - 1 - i) == 1)
                {
                    if (number[i].ToString().Equals("1"))
                    {
                        if (number[i + 1].ToString().Equals("0"))
                            phrase += tens[Int32.Parse(number[i].ToString() + number[i + 1].ToString())];
                        else
                            phrase += specials[Int32.Parse(number[i].ToString() + number[i + 1].ToString())];
                        i++;
                    }
                    else
                    {
                        phrase += tens[Int32.Parse(number[i].ToString() + "0")];
                    }
                }
                else
                {
                    phrase += units[Int32.Parse(number[i].ToString())];
                }
            }
            if (sentence.Contains("Menos")) sentence.Insert(1, phrase);
            else sentence.Insert(0, phrase);
        }

        private string GetCardinalNumberSentence()
        {
            string phrase = "";
            foreach (string item in sentence)
            {
                phrase += item + " ";
            }
            return (char.ToUpper(phrase[0]) + phrase.Substring(1)).Trim();
        }
    }
}
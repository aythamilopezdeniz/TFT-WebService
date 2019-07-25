using Entities;
using NumbersTranslatorWebService.Rules;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NumbersTranslatorWebService.Entities
{
    public class DecimalNumber : Number
    {
        private SortedList units;
        private SortedList tens;
        private SortedList hundreds;
        private SortedList specials;
        private SortedList<string, string> shortScale;
        private SortedList<string, string> longScale;
        private SortedList<string, int> millonsValues;
        private ArrayList sentence;
        private ArrayList digits;
        private ArrayList number;
        private string Zero { get; set; }
        private int ParamThousands { get; set; }
        private string ParamMillions { get; set; }
        private int Iter { get; set; }

        public DecimalNumber(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            number = new ArrayList();
            digits = new ArrayList();
            millonsValues = new SortedList<string, int>();
            ParamThousands = 0;
            ParamMillions = "1";
            Iter = 0;
            Zero = "";
        }

        public override void Translate(Treatment treatment)
        {
            GenerateListsNumbers();
            DescomposeNumber(treatment);
        }

        private void GenerateListsNumbers()
        {
            CardinalRules cardinalRules = new CardinalRules();
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
            ArrayList number = new ArrayList();
            if (treatment.getDecimalNumber().Equals(true))
            {
                number = GetIntegerAndDecimalPart(treatment);
                if ((number[0].ToString().Length + number[1].ToString().Length) > 126)
                    throw new InvalidNumber("1");
                TransformDecimalNumber(number[1].ToString());
                DescomposeIntegerNumber(number[0].ToString());
            }
            else if (treatment.getScientificNotationNumber().Equals(true))
            {
                if (treatment.getText().Length > 126) throw new InvalidNumber("1");
                number = GetBaseAndExponentPart(treatment);
            }
        }

        private ArrayList GetIntegerAndDecimalPart(Treatment treatment)
        {
            ArrayList decimalNumber = new ArrayList();
            if (GetNumber().Contains("."))
            {
                decimalNumber.Add(treatment.getText().Substring(0, GetNumber().IndexOf(".")));
                decimalNumber.Add(treatment.getText().Substring(GetNumber().IndexOf(".") + 1));
            }
            else if (GetNumber().Contains(","))
            {
                decimalNumber.Add(treatment.getText().Substring(0, GetNumber().IndexOf(",")));
                decimalNumber.Add(treatment.getText().Substring(GetNumber().IndexOf(",") + 1));
            }
            return decimalNumber;
        }

        private ArrayList GetBaseAndExponentPart(Treatment treatment)
        {
            ArrayList exponentialNumber = new ArrayList();
            return null;
        }

        private void DescomposeIntegerNumber(string dato)
        {
            ValidateNegativeNumber(dato);
            TransformIntegerNumber(number[0].ToString());
        }

        private void ValidateNegativeNumber(string text)
        {
            if (text.StartsWith("-"))
            {
                if (sentence.Contains("Menos")) sentence.Remove("Menos");
                else InsertSentence("Menos");
                number.Add(text.Substring(text.IndexOf("-") + 1));
            }
            else
                number.Add(text);
        }

        private void TransformIntegerNumber(string number)
        {
            if (number.Length == 1 && number.Equals("0")) InsertSentence("Zero");
            else
            {
                TakeApartNumber(number, "integer");
            }
        }

        private void TransformDecimalNumber(string number)
        {
            int numberOfZeros = CheckZerosToLeft(number);
            AddZerosToLeft(numberOfZeros);
            number = number.Substring(numberOfZeros);
            TakeApartNumber(number, "decimal");
            InsertSentence(Zero);
            InsertSentence("vírgula");
            CleanParameters();
        }

        private void CleanParameters()
        {
            Iter = 0;
            ParamThousands = 0;
            ParamMillions = "1";
            digits.RemoveRange(0, digits.Count);
            millonsValues = new SortedList<string, int>();
        }

        private int CheckZerosToLeft(string number)
        {
            int zeros = 0;
            for (int i = 0; i < number.Length; i++)
            {
                if (!number[i].ToString().Equals("0"))
                {
                    return zeros;
                }
                zeros++;
            }
            return zeros;
        }

        private void AddZerosToLeft(int numberOfZeros)
        {
            string zeros = "";
            for (int i = 0; i < numberOfZeros; i++)
            {
                zeros += "zero ";
            }
            Zero = zeros.Trim();
        }

        private void TakeApartNumber(string number, string type)
        {
            while (number.Length > 0)
            {
                if (number.Length > 2)
                {
                    WriteNumber(number.Substring(number.Length - 3), type);
                    number = number.Substring(0, number.Length - 3);
                }
                else
                {
                    WriteNumber(number.Substring(0, number.Length), type);
                    number = "";
                }
            }
        }

        private void WriteNumber(string number, string type)
        {
            string phrase = "";
            digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                Iter++;
                ParamThousands++;
                if (ParamMillions.Length < Iter) ParamMillions += "0";
                if (!phrase.Equals("") && !number[i].ToString().Equals("0")) phrase += " e ";
                if ((number.Length - 1 - i) > 1)
                {
                    if (number[i].ToString().Equals("1") &&
                        (!number[i + 1].ToString().Equals("0") || !number[i + 2].ToString().Equals("0")))
                    {
                        phrase += "cento";
                    }
                    else
                    {
                        phrase += hundreds[Int32.Parse(number[i].ToString() + "00")];
                    }
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
                        Iter++;
                        ParamThousands++;
                        if (ParamMillions.Length < Iter) ParamMillions += "0";
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
            phrase = CheckTextThousands(phrase);
            CheckTextMillons(phrase);
            ResetParameters();
            InsertSentence(phrase);
        }

        private string CheckTextThousands(string phrase)
        {
            if (digits.Count.Equals(2))
            {
                if (ParamThousands > 3 && ParamThousands < 7 && !phrase.Equals(""))
                {
                    if (Int32.Parse(digits[0].ToString()) > 1)
                    {
                        if (Int32.Parse(digits[1].ToString()) < 100 && Int32.Parse(digits[1].ToString()) > 0)
                            phrase += " mil e";
                        else if (Int32.Parse(digits[1].ToString()) >= 100 || Int32.Parse(digits[1].ToString()) == 0)
                            phrase += " mil";
                    }
                    else if (Int32.Parse(digits[0].ToString()) == 1)
                    {
                        if (Int32.Parse(digits[1].ToString()) < 100 && Int32.Parse(digits[1].ToString()) > 0)
                            phrase = " mil e";
                        else if (Int32.Parse(digits[1].ToString()) >= 100 || Int32.Parse(digits[1].ToString()) == 0)
                            phrase = "mil";
                    }
                }
            }
            return phrase;
        }

        private void CheckTextMillons(string phrase)
        {
            string aux = "";
            if (!phrase.Equals(""))
            {
                if (digits.Count.Equals(2))
                {
                    aux = ParamMillions.Substring(0, Iter - (digits[0].ToString().Length + digits[1].ToString().Length) + 1);
                    CheckAppearTimesMillons(aux);
                }
                else
                {
                    aux = ParamMillions.Substring(0, ParamMillions.Length - digits[0].ToString().Length + 1);
                    CheckAppearTimesMillons(aux);
                }
            }
        }

        private void CheckAppearTimesMillons(string millons)
        {
            if (longScale.ContainsKey(millons))
            {
                string name = longScale[millons];
                InsertMillonsValues(name);
                if (!sentence.Contains(name) && !sentence.Contains(CheckPlural(name)))
                    InsertSentence(CheckPlural(name));
                else if (sentence.Contains(name) && millonsValues[name] > 1)
                {
                    int pos = sentence.IndexOf(name);
                    sentence.Remove(name);
                    sentence.Insert(pos, CheckPlural(name));
                }
            }
        }

        private void InsertMillonsValues(string millons)
        {
            if (millonsValues.ContainsKey(millons))
                millonsValues.RemoveAt(millonsValues.IndexOfKey(millons));
            if (digits.Count.Equals(2))
                millonsValues.Add(millons, Int32.Parse(digits[0].ToString() + digits[1].ToString()));
            else
                millonsValues.Add(millons, Int32.Parse(digits[0].ToString()));
        }

        private string CheckPlural(string millons)
        {
            string value = millons;
            if (!millons.Equals(""))
            {
                if (digits.Count.Equals(2))
                {
                    if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.Substring(millons.Length - 3).Equals("ião"))
                        value = millons.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.Substring(millons.Length - 2).Equals("ão"))
                        value = millons.Replace("ão", "ões");
                }
                else if (digits.Count.Equals(1))
                {
                    if ((Int32.Parse(digits[0].ToString()) > 1) && millons.Substring(millons.Length - 3).Equals("ião"))
                        value = millons.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString()) > 1) && millons.Substring(millons.Length - 2).Equals("ão"))
                        value = millons.Replace("ão", "ões");
                }
            }
            return value;
        }

        private void ResetParameters()
        {
            if (ParamThousands > 3 && ParamThousands < 7 && digits.Count.Equals(2))
            {
                ParamThousands = 0;
                digits.RemoveRange(0, digits.Count);
            }
        }

        private void InsertSentence(string phrase)
        {
            if (sentence.Contains("Menos")) sentence.Insert(1, phrase);
            else sentence.Insert(0, phrase);
        }

        public override string GetSentence()
        {
            string phrase = "";
            foreach (string item in sentence)
            {
                if (!item.Equals(""))
                    phrase += item + " ";
            }
            if (phrase.Equals("")) return "";
            return (char.ToUpper(phrase[0]) + phrase.Substring(1)).Trim();
        }
    }
}
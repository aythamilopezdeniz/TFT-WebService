﻿using System;
using Entities;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;

namespace NumbersTranslatorWebService.Entities
{
    public class IntegerNumber : Number
    {
        private SortedList<string, string> units;
        private SortedList<string, string> tens;
        private SortedList<string, string> hundreds;
        private SortedList<string, string> specials;
        private SortedList<string, string> longScale;
        private SortedList<string, string> alternativeSpecials;
        private SortedList<string, string> alternativeLongScale;
        private SortedList<string, int> millonsValues;
        private ArrayList sentence;
        private ArrayList alternativeSentence;
        private ArrayList digits;
        private StringBuilder numberEdited;
        private int ParamThousands { get; set; }
        private StringBuilder ParamMillions { get; set; }
        private int Iter { get; set; }

        public IntegerNumber(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            alternativeSentence = new ArrayList();
            numberEdited = new StringBuilder("");
            digits = new ArrayList();
            millonsValues = new SortedList<string, int>();
            ParamThousands = 0;
            ParamMillions = new StringBuilder("1");
            Iter = 0;
        }

        public override void Translate(Treatment treatment)
        {
            GenerateListsNumbers();
            DescomposeNumber(treatment);
        }

        private void GenerateListsNumbers()
        {
            CardinalRules cardinalRules = new CardinalRules();
            cardinalRules.Initialize();
            units = cardinalRules.GetSortedListUnitsNumbers();
            tens = cardinalRules.GetSortedListTensNumbers();
            hundreds = cardinalRules.GetSortedListHundredsNumbers();
            specials = cardinalRules.GetSortedListSpecialNumbers();
            longScale = cardinalRules.GetSortedListMillonsNumbers();
            alternativeSpecials = cardinalRules.GetSortedListAlternativeSpecialNumbers();
            alternativeLongScale = cardinalRules.GetSortedListAlternativeMillonsNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.GetIntegerNumber().Equals(true) && GetTypeNumber().Equals("Cardinal"))
            {
                ValidateNegativeOrPositiveNumber(treatment.GetText());
                if (numberEdited.Length > 126) throw new InvalidNumber("1");
                TransformIntegerNumber(numberEdited);
            }
            else if (treatment.GetDecimalNumber().Equals(true) && GetTypeNumber().Equals("Decimal"))
            {
                ValidateNegativeOrPositiveNumber(treatment.GetText());
                string integerNumber = GetIntegerPart(numberEdited.ToString());
                if (integerNumber.Length > 126) throw new InvalidNumber("1");
                TransformIntegerNumber(new StringBuilder(integerNumber));
            }
            else if (treatment.GetFractionalNumber().Equals(true) && GetTypeNumber().Equals("Fractional"))
            {
                ValidateNegativeOrPositiveNumber(treatment.GetText());
                string numerator = GetNumerator(numberEdited.ToString());
                if (numerator.Length > 126) throw new InvalidNumber("1");
                TransformIntegerNumber(new StringBuilder(numerator));
            }
        }

        private void ValidateNegativeOrPositiveNumber(string text)
        {
            if (text.StartsWith("-"))
            {
                if (sentence.Contains("Menos") && alternativeSentence.Contains("Menos"))
                {
                    sentence.Remove("Menos");
                    alternativeSentence.Remove("Menos");
                }
                else
                {
                    InsertSentence(sentence, "Menos");
                    InsertSentence(alternativeSentence, "Menos");
                }
                numberEdited = new StringBuilder(text.Substring(text.IndexOf("-") + 1));
            }
            else if (text.StartsWith("+"))
                numberEdited = new StringBuilder(text.Substring(text.IndexOf("+") + 1));
            else
                numberEdited.Append(text);
        }

        private string GetIntegerPart(string number)
        {
            string integerNumber = "";
            if (number.Contains("."))
                integerNumber = number.Substring(0, number.IndexOf("."));
            else if (number.Contains(","))
                integerNumber = number.Substring(0, number.IndexOf(","));
            return integerNumber;
        }

        private string GetNumerator(string number)
        {
            string numerator = "";
            if (number.Contains("/"))
                numerator = number.Substring(0, number.IndexOf("/"));
            return numerator;
        }

        private void TransformIntegerNumber(StringBuilder number)
        {
            if (number.Length == 1 && number.Equals(new StringBuilder("0")))
            {
                InsertSentence(sentence, "Zero");
                InsertSentence(alternativeSentence, "Zero");
            }
            else
            {
                TakeApartNumber(number);
            }
        }

        private void TakeApartNumber(StringBuilder number)
        {
            while (number.Length > 0)
            {
                if (number.Length > 2)
                {
                    WriteNumber(number.ToString().Substring(number.Length - 3));
                    number = new StringBuilder(number.ToString().Substring(0, number.Length - 3));
                }
                else
                {
                    WriteNumber(number.ToString().Substring(0, number.Length));
                    number = new StringBuilder("");
                }
            }
        }

        private void WriteNumber(string number)
        {
            StringBuilder phrase = new StringBuilder("");
            StringBuilder alternative = new StringBuilder("");
            digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                Iter++;
                ParamThousands++;
                if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                if (!phrase.Equals(new StringBuilder("")) && !number[i].ToString().Equals(new StringBuilder("0").ToString())) phrase.Append(" e ");
                if (!alternative.Equals(new StringBuilder("")) && !number[i].ToString().Equals(new StringBuilder("0").ToString())) alternative.Append(" e ");
                if ((number.Length - 1 - i) > 1)
                {
                    if (number[i].ToString().Equals("1") &&
                        (!number[i + 1].ToString().Equals("0") || !number[i + 2].ToString().Equals("0")))
                    {
                        phrase.Append("cento");
                        alternative.Append("cento");
                    }
                    else
                    {
                        if (hundreds.ContainsKey(number[i].ToString() + "00"))
                        {
                            phrase.Append(hundreds[number[i].ToString() + "00"]);
                            alternative.Append(hundreds[number[i].ToString() + "00"]);
                        }
                    }
                }
                else if ((number.Length - 1 - i) == 1)
                {
                    if (number[i].ToString().Equals("1"))
                    {
                        if (number[i + 1].ToString().Equals("0"))
                        {
                            phrase.Append(tens[number[i].ToString() + number[i + 1].ToString()]);
                            alternative.Append(tens[number[i].ToString() + number[i + 1].ToString()]);
                        }
                        else
                        {
                            if (number[i + 1].ToString().Equals("4"))
                                alternative.Append(alternativeSpecials[number[i].ToString() + number[i + 1].ToString()]);
                            else
                                alternative.Append(specials[number[i].ToString() + number[i + 1].ToString()]);
                            phrase.Append(specials[number[i].ToString() + number[i + 1].ToString()]);
                        }
                        i++;
                        Iter++;
                        ParamThousands++;
                        if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                    }
                    else
                    {
                        if (tens.ContainsKey(number[i].ToString() + "0"))
                        {
                            phrase.Append(tens[number[i].ToString() + "0"]);
                            alternative.Append(tens[number[i].ToString() + "0"]);
                        }
                    }
                }
                else
                {
                    if (units.ContainsKey(number[i].ToString()))
                    {
                        phrase.Append(units[number[i].ToString()]);
                        alternative.Append(units[number[i].ToString()]);
                    }
                }
            }
            phrase = CheckTextThousands(phrase);
            alternative = CheckTextThousands(alternative);
            CheckTextMillons(phrase);
            ResetParameters();
            InsertSentence(sentence, phrase.ToString());
            InsertSentence(alternativeSentence, alternative.ToString());
        }

        private StringBuilder CheckTextThousands(StringBuilder phrase)
        {
            if (digits.Count.Equals(2))
            {
                if (ParamThousands > 3 && ParamThousands < 7 && !phrase.Equals(""))
                {
                    if (Int32.Parse(digits[0].ToString()) > 1)
                    {
                        if (Int32.Parse(digits[1].ToString()) < 100 && Int32.Parse(digits[1].ToString()) > 0)
                            phrase.Append(" mil e");
                        else if (Int32.Parse(digits[1].ToString()) >= 100 || Int32.Parse(digits[1].ToString()) == 0)
                            phrase.Append(" mil");
                    }
                    else if (Int32.Parse(digits[0].ToString()) == 1)
                    {
                        if (Int32.Parse(digits[1].ToString()) < 100 && Int32.Parse(digits[1].ToString()) > 0)
                            phrase = new StringBuilder(" mil e");
                        else if (Int32.Parse(digits[1].ToString()) >= 100 || Int32.Parse(digits[1].ToString()) == 0)
                            phrase = new StringBuilder("mil");
                    }
                }
            }
            return phrase;
        }

        private void CheckTextMillons(StringBuilder phrase)
        {
            string aux = "";
            if (!phrase.Equals(new StringBuilder("")) && ParamMillions.Length > 6)
            {
                if (digits.Count.Equals(2))
                {
                    aux = ParamMillions.ToString().Substring(0, Iter - (digits[0].ToString().Length + digits[1].ToString().Length) + 1);
                    CheckAppearTimesMillons(aux);
                }
                else
                {
                    aux = ParamMillions.ToString().Substring(0, ParamMillions.Length - digits[0].ToString().Length + 1);
                    CheckAppearTimesMillons(aux);
                }
            }
        }

        private void CheckAppearTimesMillons(string millons)
        {
            if (longScale.ContainsKey(millons))
            {
                StringBuilder name = new StringBuilder(longScale[millons]);
                StringBuilder alternativeName = new StringBuilder(alternativeLongScale[millons]);
                InsertMillonsValues(name.ToString());
                InsertMillonsValues(alternativeName.ToString());
                if (!sentence.Contains(name.ToString()) && !sentence.Contains(CheckPlural(name.ToString())))
                    InsertSentence(sentence, CheckPlural(name.ToString()));
                else if (sentence.Contains(name.ToString()) && millonsValues[name.ToString()] > 1)
                {
                    int pos = sentence.IndexOf(name.ToString());
                    sentence.Remove(name.ToString());
                    sentence.Insert(pos, CheckPlural(name.ToString()));
                }
                if (!alternativeSentence.Contains(alternativeName.ToString()) && 
                    !alternativeSentence.Contains(CheckPlural(alternativeName.ToString())))
                    InsertSentence(alternativeSentence, CheckPlural(alternativeName.ToString()));
                else if (alternativeSentence.Contains(alternativeName.ToString()) && 
                    millonsValues[alternativeName.ToString()] > 1)
                {
                    int pos = alternativeSentence.IndexOf(alternativeName.ToString());
                    alternativeSentence.Remove(alternativeName.ToString());
                    alternativeSentence.Insert(pos, CheckPlural(alternativeName.ToString()));
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
            StringBuilder value = new StringBuilder(millons);
            if (!millons.Equals(new StringBuilder("")))
            {
                if (digits.Count.Equals(2))
                {
                    if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.ToString().Substring(millons.Length - 3).Equals("ião"))
                        value = value.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.ToString().Substring(millons.Length - 2).Equals("ão"))
                        value = value.Replace("ão", "ões");
                }
                else if (digits.Count.Equals(1))
                {
                    if ((Int32.Parse(digits[0].ToString()) > 1) && millons.ToString().Substring(millons.Length - 3).Equals("ião"))
                        value = value.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString()) > 1) && millons.ToString().Substring(millons.Length - 2).Equals("ão"))
                        value = value.Replace("ão", "ões");
                }
            }
            return value.ToString();
        }

        private void ResetParameters()
        {
            if (ParamThousands > 3 && ParamThousands < 7 && digits.Count.Equals(2))
            {
                ParamThousands = 0;
                digits.RemoveRange(0, digits.Count);
            }
        }

        private void InsertSentence(ArrayList list, string phrase)
        {
            if (list.Contains("Menos")) list.Insert(1, phrase);
            else list.Insert(0, phrase);
        }

        public override List<string> GetResults()
        {
            List<string> results = new List<string>();
            string firtsResult = GetSentence(sentence);
            string secondResult = GetSentence(alternativeSentence);
            if (firtsResult.Equals("")) return new List<string>();
            if (secondResult.Equals("")) return new List<string>() { firtsResult };
            if (IsSentencesEquals(firtsResult, secondResult))
                results.Add(firtsResult);
            else
            {
                results.Add(firtsResult);
                results.Add(secondResult);
            }
            return results;
        }

        private string GetSentence(ArrayList list)
        {
            StringBuilder phrase = new StringBuilder("");
            foreach (string item in list)
            {
                if (!item.Equals(""))
                    phrase.Append(new StringBuilder(item + " "));
            }
            if (phrase.Equals(new StringBuilder(""))) return "";
            return (char.ToUpper(phrase[0]) + phrase.ToString().Substring(1)).Trim();
        }

        private bool IsSentencesEquals(string firstResult, string secondResult)
        {
            return firstResult.Equals(secondResult);
        }
    }
}
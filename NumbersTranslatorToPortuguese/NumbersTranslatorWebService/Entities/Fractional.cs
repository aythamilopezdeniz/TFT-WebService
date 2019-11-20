using System;
using Entities;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;

namespace NumbersTranslatorWebService.Entities
{
    public class Fractional : Number
    {
        private ArrayList sentence;
        private ArrayList alternativeSentence1;
        private ArrayList alternativeSentence2;
        private ArrayList alternativeSentence3;
        private SortedList<string, string> unitsOrdinal;
        private SortedList<string, string> tensOrdinal;
        private SortedList<string, string> hundredsOrdinal;
        private SortedList<string, string> largeOrdinal;
        private SortedList<string, string> alternativeTensOrdinal;
        private SortedList<string, string> specialsOrdinal;
        private SortedList<string, string> alternativeHundredsOrdinal;
        private SortedList<string, string> unitsCardinal;
        private SortedList<string, string> tensCardinal;
        private SortedList<string, string> specialTensCardinal;
        private SortedList<string, string> hundredsCardinal;
        private SortedList<string, string> largeCardinal;
        private SortedList<string, string> specialAlternativeTensCardinal;
        private SortedList<string, string> largeAlternativeCardinal;
        private StringBuilder numberEdited;
        private ArrayList digits;
        private int Iter;
        private int ParamThousands;
        private StringBuilder ParamMillions;
        private SortedList<string, int> millonsValues;
        private SortedList<string, string> specialFractional;

        public Fractional(string dato)
        {
            Initialize(dato);
            Iter = 0;
            ParamThousands = 0;
            digits = new ArrayList();
            numberEdited = new StringBuilder("");
            sentence = new ArrayList();
            alternativeSentence1 = new ArrayList();
            alternativeSentence2 = new ArrayList();
            alternativeSentence3 = new ArrayList();
            ParamMillions = new StringBuilder("1");
            millonsValues = new SortedList<string, int>();
        }

        public override void Translate(Treatment treatment)
        {
            GeneratedListNumbers();
            DescomposeNumber(treatment);
        }

        private void GeneratedListNumbers()
        {
            FractionalRules fractionalRules = new FractionalRules();
            fractionalRules.Initialize();
            specialFractional = fractionalRules.GetSortedListSpecialNumbers();
            OrdinalRules ordinalRules = new OrdinalRules();
            ordinalRules.Initialize();
            unitsOrdinal = ordinalRules.GetSortedListUnitsNumbers();
            tensOrdinal = ordinalRules.GetSortedListTensNumbers();
            hundredsOrdinal = ordinalRules.GetSortedListHundredsNumbers();
            largeOrdinal = ordinalRules.GetSortedListMillonsNumbers();
            alternativeTensOrdinal = ordinalRules.GetSortedListAlternativeTensNumbers();
            specialsOrdinal = ordinalRules.GetSortedListAlternativeSpecialsNumbers();
            alternativeHundredsOrdinal = ordinalRules.GetSortedListAlternativeHundredsNumbers();
            CardinalRules cardinalRules = new CardinalRules();
            cardinalRules.Initialize();
            unitsCardinal = cardinalRules.GetSortedListUnitsNumbers();
            tensCardinal = cardinalRules.GetSortedListTensNumbers();
            specialTensCardinal = cardinalRules.GetSortedListSpecialNumbers();
            hundredsCardinal = cardinalRules.GetSortedListHundredsNumbers();
            largeCardinal = cardinalRules.GetSortedListMillonsNumbers();
            specialAlternativeTensCardinal = cardinalRules.GetSortedListAlternativeSpecialNumbers();
            largeAlternativeCardinal = cardinalRules.GetSortedListAlternativeMillonsNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.GetFractionalNumber().Equals(true) && GetTypeNumber().Equals("Fractional"))
            {
                StringBuilder denominator = new StringBuilder(GetDenominator(treatment.GetText()));
                ValidateNegativeOrPositiveNumber(denominator);
                if (numberEdited.Length > 126)
                    throw new InvalidNumber("1");
                TransformNumeratorNumber(numberEdited);
            }
        }

        private string GetDenominator(string number)
        {
            string denominator = "";
            if (number.Contains("/"))
                denominator = number.Substring(number.IndexOf("/") + 1);
            return denominator;
        }

        private void ValidateNegativeOrPositiveNumber(StringBuilder text)
        {
            if (text.ToString().StartsWith("-"))
            {
                if (sentence.Contains("Menos"))
                {
                    sentence.Remove("Menos");
                    alternativeSentence1.Remove("Menos");
                    alternativeSentence2.Remove("Menos");
                }
                else
                {
                    InsertSentence(sentence, "Menos");
                    InsertSentence(alternativeSentence1, "Menos");
                    InsertSentence(alternativeSentence2, "Menos");
                }
                numberEdited = new StringBuilder(text.ToString().Substring(text.ToString().IndexOf("-") + 1));
            }
            else if (text.ToString().StartsWith("+"))
                numberEdited = new StringBuilder(text.ToString().Substring(text.ToString().IndexOf("+") + 1));
            else
                numberEdited = text;
        }

        private void TransformNumeratorNumber(StringBuilder number)
        {
            if (number.Length == 1 && number.Equals(new StringBuilder("0")))
            {
                InsertSentence(sentence, "Zero");
                InsertSentence(alternativeSentence1, "Zero");
                InsertSentence(alternativeSentence2, "Zero");
            }
            else
                TakeApartNumber(number);
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
            StringBuilder alternative1 = new StringBuilder("");
            StringBuilder alternative2 = new StringBuilder("");
            StringBuilder alternative3 = new StringBuilder("");
            StringBuilder comparation = new StringBuilder("");
            digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                Iter++;
                ParamThousands++;
                if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                if (!phrase.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) phrase.Append(" ");
                if (!alternative1.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) alternative1.Append(" ");
                if (!alternative2.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) alternative2.Append(" ");
                if (!alternative3.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) alternative3.Append(" ");
                if ((number.Length - 1 - i) > 1)
                {
                    if (alternativeHundredsOrdinal.ContainsKey(number[i].ToString() + "00"))
                        alternative1.Append(alternativeHundredsOrdinal[number[i].ToString() + "00"]);
                    else
                    {
                        if (hundredsOrdinal.ContainsKey(number[i].ToString() + "00"))
                            alternative1.Append(hundredsOrdinal[number[i].ToString() + "00"]);
                    }
                    if (hundredsOrdinal.ContainsKey(number[i].ToString() + "00"))
                        phrase.Append(hundredsOrdinal[number[i].ToString() + "00"]);
                    if (number[i].ToString().Equals("1") &&
                        (!number[i + 1].ToString().Equals("0") || !number[i + 2].ToString().Equals("0")))
                    {
                            alternative2.Append("cento");
                            alternative3.Append("cento");
                    }
                    else
                    {
                        if (hundredsCardinal.ContainsKey(number[i].ToString() + "00"))
                        {
                            alternative2.Append(hundredsCardinal[number[i].ToString() + "00"]);
                            alternative3.Append(hundredsCardinal[number[i].ToString() + "00"]);
                        }
                    }
                }
                else if ((number.Length - 1 - i) == 1)
                {
                    comparation.Append(number[i].ToString());
                    if (alternativeTensOrdinal.ContainsKey(number[i].ToString() + "0"))
                        alternative1.Append(alternativeTensOrdinal[number[i].ToString() + "0"]);
                    else
                    {
                        if (tensOrdinal.ContainsKey(number[i].ToString() + "0"))
                            alternative1.Append(tensOrdinal[number[i].ToString() + "0"]);
                    }
                    if (specialsOrdinal.ContainsKey(number[i].ToString() + number[i + 1].ToString()))
                        phrase.Append(specialsOrdinal[number[i].ToString() + number[i + 1].ToString()]);
                    else
                    {
                        if (tensOrdinal.ContainsKey(number[i].ToString() + "0"))
                            phrase.Append(tensOrdinal[number[i].ToString() + "0"]);
                    }
                    if (number[i].ToString().Equals("1"))
                    {
                        if (number[i + 1].ToString().Equals("0"))
                        {
                            alternative2.Append(tensCardinal[number[i].ToString() + "0"]);
                            alternative3.Append(tensCardinal[number[i].ToString() + "0"]);
                        }
                        else
                        {
                            if (specialTensCardinal.ContainsKey(number[i].ToString() + number[i + 1].ToString()))
                            {
                                alternative2.Append(specialTensCardinal[number[i].ToString() + number[i + 1].ToString()]);
                                if(specialAlternativeTensCardinal.ContainsKey(number[i].ToString() + number[i + 1].ToString()))
                                    alternative3.Append(specialAlternativeTensCardinal[number[i].ToString() + number[i + 1].ToString()]);
                                else
                                    alternative3.Append(specialTensCardinal[number[i].ToString() + number[i + 1].ToString()]);
                            }
                        }
                    }
                    else
                    {
                        if (tensCardinal.ContainsKey(number[i].ToString() + "0"))
                        {
                            alternative2.Append(tensCardinal[number[i].ToString() + "0"]);
                            alternative3.Append(tensCardinal[number[i].ToString() + "0"]);
                        }
                    }
                }
                else 
                {
                    comparation.Append(number[i].ToString());
                    if (specialFractional.ContainsKey(number[i].ToString()) && Iter.Equals(1))
                    {
                        phrase.Append(specialFractional[number[i].ToString()]);
                        alternative1.Append(specialFractional[number[i].ToString()]);
                    }
                    else if (unitsOrdinal.ContainsKey(number[i].ToString()))
                    {
                        if (!comparation.Equals(new StringBuilder("11")) && !comparation.Equals(new StringBuilder("12")))
                            phrase.Append(unitsOrdinal[number[i].ToString()]);
                        else
                            phrase = new StringBuilder(phrase.ToString().Trim());
                        alternative1.Append(unitsOrdinal[number[i].ToString()]);
                    }
                    if (unitsCardinal.ContainsKey(number[i].ToString()))
                    {
                        alternative2.Append(unitsCardinal[number[i].ToString()]);
                        alternative3.Append(unitsCardinal[number[i].ToString()]);
                    }
                }
            }
            phrase = CheckTextThousands(phrase, "ordinal");
            alternative1 = CheckTextThousands(alternative1, "ordinal");
            alternative2 = CheckTextThousands(alternative2, "cardinal");
            alternative3 = CheckTextThousands(alternative3, "cardinal");
            phrase = CheckTextMillons(phrase, new List<StringBuilder> { new StringBuilder("ordinal"), new StringBuilder("sentence") });
            alternative1 = CheckTextMillons(alternative1, new List<StringBuilder> { new StringBuilder("ordinal"), new StringBuilder("alternative1") });
            alternative2 = CheckTextMillons(alternative2, new List<StringBuilder> { new StringBuilder("cardinal1"), new StringBuilder("alternative2") });
            alternative3 = CheckTextMillons(alternative3, new List<StringBuilder> { new StringBuilder("cardinal2"), new StringBuilder("alternative3") });
            ResetParameters();
            InsertSentence(sentence, phrase.ToString());
            InsertSentence(alternativeSentence1, alternative1.ToString());
            InsertSentence(alternativeSentence2, alternative2.ToString());
            InsertSentence(alternativeSentence3, alternative3.ToString());
        }

        private StringBuilder CheckTextThousands(StringBuilder phrase, string type)
        {
            if (digits.Count.Equals(2))
            {
                if (ParamThousands > 3 && ParamThousands < 7 && !phrase.Equals(""))
                {
                    if (type.Equals("ordinal"))
                    {
                        if (Int32.Parse(digits[0].ToString()) > 1)
                            phrase.Append(" milésimo");
                        else if (Int32.Parse(digits[0].ToString()) == 1)
                            phrase = new StringBuilder("milésimo");
                    }
                    else if (type.Equals("cardinal"))
                    {
                        if (Int32.Parse(digits[0].ToString()) > 1)
                            phrase.Append(" mil");
                        else if (Int32.Parse(digits[0].ToString()) == 1)
                            phrase = new StringBuilder("mil");
                    }
                }
            }
            return phrase;
        }

        private StringBuilder CheckTextMillons(StringBuilder phrase, List<StringBuilder> list)
        {
            StringBuilder millonNumber = GetMillonNumber(phrase);
            StringBuilder nameMillon = GetMillonName(millonNumber, list[0]);
            if (phrase.Equals(new StringBuilder("primeiro"))) phrase = new StringBuilder("");
            CheckAppearTimesMillons(nameMillon, list[1]);
            return phrase;
        }

        private StringBuilder GetMillonNumber(StringBuilder phrase)
        {
            if (!phrase.Equals(new StringBuilder("")) && ParamMillions.Length > 6)
            {
                if (digits.Count.Equals(2))
                    return new StringBuilder(ParamMillions.ToString().Substring(0, Iter - (digits[0].ToString().Length + digits[1].ToString().Length) + 1));
                else
                    return new StringBuilder(ParamMillions.ToString().Substring(0, ParamMillions.Length - digits[0].ToString().Length + 1));
            }
            return new StringBuilder("");
        }

        private StringBuilder GetMillonName(StringBuilder millonNumber, StringBuilder list)
        {
            if (list.Equals(new StringBuilder("ordinal")))
            {
                if (largeOrdinal.ContainsKey(millonNumber.ToString()))
                    return new StringBuilder(largeOrdinal[millonNumber.ToString()]);
            }
            else if (list.Equals(new StringBuilder("cardinal1")))
            {
                if (largeCardinal.ContainsKey(millonNumber.ToString()))
                    return new StringBuilder(largeCardinal[millonNumber.ToString()]);
            }
            else if (list.Equals(new StringBuilder("cardinal2")))
            {
                if (largeAlternativeCardinal.ContainsKey(millonNumber.ToString()))
                    return new StringBuilder(largeAlternativeCardinal[millonNumber.ToString()]);
            }
            return new StringBuilder("");
        }

        private void CheckAppearTimesMillons(StringBuilder nameMillon, StringBuilder list)
        {
            if (!nameMillon.Equals(new StringBuilder("")))
            {
                InsertMillonsValues(nameMillon.ToString());
                if (list.Equals(new StringBuilder("sentence")))
                    if (!sentence.Contains(nameMillon.ToString()))
                        InsertSentence(sentence, nameMillon.ToString());
                if (list.Equals(new StringBuilder("alternative1")))
                    if (!alternativeSentence1.Contains(nameMillon.ToString()))
                        InsertSentence(alternativeSentence1, nameMillon.ToString());
                if (list.Equals(new StringBuilder("alternative2")))
                {
                    if (!alternativeSentence2.Contains(nameMillon.ToString()) &&
                        !alternativeSentence2.Contains(CheckPlural(nameMillon)))
                        InsertSentence(alternativeSentence2, CheckPlural(nameMillon));
                    else if (alternativeSentence2.Contains(nameMillon.ToString()) &&
                        millonsValues[nameMillon.ToString()] > 1)
                    {
                        int pos = alternativeSentence2.IndexOf(nameMillon.ToString());
                        alternativeSentence2.Remove(nameMillon.ToString());
                        alternativeSentence2.Insert(pos, CheckPlural(nameMillon));
                    }
                }
                if (list.Equals(new StringBuilder("alternative3")))
                {
                    if (!alternativeSentence3.Contains(nameMillon.ToString()) &&
                        !alternativeSentence3.Contains(CheckPlural(nameMillon)))
                        InsertSentence(alternativeSentence3, CheckPlural(nameMillon));
                    else if (alternativeSentence3.Contains(nameMillon.ToString()) &&
                        millonsValues[nameMillon.ToString()] > 1)
                    {
                        int pos = alternativeSentence3.IndexOf(nameMillon.ToString());
                        alternativeSentence3.Remove(nameMillon.ToString());
                        alternativeSentence3.Insert(pos, CheckPlural(nameMillon));
                    }
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

        private string CheckPlural(StringBuilder nameMillon)
        {
            StringBuilder value = new StringBuilder(nameMillon.ToString());
            if (digits.Count.Equals(2))
            {
                if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && nameMillon.ToString().Substring(nameMillon.Length - 3).Equals("ião"))
                    value = value.Replace("ão", "ões");
                else if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && nameMillon.ToString().Substring(nameMillon.Length - 2).Equals("ão"))
                    value = value.Replace("ão", "ões");
            }
            else if (digits.Count.Equals(1))
            {
                if ((Int32.Parse(digits[0].ToString()) > 1) && nameMillon.ToString().Substring(nameMillon.Length - 3).Equals("ião"))
                    value = value.Replace("ão", "ões");
                else if ((Int32.Parse(digits[0].ToString()) > 1) && nameMillon.ToString().Substring(nameMillon.Length - 2).Equals("ão"))
                    value = value.Replace("ão", "ões");
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
            string secondResult = GetSentence(alternativeSentence1);
            string thirdResult = GetSentence(alternativeSentence2) + " avos";
            string fourthResult = GetSentence(alternativeSentence3) + " avos";
            if (firtsResult.Equals("")) return new List<string>();
            if (secondResult.Equals(""))
            {
                if (thirdResult.Equals("")) return new List<string>() { firtsResult };
                else if (!thirdResult.Equals("")) return new List<string>() { firtsResult, thirdResult };
            }
            if (thirdResult.Equals("")) return new List<string>();
            if (fourthResult.Equals(""))
            {
                if (secondResult.Equals("")) return new List<string>() { firtsResult, thirdResult };
                else if (!secondResult.Equals("") && !IsSentencesEquals(firtsResult, secondResult))
                    return new List<string>() { firtsResult, secondResult, thirdResult };
            }
            if (IsSentencesEquals(firtsResult, secondResult))
                results.Add(firtsResult);
            else
            {
                results.Add(firtsResult);
                results.Add(secondResult);
            }
            if (IsSentencesEquals(thirdResult, fourthResult))
                results.Add(thirdResult);
            else
            {
                results.Add(thirdResult);
                results.Add(fourthResult);
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
            return phrase.ToString().Trim();
        }

        private bool IsSentencesEquals(string firstResult, string secondResult)
        {
            return firstResult.Equals(secondResult);
        }
    }
}
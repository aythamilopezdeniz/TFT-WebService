using System;
using Entities;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;

namespace NumbersTranslatorWebService.Entities
{
    public class Ordinal : Number
    {
        private SortedList<string, string> Units;
        private SortedList<string, string> Tens;
        private SortedList<string, string> Hundreds;
        private SortedList<string, string> BigNumbers;
        private SortedList<string, string> alternativeTens;
        private SortedList<string, string> alternativeSpecials;
        private SortedList<string, string> alternativeHundreds;
        private ArrayList digits;
        private ArrayList sentence;
        private ArrayList alternativeSentence;
        private int ParamThousands;
        private int Iter;
        private StringBuilder ParamMillions;
        private SortedList<string, int> millonsValues;

        public Ordinal(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            alternativeSentence = new ArrayList();
            digits = new ArrayList();
            ParamThousands = 0;
            Iter = 0;
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
            OrdinalRules ordinalRules = new OrdinalRules();
            ordinalRules.Initialize();
            Units = ordinalRules.GetSortedListUnitsNumbers();
            Tens = ordinalRules.GetSortedListTensNumbers();
            Hundreds = ordinalRules.GetSortedListHundredsNumbers();
            BigNumbers = ordinalRules.GetSortedListMillonsNumbers();
            alternativeTens = ordinalRules.GetSortedListAlternativeTensNumbers();
            alternativeSpecials = ordinalRules.GetSortedListAlternativeSpecialsNumbers();
            alternativeHundreds = ordinalRules.GetSortedListAlternativeHundredsNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.GetIntegerNumber().Equals(true) &&
                !IsMinusContains(treatment.GetText()))
            {
                if (treatment.GetText().Length > 126) throw new InvalidNumber("1");
                    TransforNumber(new StringBuilder(treatment.GetText()));
            }
        }

        private bool IsMinusContains(string text)
        {
            if (text.StartsWith("-")) return true;
            return false;
        }

        private void TransforNumber(StringBuilder number)
        {
            if (number.Length >= 1 && !number.Equals("0"))
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
        }

        private void WriteNumber(string number)
        {
            StringBuilder phrase = new StringBuilder("");
            StringBuilder alternative = new StringBuilder("");
            StringBuilder comparation = new StringBuilder("");
            digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                Iter++;
                ParamThousands++;
                if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                if (!phrase.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) phrase.Append(" ");
                if (!alternative.Equals(new StringBuilder("")) && !number[i].ToString().Equals("0")) alternative.Append(" ");
                if ((number.Length - 1 - i) > 1)
                {
                    if (alternativeHundreds.ContainsKey(number[i].ToString() + "00"))
                        alternative.Append(alternativeHundreds[number[i].ToString() + "00"]);
                    else
                    {
                        if (Hundreds.ContainsKey(number[i].ToString() + "00"))
                            alternative.Append(Hundreds[number[i].ToString() + "00"]);
                    }
                    if (Hundreds.ContainsKey(number[i].ToString() + "00"))
                        phrase.Append(Hundreds[number[i].ToString() + "00"]);
                }
                else if ((number.Length - 1 - i) == 1)
                {
                    comparation.Append(number[i].ToString());
                    if (alternativeTens.ContainsKey(number[i].ToString() + "0"))
                        alternative.Append(alternativeTens[number[i].ToString() + "0"]);
                    else
                    {
                        if (alternativeSpecials.ContainsKey(number[i].ToString() + number[i + 1].ToString()))
                            alternative.Append(alternativeSpecials[number[i].ToString() + number[i + 1].ToString()]);
                        else
                        {
                            if (Tens.ContainsKey(number[i].ToString() + "0"))
                                alternative.Append(Tens[number[i].ToString() + "0"]);
                        }
                    }
                    if (Tens.ContainsKey(number[i].ToString() + "0"))
                    {
                        phrase.Append(Tens[number[i].ToString() + "0"]);
                        if (number[i + 1].ToString().Equals("0"))
                        {
                            i++;
                            Iter++;
                            ParamThousands++;
                            if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                        }
                    }
                }
                else
                {
                    comparation.Append(number[i].ToString());
                    if (Units.ContainsKey(number[i].ToString()))
                    {
                        phrase.Append(Units[number[i].ToString()]);
                        if (!comparation.Equals(new StringBuilder("11")) && !comparation.Equals(new StringBuilder("12")))
                            alternative.Append(Units[number[i].ToString()]);
                        else
                            alternative = new StringBuilder(alternative.ToString().Trim());
                    }
                }
            }
            phrase = CheckTextThousands(phrase);
            alternative = CheckTextThousands(alternative);
            phrase = CheckTextMillons(phrase, new StringBuilder("sentence"));
            alternative = CheckTextMillons(alternative, new StringBuilder("alternative"));
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
                        phrase.Append(" milésimo");
                    else if (Int32.Parse(digits[0].ToString()) == 1)
                        phrase = new StringBuilder("milésimo");
                }
            }
            return phrase;
        }

        private StringBuilder CheckTextMillons(StringBuilder phrase, StringBuilder type)
        {
            StringBuilder millonNumber = GetMillonNumber(phrase);
            StringBuilder nameMillon = GetMillonName(millonNumber);
            if (phrase.Equals(new StringBuilder("primeiro"))) phrase = new StringBuilder("");
            CheckAppearTimesMillons(nameMillon, type);
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

        private StringBuilder GetMillonName(StringBuilder millonNumber)
        {
            if (BigNumbers.ContainsKey(millonNumber.ToString()))
                return new StringBuilder(BigNumbers[millonNumber.ToString()]);
            return new StringBuilder("");
        }

        private void CheckAppearTimesMillons(StringBuilder nameMillon, StringBuilder type)
        {
            if (!nameMillon.Equals(new StringBuilder("")))
            {
                InsertMillonsValues(nameMillon.ToString());
                if (type.Equals(new StringBuilder("sentence")))
                {
                    if (!sentence.Contains(nameMillon.ToString()))
                        InsertSentence(sentence, nameMillon.ToString());
                }
                else if (type.Equals(new StringBuilder("alternative")))
                {
                    if (!alternativeSentence.Contains(nameMillon.ToString()))
                        InsertSentence(alternativeSentence, nameMillon.ToString());
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
            list.Insert(0, phrase);
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
using Entities;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;
using System;
using System.Text;

namespace NumbersTranslatorWebService.Entities
{
    public class Multiplicative : Number
    {
        private ArrayList sentence;
        private ArrayList alternativeSentence;
        private SortedList<string, string> units;
        private SortedList<string, string> tens;
        private SortedList<string, string> hundreds;
        private SortedList<string, string> specials;
        private SortedList<string, string> longScale;
        private SortedList<string, string> alternativeSpecials;
        private SortedList<string, string> alternativeLongScale;
        private SortedList<string, string> multiplicative;
        private SortedList<string, string> alternativeMultiplicative;
        private int Iter { get; set; }
        private bool Vezes { get; set; }
        private bool applyMultiplicativeTerm;
        private ArrayList digits;
        private int ParamThousands;
        private StringBuilder ParamMillions;
        private SortedList<string, int> millonsValues;

        public Multiplicative(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            alternativeSentence = new ArrayList();
            Iter = 0;
            Vezes = true;
            applyMultiplicativeTerm = false;
            digits = new ArrayList();
            ParamThousands = 0;
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
            MultiplicativeRules multiplicativeRules = new MultiplicativeRules();
            multiplicativeRules.Initialize();
            multiplicative = multiplicativeRules.GetSortedListSpecialNumbers();
            alternativeMultiplicative = multiplicativeRules.GetSortedListAlternativeSpecialNumbers();
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
            if (treatment.getIntegerNumber().Equals(true) &&
                !IsMinusContains(treatment.getText()))
                TakeApartNumber(new StringBuilder(treatment.getText()));
        }

        private bool IsMinusContains(string text)
        {
            if (text.StartsWith("-")) return true;
            return false;
        }

        private void TakeApartNumber(StringBuilder number)
        {
            if (number.Length >= 1 && !number.Equals("0"))
            {
                while (number.Length > 0)
                {
                    if (number.Length > 2)
                    {
                        StringBuilder src = new StringBuilder(number.ToString().Substring(number.Length - 3));
                        number = new StringBuilder(number.ToString().Substring(0, number.Length - 3));
                        if (number.Length.Equals(0)) applyMultiplicativeTerm = true;
                        WriteNumber(src);
                    }
                    else
                    {
                        StringBuilder src = new StringBuilder(number.ToString().Substring(0, number.Length));
                        number = new StringBuilder("");
                        if (number.Length.Equals(0)) applyMultiplicativeTerm = true;
                        WriteNumber(src);
                    }
                }
            }
        }

        private void WriteNumber(StringBuilder number)
        {
            StringBuilder phrase = new StringBuilder("");
            StringBuilder alternative = new StringBuilder("");
            digits.Insert(0, number);
            for (int i = 0; i < number.Length; i++)
            {
                Iter++;
                ParamThousands++;
                if (ParamMillions.Length < Iter) ParamMillions.Append("0");
                if (!phrase.Equals(new StringBuilder("")) &&
                    !number[i].ToString().Equals(new StringBuilder("0"))) phrase.Append(" e ");
                if (!alternative.Equals(new StringBuilder("")) &&
                    !number[i].ToString().Equals(new StringBuilder("0"))) alternative.Append(" e ");
                if ((number.Length - 1 - i) > 1)
                {
                    if (applyMultiplicativeTerm.Equals(true) && Iter.Equals(1) &&
                        multiplicative.ContainsKey(number.ToString()))
                    {
                        Vezes = false;
                        phrase.Append(multiplicative[number.ToString()]);
                        i = i + 2;
                    }
                    else
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
                                phrase.Append(hundreds[number[i].ToString() + "00"]);
                            alternative.Append(hundreds[number[i].ToString() + "00"]);
                        }
                    }
                }
                else if ((number.Length - 1 - i) == 1)
                {
                    if (applyMultiplicativeTerm.Equals(true) && Iter.Equals(1) &&
                        multiplicative.ContainsKey(number.ToString()))
                    {
                        Vezes = false;
                        phrase.Append(multiplicative[number.ToString()]);
                        i++;
                    }
                    else
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
                }
                else
                {
                    if (applyMultiplicativeTerm.Equals(true) && Iter.Equals(1) &&
                        multiplicative.ContainsKey(number.ToString()))
                    {
                        Vezes = false;
                        phrase.Append(multiplicative[number.ToString()]);
                        if(alternativeMultiplicative.ContainsKey(number.ToString()))
                            alternative.Append(alternativeMultiplicative[number.ToString()]);
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
            }
            phrase = CheckTextThousands(phrase);
            alternative = CheckTextThousands(alternative);
            CheckTextMillons(phrase);
            ResetParameters();
            InsertSentence(phrase.ToString());
            InsertAlternativeSentence(alternative.ToString());
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
                            phrase.Append(" mil e");
                        else if (Int32.Parse(digits[1].ToString()) >= 100 || Int32.Parse(digits[1].ToString()) == 0)
                            phrase.Append("mil");
                    }
                }
            }
            return phrase;
        }

        private void CheckTextMillons(StringBuilder phrase)
        {
            string aux = "";
            if (!phrase.Equals(new StringBuilder("")))
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
                    InsertSentence(CheckPlural(name.ToString()));
                else if (sentence.Contains(name) && millonsValues[name.ToString()] > 1)
                {
                    int pos = sentence.IndexOf(name.ToString());
                    sentence.Remove(name.ToString());
                    sentence.Insert(pos, CheckPlural(name.ToString()));
                }
                if (!alternativeSentence.Contains(alternativeName.ToString()) &&
                    !alternativeSentence.Contains(CheckPlural(alternativeName.ToString())))
                    InsertAlternativeSentence(CheckPlural(alternativeName.ToString()));
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
                    if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.Substring(millons.Length - 3).Equals("ião"))
                        value = value.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString() + digits[1].ToString()) > 1) && millons.Substring(millons.Length - 2).Equals("ão"))
                        value = value.Replace("ão", "ões");
                }
                else if (digits.Count.Equals(1))
                {
                    if ((Int32.Parse(digits[0].ToString()) > 1) && millons.Substring(millons.Length - 3).Equals("ião"))
                        value = value.Replace("ão", "ões");
                    else if ((Int32.Parse(digits[0].ToString()) > 1) && millons.Substring(millons.Length - 2).Equals("ão"))
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

        private void InsertSentence(string phrase)
        {
            sentence.Insert(0, phrase);
        }

        private void InsertAlternativeSentence(string phrase)
        {
            alternativeSentence.Insert(0, phrase);
        }

        public override string GetSentence()
        {
            StringBuilder phrase = new StringBuilder("");
            foreach (var item in sentence)
            {
                if (!item.Equals(new StringBuilder("")))
                    phrase.Append(item + " ");
            }
            if (phrase.Equals(new StringBuilder(""))) return "";
            if (Vezes.Equals(true)) phrase.Append(" vezes");
            return (char.ToUpper(phrase[0]) + phrase.ToString().Substring(1)).Trim();
        }
    }
}
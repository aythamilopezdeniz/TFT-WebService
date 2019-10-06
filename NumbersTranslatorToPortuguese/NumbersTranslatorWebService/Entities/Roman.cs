using System;
using Entities;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;
using System.Text;

namespace NumbersTranslatorWebService.Entities
{
    public class Roman : Number
    {
        private ArrayList sentence;
        private StringBuilder numberEdited;
        private SortedList<int, string> romanNumbers;

        public Roman(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
            numberEdited = new StringBuilder("");
        }

        public override void Translate(Treatment treatment)
        {
            GenerateListRomanNumbers();
            DescomposeNumber(treatment);
        }

        private void GenerateListRomanNumbers()
        {
            RomanRules romanRules = new RomanRules();
            romanRules.Initialize();
            romanNumbers = romanRules.GetSortedListRomanNumbers();
        }

        private void DescomposeNumber(Treatment treatment)
        {
            if (treatment.GetIntegerNumber().Equals(true) && GetTypeNumber().Equals("Roman")
                && !IsMinusContains(treatment.GetText()))
            {
                EditNumber(treatment.GetText());
                if (numberEdited.Length <= 13)
                {
                    long numero = Convert.ToInt64(numberEdited.ToString());
                    if (numero < 4000000000000) TakeApartNumber(numero);
                }
            }
        }

        private bool IsMinusContains(string text)
        {
            if (text.StartsWith("-")) return true;
            return false;
        }

        private void EditNumber(string text)
        {
            if (text.StartsWith("+"))
                numberEdited = new StringBuilder(text.Substring(text.IndexOf("+") + 1));
            else
                numberEdited = new StringBuilder(text);
        }

        private void TakeApartNumber(long number)
        {
            List<KeyValuePair<int, string>> listRomanNumbers = romanNumbers.ToList();
            long numero = number, ind, num;
            listRomanNumbers.Reverse();
            string res = "";
            while (numero > 0)
            {
                if (numero > 3999999999) num = numero / 1000000000;
                else if (numero > 3999999) num = numero / 1000000;
                else if (numero > 3999) num = numero / 1000;
                else num = numero;
                ind = 0;
                res = "";
                while (num > 0)
                {
                    while (num >= listRomanNumbers[(int)ind].Key)
                    {
                        res += listRomanNumbers[(int)ind].Value;
                        num -= listRomanNumbers[(int)ind].Key;
                    }
                    ind++;
                }
                if (numero > 3999999999)
                {
                    res = "<span style=\"background-image:url(./img/lineas3.gif);background-repeat:repeat-x;margin-top:-4px;padding-top:4px\">" + res + "</span>";
                    numero = numero - ((numero / 1000000000) * 1000000000);
                }
                else if (numero > 3999999)
                {
                    res = "<span style=\"background-image:url(./img/lineas2.gif);background-repeat:repeat-x;margin-top:-2px;padding-top:2px\">" + res + "</span>";
                    numero = numero - ((numero / 1000000) * 1000000);
                }
                else if (numero > 3999)
                {
                    res = "<span style=\"text-decoration:overline;\">" + res + "</span>";
                    numero = numero - ((numero / 1000) * 1000);
                }
                else numero = 0;
                InsertSentence(res);
            }
        }

        private void InsertSentence(string phrase)
        {
            sentence.Add(phrase);
        }

        public override List<string> GetResults()
        {
            string phrase = "";
            foreach (string item in sentence)
                phrase += item;
            if (phrase.Equals("")) return new List<string>();
            return new List<string>() { phrase };
        }
    }
}
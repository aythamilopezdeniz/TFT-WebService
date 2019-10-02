using System;
using Entities;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NumbersTranslatorWebService.RulesDB;

namespace NumbersTranslatorWebService.Entities
{
    public class Roman : Number
    {
        private ArrayList sentence;
        private SortedList<int, string> romanNumbers;

        public Roman(string dato)
        {
            Initialize(dato);
            sentence = new ArrayList();
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
                long numero = Convert.ToInt64(treatment.GetText());
                if (treatment.GetText().Length < 13 && numero < 4000000000000) TakeApartNumber(numero);
            }
        }

        private bool IsMinusContains(string text)
        {
            if (text.StartsWith("-")) return true;
            return false;
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
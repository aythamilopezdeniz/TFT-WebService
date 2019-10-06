using System;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace Entities
{
    public class Treatment
    {
        private StringBuilder Text;
        private bool valideNumber;
        private bool integerNumber;
        private bool decimalNumber;
        private bool fractionalNumber;
        private bool scientificNotationNumber;
        private bool romanNumber;
        private readonly string integerRegularExpression;
        private readonly string decimalRegularExpression;
        private readonly string fractionalRegularExpression;
        private readonly string scientificNotationRegularExpression;
        private ArrayList exponentialNumber;
        private StringBuilder integerPartNumber;
        private StringBuilder decimalPartNumber;
        private StringBuilder minus;

        public Treatment(string text)
        {
            Text = new StringBuilder(text);
            valideNumber = false;
            integerNumber = false;
            decimalNumber = false;
            fractionalNumber = false;
            scientificNotationNumber = false;
            romanNumber = false;
            //integerRegularExpression = "^(\\+|-)?\\d+$"; //Válida
            integerRegularExpression = "^(\\+|-)?(\\d+|(\\d{1,3}\\s+\\d{1,3}))+$"; //Probando
            //decimalRegularExpression = "^(\\+|-)?\\d+[\\.|,]{1}\\d+$"; //Válida
            decimalRegularExpression = "^(\\+|-)?(\\d+|(\\d{1,3}\\s+\\d{1,3}))+[\\.|,]{1}(\\d+|(\\d{1,3}\\s+\\d{1,3}))+$"; //Probando
            //fractionalRegularExpression = "^(\\+|-)?\\d+\\/{1}[\\+|-]?\\d+$"; //Válida
            fractionalRegularExpression = "^(\\+|-)?(\\d+|(\\d{1,3}\\s+\\d{1,3}))+\\/{1}[\\+|-]?(\\d+|(\\d{1,3}\\s+\\d{1,3}))+$"; //Probando
            //scientificNotationRegularExpression = "^(\\+|-)?\\d+((\\.|,)\\d+)?[Ee]{1}(\\+|-)?\\d+$"; //Válida
            scientificNotationRegularExpression = "^(\\+|-)?(\\d+|(\\d{1,3}\\s+\\d{1,3}))+([\\.|,]{1}(\\d+|(\\d{1,3}\\s+\\d{1,3}))+)?[Ee]{1}(\\+|-)?\\d+$"; //Probando
            exponentialNumber = new ArrayList();
            integerPartNumber = new StringBuilder("");
            decimalPartNumber = new StringBuilder("");
            minus = new StringBuilder("");
        }

        public void checkNumber()
        {
            romanNumber = CheckRomanNumber(Text.ToString());
            scientificNotationNumber = CheckScientificNotationNumber(Text.ToString());
            if (scientificNotationNumber.Equals(true))
            {
                exponentialNumber = GetBaseAndExponentPart();
                GetIntegerAndDecimalPart(exponentialNumber);
                ConvertIntegerOrDecimalNumber();
            }
            decimalNumber = CheckDecimalNumber(Text.ToString());
            if (decimalNumber.Equals(true))
            {
                GetIntegerAndDecimalPart(new ArrayList() { Text });
                CheckDecimalPart();
            }
            fractionalNumber = CheckFractionalNumber(Text.ToString());
            if (fractionalNumber.Equals(true))
            {
                ArrayList denominator = GetFractionsPart(Text.ToString());
                CheckFractions(denominator);
            }
            integerNumber = CheckIntegerNumber(Text.ToString());
            decimalNumber = CheckDecimalNumber(Text.ToString());
            fractionalNumber = CheckFractionalNumber(Text.ToString());
            scientificNotationNumber = CheckScientificNotationNumber(Text.ToString());
            romanNumber = CheckRomanNumber(Text.ToString());
            if (integerNumber.Equals(true) || decimalNumber.Equals(true) ||
                fractionalNumber.Equals(true) || scientificNotationNumber.Equals(true) ||
                romanNumber.Equals(true))
                valideNumber = true;
            Text.Replace(" ", "");
        }

        private bool CheckRomanNumber(string numRomano)
        {
            ArrayList romanosBis = new ArrayList() { "M", "D", "C", "L", "X", "V", "I" };
            int[] valorRomanosBis = new int[] { 1, 5, 10, 50, 100, 500, 1000 };
            int numero = 0, posAnt = -1, pos, rep = 1;
            romanosBis.Reverse();
            numRomano = numRomano.Replace(" ", "");
            numRomano = numRomano.ToUpper();
            pos = romanosBis.IndexOf(numRomano[numRomano.Length - 1].ToString());
            if (pos == -1) return false;
            numero = valorRomanosBis[pos];
            for (int i = numRomano.Length - 2; i >= 0; i--)
            {
                posAnt = pos;
                pos = romanosBis.IndexOf(numRomano[i].ToString());

                if (posAnt == pos) rep++;
                else rep = 1;
                if (pos < posAnt)
                    numero -= valorRomanosBis[pos];
                else numero += valorRomanosBis[pos];
            }
            Text = new StringBuilder(Convert.ToString(numero));
            return true;
        }

        private bool CheckIntegerNumber(string text)
        {
            return Regex.Match(text, integerRegularExpression).Success;
        }

        private bool CheckDecimalNumber(string text)
        {
            return Regex.Match(text, decimalRegularExpression).Success;
        }

        private bool CheckFractionalNumber(string text)
        {
            return Regex.Match(text, fractionalRegularExpression).Success;
        }

        private bool CheckScientificNotationNumber(string text)
        {
            return Regex.Match(text, scientificNotationRegularExpression).Success;
        }

        public string GetText()
        {
            return Text.ToString();
        }

        public bool GetValideNumber()
        {
            return valideNumber;
        }

        public bool GetIntegerNumber()
        {
            return integerNumber;
        }

        public bool GetDecimalNumber()
        {
            return decimalNumber;
        }

        public bool GetFractionalNumber()
        {
            return fractionalNumber;
        }

        public bool GetScientificNotationNumber()
        {
            return scientificNotationNumber;
        }

        private ArrayList GetBaseAndExponentPart()
        {
            ArrayList exponentialNumber = new ArrayList();
            if (Text.ToString().Contains("E"))
            {
                exponentialNumber.Add(Text.ToString().Substring(0, Text.ToString().IndexOf("E")).Replace(" ", ""));
                exponentialNumber.Add(Text.ToString().Substring(Text.ToString().IndexOf("E") + 1));
            }
            else if (Text.ToString().Contains("e"))
            {
                exponentialNumber.Add(Text.ToString().Substring(0, Text.ToString().IndexOf("e")).Replace(" ", ""));
                exponentialNumber.Add(Text.ToString().Substring(Text.ToString().IndexOf("e") + 1));
            }
            return exponentialNumber;
        }

        private void GetIntegerAndDecimalPart(ArrayList number)
        {
            if (!Text.ToString().Contains(",") && !Text.ToString().Contains(".")) integerPartNumber = new StringBuilder(number[0].ToString());
            if (Text.ToString().Contains(","))
            {
                integerPartNumber = new StringBuilder(number[0].ToString().Substring(0, number[0].ToString().IndexOf(",")));
                decimalPartNumber = new StringBuilder(number[0].ToString().Substring(number[0].ToString().IndexOf(",") + 1));
            }
            else if (Text.ToString().Contains("."))
            {
                integerPartNumber = new StringBuilder(number[0].ToString().Substring(0, number[0].ToString().IndexOf(".")));
                decimalPartNumber = new StringBuilder(number[0].ToString().Substring(number[0].ToString().IndexOf(".") + 1));
            }
        }

        private void ConvertIntegerOrDecimalNumber()
        {
            string zeros = "";
            if (integerPartNumber.ToString().Contains("-"))
            {
                minus = new StringBuilder("-");
                integerPartNumber = new StringBuilder(integerPartNumber.ToString().Substring(integerPartNumber.ToString().IndexOf("-") + 1));
            }
            int exp = Int32.Parse(exponentialNumber[1].ToString());
            if (exp == 0)
            {
                if (DecimalPartIsZero())
                    Text = new StringBuilder(minus.ToString() + integerPartNumber.ToString());
                else
                    Text = new StringBuilder(minus.ToString() + exponentialNumber[0].ToString());
            }
            if (exp > 0)
            {
                if (decimalPartNumber.Length.Equals(0))
                {
                    zeros = new string('0', exp);
                    Text = new StringBuilder(minus.ToString() + integerPartNumber.ToString() + zeros);
                }
                else if (decimalPartNumber.Length > 0)
                {
                    if (exp > decimalPartNumber.Length)
                    {
                        if(!(exp - decimalPartNumber.Length).Equals(0))
                            zeros = new string('0', (exp - decimalPartNumber.Length));
                        integerPartNumber = new StringBuilder(integerPartNumber.ToString() + decimalPartNumber.ToString() + zeros);
                        decimalPartNumber = new StringBuilder("");
                        Text = new StringBuilder(minus + integerPartNumber.ToString().TrimStart('0'));
                    }
                    else if (exp < decimalPartNumber.Length)
                    {
                        integerPartNumber = new StringBuilder(integerPartNumber.ToString() + decimalPartNumber.ToString().Substring(0, exp));
                        decimalPartNumber = new StringBuilder(decimalPartNumber.ToString().Substring(exp));
                        CheckIntegerNumber();
                        CheckDecimalPart();
                    }
                    else
                    {
                        integerPartNumber = new StringBuilder(integerPartNumber.ToString() + decimalPartNumber.ToString());
                        decimalPartNumber = new StringBuilder("");
                        Text = new StringBuilder(minus + integerPartNumber.ToString().TrimStart('0'));
                    }
                }
            }
            else if (exp < 0)
            {
                exp = (-1) * exp;
                if (exp > integerPartNumber.Length)
                {
                    if (!(exp - integerPartNumber.Length).Equals(0))
                        zeros = new string('0', (exp - integerPartNumber.Length));
                    decimalPartNumber = new StringBuilder((zeros + integerPartNumber.ToString() + decimalPartNumber.ToString()).TrimEnd('0'));
                    integerPartNumber = new StringBuilder("0");
                }
                else if (exp < integerPartNumber.Length)
                {
                    decimalPartNumber = new StringBuilder(integerPartNumber.ToString().Substring((integerPartNumber.Length - exp)) + decimalPartNumber.ToString().TrimEnd('0'));
                    integerPartNumber = new StringBuilder(integerPartNumber.ToString().Substring(0, (integerPartNumber.Length - exp)));
                }
                else
                {
                    decimalPartNumber = new StringBuilder((integerPartNumber.ToString() + decimalPartNumber.ToString()).TrimEnd('0'));
                    integerPartNumber = new StringBuilder("0");
                }
                CheckDecimalPart();
            }
            minus = new StringBuilder("");
        }

        private bool DecimalPartIsZero()
        {
            for (int i = 0; i < decimalPartNumber.Length; i++)
            {
                if (!decimalPartNumber[i].ToString().Equals("0")) return false;
            }
            return true;
        }

        private void CheckIntegerNumber()
        {
            integerPartNumber = new StringBuilder(integerPartNumber.ToString().TrimStart('0'));
            if (integerPartNumber.Length.Equals(0)) integerPartNumber = new StringBuilder("0");
        }

        private void CheckDecimalPart()
        {
            if (DecimalPartIsZero())
                Text = new StringBuilder(minus.ToString() + integerPartNumber.ToString());
            else
                Text = new StringBuilder(minus.ToString() + integerPartNumber.ToString() + "," + decimalPartNumber);
        }

        private ArrayList GetFractionsPart(string text)
        {
            ArrayList fraction = new ArrayList();
            fraction.Add(text.Substring(0, text.IndexOf("/")));
            fraction.Add(text.Substring(text.IndexOf("/") + 1));
            return fraction;
        }

        private void CheckFractions(ArrayList fraction)
        {
            if (fraction[1].ToString().Equals("1"))
                Text = new StringBuilder(fraction[0].ToString());
            else if (fraction[1].ToString().Equals("-1"))
            {
                if (fraction[0].ToString().Contains("-"))
                    Text = new StringBuilder(fraction[0].ToString().Substring(fraction[0].ToString().IndexOf("-") + 1));
                else
                    Text = new StringBuilder("-" + fraction[0].ToString());
            }
        }
    }
}
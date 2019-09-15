using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Entities
{
    public class Treatment
    {
        private string Text;
        private bool valideNumber;
        private bool integerNumber;
        private bool decimalNumber;
        private bool fractionalNumber;
        private bool scientificNotationNumber;
        private string integerRegularExpression;
        private string decimalRegularExpression;
        private string fractionalRegularExpression;
        private string scientificNotationRegularExpression;
        private ArrayList exponentialNumber;
        private string integerPartNumber;
        private string decimalPartNumber;
        private string minus;

        public Treatment(string text)
        {
            Text = text;
            valideNumber = false;
            integerNumber = false;
            decimalNumber = false;
            fractionalNumber = false;
            scientificNotationNumber = false;
            integerRegularExpression = "^(\\+|-)?\\d+$";
            decimalRegularExpression = "^(\\+|-)?\\d+[\\.|,]{1}\\d+$";
            fractionalRegularExpression = "^(\\+|-)?\\d+\\/{1}[\\+|-]?\\d+$";
            scientificNotationRegularExpression = "^(\\+|-)?\\d+((\\.|,)\\d+)?[Ee]{1}(\\+|-)?\\d+$";
            exponentialNumber = new ArrayList();
            integerPartNumber = "";
            decimalPartNumber = "";
            minus = "";
        }

        public void checkNumber()
        {
            this.scientificNotationNumber = checkScientificNotationNumber(Text);
            if (scientificNotationNumber.Equals(true))
            {
                exponentialNumber = GetBaseAndExponentPart();
                GetIntegerAndDecimalPart(exponentialNumber);
                ConvertIntegerOrDecimalNumber();
            }
            this.decimalNumber = checkDecimalNumber(Text);
            if (decimalNumber.Equals(true))
            {
                GetIntegerAndDecimalPart(new ArrayList() { Text });
                CheckDecimalPart();
            }
            integerNumber = checkIntegerNumber(Text);
            decimalNumber = checkDecimalNumber(Text);
            fractionalNumber = checkFractionalNumber(Text);
            scientificNotationNumber = checkScientificNotationNumber(Text);
            if (integerNumber.Equals(true) || decimalNumber.Equals(true) ||
                fractionalNumber.Equals(true) || scientificNotationNumber.Equals(true))
                valideNumber = true;
        }

        private bool checkIntegerNumber(string text)
        {
            return Regex.Match(text, integerRegularExpression).Success;
        }

        private bool checkDecimalNumber(string text)
        {
            return Regex.Match(text, decimalRegularExpression).Success;
        }

        private bool checkFractionalNumber(string text)
        {
            return Regex.Match(text, fractionalRegularExpression).Success;
        }

        private bool checkScientificNotationNumber(string text)
        {
            return Regex.Match(text, scientificNotationRegularExpression).Success;
        }

        public string GetText()
        {
            return Text;
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
            if (Text.Contains("E"))
            {
                exponentialNumber.Add(Text.Substring(0, Text.IndexOf("E")));
                exponentialNumber.Add(Text.Substring(Text.IndexOf("E") + 1));
            }
            else if (Text.Contains(","))
            {
                exponentialNumber.Add(Text.Substring(0, Text.IndexOf("e")));
                exponentialNumber.Add(Text.Substring(Text.IndexOf("e") + 1));
            }
            return exponentialNumber;
        }

        private void GetIntegerAndDecimalPart(ArrayList number)
        {
            if (!Text.Contains(",") && !Text.Contains(".")) integerPartNumber = number[0].ToString();
            if (Text.Contains(","))
            {
                integerPartNumber = number[0].ToString().Substring(0, number[0].ToString().IndexOf(","));
                decimalPartNumber = number[0].ToString().Substring(number[0].ToString().IndexOf(",") + 1);
            }
            else if (Text.Contains("."))
            {
                integerPartNumber = number[0].ToString().Substring(0, number[0].ToString().IndexOf("."));
                decimalPartNumber = number[0].ToString().Substring(number[0].ToString().IndexOf(".") + 1);
            }
        }

        private void ConvertIntegerOrDecimalNumber()
        {
            string zeros = "";
            if (integerPartNumber.Contains("-"))
            {
                minus = "-";
                integerPartNumber = integerPartNumber.Substring(integerPartNumber.IndexOf("-") + 1);
            }
            int exp = Int32.Parse(exponentialNumber[1].ToString());
            if (exp == 0)
            {
                if (DecimalPartIsZero())
                    Text = minus + integerPartNumber;
                else
                    Text = minus + exponentialNumber[0].ToString();
            }
            if (exp > 0)
            {
                if (decimalPartNumber.Length.Equals(0))
                {
                    zeros = new string('0', exp);
                    Text = minus + integerPartNumber + zeros;
                }
                else if (decimalPartNumber.Length > 0)
                {
                    if (exp > decimalPartNumber.Length)
                    {
                        if(!(exp - decimalPartNumber.Length).Equals(0))
                            zeros = new string('0', (exp - decimalPartNumber.Length));
                        integerPartNumber = integerPartNumber + decimalPartNumber + zeros;
                        decimalPartNumber = "";
                        Text = minus + integerPartNumber.TrimStart('0');
                    }
                    else if (exp < decimalPartNumber.Length)
                    {
                        integerPartNumber = integerPartNumber + decimalPartNumber.Substring(0, exp - 1);
                        decimalPartNumber = decimalPartNumber.Substring(exp);
                        CheckIntegerNumber();
                        CheckDecimalPart();
                    }
                    else
                    {
                        integerPartNumber = integerPartNumber + decimalPartNumber;
                        decimalPartNumber = "";
                        Text = minus + integerPartNumber.TrimStart('0');
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
                    decimalPartNumber = (zeros + integerPartNumber + decimalPartNumber).TrimEnd('0');
                    integerPartNumber = "0";
                }
                else if (exp < integerPartNumber.Length)
                {
                    decimalPartNumber = integerPartNumber.Substring((integerPartNumber.Length - exp)) + decimalPartNumber.TrimEnd('0');
                    integerPartNumber = integerPartNumber.Substring(0, (integerPartNumber.Length - exp));
                }
                else
                {
                    decimalPartNumber = (integerPartNumber + decimalPartNumber).TrimEnd('0');
                    integerPartNumber = "0";
                }
                CheckDecimalPart();
            }
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
            integerPartNumber = integerPartNumber.TrimStart('0');
            if (integerPartNumber.Length.Equals(0)) integerPartNumber = "0";
        }

        private void CheckDecimalPart()
        {
            if (DecimalPartIsZero())
                Text = minus + integerPartNumber;
            else
                Text = minus + integerPartNumber + "," + decimalPartNumber;
        }
    }
}
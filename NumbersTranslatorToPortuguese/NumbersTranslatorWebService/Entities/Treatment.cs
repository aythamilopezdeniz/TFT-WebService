using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Entities
{
    public class Treatment
    {
        private string text { get; set; }
        private bool valideNumber { get; set; }
        private bool integerNumber { get; set; }
        private bool decimalNumber { get; set; }
        private bool fractionalNumber { get; set; }
        private bool scientificNotationNumber { get; set; }
        private string integerRegularExpression { get; set; }
        private string decimalRegularExpression { get; set; }
        private string fractionalRegularExpression { get; set; }
        private string scientificNotationRegularExpression { get; set; }
        private ArrayList number;
        private string integerPartNumber;
        private string decimalPartNumber;

        public Treatment(string text)
        {
            this.text = text;
            this.valideNumber = false;
            this.integerNumber = false;
            this.decimalNumber = false;
            this.fractionalNumber = false;
            this.scientificNotationNumber = false;
            this.integerRegularExpression = "^(\\+|-)?\\d+$";
            this.decimalRegularExpression = "^(\\+|-)?\\d+[\\.|,]{1}\\d+$";
            this.fractionalRegularExpression = "^(\\+|-)?\\d+\\/{1}[\\+|-]?\\d+$";
            this.scientificNotationRegularExpression = "^(\\+|-)?\\d+((\\.|,)\\d+)?[Ee]{1}(\\+|-)?\\d+$";
            number = new ArrayList();
            integerPartNumber = "";
            decimalPartNumber = "";
        }

        public void checkNumber()
        {
            this.scientificNotationNumber = checkScientificNotationNumber(this.text);
            if (scientificNotationNumber.Equals(true))
            {
                number = GetBaseAndExponentPart();
                GetIntegerAndDecimalPart();
                ConvertIntegerOrDecimalNumber();
                //if (DecimalPartIsZero())
                //    text = integerPartNumber;
            }
            this.integerNumber = checkIntegerNumber(this.text);
            this.decimalNumber = checkDecimalNumber(this.text);
            this.fractionalNumber = checkFractionalNumber(this.text);
            this.scientificNotationNumber = checkScientificNotationNumber(this.text);
            if (this.integerNumber.Equals(true) || this.decimalNumber.Equals(true) ||
                this.fractionalNumber.Equals(true) || this.scientificNotationNumber.Equals(true))
                this.valideNumber = true;

            //this.integerNumber = checkIntegerNumber(this.text);
            //this.decimalNumber = checkDecimalNumber(this.text);
            //this.fractionalNumber = checkFractionalNumber(this.text);
            //this.scientificNotationNumber = checkScientificNotationNumber(this.text);
            //if (this.integerNumber.Equals(true) || this.decimalNumber.Equals(true) ||
            //    this.fractionalNumber.Equals(true) || this.scientificNotationNumber.Equals(true))
            //    this.valideNumber = true;
            //if (scientificNotationNumber.Equals(true))
            //{
            //    number = GetBaseAndExponentPart();
            //    GetIntegerAndDecimalPart();
            //    ConvertIntegerOrDecimalNumber();
            //}
        }

        private bool checkIntegerNumber(string text)
        {
            return Regex.Match(text, this.integerRegularExpression).Success;
        }

        private bool checkDecimalNumber(string text)
        {
            return Regex.Match(text, this.decimalRegularExpression).Success;
        }

        private bool checkFractionalNumber(string text)
        {
            return Regex.Match(text, this.fractionalRegularExpression).Success;
        }

        private bool checkScientificNotationNumber(string text)
        {
            return Regex.Match(text, this.scientificNotationRegularExpression).Success;
        }

        public string getText()
        {
            return this.text;
        }

        public bool getValideNumber()
        {
            return this.valideNumber;
        }

        public bool getIntegerNumber()
        {
            return this.integerNumber;
        }

        public bool getDecimalNumber()
        {
            return this.decimalNumber;
        }

        public bool getFractionalNumber()
        {
            return this.fractionalNumber;
        }

        public bool getScientificNotationNumber()
        {
            return this.scientificNotationNumber;
        }

        private ArrayList GetBaseAndExponentPart()
        {
            ArrayList exponentialNumber = new ArrayList();
            if (text.Contains("E"))
            {
                exponentialNumber.Add(text.Substring(0, text.IndexOf("E")));
                exponentialNumber.Add(text.Substring(text.IndexOf("E") + 1));
            }
            else if (text.Contains(","))
            {
                exponentialNumber.Add(text.Substring(0, text.IndexOf("e")));
                exponentialNumber.Add(text.Substring(text.IndexOf("e") + 1));
            }
            return exponentialNumber;
        }

        private void GetIntegerAndDecimalPart()
        {
            if (!text.Contains(",") && !text.Contains(".")) integerPartNumber = number[0].ToString();
            if (text.Contains(","))
            {
                integerPartNumber = number[0].ToString().Substring(0, number[0].ToString().IndexOf(","));
                decimalPartNumber = number[0].ToString().Substring(number[0].ToString().IndexOf(",") + 1);
            }
            else if (text.Contains("."))
            {
                integerPartNumber = number[0].ToString().Substring(0, number[0].ToString().IndexOf("."));
                decimalPartNumber = number[0].ToString().Substring(number[0].ToString().IndexOf(".") + 1);
            }
        }

        private void ConvertIntegerOrDecimalNumber()
        {
            string zeros = "";
            string minus = "";
            if (integerPartNumber.Contains("-"))
            {
                minus = "-";
                integerPartNumber = integerPartNumber.Substring(integerPartNumber.IndexOf("-") + 1);
            }
            int exp = Int32.Parse(number[1].ToString());
            if (exp == 0)
            {
                if (DecimalPartIsZero())
                    text = integerPartNumber;
                else
                    text = minus + number[0].ToString();
            }
            if (exp > 0)
            {
                if (decimalPartNumber.Length.Equals(0))
                {
                    zeros = new string('0', exp);
                    text = minus + integerPartNumber + zeros;
                }
                else if (decimalPartNumber.Length > 0)
                {
                    if (exp > decimalPartNumber.Length)
                    {
                        if(!(exp - decimalPartNumber.Length).Equals(0))
                            zeros = new string('0', (exp - decimalPartNumber.Length));
                        integerPartNumber = integerPartNumber + decimalPartNumber + zeros;
                        decimalPartNumber = "";
                        text = minus + integerPartNumber.TrimStart('0');
                    }
                    else if (exp < decimalPartNumber.Length)
                    {
                        integerPartNumber = integerPartNumber + decimalPartNumber.Substring(0, (decimalPartNumber.Length - exp) + 1);
                        decimalPartNumber = decimalPartNumber.Substring((decimalPartNumber.Length - exp) + 1);
                        CheckIntegerNumber();
                        text = minus + integerPartNumber + "," + decimalPartNumber;
                    }
                    else
                    {
                        integerPartNumber = integerPartNumber + decimalPartNumber;
                        decimalPartNumber = "";
                        text = minus + integerPartNumber.TrimStart('0');
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
                    decimalPartNumber = zeros + integerPartNumber + decimalPartNumber;
                    integerPartNumber = "0";
                }
                else if (exp < integerPartNumber.Length)
                {
                    decimalPartNumber = integerPartNumber.Substring((integerPartNumber.Length - exp)) + decimalPartNumber;
                    integerPartNumber = integerPartNumber.Substring(0, (integerPartNumber.Length - exp));
                }
                else
                {
                    decimalPartNumber = integerPartNumber + decimalPartNumber;
                    integerPartNumber = "0";
                }
                text = minus + integerPartNumber + "," + decimalPartNumber;
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
    }
}
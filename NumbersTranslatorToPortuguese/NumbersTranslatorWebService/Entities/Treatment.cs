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
        private bool exponentialNumber { get; set; }
        private string integerRegularExpression { get; set; }
        private string decimalRegularExpression { get; set; }
        private string fractionalRegularExpression { get; set; }
        private string exponentialRegularExpression { get; set; }

        public Treatment(string text)
        {
            this.text = text;
            this.valideNumber = false;
            this.integerNumber = false;
            this.decimalNumber = false;
            this.fractionalNumber = false;
            this.exponentialNumber = false;
            this.integerRegularExpression = "^(\\+|-)?\\d+$";
            this.decimalRegularExpression = "^(\\+|-)?\\d+[\\.|,]{1}\\d+$";
            this.fractionalRegularExpression = "^(\\+|-)?\\d+\\/{1}[\\+|-]?\\d+$";
            this.exponentialRegularExpression = "^(\\+|-)?\\d((\\.|,)\\d)?[Ee]{1}(\\+|-)?\\d+$";
        }

        public void checkNumber()
        {
            this.integerNumber = checkIntegerNumber(this.text);
            this.decimalNumber = checkDecimalNumber(this.text);
            this.fractionalNumber = checkFractionalNumber(this.text);
            this.exponentialNumber = checkExponentialNumber(this.text);
            if (this.integerNumber.Equals(true) || this.decimalNumber.Equals(true) ||
                this.fractionalNumber.Equals(true) || this.exponentialNumber.Equals(true))
                this.valideNumber = true;
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

        private bool checkExponentialNumber(string text)
        {
            return Regex.Match(text, this.exponentialRegularExpression).Success;
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

        public bool getExponentialNumber()
        {
            return this.exponentialNumber;
        }
    }
}
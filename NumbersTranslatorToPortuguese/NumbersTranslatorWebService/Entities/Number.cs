namespace NumbersTranslatorWebService.Entities
{
    public abstract class Number
    {
        private string number;

        internal void Initialize(string dato)
        {
            number = dato;
        }

        public bool IsNegative()
        {
            return number.StartsWith("-");
        }

        public abstract string Translate(string number);

        public string GetNumber()
        {
            return number;
        }
    }
}
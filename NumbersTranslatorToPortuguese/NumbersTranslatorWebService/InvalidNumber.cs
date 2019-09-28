using System;

namespace NumbersTranslatorWebService.Entities
{
    public class InvalidNumber : Exception
    {
        public InvalidNumber()
        {
        }

        public InvalidNumber(string message)
            : base(message)
        {
        }
    }
}
using System;
using Entities;

namespace NumbersTranslatorWebService.Entities
{
    public abstract class Number
    {
        private string number;

        internal void Initialize(string text)
        {
            number = text;
        }

        public string GetNumber()
        {
            return number;
        }

        public abstract void Translate(Treatment treatment);
        //public abstract void Translate(string text);

        public abstract string GetSentence();
    }
}
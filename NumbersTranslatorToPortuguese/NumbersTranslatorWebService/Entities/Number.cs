using System;
using Entities;

namespace NumbersTranslatorWebService.Entities
{
    public abstract class Number
    {
        private string number;
        //private string translation { get; set; }

        internal void Initialize(string dato)
        {
            number = dato;
        }

        //public bool IsNegative()
        //{
        //    return number.StartsWith("-");
        //}

        public abstract string Translate(Treatment treatment);

        public string GetNumber()
        {
            return number;
        }

        //internal void SetTranslation(string text)
        //{
        //    translation = text;
        //}

        //public string GetTranslation()
        //{
        //    return translation;
        //}
    }
}
using Entities;
using System.Collections.Generic;

namespace NumbersTranslatorWebService.Entities
{
    public abstract class Number
    {
        private string type;

        internal void Initialize(string text)
        {
            type = text;
        }

        public string GetTypeNumber()
        {
            return type;
        }

        public abstract void Translate(Treatment treatment);

        public abstract List<string> GetResults();
    }
}
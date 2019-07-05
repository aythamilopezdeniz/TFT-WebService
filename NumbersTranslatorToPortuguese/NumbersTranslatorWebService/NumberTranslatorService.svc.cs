using System;
using System.Collections;
using Entities;
using NumbersTranslatorWebService.Entities;

namespace NumbersTranslatorWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NumberTranslatorService : NumberTranslatorIService
    {
        private ArrayList list;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Treatment ValidateText(string text)
        {
            Treatment treatment = new Treatment(text);
            treatment.checkNumber();
            return treatment;
        }

        //public ArrayList TranslateText(Treatment treatment, string text)
        public ArrayList TranslateText(Treatment treatment)
        {
            list = new ArrayList();
            CardinalNumber(treatment);
            OrdinalNumber(treatment);
            FractionalNumber(treatment);
            MultiplicativeNumber(treatment);
            RomanNumber(treatment);
            //if (treatment.getValideNumber().Equals(true))
            //{
            //    if (treatment.getIntegerNumber().Equals(true))
            //    {
                    //CardinalNumber(text);
                    //OrdinalNumber(text);
                    //FractionalNumber(text);
                    //MultiplicativeNumber(text);
                    //RomanNumber(text);
                //}
            //}
            return list;
        }

        //private void CardinalNumber(string text)
        private void CardinalNumber(Treatment treatment)
        {
            //Number cardinal = new Cardinal(text);
            //cardinal.Translate(text);
            Number cardinal = new Cardinal(treatment.getText());
            cardinal.Translate(treatment);
            list.Add(cardinal.GetSentence());
        }

        //private void OrdinalNumber(string text)
        private void OrdinalNumber(Treatment treatment)
        {
            //Number ordinal = new Ordinal(text);
            //ordinal.Translate(text);
            Number ordinal = new Ordinal(treatment.getText());
            ordinal.Translate(treatment);
            list.Add(ordinal.GetSentence());
        }

        //private void FractionalNumber(string text)
        private void FractionalNumber(Treatment treatment)
        {
            //Number fractional = new Fractional(text);
            //fractional.Translate(text);
            Number fractional = new Fractional(treatment.getText());
            fractional.Translate(treatment);
            list.Add(fractional.GetSentence());
        }

        private void MultiplicativeNumber(Treatment treatment)
        {
        }

        private void RomanNumber(Treatment treatment)
        {
        }
    }
}
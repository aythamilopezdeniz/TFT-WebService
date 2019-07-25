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
            if (treatment.getValideNumber().Equals(false))
                throw new InvalidNumber("0");
            return treatment;
        }

        public ArrayList TranslateText(Treatment treatment)
        {
            list = new ArrayList();
            CardinalNumber(treatment);
            DecimalNumber(treatment);
            OrdinalNumber(treatment);
            FractionalNumber(treatment);
            MultiplicativeNumber(treatment);
            RomanNumber(treatment);
            return list;
        }

        private void CardinalNumber(Treatment treatment)
        {
            Number cardinal = new IntegerNumber(treatment.getText());
            cardinal.Translate(treatment);
            list.Add(cardinal.GetSentence());
            //list.Add(treatment.getText());
        }

        private void DecimalNumber(Treatment treatment)
        {
            Number decimalNumber = new DecimalNumber(treatment.getText());
            decimalNumber.Translate(treatment);
            list.Add(decimalNumber.GetSentence());
        }

        private void OrdinalNumber(Treatment treatment)
        {
            Number ordinal = new Ordinal(treatment.getText());
            ordinal.Translate(treatment);
            list.Add(ordinal.GetSentence());
        }

        private void FractionalNumber(Treatment treatment)
        {
            Number fractional = new Fractional(treatment.getText());
            fractional.Translate(treatment);
            list.Add(fractional.GetSentence());
        }

        private void MultiplicativeNumber(Treatment treatment)
        {
            list.Add("");
        }

        private void RomanNumber(Treatment treatment)
        {
            list.Add("");
        }
    }
}
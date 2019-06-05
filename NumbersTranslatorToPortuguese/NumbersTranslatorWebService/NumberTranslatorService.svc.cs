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

        public ArrayList TranslateText(Treatment treatment, string text)
        {
            ArrayList list = new ArrayList();
            if (treatment.getValideNumber().Equals(true))
            {
                //Number number = new Cardinal();
                //list.Add(number.GetNumber(treatment.getText()));
                Number cardinal = new Cardinal(text);
                Number ordinal = new Ordinal(text);
                //if (cardinal.IsNegative())
                //    list.Add("Menos");
                list.Add(cardinal.Translate(treatment));
                list.Add("Nada"/*ordinal.Translate(treatment)*/);
                list.Add(cardinal.GetNumber());
            }
            return list;
        }
    }
}
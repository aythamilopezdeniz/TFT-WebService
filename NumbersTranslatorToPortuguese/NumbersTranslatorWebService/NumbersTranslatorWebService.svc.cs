using System;
using Entities;
using NumbersTranslatorWebService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Text;

namespace NumbersTranslatorWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NumbersTranslatorWebService : INumbersTranslatorWebService
    {
        private List<string> list;
        private ArrayList taskList;
        private ArrayList arrayList;

        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

        //public ArrayList TranslateText(Treatment treatment)
        //{
        //    list = new ArrayList();
        //    CardinalNumber(treatment);
        //    DecimalNumber(treatment);
        //    OrdinalNumber(treatment);
        //    FractionalNumber(treatment);
        //    MultiplicativeNumber(treatment);
        //    RomanNumber(treatment);
        //    return list;
        //}

        //public List<string> TranslateText(string text)
        public ArrayList TranslateText(string text)
        {
            list = new List<string>();
            arrayList = new ArrayList();
            //Task<string> task = Task.Run(() => "hello"); Ejemplo
            try
            {
                Treatment treatment = ValidateText(text);
                StartTask(treatment);
                SaveResults();
            }
            catch (InvalidNumber ex)
            {
                arrayList = new ArrayList() { "Error", ex.Message };
                //throw new InvalidNumber(ex.Message);
            }
            //GenerateTask(treatment);
            //Task<string> task = null;
            //task = Task.Run(() => CardinalNumber(treatment));
            //list.Add(task.Result);
            //task = Task.Run(() => DecimalNumber(treatment));
            //list.Add(task.Result);
            //task = Task.Run(() => OrdinalNumber(treatment));
            //list.Add(task.Result);
            //task = Task.Run(() => MultiplicativeNumber(treatment));
            //list.Add(task.Result);
            //task = Task.Run(() => RomanNumber(treatment));
            //list.Add(task.Result);
            //CardinalNumber(treatment);
            //    DecimalNumber(treatment);
            //    OrdinalNumber(treatment);
            //    FractionalNumber(treatment);
            //    MultiplicativeNumber(treatment);
            //    RomanNumber(treatment);
            //list.Add(treatment.getValideNumber().ToString());
            //return list;
            return arrayList;
        }

        private Treatment ValidateText(string text)
        {
            Treatment treatment = new Treatment(text);
            treatment.checkNumber();
            if (treatment.getValideNumber().Equals(false))
                throw new InvalidNumber("0");
            return treatment;
        }

        //private void GenerateTask(Treatment treatment)
        //{
            //taskList.Add(new Task(() => CardinalNumber(treatment)));
            //taskList.Add(new Task(() => DecimalNumber(treatment)));
            //taskList.Add(new Task(() => OrdinalNumber(treatment)));
            //taskList.Add(new Task(() => MultiplicativeNumber(treatment)));
            //taskList.Add(new Task(() => RomanNumber(treatment)));
        //}

        private void StartTask(Treatment treatment)
        {
            taskList = new ArrayList
            {
                Task.Run(() => CardinalNumber(treatment)),
                Task.Run(() => DecimalNumber(treatment)),
                Task.Run(() => OrdinalNumber(treatment)),
                Task.Run(() => FractionalNumber(treatment)),
                //Task.Run(() => ""),
                Task.Run(() => MultiplicativeNumber(treatment)),
                Task.Run(() => RomanNumber(treatment))
            };
            //foreach (Task<string> task in taskList)
            //{
            //    task.Start();
            //}
        }

        private void SaveResults()
        {
            foreach (Task<string> task in taskList)
                arrayList.Add(task.Result);
                //list.Add(task.Result);
        }

        //private void CardinalNumber(Treatment treatment)
        //{
        //    Number cardinal = new IntegerNumber("Cardinal");
        //    cardinal.Translate(treatment);
        //    list.Add(cardinal.GetSentence());
        //}

        private string CardinalNumber(Treatment treatment)
        {
            Number cardinal = new IntegerNumber("Cardinal");
            cardinal.Translate(treatment);
            return cardinal.GetSentence();
        }

        //private void DecimalNumber(Treatment treatment)
        //{
        //    Number decimalNumber = new DecimalNumber("Decimal");
        //    decimalNumber.Translate(treatment);
        //    list.Add(decimalNumber.GetSentence());
        //}

        private string DecimalNumber(Treatment treatment)
        {
            Number decimalNumber = new DecimalNumber("Decimal");
            decimalNumber.Translate(treatment);
            return decimalNumber.GetSentence();
        }

        //private void OrdinalNumber(Treatment treatment)
        //{
        //    Number ordinal = new Ordinal("Ordinal");
        //    ordinal.Translate(treatment);
        //    list.Add(ordinal.GetSentence());
        //}

        private string OrdinalNumber(Treatment treatment)
        {
            Number ordinal = new Ordinal("Ordinal");
            ordinal.Translate(treatment);
            return ordinal.GetSentence();
        }

        //private void FractionalNumber(Treatment treatment)
        //{
        //    Number integerNumber = new IntegerNumber("Fractional");
        //    Number fractional = new Fractional("Fractional");
        //    integerNumber.Translate(treatment);
        //    fractional.Translate(treatment);
        //    CheckNegativeNumber(integerNumber, fractional);
        //}

        private string FractionalNumber(Treatment treatment)
        {
            Number integerNumber = new IntegerNumber("Fractional");
            Number fractional = new Fractional("Fractional");
            integerNumber.Translate(treatment);
            fractional.Translate(treatment);
            return CheckNegativeNumber(integerNumber, fractional);
        }

        private string CheckNegativeNumber(Number integerNumber, Number fractional)
        {
            StringBuilder text = new StringBuilder();
            if (!integerNumber.GetSentence().Equals("") && !fractional.GetSentence().Equals(""))
            {
                if (integerNumber.GetSentence().Contains("-") && fractional.GetSentence().Contains("-"))
                {
                    integerNumber.GetSentence().Replace("Menos ", "");
                    fractional.GetSentence().Replace("Menos ", "");
                    //list.Add(integerNumber.GetSentence() + " " + fractional.GetSentence());
                    return text.Append(integerNumber.GetSentence() + " " + fractional.GetSentence()).ToString();
                }
                else
                    return text.Append(integerNumber.GetSentence() + " " + fractional.GetSentence()).ToString();
            }
            else
                return text.Append("").ToString();
        }

        //private void MultiplicativeNumber(Treatment treatment)
        //{
        //    Number multiplicative = new Multiplicative("Multiplicative");
        //    multiplicative.Translate(treatment);
        //    list.Add(multiplicative.GetSentence());
        //}

        private string MultiplicativeNumber(Treatment treatment)
        {
            Number multiplicative = new Multiplicative("Multiplicative");
            multiplicative.Translate(treatment);
            return multiplicative.GetSentence();
        }

        //private void RomanNumber(Treatment treatment)
        //{
        //    Number roman = new Roman("Roman");
        //    roman.Translate(treatment);
        //    list.Add(roman.GetSentence());
        //}

        private string RomanNumber(Treatment treatment)
        {
            Number roman = new Roman("Roman");
            roman.Translate(treatment);
            return roman.GetSentence();
        }
    }
}
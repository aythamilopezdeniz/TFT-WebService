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
        private List<List<string>> list;
        private ArrayList taskList;

        public List<List<string>> TranslateText(string text)
        {
            list = new List<List<string>>();
            try
            {
                Treatment treatment = ValidateText(text);
                StartTask(treatment);
                SaveResults();
            }
            catch (InvalidNumber ex)
            {
                list = new List<List<string>>() { new List<string> { "Error", ex.Message } };
            }
            return list;
            //return new WebService().Convertion(text);
        }

        private Treatment ValidateText(string text)
        {
            Treatment treatment = new Treatment(text);
            treatment.checkNumber();
            if (treatment.GetValideNumber().Equals(false))
                throw new InvalidNumber("0");
            return treatment;
        }

        private void StartTask(Treatment treatment)
        {
            taskList = new ArrayList
            {
                Task.Run(() => CardinalNumber(treatment)),
                Task.Run(() => DecimalNumber(treatment)),
                Task.Run(() => OrdinalNumber(treatment))/*,*/
                //Task.Run(() => FractionalNumber(treatment)),
                //Task.Run(() => MultiplicativeNumber(treatment)),
                //Task.Run(() => RomanNumber(treatment))
            };
        }

        private void SaveResults()
        {
            foreach (Task<List<string>> task in taskList)
                list.Add(task.Result);
        }

        private List<string> CardinalNumber(Treatment treatment)
        {
            Number cardinal = new IntegerNumber("Cardinal");
            cardinal.Translate(treatment);
            List<string> list = cardinal.GetResults();
            if (list.Count.Equals(2))
            {
                list[0] = "<b>(DPLP)</b> " + list[0];
                list[1] = "<b>(MAT)</b> " + list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = "<b>(DPLP)</b> " + list[0];
            return list;
        }

        private List<string> DecimalNumber(Treatment treatment)
        {
            Number integerNumber = new IntegerNumber("Decimal");
            Number decimalNumber = new DecimalNumber("Decimal");
            integerNumber.Translate(treatment);
            decimalNumber.Translate(treatment);
            return CheckDecimalNumber(integerNumber, decimalNumber);
        }

        private List<string> CheckDecimalNumber(Number integerNumber, Number decimalNumber)
        {
            List<string> list = new List<string>();
            if (integerNumber.GetResults().Count > 0 && decimalNumber.GetResults().Count > 0)
            {
                if (integerNumber.GetResults().Count.Equals(2) && decimalNumber.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Contains("Zero") &&
                        integerNumber.GetResults()[1].Contains("Zero"))
                    {
                        list.Add("<b>(DPLP)</b> " + decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add("<b>(DPLP)</b> " + integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(2) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Contains("Zero") &&
                        integerNumber.GetResults()[1].Contains("Zero"))
                        list.Add("<b>(DPLP)</b> " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add("<b>(DPLP)</b> " + integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Contains("Zero"))
                    {
                        list.Add("<b>(DPLP)</b> " + decimalNumber.GetResults()[0]);
                        list.Add("<b>(DPLP)</b> " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add("<b>(DPLP)</b> " + integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Contains("Zero"))
                        list.Add("<b>(DPLP)</b> " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add("<b>(DPLP)</b> " + integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add("<b>(MAT)</b> " + integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                    }
                }
            }
            return list;
        }

        private List<string> OrdinalNumber(Treatment treatment)
        {
            Number ordinal = new Ordinal("Ordinal");
            ordinal.Translate(treatment);
            List<string> list = ordinal.GetResults();
            if (list.Count.Equals(2))
            {
                list[0] = "<b>(DPLP)</b> " + list[0];
                list[1] = "<b>(MAT)</b> " + list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = "<b>(DPLP)</b> " + list[0];
            return list;
        }

        private List<string> FractionalNumber(Treatment treatment)
        {
            Number integerNumber = new IntegerNumber("Fractional");
            Number fractional = new Fractional("Fractional");
            integerNumber.Translate(treatment);
            fractional.Translate(treatment);
            return CheckNegativeNumber(integerNumber, fractional);
        }

        private List<string> CheckNegativeNumber(Number integerNumber, Number fractional)
        {
            StringBuilder text = new StringBuilder();
            if (!integerNumber.GetResults().Equals("") && !fractional.GetResults().Equals(""))
            {
                if (integerNumber.GetResults().Contains("-") && fractional.GetResults().Contains("-"))
                {
                    //integerNumber.GetResults().Replace("Menos ", "");
                    //fractional.GetResults().Replace("Menos ", "");
                    //return text.Append(integerNumber.GetResults() + " " + fractional.GetResults()).ToString();
                }
                //else
                //return text.Append(integerNumber.GetResults() + " " + fractional.GetResults()).ToString();
            }
            //else
            //return text.Append("").ToString();
            return new List<string>();
        }

        private List<string> MultiplicativeNumber(Treatment treatment)
        {
            Number multiplicative = new Multiplicative("Multiplicative");
            multiplicative.Translate(treatment);
            return multiplicative.GetResults();
        }

        private List<string> RomanNumber(Treatment treatment)
        {
            Number roman = new Roman("Roman");
            roman.Translate(treatment);
            return roman.GetResults();
        }
    }
}
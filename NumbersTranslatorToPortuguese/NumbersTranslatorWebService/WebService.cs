using Entities;
using NumbersTranslatorWebService.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NumbersTranslatorWebService
{
    public class WebService
    {
        private List<List<string>> list;
        private ArrayList taskList;

        public List<List<string>> Convertion(string text)
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
                Task.Run(() => OrdinalNumber(treatment)),
                //Task.Run(() => FractionalNumber(treatment)),
                Task.Run(() => MultiplicativeNumber(treatment))/*,*/
                //Task.Run(() => RomanNumber(treatment))
            };
            Task[] tasks = (Task[]) taskList.ToArray(typeof(Task));
            Task.WaitAll(tasks);
        }

        private void SaveResults()
        {
            foreach (Task<List<string>> task in taskList)
                list.Add(task.Result);
        }

        private List<string> CardinalNumber(Treatment treatment)
        {
            Number cardinal = new IntegerNumber("Cardinal");
            try
            {
                cardinal.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            List<string> list = cardinal.GetResults();
            if (list.Count.Equals(2))
            {
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
                list[1] = /*"<b>(MAT)</b> " +*/ list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
            if (list.Count > 0)
                list.Insert(0, "0");
            return list;
        }

        private List<string> DecimalNumber(Treatment treatment)
        {
            Number integerNumber = new IntegerNumber("Decimal");
            Number decimalNumber = new DecimalNumber("Decimal");
            try
            {
                integerNumber.Translate(treatment);
                decimalNumber.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            return CheckDecimalNumber(integerNumber, decimalNumber);
        }

        private List<string> CheckDecimalNumber(Number integerNumber, Number decimalNumber)
        {
            List<string> list = new List<string>();
            if (integerNumber.GetResults().Count > 0 && decimalNumber.GetResults().Count > 0)
            {
                if (integerNumber.GetResults().Count.Equals(2) && decimalNumber.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero") &&
                        integerNumber.GetResults()[1].Equals("Zero"))
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */decimalNumber.GetResults()[1]);
                    }
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                    {
                        list.Add(/*"<b>(DPLP)</b> Menos " + */"Menos " + decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> Menos " + */"Menos " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(2) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero") &&
                        integerNumber.GetResults()[1].Equals("Zero"))
                        list.Add(/*"<b>(DPLP)</b> " + */decimalNumber.GetResults()[0]);
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                        list.Add(/*"<b>(DPLP)</b> Menos " + */"Menos " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero"))
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */decimalNumber.GetResults()[1]);
                    }
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                    {
                        list.Add(/*"<b>(DPLP)</b> Menos " + */"Menos " + decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> Menos " + */"Menos " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero"))
                        list.Add(/*"<b>(DPLP)</b> " + */decimalNumber.GetResults()[0]);
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                        list.Add(/*"<b>(DPLP)</b> Menos " + */"Menos " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add(/*"<b>(DPLP)</b> " + */integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(/*"<b>(MAT)</b> " + */integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                    }
                }
            }
            if (list.Count > 0)
                list.Insert(0, "1");
            return list;
        }

        private List<string> OrdinalNumber(Treatment treatment)
        {
            Number ordinal = new Ordinal("Ordinal");
            try
            {
                ordinal.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            List<string> list = ordinal.GetResults();
            if (list.Count.Equals(2))
            {
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
                list[1] = /*"<b>(MAT)</b> " +*/ list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
            if (list.Count > 0)
                list.Insert(0, "2");
            return list;
        }

        private List<string> FractionalNumber(Treatment treatment)
        {
            Number integerNumber = new IntegerNumber("Fractional");
            Number fractional = new Fractional("Fractional");
            try
            {
                integerNumber.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
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
            try
            {
                multiplicative.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            List<string> list = multiplicative.GetResults();
            if (list.Count.Equals(2))
            {
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
                list[1] = /*"<b>(MAT)</b> " +*/ list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = /*"<b>(DPLP)</b> " +*/ list[0];
            if (list.Count > 0)
                list.Insert(0, "4");
            return list;
        }

        private List<string> RomanNumber(Treatment treatment)
        {
            Number roman = new Roman("Roman");
            try
            {
                roman.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            List<string> list = roman.GetResults();
            if (list.Count > 0)
                list.Insert(0, "5");
            return list;
        }
    }
}
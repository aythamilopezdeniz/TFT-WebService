using Entities;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using NumbersTranslatorWebService.Entities;

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
                Task.Run(() => FractionalNumber(treatment)),
                Task.Run(() => MultiplicativeNumber(treatment)),
                Task.Run(() => RomanNumber(treatment))
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
                list[0] = list[0];
                list[1] = list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = list[0];
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
                        list.Add(decimalNumber.GetResults()[0]);
                        list.Add(decimalNumber.GetResults()[1]);
                    }
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                    {
                        list.Add("Menos " + decimalNumber.GetResults()[0]);
                        list.Add("Menos " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add(integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add(integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(2) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero") &&
                        integerNumber.GetResults()[1].Equals("Zero"))
                        list.Add(decimalNumber.GetResults()[0]);
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                        list.Add("Menos " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add(integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[1] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[1] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero"))
                    {
                        list.Add(decimalNumber.GetResults()[0]);
                        list.Add(decimalNumber.GetResults()[1]);
                    }
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                    {
                        list.Add("Menos " + decimalNumber.GetResults()[0]);
                        list.Add("Menos " + decimalNumber.GetResults()[1]);
                    }
                    else
                    {
                        list.Add(integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[1]);
                        list.Add(integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[0] + " <b>vírgula</b> " +
                            decimalNumber.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && decimalNumber.GetResults().Count.Equals(1))
                {
                    if (integerNumber.GetResults()[0].Equals("Zero"))
                        list.Add(decimalNumber.GetResults()[0]);
                    else if (integerNumber.GetResults()[0].Equals("Menos Zero"))
                        list.Add("Menos " + decimalNumber.GetResults()[0]);
                    else
                    {
                        list.Add(integerNumber.GetResults()[0] + " <b>inteiros e</b> " +
                            decimalNumber.GetResults()[0]);
                        list.Add(integerNumber.GetResults()[0] + " <b>vírgula</b> " +
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
                list[0] = list[0];
                list[1] = list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = list[0];
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
                fractional.Translate(treatment);
            }
            catch (InvalidNumber ex)
            {
                return new List<string> { "Error", ex.Message };
            }
            return CheckNegativeNumber(integerNumber, fractional);
        }

        private List<string> CheckNegativeNumber(Number integerNumber, Number fractional)
        {
            List<string> list = new List<string>();
            StringBuilder textNumerator = new StringBuilder();
            StringBuilder textDenominator = new StringBuilder();
            if (integerNumber.GetResults().Count > 0 && fractional.GetResults().Count > 0)
            {
                if (integerNumber.GetResults().Count.Equals(2) && fractional.GetResults().Count.Equals(4))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator = new StringBuilder(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[3].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[3]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[1].ToString()[0]) + integerNumber.GetResults()[1].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[1].ToString()[0]) + integerNumber.GetResults()[1].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[3].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[1]), new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[3]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(2) && fractional.GetResults().Count.Equals(3))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        list.Add(textNumerator + " " + textDenominator);
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        list.Add(textNumerator + " "+ textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[2]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[1].ToString()[0]) + integerNumber.GetResults()[1].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[1].ToString()[0]) + integerNumber.GetResults()[1].Substring(1));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[1]), new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[2]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(2) && fractional.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[1].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[1]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[1].ToString()[0]) + integerNumber.GetResults()[1].Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        list.Add(CheckPluralResult("", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(integerNumber.GetResults()[1] + " " + fractional.GetResults()[1]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && fractional.GetResults().Count.Equals(4))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                        textDenominator = new StringBuilder(fractional.GetResults()[3].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[3]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToUpper(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                        textDenominator = new StringBuilder(fractional.GetResults()[3].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[3]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && fractional.GetResults().Count.Equals(3))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[2].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[0])));
                        list.Add(CheckPluralResult("", new StringBuilder(integerNumber.GetResults()[0]), new StringBuilder(fractional.GetResults()[1])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[2]);
                    }
                }
                else if (integerNumber.GetResults().Count.Equals(1) && fractional.GetResults().Count.Equals(2))
                {
                    if (integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("", textNumerator, textDenominator));
                        textNumerator = new StringBuilder(char.ToUpper(textNumerator.ToString()[0]) + textNumerator.ToString().Substring(1));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add(textNumerator + " " + textDenominator);
                    }
                    else if (integerNumber.GetResults()[0].Contains("Menos") && !fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator.Append(integerNumber.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[1]);
                    }
                    else if (!integerNumber.GetResults()[0].Contains("Menos") && fractional.GetResults()[0].Contains("Menos"))
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        textDenominator.Append(fractional.GetResults()[0].Replace("Menos ", ""));
                        list.Add(CheckPluralResult("Menos ", textNumerator, textDenominator));
                        textDenominator = new StringBuilder(fractional.GetResults()[1].Replace("Menos ", ""));
                        list.Add("Menos " + textNumerator + " " + textDenominator);
                    }
                    else
                    {
                        textNumerator = new StringBuilder(char.ToLower(integerNumber.GetResults()[0].ToString()[0]) + integerNumber.GetResults()[0].Substring(1));
                        list.Add(CheckPluralResult("", textNumerator, new StringBuilder(fractional.GetResults()[0])));
                        list.Add(integerNumber.GetResults()[0] + " " + fractional.GetResults()[1]);
                    }
                }
            }
            if (list.Count > 0)
                list.Insert(0, "3");
            return list;
        }

        private string CheckPluralResult(string minus, StringBuilder numerator, StringBuilder denominator)
        {
            StringBuilder aux = numerator;
            if (minus.Equals("")) numerator = new StringBuilder(char.ToUpper(numerator[0]) + numerator.ToString().Substring(1));
            if (!aux.Equals(new StringBuilder("um")))
                return minus + numerator + " " + denominator + "s";
            else
                return minus + numerator + " " + denominator;
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
                list[0] = list[0];
                list[1] = list[1];
            }
            else if (list.Count.Equals(1))
                list[0] = list[0];
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
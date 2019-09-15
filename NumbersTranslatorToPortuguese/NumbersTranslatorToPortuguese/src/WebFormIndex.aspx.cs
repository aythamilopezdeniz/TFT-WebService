using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using NumbersTranslatorWebService.Entities;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        private ArrayList NameTabs;
        private ArrayList Numbers;
        private ArrayList Mistakes;
        private ArrayList romanNumbers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Title"] == null && Session["NameTabs"] == null)
                SpanishPage(sender, e);
            Title = (string) Session["Title"];
            NameTabs = (ArrayList) Session["NameTabs"];
            Numbers = (ArrayList) Session["Numbers"];
            Mistakes = (ArrayList) Session["Mistakes"];
        }

        protected void SpanishPage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardinal", "Decimal", "Ordinal", "Fraccionario", "Multiplicativo", "Romano" };
            Session["Title"] = "Conversor Cifras a Portugués";
            Title = "Conversor Cifras a Portugués";
            TitleLabel.Text = "Conversor cifras a texto";
            Text.Attributes.Add("placeholder", "Introduzca un número");
            Translate.Text = "Traducir";
            Session["Listen"] = "Escuchar";
            Session["Error"] = "Error";
            Session["Numbers"] = new ArrayList() { "Los números cardinales expresan cantidad en relación con la serie de los números naturales."
            , "Los números decimales expresan una cantidad en relación con la serie de los números naturales más una fracción de una unidad separada por una coma o un punto.",
            "Los números ordinales expresan orden o sucesión e indican el lugar que ocupa el elemento en una serie ordenada.",
            "Los números fraccionarios expresan división de un todo en partes y designan las fracciones iguales en que se ha dividido la unidad.",
            "Los números multiplicativos expresan que el sustantivo al que se refieren se compone de tantas unidades o implica tantas repeticiones como el numeral indica.",
            "Los números romanos expresan los valores numéricos de nuestro sistema de cifras con un repertorio de signos distintos." };
            Session["Mistakes"] = new ArrayList() { "El formato escrito no es compatible.",
                "El número es demasiado largo." };
        }

        protected void EnglishPage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardinal", "Decimal", "Ordinal", "Fractional", "Multiplicative", "Roman" };
            Session["Title"] = "Convert Numbers to Portuguese";
            Title = "Convert Numbers to Portuguese";
            TitleLabel.Text = "Convert Numbers to text";
            Text.Attributes.Add("placeholder", "Insert a number");
            Translate.Text = "Translate";
            Session["Listen"] = "Listen";
            Session["Error"] = "Error";
            Session["Numbers"] = new ArrayList() { "Cardinal numbers are a generalization of the natural numbers used to measure the size of sets.",
            "Decimal numbers describe a quantity in relation to the series of natural numbers plus a fraction of a unit separated by a comma or a point.",
            "Ordinal numbers describe order or sequence and indicate the place of the element in an well-ordered set.",
            "The fractional numbers describe division of a whole into parts and designate the fractions equal in that has been divided the unit.",
            "The multiplicative numbers describe that the noun to which they refer is composed of as many units or involves many repetitions as the numeral indicates.",
            "The Roman numerals describe the numerical values of our decimal system with different signs set." };
            Session["Mistakes"] = new ArrayList() { "The written format is not compatible.",
                "The number is too large." };
        }

        protected void PortuguesePage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardeal", "Decimal", "Ordinal", "Fracionário", "Multiplicativo", "Romano" };
            Session["Title"] = "Conversor de Numeros Portugueses";
            Title = "Conversor de Numeros Portugueses";
            TitleLabel.Text = "Converter números em texto";
            Text.Attributes.Add("placeholder", "Digite um número");
            Translate.Text = "Traduzir";
            Session["Listen"] = "Escutar";
            Session["Error"] = "Engano";
            Session["Numbers"] = new ArrayList() { "Os números cardinais expressam quantidade em relação à série de números naturais.",
            "Os números decimais expressam uma quantia em relação à série de números naturais mais uma fração de uma unidade separada por uma vírgula ou um período.",
            "Os números ordinais expressam ordem ou sequência e indicam o local que o elemento ocupa em uma série ordenada.",
            "Os números fracionários expressam a divisão de um todo em partes e designam as mesmas frações em que a unidade foi dividida.",
            "Os números multiplicativos expressam que o substantivo ao qual eles se referem é composto de tantas unidades ou implica quantas repetições o numeral indicar.",
            "Os números romanos expressam os valores numéricos do nosso sistema de figuras com um repertório de diferentes signos." };
            Session["Mistakes"] = new ArrayList() { "O formato escrito não é compatível.",
                "O número é muito longo." };
        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            if (!Text.Text.Equals(""))
            {
                NumberTranslatorWebService.NumbersTranslatorWebServiceClient service = new 
                    NumberTranslatorWebService.NumbersTranslatorWebServiceClient();
                List<IList<string>> translation = new List<IList<string>>(service.TranslateText(Text.Text));

                if (translation[0][0].Equals("Error")) Problem((string) translation[0][1]);
                else
                {
                    //CreateTabs(translation);
                    //CreateContents(translation);
                }
            }
        }

        private void Problem(string ex)
        {
            ErrorTab();
            ErrorContent((string)Mistakes[Int32.Parse(ex)]);
        }

        private void CreateTabs(List<IList<string>> translation)
        //private void CreateTabs(Object[] translation)
        {
            HtmlGenericControl tab = GetHtmlGenericControlUl();
            TabsPanel.Controls.Add(tab);
            int iter = 0;
            for (int i = 0; i < 6; i++)
            {
                HtmlGenericControl li = GetHtmlGenericControlLi();
                tab.Controls.Add(li);
                if (!translation[i].Equals(""))
                {
                    HtmlAnchor a = GetHtmlAnchorA(NameTabs[i].ToString());
                    if (iter.Equals(0)) a.Attributes["class"] += " active";
                    li.Controls.Add(a);
                    iter++;
                }
            }
        }

        private HtmlGenericControl GetHtmlGenericControlUl()
        {
            HtmlGenericControl tabs = new HtmlGenericControl("ul");
            tabs.Attributes["class"] = "nav nav-tabs";
            return tabs;
        }

        private HtmlGenericControl GetHtmlGenericControlLi()
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes["class"] = "nav-item";
            return li;
        }

        private HtmlAnchor GetHtmlAnchorA(string text)
        {
            HtmlAnchor a = new HtmlAnchor();
            a.Attributes["class"] = "nav-link";
            a.Attributes.Add("data-toggle", "tab");
            a.HRef = "#" + text;
            a.InnerText = text;
            return a;
        }

        private void CreateContents(List<IList<string>> translation)
        //private void CreateContents(Object[] translation)
        {
            HtmlGenericControl content = GetHtmlGenericControlContent();
            TabsPanel.Controls.Add(content);
            HtmlGenericControl p;
            int iter = 0;
            for (int i = 0; i < 6; i++)
            {
                if (!translation[i].Equals(""))
                {
                    HtmlGenericControl pane = GetHtmlGenericControlPane(NameTabs[i].ToString());
                    if (iter.Equals(0)) pane.Attributes["class"] += " show active";
                    content.Controls.Add(pane);
                    HtmlGenericControl number = GetHtmlGenericControlNumber((string) Numbers[i]);
                    pane.Controls.Add(number);
                    if (i.Equals(5))
                    {
                        romanNumbers = LevelsRomanNumbers((string) translation[i][0]);
                        ArrayList romanLines = GetNumLines(romanNumbers);
                        p = GetHtmlGenericControlRomanNumber(romanNumbers, romanLines);
                        pane.Controls.Add(p);
                    }
                    else
                    {
                        p = GetHtmlGenericControlP((string) translation[i][0]);
                        pane.Controls.Add(p);
                        HtmlButton voice = GetHtmlButton((string) translation[i][0]);
                        pane.Controls.Add(voice);
                    }
                    iter++;
                }
            }
        }

        private HtmlGenericControl GetHtmlGenericControlContent()
        {
            HtmlGenericControl content = new HtmlGenericControl("div");
            content.Attributes["id"] = "myTabContent";
            content.Attributes["class"] = "tab-content";
            return content;
        }

        private HtmlGenericControl GetHtmlGenericControlPane(string name)
        {
            HtmlGenericControl pane = new HtmlGenericControl("div");
            pane.Attributes["class"] = "tab-pane fade";
            pane.Attributes["id"] = name;
            return pane;
        }

        private HtmlGenericControl GetHtmlGenericControlNumber(string text)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.Attributes.Add("id", "number");
            p.InnerHtml = text;
            return p;
        }

        private HtmlGenericControl GetHtmlGenericControlP(string text)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.Attributes.Add("id", "text-justify");
            p.InnerHtml = text;
            return p;
        }

        private ArrayList LevelsRomanNumbers(string text)
        {
            ArrayList list = new ArrayList();
            while (text.Length > 0)
            {
                if (text.Contains(","))
                {
                    list.Add(text.Substring(0, text.IndexOf(",")));
                    text = text.Substring(text.IndexOf(",") + 1);
                }
                else
                {
                    list.Add(text);
                    text = "";
                }
            }
            return list;
        }

        private ArrayList GetNumLines(ArrayList text)
        {
            string number = "";
            int numberLines = 0;
            ArrayList lines = new ArrayList();
            ArrayList modify = new ArrayList();
            foreach (string roman in text)
            {
                number = roman;
                while (number.Contains("raya"))
                {
                    if (number.Contains("raya"))
                    {
                        numberLines++;
                        number = number.Substring(number.IndexOf("raya") + 4);
                    }
                }
                modify.Add(number);
                lines.Add(numberLines);
                numberLines = 0;
            }
            romanNumbers = modify;
            return lines;
        }

        private HtmlGenericControl GetHtmlGenericControlRomanNumber(ArrayList romanNumbers, ArrayList numberLines)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.Attributes.Add("id", "text-justify");
            for (int i = 0; i < romanNumbers.Count; i++)
            {
                p.Controls.Add(GetHtmlSpan(romanNumbers[i].ToString(), Int32.Parse(numberLines[i].ToString())));
            }
            return p;
        }

        private HtmlGenericControl GetHtmlSpan(string text, int lines)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            if (lines.Equals(1)) span.Attributes["id"] = "decoration_one_line";
            else if (lines.Equals(2)) span.Attributes["id"] = "decoration_two_line";
            else if (lines.Equals(3)) span.Attributes["id"] = "decoration_three_line";
            span.InnerHtml = text;
            return span;
        }

        private HtmlButton GetHtmlButton(string text)
        {
            HtmlButton voice = new HtmlButton();
            voice.Attributes["class"] = "btn btn-outline-primary btn-lg btn-block";
            voice.Attributes["type"] = "button";
            voice.Attributes.Add("onclick", "responsiveVoice.speak(\'" + text + "\', \'Portuguese Female\');");
            voice.InnerText = (string)Session["Listen"];
            return voice;
        }

        private void ErrorTab()
        {
            HtmlGenericControl tab = GetHtmlGenericControlUl();
            TabsPanel.Controls.Add(tab);
            HtmlGenericControl li = GetHtmlGenericControlLi();
            tab.Controls.Add(li);
            HtmlAnchor a = GetHtmlAnchorA((string) Session["Error"]);
            a.Attributes["class"] += " active";
            li.Controls.Add(a);
        }

        private void ErrorContent(string error)
        {
            HtmlGenericControl content = GetHtmlGenericControlContent();
            TabsPanel.Controls.Add(content);
            HtmlGenericControl pane = GetHtmlGenericControlPane((string) Session["Error"]);
            pane.Attributes["class"] += " show active";
            content.Controls.Add(pane);
            HtmlGenericControl number = GetHtmlGenericControlNumber(error);
            pane.Controls.Add(number);
        }
    }
}
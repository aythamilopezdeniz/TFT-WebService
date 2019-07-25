using Entities;
using NumbersTranslatorWebService;
using NumbersTranslatorWebService.Entities;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        private ArrayList NameTabs;
        private ArrayList Numbers;
        private ArrayList Mistakes;

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
                NumberTranslatorService service = new NumberTranslatorService();
                try
                {
                    Treatment treatment = service.ValidateText(Text.Text);
                    try
                    {
                        ArrayList translation = service.TranslateText(treatment);

                        CreateTabs(translation);
                        CreateContents(translation);
                    }
                    catch (InvalidNumber ex)
                    {
                        ErrorTab();
                        ErrorContent((string) Mistakes[Int32.Parse(ex.Message)]);
                    }
                }
                catch (InvalidNumber ex)
                {
                        ErrorTab();
                        ErrorContent((string) Mistakes[Int32.Parse(ex.Message)]);
                }
            }
        }

        private void CreateTabs(ArrayList translation)
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

        private void CreateContents(ArrayList translation)
        {
            HtmlGenericControl content = GetHtmlGenericControlContent();
            TabsPanel.Controls.Add(content);
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
                    HtmlGenericControl p = GetHtmlGenericControlP((string) translation[i]);
                    pane.Controls.Add(p);
                    HtmlButton voice = GetHtmlButton((string) translation[i]);
                    pane.Controls.Add(voice);
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
            p.InnerHtml = text;
            return p;
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
using Entities;
using NumbersTranslatorWebService;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        private ArrayList NameTabs;
        private ArrayList Numbers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Title"] == null && Session["NameTabs"] == null)
                SpanishPage(sender, e);
            Title = (string)Session["Title"];
            NameTabs = (ArrayList) Session["NameTabs"];
            Numbers = (ArrayList) Session["Numbers"];
        }

        protected void SpanishPage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardinal", "Ordinal", "Fraccionario", "Multiplicativo", "Romano" };
            Session["Title"] = "Conversor Cifras a Portugués";
            Title = "Conversor Cifras a Portugués";
            TitleLabel.Text = "Conversor cifras a texto";
            Text.Attributes.Add("placeholder", "Introduzca un número");
            Translate.Text = "Traducir";
            Session["Listen"] = "Escuchar";
            Session["Numbers"] = new ArrayList() { "Los números cardinales expresan cantidad en relación con la serie de los números naturales."
            , "Los números ordinales expresan orden o sucesión e indican el lugar que ocupa el elemento en una serie ordenada.",
            "Los números fraccionarios expresan división de un todo en partes y designan las fracciones iguales en que se ha dividido la unidad.",
            "Los números multiplicativos expresan que el sustantivo al que se refieren se compone de tantas unidades o implica tantas repeticiones como el numeral indica.",
            "Los números romanos expresan los valores numéricos de nuestro sistema de cifras con un repertorio de signos distintos." };
        }

        protected void EnglishPage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardinal", "Ordinal", "Fractional", "Multiplicative", "Roman" };
            Session["Title"] = "Convert Numbers to Portuguese";
            Title = "Convert Numbers to Portuguese";
            TitleLabel.Text = "Convert Numbers to text";
            Text.Attributes.Add("placeholder", "Insert a number");
            Translate.Text = "Translate";
            Session["Listen"] = "Listen";
            Session["Numbers"] = new ArrayList() { "Cardinal numbers are a generalization of the natural numbers used to measure the size of sets.",
            "Ordinal numbers describe order or sequence and indicate the place of the element in an well-ordered set.",
            "The fractional numbers describe division of a whole into parts and designate the fractions equal in that has been divided the unit.",
            "The multiplicative numbers describe that the noun to which they refer is composed of as many units or involves many repetitions as the numeral indicates.",
            "The Roman numerals describe the numerical values of our decimal system with different signs set." };
        }

        protected void PortuguesePage(object sender, EventArgs e)
        {
            Session["NameTabs"] = new ArrayList() { "Cardeal", "Ordinal", "Fracionário", "Multiplicativo", "Romano" };
            Session["Title"] = "Conversor de Numeros Portugueses";
            Title = "Conversor de Numeros Portugueses";
            TitleLabel.Text = "Converter números em texto";
            Text.Attributes.Add("placeholder", "Digite um número");
            Translate.Text = "Traduzir";
            Session["Listen"] = "Escutar";
            Session["Numbers"] = new ArrayList() { "Os números cardinais expressam quantidade em relação à série de números naturais.",
            "Os números ordinais expressam ordem ou sequência e indicam o local que o elemento ocupa em uma série ordenada.",
            "Os números fracionários expressam a divisão de um todo em partes e designam as mesmas frações em que a unidade foi dividida.",
            "Os números multiplicativos expressam que o substantivo ao qual eles se referem é composto de tantas unidades ou implica quantas repetições o numeral indicar.",
            "Os números romanos expressam os valores numéricos do nosso sistema de figuras com um repertório de diferentes signos." };
        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            if (!Text.Text.Equals(""))
            {
                NumberTranslatorService service = new NumberTranslatorService();
                Treatment treatment = service.ValidateText(Text.Text);
                ArrayList translation = service.TranslateText(treatment);

                CreateTabs(translation);
                CreateContents(translation);
            }
        }

        private void CreateTabs(ArrayList translation)
        {
            HtmlGenericControl tab = GetHtmlGenericControlUl();
            TabsPanel.Controls.Add(tab);
            for (int i = 0; i < 5; i++)
            {
                HtmlGenericControl li = GetHtmlGenericControlLi();
                tab.Controls.Add(li);
                if (!translation[i].Equals(""))
                {
                    HtmlAnchor a = GetHtmlAnchorA(NameTabs[i].ToString());
                    if (i.Equals(0)) a.Attributes["class"] += " active";
                    li.Controls.Add(a);
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
            for (int i = 0; i < 5; i++)
            {
                if (!translation[i].Equals(""))
                {
                    HtmlGenericControl pane = GetHtmlGenericControlPane(NameTabs[i].ToString());
                    if (i.Equals(0)) pane.Attributes["class"] += " show active";
                    content.Controls.Add(pane);
                    HtmlGenericControl number = GetHtmlGenericControlNumber((string) Numbers[i]);
                    pane.Controls.Add(number);
                    HtmlGenericControl p = GetHtmlGenericControlP((string) translation[i]);
                    pane.Controls.Add(p);
                    HtmlButton voice = GetHtmlButton((string) translation[i]);
                    pane.Controls.Add(voice);
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
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        private ArrayList NameTabs;
        private ArrayList Numbers;
        private ArrayList Mistakes;
        private string Options;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Title"] == null && Session["NameTabs"] == null)
                SpanishPage(sender, e);
            Title = (string) Session["Title"];
            NameTabs = (ArrayList) Session["NameTabs"];
            Numbers = (ArrayList) Session["Numbers"];
            Mistakes = (ArrayList) Session["Mistakes"];
            Options = (string) Session["Options"];
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
            Session["Options"] = "Otras alternativas";
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
            Session["Options"] = "Others alternatives";
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
            Session["Options"] = "Outras alternativas";
        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            if (!Text.Text.Equals(""))
            {
                NumberTranslatorWebService.NumbersTranslatorWebServiceClient service = new 
                    NumberTranslatorWebService.NumbersTranslatorWebServiceClient();
                List<IList<string>> translation = new List<IList<string>>(service.TranslateText(Text.Text));

                if ((translation[0].Count > 0) && translation[0][0].ToString().Equals("Error"))
                    Problem(translation[0][1].ToString());
                else
                {
                    CreateTabs(translation);
                    CreateContents(translation);
                }
            }
        }

        private void Problem(string ex)
        {
            ErrorTab();
            ErrorContent((string)Mistakes[Int32.Parse(ex)]);
        }

        private void CreateTabs(List<IList<string>> translation)
        {
            HtmlGenericControl tab = GetHtmlGenericControlUl();
            TabsPanel.Controls.Add(tab);
            int iter = 0;
            for (int i = 0; i < translation.Count; i++)
            {
                HtmlGenericControl li = GetHtmlGenericControlLi();
                tab.Controls.Add(li);
                for (int j = 0; j < translation[i].Count; j++)
                {
                    if (!translation[i][j].Equals("") && j.Equals(0))
                    {
                        HtmlAnchor a = GetHtmlAnchorA(NameTabs[Int32.Parse(translation[i][j].ToString())].ToString());
                        if (iter.Equals(0)) a.Attributes["class"] += " active";
                        li.Controls.Add(a);
                        iter++;
                    }
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
        {
            HtmlGenericControl content = GetHtmlGenericControlContent();
            TabsPanel.Controls.Add(content);
            HtmlGenericControl p;
            HtmlGenericControl pane = null;
            int iter = 0;
            for (int i = 0; i < translation.Count; i++)
            {
                bool accordion = false;
                HtmlGenericControl accordionContainer = null;
                HtmlGenericControl accordionPanelDefault = null;
                for (int j = 0; j < translation[i].Count; j++)
                {
                    if (translation[i].Count > 2 && accordion.Equals(false))
                    {
                        accordionContainer = GetHtmlGenericAccordion("container");
                        HtmlGenericControl accordionGroup = GetHtmlGenericAccordion("panel-group");
                        accordionContainer.Controls.Add(accordionGroup);
                        accordionPanelDefault = GetHtmlGenericAccordion("panel panel-default");
                        accordionGroup.Controls.Add(accordionPanelDefault);
                        HtmlGenericControl accordionPanelHeading = GetHtmlGenericAccordion("panel-heading");
                        accordionPanelDefault.Controls.Add(accordionPanelHeading);
                        HtmlAnchor title = GetHtmlGenericAccordionTitle(Options);
                        accordionPanelHeading.Controls.Add(title);
                        accordion = true;
                    }
                    if (j.Equals(0))
                    {
                        pane = GetHtmlGenericControlPane(NameTabs[Int32.Parse(translation[i][j].ToString())].ToString());
                        if (iter.Equals(0)) pane.Attributes["class"] += " show active";
                        content.Controls.Add(pane);
                        HtmlGenericControl number = GetHtmlGenericControlNumber((string)Numbers[i]);
                        pane.Controls.Add(number);
                        iter++;
                    }
                    else if (j.Equals(1))
                    {
                        if (i.Equals(5))
                        {
                            p = GetHtmlGenericControlP("<b>"/*(DPLP)*/ + (string)translation[i][j] + "</ b >");
                            pane.Controls.Add(p);
                        }
                        else
                        {
                            p = GetHtmlGenericControlP("<b>(DPLP)</b> " + (string)translation[i][j]);
                            pane.Controls.Add(p);
                            HtmlButton voice = GetHtmlButton((string)translation[i][j]);
                            pane.Controls.Add(voice);
                        }
                    }
                    else
                    {
                        HtmlGenericControl accordionElements = GetHtmlGenericAccordionPanelElements();
                        HtmlGenericControl accordionElement = GetHtmlGenericAccordionElement("<b>(MAT)</b> " + (string)translation[i][j]);
                        accordionElements.Controls.Add(accordionElement);
                        accordionPanelDefault.Controls.Add(accordionElements);
                    }
                }
                if (accordionContainer != null)
                    pane.Controls.Add(accordionContainer);
            }
        }

        private HtmlGenericControl GetHtmlGenericAccordion(string text)
        {
            HtmlGenericControl accordion = new HtmlGenericControl("div");
            accordion.Attributes.Add("class", text);
            return accordion;
        }

        private HtmlAnchor GetHtmlGenericAccordionTitle(string text)
        {
            HtmlAnchor title = new HtmlAnchor();
            title.Attributes.Add("id", "alternativeTitle");
            title.Attributes.Add("data-toggle", "collapse");
            title.HRef = "#collapse1";
            title.InnerHtml = "<b>+ " + text + "</b>";
            return title;
        }

        private HtmlGenericControl GetHtmlGenericAccordionPanelElements()
        {
            HtmlGenericControl accordion = new HtmlGenericControl("div");
            accordion.Attributes.Add("id", "collapse1");
            accordion.Attributes.Add("class", "panel-collapse collapse");
            return accordion;
        }

        private HtmlGenericControl GetHtmlGenericAccordionElement(string text)
        {
            HtmlGenericControl accordionElement = new HtmlGenericControl("div");
            accordionElement.Attributes.Add("id", "text-justify");
            accordionElement.Attributes.Add("class", "panel-body");
            accordionElement.InnerHtml = text;
            return accordionElement;
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
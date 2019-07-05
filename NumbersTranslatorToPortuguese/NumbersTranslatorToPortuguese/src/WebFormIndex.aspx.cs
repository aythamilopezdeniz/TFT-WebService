using Entities;
using NumbersTranslatorWebService;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        private ArrayList NameTabs { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            NameTabs = new ArrayList() { "Cardinal", "Ordinal", "Fraccionario", "Multiplicativo", "Romano" };
            if (!Text.Text.Equals(""))
            {
                ShowTextBox();

                //CreateTabs();

                NumberTranslatorService service = new NumberTranslatorService();
                Treatment treatment = service.ValidateText(Text.Text);
                ArrayList translation = service.TranslateText(treatment);
                
                //CreateContents(service);
                
                // Prueba
                //TextResult1.Text = treatment.getIntegerNumber().ToString();
                //ArrayList list = service.TranslateText(treatment, Text.Text);
                //string number = (string) list[0]; // otra opción: string number = list[0].ToString();
                //TextResult1.Text = number;

                //TextResult1.Text = (string)service.TranslateText(treatment, Text.Text)[0];
                //TextResult2.Text = (string)service.TranslateText(treatment, Text.Text)[1];
                //TextResult3.Text = (string)service.TranslateText(treatment, Text.Text)[2];
                //TextResult4.Text = treatment.getExponentialNumber().ToString();

                TextResult1.Text = (string) translation[0];
                TextResult2.Text = (string) translation[1];
                TextResult3.Text = (string) translation[2];
                TextResult4.Text = treatment.getExponentialNumber().ToString();

                //TextResult1.InnerHtml = (string)service.TranslateText(treatment, Text.Text)[0];
                //TextResult2.InnerHtml = (string)service.TranslateText(treatment, Text.Text)[1];
                //TextResult3.InnerHtml = treatment.getFractionalNumber().ToString();
                //TextResult4.InnerHtml = treatment.getExponentialNumber().ToString();
            }
            else
            {
                DontShowTextBox();
            }
        }

        private void CreateTabs()
        {
            HtmlGenericControl tabs = new HtmlGenericControl("ul");
            tabs.Attributes["class"] = "nav nav-tabs";
            TabsPanel.Controls.Add(tabs);

            for (int i = 0; i < NameTabs.ToArray().Length; i++)
            {
                HtmlGenericControl list = new HtmlGenericControl("li");
                list.Attributes["class"] = "nav-item";
                tabs.Controls.Add(list);

                HtmlGenericControl linkTab = new HtmlGenericControl("a");
                linkTab.Attributes["class"] = "nav-link";
                if(i.Equals(0))
                    linkTab.Attributes["class"] += " active";
                linkTab.Attributes.Add("data-toggle", "tab");
                linkTab.Attributes.Add("href", "#" + NameTabs[i].ToString());
                linkTab.InnerText = NameTabs[i].ToString();
                list.Controls.Add(linkTab);
            }
        }

        private void CreateContents(NumberTranslatorService service)
        {
            HtmlGenericControl tabsContent = new HtmlGenericControl("div");
            tabsContent.Attributes["id"] = "myTabContent";
            tabsContent.Attributes["class"] = "tab-content";
            TabsPanel.Controls.Add(tabsContent);

            Treatment treatment = service.ValidateText(Text.Text);
            //ArrayList list = service.TranslateText(treatment, Text.Text);

            for (int i = 0; i < 3; i++)
            {
                HtmlGenericControl pane = new HtmlGenericControl("div");
                pane.Attributes["class"] = "tab-pane fade";
                if (i.Equals(0))
                    pane.Attributes["class"] += "show active";
                pane.Attributes["id"] = NameTabs[i].ToString();
                tabsContent.Controls.Add(pane);

                HtmlGenericControl p = new HtmlGenericControl("p");
                //p.InnerHtml = (string) service.TranslateText(treatment, Text.Text)[i];
                p.InnerHtml = (string) service.TranslateText(treatment)[i];
                pane.Controls.Add(p);
            }
        }

        private void ShowTextBox()
        {
            //if (tabs.Visible == false) tabs.Visible = true;
            if (TextResult1.Visible == false) TextResult1.Visible = true;
            if (TextResult2.Visible == false) TextResult2.Visible = true;
            if (TextResult3.Visible == false) TextResult3.Visible = true;
            if (TextResult4.Visible == false) TextResult4.Visible = true;
            TextResult1.ReadOnly = true;
            TextResult2.ReadOnly = true;
            TextResult3.ReadOnly = true;
            TextResult4.ReadOnly = true;
        }

        private void DontShowTextBox()
        {
            TextResult1.Visible = false;
            TextResult2.Visible = false;
            TextResult3.Visible = false;
            TextResult4.Visible = false;
        }
    }
}
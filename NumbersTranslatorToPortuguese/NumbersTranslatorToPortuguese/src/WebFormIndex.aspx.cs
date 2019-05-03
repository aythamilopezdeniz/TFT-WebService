using Entities;
using NumbersTranslatorWebService;
using System;
using System.Collections;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            if (!Text.Text.Equals(""))
            {
                NumberTranslatorService service = new NumberTranslatorService();
                Treatment treatment = service.ValidateText(Text.Text);
                //TextResult1.Text = treatment.getIntegerNumber().ToString();
                //ArrayList list = service.TranslateText(treatment, Text.Text);
                //string number = (string) list[0]; // otra opción: string number = list[0].ToString();
                //TextResult1.Text = number;
                if (TextResult1.Visible == false) TextResult1.Visible = true;
                if (TextResult2.Visible == false) TextResult2.Visible = true;
                if (TextResult3.Visible == false) TextResult3.Visible = true;
                if (TextResult4.Visible == false) TextResult4.Visible = true;

                TextResult1.Text = (string)service.TranslateText(treatment, Text.Text)[0];
                TextResult2.Text = (string)service.TranslateText(treatment, Text.Text)[1];
                TextResult3.Text = treatment.getFractionalNumber().ToString();
                TextResult4.Text = treatment.getExponentialNumber().ToString();
            }
            else
            {
                TextResult1.Visible = false;
                TextResult2.Visible = false;
                TextResult3.Visible = false;
                TextResult4.Visible = false;
            }
        }
    }
}
using Entities;
using NumbersTranslatorWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NumbersTranslatorToPortuguese.src
{
    public partial class WebFormIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Validate_Text(object sender, EventArgs e)
        {
            NumberTranslatorService service = new NumberTranslatorService();
            Treatment treatment = service.ValidateText(Text.Text);
            TextResult1.Text = treatment.getIntegerNumber().ToString();
            TextResult2.Text = treatment.getDecimalNumber().ToString();
            TextResult3.Text = treatment.getFractionalNumber().ToString();
            TextResult4.Text = treatment.getExponentialNumber().ToString();
        }
    }
}
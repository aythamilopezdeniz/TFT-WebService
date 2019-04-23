<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormIndex.aspx.cs" Inherits="NumbersTranslatorToPortuguese.src.WebFormIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <title>Conversor Cifras a Portugués</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="background-image">
            <img src="img/background-image.jpg" alt="background-image" />
        </div>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="input-group">
                        <asp:TextBox ID="Text" class="form-control border-secondary py-2" runat="server" placeholder="Introduzca un número"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="Translate" class="btn btn-primary" runat="server" Text="Validar" OnClick="Validate_Text"/>
                        </div>
                    </div>
                    <br />
                    <asp:TextBox ID="TextResult1" class="form-control border-secondary py-2" runat="server" placeholder="Resultado 1"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult2" class="form-control border-secondary py-2" runat="server" placeholder="Resultado 2"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult3" class="form-control border-secondary py-2" runat="server" placeholder="Resultado 3"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult4" class="form-control border-secondary py-2" runat="server" placeholder="Resultado 4"></asp:TextBox>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
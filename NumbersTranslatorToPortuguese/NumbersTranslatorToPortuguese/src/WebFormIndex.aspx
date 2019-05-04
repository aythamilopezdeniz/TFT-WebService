<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormIndex.aspx.cs" Inherits="NumbersTranslatorToPortuguese.src.WebFormIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Conversor Cifras a Portugués</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/bootstrap-lumen.min.css" />
</head>
<body>
    <div class="jumbotron">
<%--        <h1 id="title">Jumbotron</h1>
        <p>This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
        <p><a class="btn btn-primary btn-lg">Learn more</a></p>--%>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <form id="form1" runat="server">
                    <div class="input-group">
                        <asp:TextBox ID="Text" CssClass="form-control border-secondary py-2" runat="server" placeholder="Introduzca un número"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="Translate" CssClass="btn btn-primary" runat="server" Text="Traducir" OnClick="Validate_Text" />
                        </span>
                    </div>
                    <br />
                    <asp:TextBox ID="TextResult1" CssClass="form-control border-secondary py-2" runat="server" placeholder="Resultado 1" Visible="false"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult2" CssClass="form-control border-secondary py-2" runat="server" placeholder="Resultado 2" Visible="false"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult3" CssClass="form-control border-secondary py-2" runat="server" placeholder="Resultado 3" Visible="false"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextResult4" CssClass="form-control border-secondary py-2" runat="server" placeholder="Resultado 4" Visible="false"></asp:TextBox>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
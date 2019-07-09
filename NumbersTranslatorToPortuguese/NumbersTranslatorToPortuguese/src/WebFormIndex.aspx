<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormIndex.aspx.cs" Inherits="NumbersTranslatorToPortuguese.src.WebFormIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Conversor Cifras a Portugués</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/bootstrap-lumen.min.css" />
    <link type="text/css" rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="jumbotron">
            <h1 id="title">
                <asp:Label ID="TitleLabel" runat="server" Text="Label">Traductor cifras a texto</asp:Label>
            </h1>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="box-lang">
                        <div class="icon-left-lang">
                            <i class="fa fa-language fa-lg"></i>
                            <div class="links-lang">
                                <asp:LinkButton ID="Spanish" runat="server" OnClick="SpanishPage">ES</asp:LinkButton>
                                <asp:LinkButton ID="English" runat="server" OnClick="EnglishPage">EN</asp:LinkButton>
                                <asp:LinkButton ID="Portuguese" runat="server" OnClick="PortuguesePage">PT</asp:LinkButton>
                            </div>
                        </div>
                    </div>
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
                </div>
            </div>
        </div>
        <br />
        <asp:Panel ID="TabsPanel" CssClass="container" runat="server"></asp:Panel>
    </form>
</body>
</html>
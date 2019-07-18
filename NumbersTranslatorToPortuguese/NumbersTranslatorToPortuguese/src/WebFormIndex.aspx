<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormIndex.aspx.cs" Inherits="NumbersTranslatorToPortuguese.src.WebFormIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Conversor Cifras a Portugués</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/bootstrap-lumen.min.css" />
    <link type="text/css" rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/responsivevoice.js"></script>
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
                    <asp:Panel ID="TabsPanel" CssClass="container" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

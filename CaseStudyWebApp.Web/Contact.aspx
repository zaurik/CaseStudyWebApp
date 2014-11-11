<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="CaseStudyWebApp.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        65, Walukarama Road,<br />
        Colombo 03, SRI LANKA<br />
        <abbr title="Phone">P:</abbr>
        (+94) 114 721 194
    </address>

    <address>
        <strong>Email: </strong><a href="mailto:info@99x.lk">info@99x.lk</a><br />
    </address>
</asp:Content>

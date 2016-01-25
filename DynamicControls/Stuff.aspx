<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stuff.aspx.cs" Inherits="DynamicControls.Stuff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox runat="server" ID="FilterField"/>
    <asp:Panel ID="FilterPanel" runat="server" CssClass="panel panel-default"></asp:Panel>
    <asp:Panel ID="DataPanel" runat="server" CssClass="panel panel-default"></asp:Panel>
</asp:Content>

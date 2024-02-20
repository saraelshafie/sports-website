<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="Fantasy.TestPage" %>

<!DOCTYPE html>

<html
  lang="en"
  class="light-style layout-menu-fixed"
  dir="ltr"
  data-theme="theme-default"
  data-assets-path="../assets/"
  data-template="vertical-menu-template-free"
>
  <head>
  
      <title>
          Fantasy
      </title>

  </head>

<body>
    <form runat="server"> 

    <asp:TextBox ID="test" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
    <asp:Button ID="button" runat="server" Text="Print" onclick="onPrint"/>

    </form>

</body>
</html>
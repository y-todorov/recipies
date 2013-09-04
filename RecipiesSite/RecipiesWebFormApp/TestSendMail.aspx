<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestSendMail.aspx.cs" Inherits="RecipiesWebFormApp.TestSendMail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script src=http://cdn.pubnub.com/pubnub-3.5.3.1.min.js ></script>
<script>(function () {

    debugger;
    // Init
    var pubnub = PUBNUB.init({
        publish_key: 'demo',
        subscribe_key: 'demo'
    })

    // LISTEN
    pubnub.subscribe({
        channel: "hello_world",
        message: function (m) { alert(m) },
        connect: publish
    })

    // SEND
    function publish() {
        pubnub.publish({
            channel: "hello_world",
            message: "Hi."
        })
    }

})();</script>
    </telerik:RadCodeBlock>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="EntryWebFormsApplication1.UserPage" %>

<!DOCTYPE html>
<html>
<head>
    <title>Employee Entry History</title>
    <!-- Include SignalR JavaScript library
        <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/2.4.2/jquery.signalR.min.js"></script>
        
    
        <script src="/signalr/hubs"></script> <!-- Auto-generated SignalR hubs script
    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
        <script src="~/lib/signalr/signalr.js"></script>
        -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/jquery.signalR-2.4.3.js") %>" type="text/javascript"></script>
     <script src="/signalr/js"></script>
    <style>
        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee Entry History</h2>
            <p>
                Select Employee:
                <asp:DropDownList ID="ddlEmployees" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
                    <asp:ListItem Value="">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <br />
                Or Enter Employee ID:
                <asp:TextBox ID="txtEmployeeId" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchById" runat="server" Text="Search by ID" OnClick="btnSearchById_Click" />
            </p>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Table ID="tblEntries" runat="server" BorderWidth="1" CellPadding="5" CellSpacing="0">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Event Type</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Timestamp</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </form>

    <script type="text/javascript">
        $(function () {
            var entryHub = $.connection.entryHub;

            // Function to add a new entry to the table
            function addEntry(employeeId, actionType, timestamp) {
                console.log(`added entry: ${employeeId}, ${actionType}, ${timestamp}`)
                $('#lblErrorMessage').hide();
                $('tbody').children('tr:first').after('<tr><td>' + actionType + '</td><td>' + timestamp + '</td></tr>');
            }

            // Now that the connection is established, define the client-side event handler
            entryHub.client.NewEntry = function (employeeId, actionType, timestamp) {
                console.log("new entry call");
                addEntry(employeeId, actionType, timestamp);
            };

            // Start the SignalR connection
            $.connection.hub.start().done(function () {
                console.log(`Connected to SignalR Hub`);

                

                // Optionally, you can request initial data here if needed
            }).fail(function (err) {
                console.error('Could not connect to SignalR Hub: ' + err);
            });
        });
    </script>
</body>
</html>

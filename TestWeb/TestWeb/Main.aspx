<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TestWeb.Main" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="ProjectsURL" />
    <div class="row">
        <asp:Label runat="server" Text="Projects"></asp:Label>
        <div class="col-lg-12">
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" OnClientClick="RefreshTable(); return false;" Text="Refresh" />
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" OnClientClick="AddProduct(); return false;" Text="Add Product" />
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" OnClientClick="DeleteProduct(); return false;" Text="Delete" />
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" OnClientClick="UpdateProduct(); return false;" Text="Update" /> 
            </div>
        </div>
    </div>
    <%-- projects table init --%>
    <div class="row">
        <div id="ProjectsTable"></div>
    </div>

    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/respond.js"></script>
    <script src= "Scripts/dx.all.js" ></script>
    <script src="Scripts/datajs-1.1.2.min.js"></script>
    <script>

        $(document).ready(function () {
            if (sessionStorage.getItem("Token") != null) {
                //token found
            }
            else {
                //no token login again.
                window.location.replace("Login.aspx");
            }
        });

        function LoadProjects() {
            //GET
            $("#ProjectsTable").dxdatagrid({
                dataSource: {
                    store: {
                        type: "odata",
                        url: $("#<%=ProjectsURL.ClientID%>").val(),
                        headers: {
                            "Authorization": "Token" + sessionStorage.getItem("Token")
                        }
                    }
                },
                columns: [{
                    dataField: "pk",
                    visible: false 
                }, {
                        dataField: "title",
                        caption: "Title",
                    }, {
                        dataField: "description",
                        caption: "Description"
                }, {
                        dataField: "start_Date",
                        caption: "Start Date"
                    }, {
                        dataField: "end_date",
                        caption: "End Date"
                }, {
                        dataField: "is_Billable",
                        caption: "Billable"
                    }, {
                        dataField: "is_Active",
                        caption: "Active"
                }],
                onRowClick: function (e) {
                    //open modal to view project details.
                }
            })
        }

        function LoadTasks() {
            //GET
            $("#ProjectsTable").dxdatagrid({
                dataSource: {
                    store: {
                        type: "odata",
                        url: $("#<%=ProjectsURL.ClientID%>").val(),
                        headers: {
                            "Authorization": "Token" + sessionStorage.getItem("Token")
                        }
                    }
                },
                columns: [{
                    dataField: "id",
                    visible: false
                }, {
                    dataField: "title",
                    caption: "Title",
                }, {
                    dataField: "description",
                    caption: "Description"
                }, {
                    dataField: "due_date",
                    caption: "Due Date"
                }, {
                    dataField: "estimated_hours",
                    caption: "Estimated Hours"
                }, {
                    dataField: "project",
                    visible: false
                }],
                onRowClick: function (e) {
                    //open modal to view task details.
                }
            })
        }

        function RefreshTable() {
            LoadProjects();
            LoadTasks();
        }

        function DeleteProduct() {

        }

        function UpdateProduct() {

        }

    </script>
</asp:Content>

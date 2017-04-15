<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TestWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/styles.css" rel="stylesheet" />
</head>
<body class="login">
    <div class='wrapper'>
      <div class='row'>
        <div class='col-lg-12'>
          <div class='brand text-center'>
            <h1>
              <div class='logo-icon'>
              </div>
              Tangent Projects
            </h1>
          </div>
        </div>
      </div>
      <div class='row'>
        <div class='col-lg-12'>
          <form runat="server">
              <asp:HiddenField runat="server" ID="LoginURL" />
            <fieldset class='text-center'>
                <legend>Account Login</legend>
              <div class='form-group'>
                <input runat="server" class='form-control' placeholder='User Name' type='text' id="User" name="txtUser"/>
              </div>
              <div class='form-group'>
                <input runat="server" class='form-control' placeholder='Password' type='password' id="Password" name="txtPassword"/>
              </div>
              <div class='text-center'>
                <div class='checkbox'>
                  <label>
                    <input type='checkbox'>
                    Remember me on this computer
                  </label>
                </div>
                <asp:Button runat="server" CssClass="btn btn-default" ID="btnLogin" OnClick="btnLogin_Click" Text="Login"/>
                <br>
                <asp:Label runat="server" Text="" ID="lblError" ForeColor="#ff3300"></asp:Label>
                <br />
                  <div class="row">
                      <a href="forgot_password.html" onclick="errormessage(); return false;">Forgot password?</a>
                  </div>
                  <div class="row">
                      <a href="Register.aspx">Registration</a>
                  </div>   
              </div>
            </fieldset>
          </form>
        </div>
      </div>
    </div>
</body>
</html>
<script src="Scripts/jquery-1.10.2.js"></script>
<script src="Scripts/bootstrap.js"></script>
<script>

    function Login() {
        var name = $("#<%=User.ClientID%>").val();
        var password = $("#<%=Password.ClientID%>").val();

        var Loginurl = $("<%=LoginURL.ClientID%>").val();

        //do ajax post to api.
        $.ajax({
            contentType: "application/json",
            method: "POST",
            url: Loginurl,
            data: { "username": name, "password": password },
            success: function (data) {
                //set session var.
                alert(data);
                if (data["token"] != null) {
                    sessionStorage.setItem("Token", data["token"]);
                    window.location.replace("Main.aspx");
                }
                else {
                    $("#<%=lblError.ClientID%>").val("Incorrect login credentials");
                }
            },
            error: function (data) {
                $("#<%=lblError.ClientID%>").val("Error login in check internet connection");
            }
        })

    }

</script>

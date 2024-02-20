<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Club Representative.aspx.cs" Inherits="Fantasy.Club_Representative" %>

<!DOCTYPE html>


<html
  lang="en"
  class="light-style customizer-hide"
  dir="ltr"
  data-theme="theme-default"
  data-assets-path="../assets/"
  data-template="vertical-menu-template-free"
>
  <head>
    <meta charset="utf-8" />
    <meta
      name="viewport"
      content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"
    />

    <title>Fantasy</title>

    <meta name="description" content="" />

    <!-- Favicon -->
    <link rel="icon" type="image/x-icon" href="../assets/img/favicon/favicon.ico" />

    <!-- Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
      href="https://fonts.googleapis.com/css2?family=Public+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&display=swap"
      rel="stylesheet"
    />

    <!-- Icons. Uncomment required icon fonts -->
    <link rel="stylesheet" href="boxicons.css" />

    <!-- Core CSS -->
    <link rel="stylesheet" href="core.css" class="template-customizer-core-css" />
    <link rel="stylesheet" href="theme-default.css" class="template-customizer-theme-css" />
    <link rel="stylesheet" href="demo.css" />

    <!-- Vendors CSS -->
    <link rel="stylesheet" href="perfect-scrollbar.css" />

    <!-- Page CSS -->
    <!-- Page -->
    <link rel="stylesheet" href="page-auth.css" />
    <!-- Helpers -->
    <script src="../assets/vendor/js/helpers.js"></script>

    <!--! Template customizer & Theme config files MUST be included after core stylesheets and helpers.js in the <head> section -->
    <!--? Config:  Mandatory theme config file contain global vars & default theme options, Set your preferred theme option in this file.  -->
    <script src="../assets/js/config.js"></script>
  </head>


  <body>
    <!-- Content -->

    <div class="container-xxl">
      <div class="authentication-wrapper authentication-basic container-p-y">
        <div class="authentication-inner">
          <!-- Register Card -->
          <div class="card">
            <div class="card-body">

              <h4 class="mb-2">Join Fantasy!</h4>

              <form id="formAuthentication" class="mb-3" method="POST" runat="server">
                <div class="mb-3">
                    <label for="name" class="form-label">Name</label>
                
                    <asp:TextBox class="form-control" id="name" name="name" placeholder="Enter your full name" runat="server" />

                </div>

                <div class="mb-3">
                  <label for="username" class="form-label">Username</label>
                  <asp:TextBox class="form-control" id="username" name="username" placeholder="Enter your username" runat="server" />

                </div>


                <div class="mb-3 form-password-toggle">
                  <label class="form-label" for="password">Password</label>
                  <div class="input-group input-group-merge">

                   <asp:TextBox class="form-control" id="password" name="password" 
                        placeholder="&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;" runat="server" TextMode="Password"></asp:TextBox>


                    <span class="input-group-text cursor-pointer"><i class="bx bx-hide"></i></span>

                  </div>
                </div>

                 <div class="mb-3">
                    <label for="club" class="form-label">Choose your Club</label> <br />
                    <asp:DropDownList ID="dropdownclub" CssClass="form-select color-dropdown" runat="server" AppendDataBoundItems ="true"></asp:DropDownList>

                </div>

                

                <div class="mb-3">

                    <asp:Button ID="Reg" class="btn btn-primary d-grid w-100" runat="server" onClick="onRegisterCR" Text="Sign Up" />

                </div>

              </form>

              <p class="text-center">
                <span>Already have an account?</span>
                <a href="Login.aspx">
                  <span>Sign in instead</span>
                </a>
              </p>
            </div>
          </div>
          <!-- Register Card -->
        </div>
      </div>
    </div>



    <!-- Core JS -->
    <!-- build:js assets/vendor/js/core.js -->
    <script src="../assets/vendor/libs/jquery/jquery.js"></script>
    <script src="../assets/vendor/libs/popper/popper.js"></script>
    <script src="../assets/vendor/js/bootstrap.js"></script>
    <script src="../assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"></script>

    <script src="../assets/vendor/js/menu.js"></script>
    <!-- endbuild -->

    <!-- Vendors JS -->

    <!-- Main JS -->
    <script src="../assets/js/main.js"></script>

    <!-- Page JS -->

    <!-- Place this tag in your head or just before your close body tag. -->
    <script async defer src="https://buttons.github.io/buttons.js"></script>
  </body>
</html>
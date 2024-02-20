<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Fantasy.Login" %>

<!DOCTYPE html>

<html
  lang="en"
  class="light-style customizer-hide"
  dir="ltr"
  data-theme="theme-default"
  data-assets-path="~/assets/"
  data-template="vertical-menu-template-free"s
>
  <head>
    <meta charset="utf-8" />
    <meta
      name="viewport"
      content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"
    />

    <title>Login</title>

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
    <link rel="stylesheet" href="../assets/vendor/fonts/boxicons.css" />

    <!-- Core CSS -->
    <link rel="stylesheet" href="core.css" />
    <link rel="stylesheet" href="theme-default.css"/>
    <link rel="stylesheet" href="demo.css" />

    <!-- Vendors CSS -->
    <link rel="stylesheet" href="perfect-scrollbar.css" />

    <!-- Page CSS -->
    <!-- Page -->
    <link rel="stylesheet" href="page-auth.css" />
    <!-- Helpers -->
    <script src="sneat-1.0.0/sneat-1.0.0/assets/vendor/js/helpers.js"></script>

    <!--! Template customizer & Theme config files MUST be included after core stylesheets and helpers.js in the <head> section -->
    <!--? Config:  Mandatory theme config file contain global vars & default theme options, Set your preferred theme option in this file.  -->
    <script src="sneat-1.0.0/sneat-1.0.0/assets/js/config.js"></script>

    <style>

        .text{
            color: #566a7f;
            font-size: 14px;
        
        }

        .create-acc-btn{
              background-color: transparent;
              color: #696CFF;

              font-size: 14px;
              cursor: pointer;

              margin-left: 5px;

        }

        .center{
            display: flex;
            justify-content: center;
            align-content: center;
        }
    </style>

  </head>

  <body>
    <!-- Content -->

    <div class="container-xxl">
      <div class="authentication-wrapper authentication-basic container-p-y">
        <div class="authentication-inner">
          <!-- Register -->
          <div class="card">
            <div class="card-body">

              <h4 class="mb-2">Welcome to Fantasy! 👋</h4>
              <p class="mb-4">Please sign-in to your account </p>

              <form id="formAuthentication" class="mb-3" method="POST" runat="server">
                <div class="mb-3">
                  <label for="email" class="form-label">Username</label>

                  <input
                    type="text"
                    class="form-control"
                    id="username"
                    name="email-username"
                    placeholder="Enter username"
                    runat="server"
                  />

                </div>
                <div class="mb-3 form-password-toggle">
                  <div class="d-flex justify-content-between">
                    <label class="form-label" for="password">Password</label>
                    <a href="Login.aspx">
                      <small>Forgot Password?</small>
                    </a>
                  </div>

                  <div class="input-group input-group-merge">
                    <input
                      type="password"
                      id="password"
                      class="form-control"
                      name="password"
                      placeholder="&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;"
                      aria-describedby="password"
                      runat="server"  
                    />

                    <span class="input-group-text cursor-pointer"><i class="bx bx-hide"></i></span>
                  </div>
                </div>
                <div class="mb-3">
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" id="remember-me" />
                    <label class="form-check-label" for="remember-me"> Remember Me </label>
                  </div>
                </div>
                <div class="mb-3">
                    <asp:Button ID="Sign" class="btn btn-primary d-grid w-100" runat="server" onClick="onLogin" Text="Sign In" />
                </div>

              </form>

                <div class="center btn-group dropend ">
                    <span class="text">
                        New on our platform? 
                    </span>

                    <span class="create-acc-btn" data-bs-toggle="dropdown" aria-expanded="false"> Create an account </span> 

                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="Sports Association Manager.aspx">Sports Association Manager</a>
                        <a class="dropdown-item" href="Club Representative.aspx">Club Representative</a>
                        <a class="dropdown-item" href="Stadium Manager.aspx">Stadium Manager</a>
                        <a class="dropdown-item" href="Fan.aspx">Fan</a>
                    </div>
                </div> 

            </div>
          </div>
          <!-- /Register -->
        </div>
      </div>
    </div>


  

    <!-- Core JS -->
    <!-- build:js assets/vendor/js/core.js -->
    <script src="Template/assets/vendor/libs/jquery/jquery.js"></script>
    <script src="Template/assets/vendor/libs/popper/popper.js"></script>
    <script src="Template/assets/vendor/js/bootstrap.js"></script>
    <script src="Template/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"></script>

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


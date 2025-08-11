<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FujiTecIntranetPortal.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<script type="text/javascript">
        function doBlink() {
            var blink = document.all.tags("BLINK")
            for (var i = 0; i < blink.length; i++)
                blink[i].style.visibility = blink[i].style.visibility == "" ? "hidden" : ""
        }
        function startBlink() {
            if (document.all) setInterval("doBlink()", 500)
        }
        window.onload = startBlink;
    </script>--%>
    <!-- Bootstrap -->
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" />
    <!-- Bootstrap -->
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("pass");
            
        }
    </script>
    <style type="text/css">
        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: white;
            color: white;
            text-align: center;
            height: 2.5%;
        }

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        p {
            background-color: #CF2331;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style1 {
            width: 107px;
        }

        .GridHeader {
            text-align: center !important;
        }

        .bck {
            background-color: white !important;
            font-family: Arial !important;
            font-size: 14px;
            font: bold;
            text-align: left;
        }

        .leftpane {
            width: 95%;
            height: 100%;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        /* blink {
            -webkit-animation-name: blink;
            -webkit-animation-iteration-count: infinite;
            -webkit-animation-timing-function: cubic-bezier(1.0,0,0,1.0);
            -webkit-animation-duration: 1s;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdUserId" runat="server" />
    <div class="page-container scroll-y leftpane">
        <h2 class="text-center" style="font-family: Verdana">Mission & Vision</h2>        
        <table>
            <tr>
                <td class="bck">
                    <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="blink" OnClick="LinkButton1_Click" Font-Size="X-Large" ForeColor="#FF3300">
                            Click to download Form 16!
                    </asp:LinkButton>
                    <br />
                    <br />--%>

                    <h4 style="font-family: Verdana">Fujitec Global Mission Statement:
                    </h4>

                    <p class="bck" style="font-family: Verdana">
                        Respecting people,technologies, and products, we collaborate with people from nations around the world to develop
                        <br />
                        beautiful and functional cities that meet the needs of a new age.
                    </p>
                    <br />
                    <h4 style="font-family: Verdana">Fujitec, a Comprehensive Manufacturer of Transportation Systems, Offers
                        <br />
                        Reliable Products and Services:
                    </h4>

                    <p class="bck" style="font-family: Verdana">
                        Fujitec is a comprehensive manufacturer of transportation systems which completely covers research and development,
                        <br />
                        production,marketing,installation and maintenance of elevators,escalators, and moving walks, Our corporate philosophy
                        <br />
                        is to contribute to the world's city functions by offering reliable products and services to users both in and outside of Japan.
                    </p>
                    <br />

                    <h4 style="font-family: Verdana">Engagement in Global Business through Close Cooperation of Group Companies:</h4>

                    <p class="bck" style="font-family: Verdana">
                        To accurately cope with an ever-changing global marketplace and economy, Fujitec has established a global operating system<br />
                        under which its group companies are divided into economic blocks of Japan,America,Europe,South Asia, East Asia, and China.<br />
                        While responding quickly to market needs through the promotion of group companies, each group company engages in its
                        <br />
                        finely-tuned business activity dedicated to each region.
                    </p>
                    <br />
                    <h4 style="font-family: Verdana">ATTAIN</h4>

                    <p class="bck" style="font-family: Verdana">
                        Leadership position in technology, high speed, super high speed segments and scale up the volume of business to reach 
                        <br />
                        top 5 in India by 2024.                          
                            
                    </p>
                </td>
                <td colspan="2">

                    <asp:Image ID="ImgMissionVision" runat="server" ImageUrl="~/assets/images/FujiAboutus.png" />
                </td>
            </tr>
            <%--  <tr>
                <td colspan="3">
                <asp:LinkButton runat="server" Text="Change Password" ID="CP"></asp:LinkButton>
                    
                </td>
            </tr>--%>
        </table>
    </div>
    <!-- Modal Popup -->
    <div id="MyPopup" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <%--<table>
                        <tr>
                            <td><TextBox Text="" ID ="password" data-dismiss="pass"></TextBox></td>
                        </tr>
                    </table>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Popup -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <div class="footer">
        <p style="text-align: left; color: white">
            <strong>Developed by Information Technology.
    <span style="float: right; color: white">© 2022 Fujitec India Pvt,Ltd.
    </span>
            </strong>
        </p>
    </div>
</asp:Content>

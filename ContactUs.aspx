<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="FujiTecIntranetPortal.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
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

        .normal-table1 {
            font-size: 1px !important;
            /*border: 5px solid black !important;*/
            padding: 2px !important;
            margin-bottom: 10px !important;
            margin-left: 150px !important;
        }

        .roundedcorners {
            -webkit-border-radius: 10px;
            /*-khtml-border-radius: 10px;*/
            -moz-border-radius: 10px;
            border-radius: 10px;
        }

        .img {
            background-image: url('../assets/images/3d.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size:1500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%--<div style="background-image: url(../assets/images/3d.jpg); height: 1000px; width: 2000px; border: 1px solid black; background-repeat: no-repeat;"> </div>--%>
    <%--<div style="background-image: url(../assets/images/3d.jpg); height: 800px; width: 2000px; border: 1px solid black; background-repeat: no-repeat;">--%>
    <div class="page-container scroll-y ">
        <h2 class="text-center" style="font-family: Verdana">Information Technology</h2>

        <table class="normal-table1 ">
            <%--<h4 class="text-center" style="font-family: Verdana">We are here to help you!!!</h4>--%>
            <tr>
                <td>
                    <%--<h4> INFORMATION TECHNOLOGY </h4>--%>
                    <img src="../assets/images/EmptyPhotoFrame.jpg" class="roundedcorners" height="100px;" />
                    <%--<strong>Name: </strong> <h5>Sankar V</h5>
                    <p class="bck" style="font-family: Verdana">
                        <br />
                    </p>--%>
                </td>
                <td>
                    <%--<h4>Assistant Mananger IT </h4>--%>
                    <img src="../assets/images/EmptyPhotoFrame.jpg" class="roundedcorners" height="100px;" />
                </td>
                <td>
                    <%--<h4>Senior Software Engineer IT </h4>--%>
                    <img src="../assets/images/EmptyPhotoFrame.jpg" class="roundedcorners" height="100px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<p class="bck" style="font-family:Verdana"> <span>NAME:</span> <strong> SANKAR V</strong> <//p>--%>
                    <br />
                    <p class="bck" style="font-family: Verdana"><span><strong>Name:</strong> </span><strong>V.SANKAR</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Designation:</strong> </span><strong>Senior Manager IT</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Email Id:</strong> </span><strong>sankar.v@in.fujitec.com</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Contact No:</strong> </span><strong>8754594639</strong> </p>
                </td>
                <td>
                    <%--<p class="bck" style="font-family:Verdana"> <span>NAME:</span> <strong> SANKAR V</strong> <//p>--%>
                    <br />
                    <p class="bck" style="font-family: Verdana"><span><strong>Name:</strong> </span><strong>M.S.V.ARUN</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Designation:</strong> </span><strong>Assistant Manager IT</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Email Id:</strong> </span><strong>arun.msv@in.fujitec.com </strong></p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Contact No:</strong> </span><strong>9894260189</strong> </p>
                </td>

                <td>
                    <%--<p class="bck" style="font-family:Verdana"> <span>NAME:</span> <strong> SANKAR V</strong> <//p>--%>
                    <br />
                    <p class="bck" style="font-family: Verdana"><span><strong>Name:</strong> </span><strong>S.VINOTH KUMAR</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Designation:</strong> </span><strong>Senior Software Engineer</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Email Id:</strong> </span><strong>vinothkumar.s@in.fujitec.com</strong> </p>
                    <p class="bck" style="font-family: Verdana"><span><strong>Contact No:</strong> </span><strong>9884899663</strong> </p>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <%--<h4> INFORMATION TECHNOLOGY </h4>--%>
                    <br />
                    <img src="../assets/images/EmptyPhotoFrame.jpg" class="roundedcorners" height="100px;" />
                    <%--<strong>Name: </strong> <h5>Sankar V</h5>
                    <p class="bck" style="font-family: Verdana">
                        <br />
                    </p>--%>
                </td>

            </tr>
            <tr>
                <td colspan="3">
                    <%--<p class="bck" style="font-family:Verdana"> <span>NAME:</span> <strong> SANKAR V</strong> <//p>--%>
                    <br />
                    <p class="bck" style="font-family: Verdana"><span><strong>Name:</strong> </span><strong>ITSUPPORT</strong> </p>

                    <p class="bck" style="font-family: Verdana"><span><strong>Contact No:</strong> </span><strong>9500043309</strong> </p>
                </td>
            </tr>
        </table>

    </div>
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

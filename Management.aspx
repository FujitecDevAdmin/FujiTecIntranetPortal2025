<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="FujiTecIntranetPortal.Management" %>

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

        .bck {
            background-color: white !important;
            font-family: Arial !important;
            font-size: 14px;
            font: bold;
            text-align: left;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style1 {
            width: 107px;
            
        }

        .leftpane {
            width: 95%;
            height: 100%;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        /*.middlepane {
            width: 90%;
            height: 100%;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }*/

        .GridHeader {
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y leftpane">
        <h2 class="text-center"></h2>
        <table>
            <tr>
                <td>
                    <h3 style="font-family:Verdana;">Our CEO Message<br />
                    </h3>
                    <br />
                    <br />
                    <h4 style="font-family:Verdana;">Opening the Door to a New Era to Remain a Step Ahead</h4>
                    <br />
                    <div style="width:750px;font-family:Verdana" >
                        <p class="bck" style="font-family:Verdana;">
                            Elevators and escalators are indispensable parts of urban life that support smooth and effortless movement. Through daily research and development, we are committed to further improving your safety, quality and comfort.<br />
                            <br />
                            System development in the not-too distant future may have functions beyond our imagination. Just as space travel was once thought to be inconceivable or fantasy-like; our persistent efforts in research and development over the years are making the unthinkable a reality. If we dream of big solutions for advancing these systems, we will achieve new methods in innovative transportation.
                            <br />
                            <br />
                            We have pursued the unknown and nurtured our dreams as "seeds of the future.” By nurturing these “seeds”, Fujitec has created the future of moving systems once thought to be far-reaching. In doing so, we are opening the door to a new era.<br />
                            <br />

                            Fujitec Co., Ltd.<br />
                            President & Chief Executive Officer<br />
                            <strong>Takakazu Uchiyama</strong>
                        </p>
                    </div>


                </td>
                <td colspan="2">
                    <asp:Image ID="ImgMissionVision" runat="server" ImageUrl="~/assets/images/CEO.PNG" Height="500px" Width="328px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

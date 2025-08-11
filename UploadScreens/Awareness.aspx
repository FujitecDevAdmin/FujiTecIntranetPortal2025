<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Awareness.aspx.cs" Inherits="FujiTecIntranetPortal.UploadScreens.Awareness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<style type="text/css">
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
         table {
        margin: 0 auto;
    }
        p {
            background-color: #CF2331;
        }

        .auto-style2 {
            width: 17%;
            height: 30px;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style3 {
            font-weight: normal;
        }
    </style>--%>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .page-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin-top: 4px;
        }
        .text-danger {
            color: red;
            font-weight: bold;
        }


        .previewContainer {
            position: relative;
            max-width: 500px;
            max-height: 500px;
            margin: 5px;
        }
      /*  #uploadcontrols {
            width: 400px;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }*/
       #uploadcontrols {
        width: 400px;
        margin-top: -250px; /* Adjust the margin-top as needed */
        padding: 20px;
        background-color: #fff;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

        h3 {
            font-size: 24px;
            margin-bottom: 20px;
            text-align: center;
        }

        table {
            width: 100%;
            margin: 0 auto;
        }

        td {
            padding: 10px;
        }

        .auto-style2 {
            width: 100%;
        }

        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: #333;
            color: white;
            text-align: center;
            height: 2.5%;
            padding: 5px 0;
        }

        p {
            background-color: #CF2331;
        }
    </style>
    <script>
        // JavaScript to display image preview
        function previewImages() {
            var preview = document.querySelector('#imagePreview');
            var files = document.querySelector('input[type=file]').files;

            preview.innerHTML = '';
            if (files) {
                [].forEach.call(files, function (file) {
                    var reader = new FileReader();

                    reader.onload = function (event) {
                        var img = new Image();
                        img.onload = function () {
                            var width = this.width;
                            var height = this.height;
                            var aspectRatio = width / height;
                            var requiredRation = 16 / 9;

                            if (Math.abs(aspectRatio - requiredRation) < 0.1) {
                                var previewContainer = document.createElement('div');
                                previewContainer.className = 'previewContainer';

                                var imgElement = document.createElement('img');
                                imgElement.className = 'previewImg';
                                imgElement.src = event.target.result;

                                imgElement.onclick = function () {
                                    openFullScreen(event.target.result);
                                };

                                var removeBtn = document.createElement('button');
                                removeBtn.className = 'removeBtn';
                                removeBtn.innerHTML = 'X';
                                removeBtn.onclick = function () {
                                    preview.removeChild(previewContainer);
                                };

                                var maximizeBtn = document.createElement('button');
                                maximizeBtn.className = 'maximizeBtn';
                                maximizeBtn.innerHTML = '[ ]';
                                maximizeBtn.onclick = function (e) {
                                    e.preventDefault(); // Prevent default action
                                    openFullScreen(event.target.result, file.name);
                                    return false; // Return false to prevent default action
                                };


                                previewContainer.appendChild(imgElement);
                                previewContainer.appendChild(removeBtn);
                                previewContainer.appendChild(maximizeBtn);
                                preview.appendChild(previewContainer);
                            } else {


                                alert(`The file '${file.name}' has an aspect ratio of ${width}:${height}.Please upload images with a 16:9 aspect ratio.`);
                            }

                        };
                        img.src = event.target.result;
                    };

                    reader.readAsDataURL(file);
                });
            }
        }

        // Function to open image in full-screen mode with image name as the tab title
        function openFullScreen(imageSrc, imageName) {
            var fullScreenWindow = window.open('', '_blank');
            fullScreenWindow.document.write('<html><head></head><body><img src="' + imageSrc + '" style="width:80vw; height:80vh; object-fit: contain;" /></body></html>');
            fullScreenWindow.document.title = imageName; // Set the title after creating the document
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container">
        <div id="uploadcontrols">
            <h3>Upload Awareness Images</h3>
            <table>
                <tr>
                    <td class="auto-style2">
                        <strong>Awareness Category</strong> 
                        <asp:TextBox ID="TextBoxAwarenessCategory" runat="server" placeholder="Enter Awareness Category" Width ="200"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="text-danger" ID="AwarenessCategoryValidator" runat="server"
                            ControlToValidate="TextBoxAwarenessCategory" ErrorMessage="Please enter the awareness category" Text="*" ValidationGroup="UploadValidationGroup">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan ="2"  align ="center">
                        <asp:FileUpload ID="FileUploadImages" runat="server" multiple="multiple" onchange="previewImages()" />
                    </td>
                 </tr>
                <tr>
                    <td class="auto-style2" align ="center">
                        <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" ValidationGroup="UploadValidationGroup" Width ="100" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UploadValidationGroup" />
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div id="imagePreview"></div>
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

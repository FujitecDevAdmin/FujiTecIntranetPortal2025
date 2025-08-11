<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectGallery.aspx.cs" Inherits="ConfulenceLandingPage.UploadScreens.ProjectGallery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Images</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        #form1 {
            width: 95vw;
            height: 87vh;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }


        h2 {
            font-size: 24px;
            margin-bottom: 20px;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            margin-left: 5px;
        }


        .multiline-textbox {
            width: calc(100% - 12px);
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            transition: border-color 0.3s ease;
        }

            .multiline-textbox:hover {
                border-color: #999;
            }

            .multiline-textbox:focus {
                outline: none;
                border-color: #007bff;
            }

        {
            outline: none;
            border-color: #007bff;
        }

        input[type="file"],
        input[type="text"],
        input[type="submit"] {
            width: calc(100% - 12px);
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            transition: border-color 0.3s ease;
        }

            input[type="file"]:hover,
            input[type="text"]:hover,
            input[type="submit"]:hover {
                border-color: #999;
            }

            input[type="file"]:focus,
            input[type="text"]:focus,
            input[type="submit"]:focus {
                outline: none;
                border-color: #007bff;
            }

        input[type="submit"] {
            background-color: #007bff;
            color: #fff;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s ease, color 0.3s ease;
        }

            input[type="submit"]:hover {
                background-color: #0056b3;
            }

        .text-danger {
            color: red;
            font-weight: bold;
        }

        .container {
            display: flex;
            width: 100%;
        }

        #uploadcontrols {
            flex: 0 0 35%;
            padding: 20px;
            background-color: #f9f9f9;
            box-sizing: border-box;
            border-radius: 8px;
        }

        #imagePreview {
            flex: 1;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 10px;
            overflow-y: auto;
            overflow-x: hidden;
            max-width: 80vw;
            max-height: 500px;
            margin-top: 20px;
        }

        .previewContainer {
            position: relative;
            max-width: 500px;
            max-height: 500px;
            margin: 5px;
        }

        .previewImg {
            width: 100%;
            height: auto;
            display: block;
            border: 2px solid #ccc;
            margin-bottom: 5px;
        }

        .removeBtn {
            position: absolute;
            top: 0px;
            right: -15px;
            background: red;
            color: white;
            border: none;
            border-radius: 50%;
            padding: 5px;
            cursor: pointer;
            z-index: 1;
        }

        .maximizeBtn {
            position: absolute;
            bottom: 10px;
            right: 5px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 5px 10px;
            cursor: pointer;
            z-index: 1;
        }
        /* Styling for the DataGrid */
        #ProjectDetailsGrid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

            /* Styling for header cells */
            #ProjectDetailsGrid th {
                background-color: #f2f2f2;
                padding: 8px;
                text-align: left;
            }

            /* Styling for data cells */
            #ProjectDetailsGrid td {
                border-bottom: 1px solid #ddd;
                padding: 8px;
            }

            /* Styling for the Preview and Delete buttons */
            #ProjectDetailsGrid .btn-preview,
            #ProjectDetailsGrid .btn-delete {
                padding: 6px 12px;
                cursor: pointer;
                border: none;
                border-radius: 4px;
                background-color: #007bff;
                color: white;
                text-align: center;
                text-decoration: none;
                display: inline-block;
                transition: background-color 0.3s;
            }

                #ProjectDetailsGrid .btn-preview:hover,
                #ProjectDetailsGrid .btn-delete:hover {
                    background-color: #0056b3;
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
            console.log(imageSrc);
            var fullScreenWindow = window.open('', '_blank');
            fullScreenWindow.document.write('<html><head></head><body><img src="' + imageSrc + '" style="width:100vw; height:100vh; object-fit: contain;" /></body></html>');
            fullScreenWindow.document.title = imageName; // Set the title after creating the document
        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="uploadcontrols">
                <h2>Upload Project Gallery</h2>

                <asp:Label runat="server" AssociatedControlID="TextBoxProjectName" Text="Project Name"></asp:Label>
                <asp:TextBox ID="TextBoxProjectName" runat="server" placeholder="Enter Project Name"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="ProjectNameValidator" runat="server"
                    ControlToValidate="TextBoxProjectName" ErrorMessage="Please enter the project name" Text="*" ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>


                <asp:Label runat="server" AssociatedControlID="TextBoxProjectLocation" Text="Project Location"></asp:Label>
                <asp:TextBox ID="TextBoxProjectLocation" runat="server" placeholder="Enter Project Location"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="ProjectLocationValidator" runat="server"
                    ControlToValidate="TextBoxProjectLocation" ErrorMessage="Please enter the project location" Text="*" ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>

                <asp:Label runat="server" AssociatedControlID="TextBoxProjectDescription" Text="Project Description"></asp:Label>
                <asp:TextBox CssClass="multiline-textbox" ID="TextBoxProjectDescription" runat="server" TextMode="MultiLine" placeholder="Enter Project Description"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="ProjectDescriptionValidator" runat="server"
                    ControlToValidate="TextBoxProjectDescription" ErrorMessage="Please enter the project description" Text="*" ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>

                <br />

                <asp:FileUpload ID="FileUploadImages" runat="server" multiple="multiple" onchange="previewImages()" />
                <br />

                <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" ValidationGroup="UploadValidationGroup" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UploadValidationGroup" />
            </div>
            <div id="imagePreview"></div>

        </div>

        <%--   <input type="text" id="searchInput" placeholder="Search..." />
        <asp:DataGrid ID="ProjectDetailsGrid" runat="server" AutoGenerateColumns="false" OnItemCommand="ProjectDetailsGrid_ItemCommand">
            <Columns>
                <asp:BoundColumn HeaderText="ID" DataField="ID" />
                <asp:BoundColumn HeaderText="Project Name" DataField="ProjectName" />
                <asp:BoundColumn HeaderText="Project Location" DataField="ProjectLocation" />
                <asp:BoundColumn HeaderText="Project Description" DataField="ProjectDescription" />
                <asp:BoundColumn HeaderText="Images Path" DataField="ImagesPath" Visible="false" />
                <asp:TemplateColumn HeaderText="Preview">
                    <ItemTemplate>
                        <asp:Button ID="PreviewButton" runat="server" CommandName="PreviewImage" CommandArgument='<%# Eval("ID") %>' Text="Preview" CssClass="btn-preview" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="DeleteProject" CommandArgument='<%# Eval("ID") %>' Text="Delete" CssClass="btn-delete" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>

        --%>
    </form>



</body>
</html>

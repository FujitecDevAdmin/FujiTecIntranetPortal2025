<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlashNews.aspx.cs" Inherits="ConfulenceLandingPage.UploadScreens.FlashNews" %>

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
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="uploadcontrols">
                <h2>Upload Flash News</h2>

                <asp:Label runat="server" AssociatedControlID="TextBoxTitle" Text="Title"></asp:Label>
                <asp:TextBox ID="TextBoxTitle" runat="server" placeholder="Enter title"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="TitleValidator" runat="server"
                    ControlToValidate="TextBoxTitle" ErrorMessage="Please enter the title" Text="*" ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>


                <asp:Label runat="server" AssociatedControlID="TextBoxDescription" Text="Description"></asp:Label>
                <asp:TextBox  CssClass="multiline-textbox" ID="TextBoxDescription" runat="server" TextMode="MultiLine" placeholder="Enter Description"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="DescriptionValidator" runat="server"
                    ControlToValidate="TextBoxDescription" ErrorMessage="Please enter the description" Text="*" ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>

                <asp:Label runat="server" AssociatedControlID="TextBoxStartDate" Text="Start Date"></asp:Label>
                <asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="datepicker" placeholder="Select Start Date"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="StartDateValidator" runat="server"
                    ControlToValidate="TextBoxStartDate" ErrorMessage="Please select the start date" Text="*"
                    ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>

                <asp:Label runat="server" AssociatedControlID="TextBoxEndDate" Text="End Date"></asp:Label>
                <asp:TextBox ID="TextBoxEndDate" runat="server" CssClass="datepicker" placeholder="Select End Date"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="text-danger" ID="EndDateValidator" runat="server"
                    ControlToValidate="TextBoxEndDate" ErrorMessage="Please select the end date" Text="*"
                    ValidationGroup="UploadValidationGroup"></asp:RequiredFieldValidator>

                <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" ValidationGroup="UploadValidationGroup" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UploadValidationGroup" />

                <!-- jQuery and jQuery UI scripts -->
                <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
                <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
                <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>


                <script>
                    $(document).ready(function () {
                        $('.datepicker').datepicker({
                            dateFormat: 'yy-mm-dd', // Corrected date format
                            changeMonth: true,
                            changeYear: true,
                            minDate: 0, 
                            onSelect: function (selectedDate) {
                                var option = this.id === "TextBoxStartDate" ? "minDate" : "maxDate",
                                    instance = $(this).data("datepicker"),
                                    date = $.datepicker.parseDate(
                                        instance.settings.dateFormat || $.datepicker._defaults.dateFormat,
                                        selectedDate,
                                        instance.settings
                                    );
                                instance.settings[option] = date;
                            }
                        });
                    });

                </script>
            </div>
            <div id="imagePreview"></div>
    </form>



</body>
</html>

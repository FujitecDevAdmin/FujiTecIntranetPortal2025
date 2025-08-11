<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessSupport.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.BusinessSupport" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .upload-container {
            max-width: 600px;
            margin: auto;
            padding: 30px;
            background-color: #f5f5f5;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .upload-title {
            font-size: 22px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 25px;
            color: #333;
        }

        .form-group {
            margin-bottom: 20px;
            text-align: center;
        }

        .form-label {
            font-weight: bold;
            display: block;
            margin-bottom: 10px;
        }

        .file-upload {
            padding: 6px 12px;
        }

        .btn {
            margin: 5px;
            padding: 8px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            transition: background-color 0.3s ease;
            color: #fff !important;
        }

        .btn-upload {
            background-color: #0066cc !important; /* Blue */
            color: #fff !important;
        }

        .btn-upload:hover {
            background-color: #005bb5 !important;
        }

        .btn-template {
            background-color: #28a745 !important; /* Green */
            color: #fff !important;
        }

        .btn-template:hover {
            background-color: #218838 !important;
        }

        .btn-clear {
            background-color: #dc3545 !important; /* Red */
            color: #fff !important;
        }

        .btn-clear:hover {
            background-color: #c82333 !important;
        }

        .status-label {
            font-size: 14px;
            color: green;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="upload-container">
        <div class="upload-title">Upload Timesheet Excel</div>

        <div class="form-group">
            <label class="form-label">Choose Excel File (.xls, .xlsx) <span style="color:red">*</span></label>
            <asp:FileUpload ID="FileUpload1" CssClass="file-upload" runat="server" accept=".xls,.xlsx" />
        </div>

        <div class="form-group">
            <asp:Button ID="btnUpload" runat="server" Text="Upload File" CssClass="btn btn-upload" OnClick="Upload" />
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Template" CssClass="btn btn-template" OnClick="GenerateTemplate_OnClick" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-clear" OnClick="Clear_OnClick" />
        </div>

        <div class="form-group">
            <asp:Button ID="btnDwnldExl" runat="server" Text="Download Last Excel" OnClick="btnDwnldExl_Click" CssClass="btn btn-upload" Visible="false" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblmsg" runat="server" CssClass="status-label"></asp:Label>
        </div>
    </div>
</asp:Content>

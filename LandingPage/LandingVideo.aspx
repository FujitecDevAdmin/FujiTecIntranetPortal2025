<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingVideo.aspx.cs" Inherits="FujiTecIntranetPortal.LandingVideo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Awareness Video</title>

    <style>
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            background-color: black;
            overflow: hidden;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        /* Intro screen */
        .intro-screen {
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            background: 
                linear-gradient(rgba(0, 43, 92, 0.7), rgba(0, 64, 128, 0.7)),
                url('../LandingPageResources/Awareness/SECURITY/bigstock.jpg') no-repeat center center;
            background-size: cover;
            color: white;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            text-align: center;
            z-index: 1000;
            padding: 40px;
            backdrop-filter: blur(2px);
        }

        .intro-screen h1 {
            font-size: 32px;
            margin-bottom: 15px;
        }

        .intro-screen p {
            font-size: 18px;
            max-width: 700px;
            margin-bottom: 25px;
            line-height: 1.6;
        }

        .button-group {
            display: flex;
            gap: 20px;
        }

        .btn {
            padding: 14px 32px;
            border-radius: 8px;
            font-size: 18px;
            cursor: pointer;
            border: none;
            transition: all 0.3s ease;
            font-weight: bold;
        }

        .btn-play {
            background-color: #28a745;
            color: white;
        }

        .btn-play:hover {
            background-color: #218838;
            transform: scale(1.05);
        }

        .btn-skip {
            background-color: #6c757d;
            color: white;
            display: none; /* Initially hidden */
        }

        .btn-skip:hover {
            background-color: #5a6268;
            transform: scale(1.05);
        }

        /* Video container */
        .video-container {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            justify-content: center;
            align-items: center;
            background-color: black;
            z-index: 900;
        }

        video {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        /* Professional popup overlay */
        .popup-overlay {
            display: none;
            position: fixed;
            inset: 0;
            background: rgba(0, 0, 0, 0.55);
            backdrop-filter: blur(10px);
            -webkit-backdrop-filter: blur(10px);
            justify-content: center;
            align-items: center;
            z-index: 2000;
            animation: fadeIn 0.6s ease;
        }

        /* Clean, glassy popup */
        .popup-content {
            background: rgba(255, 255, 255, 0.08);
            border: 1px solid rgba(255, 255, 255, 0.25);
            border-radius: 18px;
            padding: 45px 40px;
            text-align: center;
            color: #ffffff;
            width: 480px;
            max-width: 90%;
            box-shadow: 0 8px 40px rgba(255, 0, 0, 0.25);
            animation: popupFade 0.8s ease;
            backdrop-filter: blur(20px);
            -webkit-backdrop-filter: blur(20px);
        }

        .popup-content h2 {
            font-size: 28px;
            margin-bottom: 18px;
            color: #ff3333;
            text-shadow: 0 0 10px rgba(255, 77, 77, 0.6);
            letter-spacing: 0.5px;
        }

        .popup-content p {
            font-size: 17px;
            line-height: 1.6;
            margin-bottom: 35px;
            opacity: 0.95;
            color: #f0f0f0;
        }

        .popup-buttons {
            display: flex;
            justify-content: center;
            gap: 25px;
        }

        .popup-buttons .btn {
            padding: 12px 30px;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            border: none;
            transition: all 0.3s ease;
        }

        .btn-primary {
            background: linear-gradient(135deg, #007bff, #00bfff);
            color: white;
            box-shadow: 0 0 10px rgba(0, 191, 255, 0.5);
        }

        .btn-primary:hover {
            background: linear-gradient(135deg, #0069d9, #0099cc);
            transform: translateY(-2px);
            box-shadow: 0 0 16px rgba(0, 191, 255, 0.8);
        }

        .btn-secondary {
            background: linear-gradient(135deg, #ff4d4d, #ff1a1a);
            color: white;
            box-shadow: 0 0 10px rgba(255, 77, 77, 0.5);
        }

        .btn-secondary:hover {
            background: linear-gradient(135deg, #cc0000, #ff3333);
            transform: translateY(-2px);
            box-shadow: 0 0 16px rgba(255, 0, 0, 0.8);
        }

        @keyframes popupFade {
            from { opacity: 0; transform: translateY(30px) scale(0.9); }
            to { opacity: 1; transform: translateY(0) scale(1); }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" onsubmit="return false;">
        <!-- Intro Screen -->
        <div id="introScreen" class="intro-screen">
            <h1>IT Awareness Program</h1>
            <p>
                This is an important <strong> Awareness video</strong>. Please watch the video completely to understand
                key information security guidelines.  
                After watching, you will be required to answer a few questions using the link provided at the end.
            </p>
            <p style="font-style: italic; margin-bottom: 30px;">
                If you have already watched this video and answered the questions, you may skip it.
            </p>

            <div class="button-group">
                <button type="button" class="btn btn-play" onclick="startVideo()">▶ Play Video</button>
                <button type="button" class="btn btn-skip" id="skipBtn" onclick="skipVideo(event);">⏭ Skip</button>
            </div>
        </div>

        <!-- Video Section -->
        <div id="videoContainer" class="video-container">
            <video id="AwarenessVideo" controls>
                <source src="<%= ResolveUrl("~/LandingPageResources/Videos/PhishingVideo.mp4") %>" type="video/mp4" />
                Your browser does not support the video tag.
            </video>
        </div>

        <!-- Enhanced Popup after video ends -->
        <div id="popupOverlay" class="popup-overlay">
            <div class="popup-content">
                <p>
                    Thanks for watching the video completely.<br><br>
                    Please click the <strong>“Next”</strong> button to open the questionnaire form in a new tab.<br>
                    After submitting your responses, kindly return to this page and click the <strong>“Go to Confluence”</strong> button to proceed.
                </p>
                <div class="popup-buttons">
                    <button type="button" class="btn btn-secondary" onclick="goBack(event);">Go to Confluence</button>
                    <button type="button" class="btn btn-primary" onclick="openTest()">Next</button>
                </div>
            </div>
        </div>
    </form>

    <script>
        const introScreen = document.getElementById('introScreen');
        const videoContainer = document.getElementById('videoContainer');
        const video = document.getElementById('AwarenessVideo');
        const popupOverlay = document.getElementById('popupOverlay');
        const skipBtn = document.getElementById('skipBtn');

        const today = new Date().toISOString().split('T')[0];
        const watchedDate = localStorage.getItem('videoWatchedDate');
        const answeredDate = localStorage.getItem('answeredQuestionsDate');

        // ✅ Show skip button if user already watched OR answered questions today
        if (watchedDate === today) {
            skipBtn.style.display = 'inline-block';
        }

        function startVideo() {
            introScreen.style.display = 'none';
            videoContainer.style.display = 'flex';
            video.play();
            localStorage.setItem('videoWatchedDate', today);
        }

        function skipVideo(event) {
            event.preventDefault();
            localStorage.setItem('VideoSkipped', 'true');

            var url = '<%= ResolveUrl("~/LandingPage/LandingPage.aspx") %>';

            alert("Redirecting to: " + url);
            console.log("Redirecting to: " + url);

            window.top.location.replace(url);
        }

        video.addEventListener('ended', function () {
            popupOverlay.style.display = 'flex';
            localStorage.setItem('videoWatchedDate', today);
        });

        function goBack(event) {
            event.preventDefault();
            event.stopPropagation();
            localStorage.setItem('VideoSkipped', 'true');

            var url = '<%= ResolveUrl("~/LandingPage/LandingPage.aspx") %>';
            console.log("Redirecting to: " + url);
            alert("Redirecting to: " + url);

            window.location.replace(url);
        }

        // ✅ Updated: Save answeredQuestionsDate when clicking the test link
        function openTest() {
            localStorage.setItem('answeredQuestionsDate', today);
            window.open('https://forms.office.com/Pages/ResponsePage.aspx?id=cGRWGYRFE0q3HXd93k8reqlqVNYTEopJulmvBRbaD2ZUOEgyMkpSTDdLOTYzVTJVNzAwRVpQNkZMUC4u', '_blank');
        }

        document.addEventListener('keydown', function (event) {
            if (event.key === 'Escape') {
                window.location.href = '<%= ResolveUrl("~/LandingPage/LandingPage.aspx") %>';
            }
        });
    </script>
</body>
</html>

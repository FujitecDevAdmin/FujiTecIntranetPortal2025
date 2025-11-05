<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs"
    Inherits="FujiTecIntranetPortal.LandingPage.LandingPage" ValidateRequest="false" %>
 
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="initial-scale=1, width=device-width" />

    <link rel="stylesheet" href="CSS/layout.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/main-header.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/event-slide-show.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/news-container.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/employee-slide-show.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/employee-contact-show.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/awareness-slide-show.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/project-gallery-slide-show.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/quotes-container.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/video-container.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/quicklinks-container.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/login-form.css?v=<%= DateTime.Now.Ticks %>" />
    <link rel="stylesheet" href="CSS/site-navigation.css?v=<%= DateTime.Now.Ticks %>" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" />
    <link rel="stylesheet"
        href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;800&display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Lato:wght@700&display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Pacifico:wght@400&display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Rambla:ital,wght@1,700&display=swap" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Outfit:wght@500&display=swap" />
    <link href='https://fonts.googleapis.com/css?family=Raleway' rel='stylesheet'>

    <style>
        /* Timer Animation */
        @keyframes pulse {
            0%, 100% {
                transform: scale(1);
            }
            50% {
                transform: scale(1.05);
            }
        }

        @keyframes slideInFromRight {
            from {
                transform: translateX(100px);
                opacity: 0;
            }
            to {
                transform: translateX(0);
                opacity: 1;
            }
        }

        .timer-pulse {
            animation: pulse 1s ease-in-out infinite;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="header-container">
            <header class="main-header">
                <div class="logo">
                    <img src="Resource/fujitec-logo.png" alt="Company Logo" class="logo-img">
                    <div class="separator"></div>
                    <h2 class="app-name">confluence</h2>
                </div>
                <% string marqueeContent = GetMarqueeContent(); if (!string.IsNullOrEmpty(marqueeContent))
                    { %>
                <div class="marquee">
                    <marquee behavior="scroll" direction="left">
                        <%= marqueeContent %>
                    </marquee>
                </div>
                <% } %>

                <!-- Button to trigger the popup form -->
                <asp:Button CssClass="login-btn" ID="ShowPopupButton" runat="server" Text="Login" OnClientClick="showPopup(); return false;" />

            </header>
            <div class="bottom-line"></div>
        </div>

        <asp:HiddenField runat="server" ClientIDMode="Static" ID="eventSlideShowData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="newsRendererData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="employeeSlideShowData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="employeeContactData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="projectGalleryData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="awarenessSlideShowData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="quotesData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="videoData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="quickLinksData" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="flashNewsData" />


        <!-- Popup form -->
        <div runat="server" id="popupForm" class="popup" clientidmode="Static">
            <div class="popup-content">
                <asp:Button ID="CloseButton" runat="server" Text="X" OnClientClick="hidePopup(); return false;" CssClass="close-button" />
                <asp:Panel ID="FormPanel" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="Login" CssClass="popup-title-label"></asp:Label>
                    <asp:Image ID="MyImage" runat="server" CssClass="login-illustration-image" ImageUrl="~/LandingPage/Resource/login_Illustration.png" />
                    <asp:Label ID="EmployeeIDLabel" runat="server" Text="Employee ID:" CssClass="popup-label"></asp:Label>
                    <asp:TextBox ID="EmployeeIDTextBox" runat="server" Required="true" onkeypress="return this.value.length<=6" placeholder="Enter the Emplyee ID" CssClass="popup-textbox"></asp:TextBox><br />
                    <asp:Label ID="PasswordLabel" runat="server" Text="Password:" CssClass="popup-label"></asp:Label>
                    <asp:TextBox ID="PasswordTextBox" runat="server" Required="true" TextMode="Password" placeholder="Enter the Password" CssClass="popup-textbox"></asp:TextBox><br />
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" CssClass="popup-button" />

                    <asp:Label runat="server" ID="LoginMessageLable" Text="" CssClass="error-label" />

                </asp:Panel>
            </div>
        </div>

        <!-- Video Overlay - Place outside of sections -->
        <div class="video-overlay" 
             style="position:fixed; 
                    top:0; 
                    left:0; 
                    width:100%; 
                    height:100%; 
                    background: 
                        linear-gradient(to top, rgba(205,28,24,0.2), rgba(0,0,0,0.4) 40%, rgba(0,0,0,0)), 
                        url('assets/images/what-is-cybersecurity.jpg') no-repeat center center; 						
                    background-size: cover;
                    color:#fff; 
                    display:flex; 
                    flex-direction:column; 
                    align-items:center; 
                    justify-content:center; 
                    font-family: 'Segoe UI', Arial, sans-serif; 
                    z-index:9999; 
                    text-align:center;
                    transition: opacity 0.5s ease-out;">
            
            <!-- Enhanced Countdown Timer in Top Right -->
            <div id="overlayTimer" 
                 style="position:absolute; 
                        top:30px; 
                        right:30px; 
                        font-size:16px; 
                        font-weight:500; 
                        background: linear-gradient(135deg, rgba(0,150,136,0.95), rgba(0,121,107,0.95));
                        padding:15px 25px; 
                        border-radius:50px;
                        border:2px solid rgba(255,255,255,0.5);
                        box-shadow: 0 8px 32px rgba(0,0,0,0.4), 0 0 20px rgba(0,150,136,0.3);
                        backdrop-filter: blur(10px);
                        display: flex;
                        align-items: center;
                        gap: 12px;
                        animation: slideInFromRight 0.5s ease-out;">
                
                <!-- Clock Icon -->
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                    <circle cx="12" cy="12" r="10"></circle>
                    <polyline points="12 6 12 12 16 14"></polyline>
                </svg>
                
                <div style="display: flex; flex-direction: column; align-items: flex-start; line-height: 1.2;">
                    <span style="font-size:11px; opacity:0.9; text-transform: uppercase; letter-spacing: 0.5px;">Auto-closing</span>
                    <span style="font-size:20px; font-weight:700; letter-spacing: 1px;">
                        <span id="timerSeconds" class="timer-pulse">10</span>s
                    </span>
                </div>
            </div>

            <div style="font-size:32px; font-weight:bold; letter-spacing:1px; line-height:1.5;">
                Do you know what is Data Protection?<br>
                Phishing?<br>
                Spoofing?<br><br>
                Stay Tuned...
            </div>
        </div>


        <script>

            function showPopup() {
                document.getElementById('popupForm').style.display = 'block';
            }

            function hidePopup() {
                document.getElementById('popupForm').style.display = 'none';
            }
        </script>

        <!-- Popup form -->


        <section id="section1" class="section">
            <div class="container">

                <div class="event-container">
                    <div class="event-images">
                        <div class="event-image-number">
                            <span id="eventCurrentImageNumber"></span>/ <span id="eventTotalImageNumber"></span>
                        </div>
                        <img src='' />
                        <div class="event-description-portion">
                            <div class="event-image-description"></div>
                            <div class="event-navigation-container">
                                <button type="button" class="event-navigation-btn event-prev-btn"
                                    onclick="eventShowPreviousImage()">
                                    ❮</button>
                                <div class="event-dot-container"></div>
                                <button type="button" class="event-navigation-btn event-next-btn"
                                    onclick="eventShowNextImage()">
                                    ❯</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>

        <section id="section2" class="section">
            <div class="column">

                <div class="news-outer-container">
                    <div class="latest-news-title">Latest News</div>
                    <div class="news-inner-container" id="newsContainer">
                        <%-- <a target="_blank" class="news-card">
                            <div id="line"></div>
                            <img />
                            <div class="news-info">
                                <div class="news-title">
                                    </h2>
                                        <div class="news-author-date">
                                            </h4>
                                        </div>
                                </div>
                            </div>
                        </a>--%>
                    </div>

                </div>

            </div>

            <div class="column">

                <div class="employee-container">
                    <div class="welcome-text">Welcome Buddies!</div>
                    <div class="employee-image">
                        <img />
                    </div>
                    <div class="employee-info">
                        <div class="employee-name">No employee update for last 30 days</div>
                        <div class="employee-dep">-</div>
                        <div class="employee-desig">-</div>
                        <div class="employee-location">-</div>
                    </div>

                    <div class="employee-navigation-container">
                        <button type="button" class="employee-navigation-btn employee-prev-btn"
                            onclick="employeeShowPreviousImage()">
                            ❮</button>
                        <div class="employee-dot-container">
                        </div>
                        <button type="button" class="employee-navigation-btn employee-next-btn"
                            onclick="employeeShowNextImage()">
                            ❯</button>
                        <div class="employee-image-number">
                            <span id="employeeCurrentImageNumber"></span>/ <span
                                id="employeeTotalImageNumber"></span>
                        </div>
                    </div>

                </div>

            </div>

            <div class="column">

                <div class="employee-contact-container">
                    <h1 class="employee-contact-title">Employee Contact</h1>

                    <div class="select-containers">
                        <div class="select-box">
                            <label for="companySelect">Company:</label>
                            <select id="companySelect" onchange="onCompanySelect()">
                                <option value="">Select</option>
                            </select>
                        </div>


                        <div class="select-box">
                            <label for="locationSelect">Location:</label>
                            <select id="locationSelect" onchange="onLocationSelect()">
                                <option value="">Select</option>
                            </select>
                        </div>

                        <div class="select-box">
                            <label for="branchSelect">Branch:</label>
                            <select id="branchSelect" onchange="onBranchSelect()" disabled>
                                <option value="">Select</option>
                            </select>
                        </div>

                        <div class="select-box">
                            <label for="departmentSelect">Department:</label>
                            <select id="departmentSelect" onchange="onDepartmentSelect()" disabled>
                                <option value="">Select</option>
                            </select>
                        </div>

                        <div class="select-box">
                            <label for="searchInput">Search:</label>
                            <input type="text" id="searchInput" onkeyup="onSearch()" placeholder="Search..." />
                        </div>
                    </div>

                    <div class="table-wrapper">
                        <table id="employeeTable">
                            <thead>
                                <tr>
                                    <th>Emp ID</th>
                                    <th>Name</th>
                                    <th>Contact</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>

            </div>
        </section>

        <section id="section3" class="section">
            <div class="column-layout">
                <div class="column">
                    <div class="row">

                        <div class="project-gallery-container">
                            <div class="project-gallery-images">
                                <img src='' />
                                <div class="project-gallery-navigation-container">
                                    <button type="button"
                                        class="project-gallery-navigation-btn project-gallery-prev-btn"
                                        onclick="projectGalleryShowPreviousImage()">
                                        ❮</button>
                                    <div id="projectGalleryDotContainer" class="project-gallery-dot-container">
                                    </div>
                                    <button type="button"
                                        class="project-gallery-navigation-btn project-gallery-next-btn"
                                        onclick="projectGalleryShowNextImage()">
                                        ❯</button>
                                </div>
                                <div class="project-gallery-image-number">
                                    <span id="projectGalleryCurrentImageNumber"></span>/ <span
                                        id="projectGalleryTotalImageNumber"></span>
                                </div>
                            </div>
                            <div class="project-gallery-description-portion">
                                  <%-- <div class="project-gallery-project-name">sbeaberqben</div>--%>
                                <div class="project-gallery-project-location">etqbt4qebneq</div>
                                <div class="project-gallery-project-description">ajvjkwvov;h;ovio;w</div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="column">
                    <div class="row">

                        <div class="quotes-container">
                            <div class="quotes-images">
                                <img />
                            </div>
                        </div>

                    </div>
                    <div class="row">					

                        <div class="video-container">
                            <video autoplay controls playsinline controls controlslist="nodownload"></video>

                            <div class="video-navigation-container">
                                <button type="button" class="video-navigation-btn video-prev-btn"
                                    onclick="videoShowPreviousVideo()">
                                    ❮</button>
                                <div class="video-dot-container">
                                    <span class="video-dot"
                                        onclick="PlayVideo(3)"></span>
                                </div>
                                <button type="button" class="video-navigation-btn video-next-btn"
                                    onclick="videoShowNextVideo()">
                                    ❯</button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </section>


        <div class="quicklinks" id="quicklinks">
            <div class="quicklinks-title">
                <span style="--i: 1">Q</span>
                <span style="--i: 2">u</span>
                <span style="--i: 3">i</span>
                <span style="--i: 4">c</span>
                <span style="--i: 5">k</span>
                <span style="--i: 6"></span>
                <span style="--i: 7">L</span>
                <span style="--i: 8">i</span>
                <span style="--i: 9">n</span>
                <span style="--i: 10">k</span>
                <span style="--i: 11">s</span>
            </div>
            <div class="quicklinks-tray" id="quicklinkstray">
                <%-- <a href="http://fujitecerp/">
                 <div class="quicklinks-card">
                     <img src="/LandingPageResources/QuickLinks/erp_logo.png" alt="ERP Logo">
                     <p>ERP</p>
                 </div>
                 </a>--%>
            </div>
        </div>
						
        <div class="site-navigation" id="sitenavigation">           
                <div class="circle" data-section="#section1"></div>
                <div class="circle" data-section="#section2"></div>
                <div class="circle" data-section="#section3"></div>    
        </div>

        <script src="JS/event-slide-show.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/news-renderer.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/employee-slide-show.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/employee-contact-show.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/awareness-slide-show.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/project-gallery-slide-show.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/quotes-container.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/video-container.js?v=<%= DateTime.Now.Ticks %>"></script>
        <script src="JS/quicklinks-container.js?v=<%= DateTime.Now.Ticks %>"></script>


        <script>

            document.addEventListener("DOMContentLoaded", function () {
                const sections = document.querySelectorAll("section");
                const circles = document.querySelectorAll(".circle");

                window.addEventListener("scroll", () => {
                    let currentSectionIndex = 0;

                    sections.forEach((section, index) => {
                        const sectionTop = section.offsetTop - window.innerHeight / 2;
                        if (window.scrollY >= sectionTop) {
                            currentSectionIndex = index;
                        }
                    });

                    circles.forEach((circle, index) => {
                        if (index === currentSectionIndex) {
                            circle.classList.add("active");
                        } else {
                            circle.classList.remove("active");
                        }
                    });
                });

                circles.forEach((circle, index) => {
                    circle.addEventListener("click", () => {
                        const targetSection = document.querySelector(circle.getAttribute("data-section"));
                        if (targetSection) {
                            targetSection.scrollIntoView({
                                behavior: "smooth"
                            });
                        }
                    });
                });

                // Auto-hide video overlay after 10 seconds with countdown timer
                const videoOverlay = document.querySelector('.video-overlay');
                const timerElement = document.getElementById('timerSeconds');
                
                if (videoOverlay && timerElement) {
                    let timeLeft = 10;
                    
                    // Update countdown every second
                    const countdownInterval = setInterval(function() {
                        timeLeft--;
                        timerElement.textContent = timeLeft;
                        
                        if (timeLeft <= 0) {
                            clearInterval(countdownInterval);
                        }
                    }, 1000);
                    
                    // Fade out after 10 seconds
                    setTimeout(function() {
                        videoOverlay.style.opacity = '0';
                        
                        // Completely hide after fade animation completes
                        setTimeout(function() {
                            videoOverlay.style.display = 'none';
                        }, 500); // Wait for 0.5s transition to complete
                    }, 10000); // 10 seconds
                }
            });


            function scrollToSection(sectionNumber) {
                const section = document.getElementById(`section${sectionNumber}`);
                section.scrollIntoView({ behavior: "smooth" });
            }
        </script>

        <script>
            // Function to scroll to Section 2 on page load
            window.onload = function () {
                //scrollToSection(3);

                //window.awarenessSlideShowFunction();
                window.displayEmployeeContactFunction();
                window.displayEmployeeFunction();
                window.eventSlideShowFunction();
                window.newsRendererFunction();
                window.projectGallerySlideShowFunction();
                window.videoPlayFunction();
                window.quotesSlideShowFunction();
                window.createQuickLinks();

            };

            // Function to scroll to a specific section number
            function scrollToSection(sectionNumber) {
                const section = document.getElementById(`section${sectionNumber}`);
                if (section) {
                    section.scrollIntoView({ behavior: "smooth" });
                }
            }

        </script>
    </form>

</body> 
 
</html>
// Retrieve combined image data and descriptions from the hidden field
const videoDataField = document.querySelector('#videoData');
let videoList = [];

if (videoDataField !== null && videoDataField.value !== '') {
    const videoData = videoDataField.value;
    videoList = JSON.parse(videoData);
} else {
    // Handle the case when data is null or empty
    console.log('Video Data is null or empty.');
}


let currentVideoIndex = 0;
const videoPerBatch = 5;

// DOM elements
const videoField = document.querySelector('.video-container video');
const videoDotContainer = document.querySelector('.video-dot-container');

function videoShowNextVideo() {
    currentVideoIndex = (currentVideoIndex + 1) % videoList.length;
    updateVideoSource();
}

function videoShowPreviousVideo() {
    currentVideoIndex = (currentVideoIndex - 1 + videoList.length) % videoList.length;
    updateVideoSource();
}

function updateVideoSource() {
    const data = videoList[currentVideoIndex];     
    videoField.src = data.VideoFile;
    videoField.load();
    videoField.muted = true;
    videoField.play().catch(error => {
        //console.error('Video playback error:', error);
    });
    videosUpdateDots()
}

function videoDotClicked(event) {
    if (event.target.classList.contains('video-dot')) {
        console.log(event.target.dataset.index);
        const clickedIndex = parseInt(event.target.dataset.index);
        currentVideoIndex = clickedIndex;
        updateVideoSource();
    }
}

// Function to show a specific image by index
function PlayVideo(index) {
    currentVideoIndex = index;
    updateVideoSource();
}

// Function to generate video dots
function videosUpdateDots() {
    const videoDotContainer = document.querySelector('.video-dot-container');

    // Remove existing dots
    videoDotContainer.innerHTML = '';

    const videoListLength = Object.keys(videoList).length;
    const numBatches = Math.ceil(videoListLength / videoPerBatch);
    const currentBatch = Math.floor(currentVideoIndex / videoPerBatch);
    const startIndex = currentBatch * videoPerBatch;
    const endIndex = Math.min((currentBatch + 1) * videoPerBatch, videoListLength)

    for (let i = startIndex; i < endIndex; i++) {
        const dot = document.createElement('span');
        dot.className = 'video-dot' + (i === currentVideoIndex ? ' active' : '');
        dot.setAttribute('onclick', `PlayVideo(${i})`);
        videoDotContainer.appendChild(dot);
    }
}

function videoPlayFunction() {
    if (videoList.length > 0) {        
        PlayVideo(0);
        videosUpdateDots();
    } else {
        console.log('No videos available for playback.');
    }
}


// Expose newsRendererFunction globally
window.videoPlayFunction = videoPlayFunction;
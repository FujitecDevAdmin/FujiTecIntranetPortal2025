// Retrieve combined image data and descriptions from the hidden field
const projectGalleryDataField = document.querySelector('#projectGalleryData');
let projectGalleryList = [];

if (projectGalleryDataField !== null && projectGalleryDataField.value !== '') {
    const projectGalleryData = projectGalleryDataField.value;
    projectGalleryList = JSON.parse(projectGalleryData);
} else {
    // Handle the case when data is null or empty
    console.log('Project Gallery Data is null or empty.');
}


let currentprojectGalleryIndex = 0;
const projectGalleryPerBatch = 20;
const projectGalleryIntervalDuration = 5000; // Interval duration for auto-changing images

// DOM elements
const projectGalleryImageElement = document.querySelector('.project-gallery-images img');
const projectGalleryDotContainer = document.querySelector('.project-gallery-dot-container');

const currentprojectGalleryNumberElement = document.getElementById('projectGalleryCurrentImageNumber');
const totalprojectGalleryNumberElement = document.getElementById('projectGalleryTotalImageNumber');

//const projectNameElement = document.querySelector('.project-gallery-project-name');
const projectLocationElement = document.querySelector('.project-gallery-project-location');
const projectDescriptionElement = document.querySelector('.project-gallery-project-description');


// Function to generate a random sentence
function projectGalleryGenerateRandomSentence() {
    const sentences = [
        // Your sentences here...
    ];
    const randomIndex = Math.floor(Math.random() * sentences.length);
    return sentences[randomIndex];
}

// Function to get image information by index
function projectGalleryGetImageByIndex(index) {
    const keys = Object.keys(projectGalleryList);
    if (index >= 0 && index < keys.length) {
        const key = keys[index];
        return projectGalleryList[key];
    } else {
        console.error("Invalid index");
        return null;
    }
}

const PROJECTGALLERY_MAX_DESCRIPTION_LENGTH = 200;
function truncateProjectGalleryDescription(description) {
    return description.length > PROJECTGALLERY_MAX_DESCRIPTION_LENGTH ?
        description.slice(0, PROJECTGALLERY_MAX_DESCRIPTION_LENGTH) + '...' :
        description;
}

// Function to update the displayed image and description
function projectGalleryUpdateImage() {
    const data = projectGalleryGetImageByIndex(currentprojectGalleryIndex);
    if (data) {
        projectGalleryImageElement.src = data.ImagesFile;
        projectGalleryImageElement.alt = data.ProjectName;

        currentprojectGalleryNumberElement.textContent = currentprojectGalleryIndex + 1;
        totalprojectGalleryNumberElement.textContent = Object.keys(projectGalleryList).length;

        //projectNameElement.innerHTML = truncateProjectGalleryDescription(data.ProjectName);
        projectLocationElement.innerHTML = truncateProjectGalleryDescription(data.ProjectLocation);
        projectDescriptionElement.innerHTML = truncateProjectGalleryDescription(data.ProjectDescription);
    }
    projectGalleryUpdateDots();
}


// Function to update the dots indicating the current image
function projectGalleryUpdateDots() {
    const projectGalleryListLength = Object.keys(projectGalleryList).length;
    projectGalleryDotContainer.innerHTML = '';
    const numBatches = Math.ceil(projectGalleryListLength / projectGalleryPerBatch);
    const currentBatch = Math.floor(currentprojectGalleryIndex / projectGalleryPerBatch);
    const startIndex = currentBatch * projectGalleryPerBatch;
    const endIndex = Math.min((currentBatch + 1) * projectGalleryPerBatch, projectGalleryListLength);

    for (let i = startIndex; i < endIndex; i++) {
        const dot = document.createElement('span');
        dot.className = 'project-gallery-dot' + (i === currentprojectGalleryIndex ? ' active' : '');
        dot.setAttribute('onclick', `projectGalleryShowImage(${i})`);
        projectGalleryDotContainer.appendChild(dot);
    }
}

// Function to show a specific image by index
function projectGalleryShowImage(index) {
    currentprojectGalleryIndex = index;
    projectGalleryUpdateImage();
    projectGalleryResetAutoDelay();
}

// Function to show the previous image
function projectGalleryShowPreviousImage() {
    const projectGalleryListLength = Object.keys(projectGalleryList).length;
    currentprojectGalleryIndex = (currentprojectGalleryIndex > 0) ? currentprojectGalleryIndex - 1 : projectGalleryListLength - 1;
    projectGalleryUpdateImage();
    projectGalleryResetAutoDelay();
}

// Function to show the next image
function projectGalleryShowNextImage() {
    const projectGalleryListLength = Object.keys(projectGalleryList).length;
    currentprojectGalleryIndex = (currentprojectGalleryIndex < projectGalleryListLength - 1) ? currentprojectGalleryIndex + 1 : 0;
    projectGalleryUpdateImage();
    projectGalleryResetAutoDelay();
}

// Function to reset the auto delay
function projectGalleryResetAutoDelay() {
    clearInterval(projectGalleryIntervalId);
    projectGalleryIntervalId = setInterval(projectGalleryAutoChangeImage, projectGalleryIntervalDuration);
}

// Function to show the next image after a delay
function projectGalleryAutoChangeImage() {
    projectGalleryShowNextImage();
    projectGalleryUpdateDots();
}

// Set interval for auto-changing images
let projectGalleryIntervalId = setInterval(projectGalleryAutoChangeImage, projectGalleryIntervalDuration);

// Call updateDots when the page is loaded
function projectGallerySlideShowFunction() {
    if (projectGalleryList.length > 0) {
        projectGalleryShowImage(0);
        projectGalleryUpdateDots();
    } else {
        console.log('No project gallery data available.');
    }
}


// Expose projectGallerySlideShowFunction to the global scope
window.projectGallerySlideShowFunction = projectGallerySlideShowFunction;

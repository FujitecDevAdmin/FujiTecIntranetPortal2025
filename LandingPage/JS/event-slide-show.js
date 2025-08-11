// Retrieve combined image data and descriptions from the hidden field
const eventSlideShowDataField = document.querySelector('#eventSlideShowData');
let eventSlideShowList = [];

if (eventSlideShowDataField !== null && eventSlideShowDataField.value !== '') {
    const eventSlideShowData = eventSlideShowDataField.value;
    eventSlideShowList = JSON.parse(eventSlideShowData);
} else {
    // Handle the case when data is null or empty
    console.log('Event Slide Show Data is null or empty.');
}


let currentEventIndex = 0;
const eventPerBatch = 10;
const eventIntervalDuration = 5000; // Interval duration for auto-changing images

// DOM elements
const eventImageElement = document.querySelector('.event-images img');
const eventDescriptionElement = document.querySelector('.event-image-description');
const currentEventNumberElement = document.getElementById('eventCurrentImageNumber');
const totalEventNumberElement = document.getElementById('eventTotalImageNumber');
const eventDotContainer = document.querySelector('.event-dot-container');

// Function to generate a random sentence
function eventGenerateRandomSentence() {
    const sentences = [
        // Your sentences here...
    ];
    const randomIndex = Math.floor(Math.random() * sentences.length);
    return sentences[randomIndex];
}

// Function to get image information by index
function eventGetImageByIndex(index) {
    const keys = Object.keys(eventSlideShowList);
    if (index >= 0 && index < keys.length) {
        const key = keys[index];
        return eventSlideShowList[key];
    } else {
        console.error("Invalid index");
        return null;
    }
}

// Function to update the displayed image and description
function eventUpdateImage() {
    const data = eventGetImageByIndex(currentEventIndex);
    if (data) {
        eventImageElement.src = data.ImageFile;
        eventImageElement.alt = data.EventName;
        eventDescriptionElement.textContent = data.EventName;
        currentEventNumberElement.textContent = currentEventIndex + 1;
        totalEventNumberElement.textContent = Object.keys(eventSlideShowList).length;
    }
    eventUpdateDots();
}

// Function to update the dots indicating the current image
function eventUpdateDots() {
    const eventSlideShowListLength = Object.keys(eventSlideShowList).length;
    eventDotContainer.innerHTML = '';
    const numBatches = Math.ceil(eventSlideShowListLength / eventPerBatch);
    const currentBatch = Math.floor(currentEventIndex / eventPerBatch);
    const startIndex = currentBatch * eventPerBatch;
    const endIndex = Math.min((currentBatch + 1) * eventPerBatch, eventSlideShowListLength);

    for (let i = startIndex; i < endIndex; i++) {
        const dot = document.createElement('span');
        dot.className = 'event-dot' + (i === currentEventIndex ? ' active' : '');
        dot.setAttribute('onclick', `eventShowImage(${i})`);
        eventDotContainer.appendChild(dot);
    }
}

// Function to show a specific image by index
function eventShowImage(index) {
    currentEventIndex = index;
    eventUpdateImage();
    eventResetAutoDelay();
}

// Function to show the previous image
function eventShowPreviousImage() {
    const eventSlideShowListLength = Object.keys(eventSlideShowList).length;
    currentEventIndex = (currentEventIndex > 0) ? currentEventIndex - 1 : eventSlideShowListLength - 1;
    eventUpdateImage();
    eventResetAutoDelay();
}

// Function to show the next image
function eventShowNextImage() {
    const eventSlideShowListLength = Object.keys(eventSlideShowList).length;
    currentEventIndex = (currentEventIndex < eventSlideShowListLength - 1) ? currentEventIndex + 1 : 0;
    eventUpdateImage();
    eventResetAutoDelay();
}

// Function to reset the auto delay
function eventResetAutoDelay() {
    clearInterval(eventIntervalId);
    eventIntervalId = setInterval(eventAutoChangeImage, eventIntervalDuration);
}

// Function to show the next image after a delay
function eventAutoChangeImage() {
    eventShowNextImage();
    eventUpdateDots();
}

// Set interval for auto-changing images
let eventIntervalId = setInterval(eventAutoChangeImage, eventIntervalDuration);

// Call updateDots when the page is loaded
function eventSlideShowFunction() {
    if (eventSlideShowList.length > 0) {

        eventShowImage(0);
        eventUpdateDots();
    } else {
        console.log('No data available for Event Slide Show.');
    }
}

// Expose eventSlideShowFunction to the global scope
window.eventSlideShowFunction = eventSlideShowFunction;

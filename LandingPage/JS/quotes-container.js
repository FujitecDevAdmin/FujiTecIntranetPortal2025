// Retrieve combined image data and descriptions from the hidden field
const quotesDataField = document.querySelector('#quotesData');
let quotesList = [];

if (quotesDataField !== null && quotesDataField.value !== '') {
    const quotesData = quotesDataField.value;
    quotesList = JSON.parse(quotesData);
} else {
    // Handle the case when data is null or empty
    console.log('Quotes Data is null or empty.');
}


let currentquotesIndex = 0;
const quotesPerBatch = 5;
const quotesIntervalDuration = 5000; // Interval duration for auto-changing images

// DOM elements
const quotesImageElement = document.querySelector('.quotes-images img');


// Function to get image information by index
function quotesGetImageByIndex(index) {
    const keys = Object.keys(quotesList);
    if (index >= 0 && index < keys.length) {
        const key = keys[index];
        return quotesList[key];
    } else {
        console.error("Invalid index");
        return null;
    }
}


// Function to update the displayed image and description
function quotesUpdateImage() {
    const data = quotesGetImageByIndex(currentquotesIndex);
    if (data) {
        quotesImageElement.src = data.ImagesFile;
        quotesImageElement.alt = data.QuotesName
    }
}


// Function to show a specific image by index
function quotesShowImage(index) {
    currentquotesIndex = index;
    quotesUpdateImage();
}

// Call updateDots when the page is loaded
function quotesSlideShowFunction() {
    if (quotesList.length > 0) {
        quotesShowImage(0);
    } else {
        console.log('No quotes available for slideshow.');
    }
}

// Expose quotesSlideShowFunction to the global scope
window.quotesSlideShowFunction = quotesSlideShowFunction;

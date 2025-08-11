//// Retrieve combined image data and descriptions from the hidden field
//const awarenessSlideShowDataField = document.querySelector('#awarenessSlideShowData');
//let awarenessSlideShowList = [];

//if (awarenessSlideShowDataField !== null && awarenessSlideShowDataField.value !== '') {
//    const awarenessSlideShowData = awarenessSlideShowDataField.value;
//    awarenessSlideShowList = JSON.parse(awarenessSlideShowData);
//} else {
//    // Handle the case when data is null or empty
//    console.log('Awareness Slide Show Data is null or empty.');
//}

//let currentAwarenessIndex = 0;
//const awarenessPerBatch = 5;
//const awarenessIntervalDuration = 2000; // Interval duration for auto-changing images

//// DOM elements
//const awarenessImageElement = document.querySelector('.awareness-images img');
//const awarenessDescriptionElement = document.querySelector('.awareness-image-description');
//const currentAwarenessNumberElement = document.getElementById('awarenessCurrentImageNumber');
//const totalAwarenessNumberElement = document.getElementById('awarenessTotalImageNumber');
//const awarenessDotContainer = document.querySelector('.awareness-dot-container');

//// Function to generate a random sentence
//function awarenessGenerateRandomSentence() {
//    const sentences = [
//        // Your sentences here...
//    ];
//    const randomIndex = Math.floor(Math.random() * sentences.length);
//    return sentences[randomIndex];
//}

//// Function to get image information by index
//function awarenessGetImageByIndex(index) {
//    const keys = Object.keys(awarenessSlideShowList);
//    if (index >= 0 && index < keys.length) {
//        const key = keys[index];
//        return awarenessSlideShowList[key];
//    } else {
//        console.error("Invalid index");
//        return null;
//    }
//}

//const AWARENESS_MAX_DESCRIPTION_LENGTH = 30;
//function truncateAwarenessDescription(description) {
//    return description.length > AWARENESS_MAX_DESCRIPTION_LENGTH ?
//        description.slice(0, AWARENESS_MAX_DESCRIPTION_LENGTH) + '...' :
//        description;
//}

//// Function to update the displayed image and description
//function awarenessUpdateImage() {
//    const data = awarenessGetImageByIndex(currentAwarenessIndex);
//    if (data) {
//        awarenessImageElement.src = data.ImagesFile;
//        awarenessImageElement.alt = data.AwarenessCategory
//        awarenessDescriptionElement.textContent = truncateAwarenessDescription(data.AwarenessCategory);
//        currentAwarenessNumberElement.textContent = currentAwarenessIndex + 1;
//        totalAwarenessNumberElement.textContent = Object.keys(awarenessSlideShowList).length;
//    }
//    awarenessUpdateDots();
//}


//// Function to update the dots indicating the current image
//function awarenessUpdateDots() {
//    const awarenessSlideShowListLength = Object.keys(awarenessSlideShowList).length;
//    awarenessDotContainer.innerHTML = '';
//    const numBatches = Math.ceil(awarenessSlideShowListLength / awarenessPerBatch);
//    const currentBatch = Math.floor(currentAwarenessIndex / awarenessPerBatch);
//    const startIndex = currentBatch * awarenessPerBatch;
//    const endIndex = Math.min((currentBatch + 1) * awarenessPerBatch, awarenessSlideShowListLength);

//    for (let i = startIndex; i < endIndex; i++) {
//        const dot = document.createElement('span');
//        dot.className = 'awareness-dot' + (i === currentAwarenessIndex ? ' active' : '');
//        dot.setAttribute('onclick', `awarenessShowImage(${i})`);
//        awarenessDotContainer.appendChild(dot);
//    }
//}

//// Function to show a specific image by index
//function awarenessShowImage(index) {
//    currentAwarenessIndex = index;
//    awarenessUpdateImage();
//    awarenessResetAutoDelay();
//}

//// Function to show the previous image
//function awarenessShowPreviousImage() {
//    const awarenessSlideShowListLength = Object.keys(awarenessSlideShowList).length;
//    currentAwarenessIndex = (currentAwarenessIndex > 0) ? currentAwarenessIndex - 1 : awarenessSlideShowListLength - 1;
//    awarenessUpdateImage();
//    awarenessResetAutoDelay();
//}

//// Function to show the next image
//function awarenessShowNextImage() {
//    const awarenessSlideShowListLength = Object.keys(awarenessSlideShowList).length;
//    currentAwarenessIndex = (currentAwarenessIndex < awarenessSlideShowListLength - 1) ? currentAwarenessIndex + 1 : 0;
//    awarenessUpdateImage();
//    awarenessResetAutoDelay();
//}

//// Function to reset the auto delay
//function awarenessResetAutoDelay() {
//    clearInterval(awarenessIntervalId);
//    awarenessIntervalId = setInterval(awarenessAutoChangeImage, awarenessIntervalDuration);
//}

//// Function to show the next image after a delay
//function awarenessAutoChangeImage() {
//    awarenessShowNextImage();
//    awarenessUpdateDots();
//}

//// Set interval for auto-changing images
//let awarenessIntervalId = setInterval(awarenessAutoChangeImage, awarenessIntervalDuration);

//// Call updateDots when the page is loaded
//function awarenessSlideShowFunction() {
//    if (awarenessSlideShowList.length > 0) {
//        awarenessShowImage(0);
//        awarenessUpdateDots();
//    } else {
//        console.log('No data available for Awareness Slide Show.');
//    }
//}

//// Expose awarenessSlideShowFunction to the global scope
//window.awarenessSlideShowFunction = awarenessSlideShowFunction;

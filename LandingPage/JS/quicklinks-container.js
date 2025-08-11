// Retrieve combined image data and descriptions from the hidden field
const quickLinksDataField = document.querySelector('#quickLinksData');
let quickLinksList = [];

if (quickLinksDataField !== null && quickLinksDataField.value !== '') {
    const quickLinksData = quickLinksDataField.value;
    quickLinksList = JSON.parse(quickLinksData);
} else {
    // Handle the case when data is null or empty
    console.log('Quick Links Data is null or empty.');
}

// Function to dynamically create and insert quick links
function createQuickLinks() {
    const tray = document.querySelector('#quicklinkstray');
    if (tray) {
        tray.addEventListener("wheel", (evt) => {
            evt.preventDefault();
            tray.scrollLeft += evt.deltaY;
            console.log(evt.deltaY);
        });
    }

    quickLinksList.forEach(link => {
        const { ApplicationName, ApplicationURL, ImageFile } = link;

        const aTag = document.createElement('a');
        aTag.href = ApplicationURL;
        aTag.target = '_blank';
        aTag.title = `Open ${ApplicationName}`;

        const divCard = document.createElement('div');
        divCard.classList.add('quicklinks-card');

        const img = document.createElement('img');
        img.src = ImageFile;
        img.alt = ApplicationName + ' Logo';

        const p = document.createElement('p');
        p.textContent = ApplicationName;

        divCard.appendChild(img);
        divCard.appendChild(p);
        aTag.appendChild(divCard);

        tray.appendChild(aTag);
    });
}


const quicklinksContainer = document.querySelector(".quicklinks");
quicklinksContainer.addEventListener("wheel", (evt) => {
    evt.preventDefault();
    quicklinksContainer.scrollLeft += evt.deltaY;
});

// Expose displayEmployeeFunction to the global scope
window.createQuickLinks = createQuickLinks;
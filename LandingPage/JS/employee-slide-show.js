// Retrieve combined employee slide show data from the hidden field
const employeeSlideShowDataField = document.querySelector('#employeeSlideShowData');
let employeeSlideShowList = [];

if (employeeSlideShowDataField !== null && employeeSlideShowDataField.value !== '') {
    const employeeSlideShowData = employeeSlideShowDataField.value;
    employeeSlideShowList = JSON.parse(employeeSlideShowData);
} else {
    // Handle the case when data is null or empty
    console.log('Employee Slide Show Data is null or empty.');
}

let currentEmployeeIndex = 0;
const employeePerBatch = 5;
const employeeIntervalDuration = 5000; // Interval duration for auto-changing images

// DOM elements
const employeeImageElement = document.querySelector('.employee-image img');
const employeeNameElement = document.querySelector('.employee-name');
const employeeDepElement = document.querySelector('.employee-dep');
const employeeDesigElement = document.querySelector('.employee-desig');
const employeeLocationElement = document.querySelector('.employee-location');
const employeeDotContainer = document.querySelector('.employee-dot-container');

const currentEmployeeNumberElement = document.getElementById('employeeCurrentImageNumber');
const totalEmployeeNumberElement = document.getElementById('employeeTotalImageNumber');

// Function to get employee information by index
function getEmployeeByIndex(index) {
    const keys = Object.keys(employeeSlideShowList);
    if (index >= 0 && index < keys.length) {
        const key = keys[index];
        return employeeSlideShowList[key];
    } else {
        console.error("Invalid index");
        return null;
    }
}

// Function to update the displayed employee information
function updateEmployee() {
    const employeeInfo = getEmployeeByIndex(currentEmployeeIndex);
    if (employeeInfo) {
        employeeImageElement.src = employeeInfo.ImageFile;
        employeeImageElement.alt = employeeInfo.EmployeeName;
        employeeNameElement.textContent = employeeInfo.EmployeeName;
        employeeDepElement.textContent = employeeInfo.Department;
        employeeDesigElement.textContent = employeeInfo.Designation;
        employeeLocationElement.textContent = employeeInfo.Location;

        currentEmployeeNumberElement.textContent = currentEmployeeIndex + 1;
        totalEmployeeNumberElement.textContent = Object.keys(employeeSlideShowList).length;
    }
    updateEmployeeDots();
}



// Function to update the dots indicating the current employee
function updateEmployeeDots() {
    const employeeSlideShowListLength = Object.keys(employeeSlideShowList).length;
    employeeDotContainer.innerHTML = '';
    const numBatches = Math.ceil(employeeSlideShowListLength / employeePerBatch);
    const currentBatch = Math.floor(currentEmployeeIndex / employeePerBatch);
    const startIndex = currentBatch * employeePerBatch;
    const endIndex = Math.min((currentBatch + 1) * employeePerBatch, employeeSlideShowListLength);

    for (let i = startIndex; i < endIndex; i++) {
        const dot = document.createElement('span');
        dot.className = 'employee-dot' + (i === currentEmployeeIndex ? ' active' : '');
        dot.setAttribute('onclick', `showEmployee(${i})`);
        employeeDotContainer.appendChild(dot);
    }
}

// Function to show a specific employee by index
function showEmployee(index) {
    currentEmployeeIndex = index;
    updateEmployee();
    employeeResetAutoDelay();
}


// Function to show the previous image
function employeeShowPreviousImage() {
    const employeeSlideShowListLength = Object.keys(employeeSlideShowList).length;
    currentEmployeeIndex = (currentEmployeeIndex > 0) ? currentEmployeeIndex - 1 : employeeSlideShowListLength - 1;
    updateEmployee();
    employeeResetAutoDelay();
}

// Function to show the next image
function employeeShowNextImage() {
    const employeeSlideShowListLength = Object.keys(employeeSlideShowList).length;
    currentEmployeeIndex = (currentEmployeeIndex < employeeSlideShowListLength - 1) ? currentEmployeeIndex + 1 : 0;
    updateEmployee();
    employeeResetAutoDelay();
}

// Function to reset the auto delay
function employeeResetAutoDelay() {
    clearInterval(employeeIntervalId);
    employeeIntervalId = setInterval(employeeAutoChangeImage, employeeIntervalDuration);
}

// Function to show the next image after a delay
function employeeAutoChangeImage() {
    employeeShowNextImage();
    updateEmployeeDots();
}


// Set interval for auto-changing images
let employeeIntervalId = setInterval(employeeAutoChangeImage, employeeIntervalDuration);


// Call updateEmployee when the page is loaded
function displayEmployeeFunction() {
    if (employeeSlideShowList.length > 0) {
        showEmployee(0);
        updateEmployeeDots();
    } else {
        console.log('No data available for Employee Slide Show.');
    }
}

// Expose displayEmployeeFunction to the global scope
window.displayEmployeeFunction = displayEmployeeFunction;

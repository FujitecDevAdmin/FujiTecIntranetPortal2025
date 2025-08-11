// Retrieve combined employee contact data from the hidden field
const employeeContactDataField = document.querySelector('#employeeContactData');
let employeeContactList = [];

if (employeeContactDataField !== null && employeeContactDataField.value !== '') {
    const employeeContactData = employeeContactDataField.value;
    employeeContactList = JSON.parse(employeeContactData);
} else {
    // Handle the case when data is null or empty
    console.log('Employee Contact Data is null or empty.');
}

// const iable to hold displayed records
var displayedRecords = [];
var filteredRecords = [];

// Function to filter records based on search input
function onSearch() {
    const searchInput = document.getElementById("searchInput").value.trim().toUpperCase();
    //console.log(searchInput);
    if (searchInput !== '') {
        //console.log(displayedRecords);
        filteredRecords = displayedRecords.filter(function (item) {
            //console.log(item.EmployeeNumber, item.EmployeeName, item.ContactNo);
            const employeeNumber = (item.EmployeeNumber || '').toString().toUpperCase();
            const employeeName = (item.EmployeeName || '').toString().toUpperCase();
            const contactNo = (item.ContactNo || '').toUpperCase();

            return (
                employeeNumber.includes(searchInput) ||
                employeeName.includes(searchInput) ||
                contactNo.includes(searchInput)
            );
        });

        displaySearchedEmployeeRecords(filteredRecords);
    } else {
        displayEmployeeRecords(displayedRecords); // Show original displayed records
        filteredRecords = displayedRecords; // Restore filtered records to previously displayed
    }
}

// Function to populate unique values into select dropdown
function populateSelectBox(id, data) {
    const selectBox = document.getElementById(id);
    selectBox.disabled = false;
    selectBox.innerHTML = '<option value="">Select</option>';
    const uniqueValues = [...new Set(data)];
    uniqueValues.forEach(function (value) {
        selectBox.innerHTML += `<option value="${value}">${value}</option>`;
    });


    // temp code for quick texting
    //if (id === "companySelect" || id === "locationSelect") {
    //    // Dynamically select the first value of the select box
    //    selectBox.selectedIndex = 1; // Select the first option (index 0 is the "Select" placeholder)

    //    if (id === "companySelect") {
    //        onCompanySelect();
    //    } else if (id === "locationSelect") {
    //        onLocationSelect();
    //    }

    //}

}

// Function to filter employee records based on company, location, branch, and department
function filterEmployeeRecords(company, location, branch, department) {
    const filteredRecords = employeeContactList.filter(function (item) {
        return (
            (company === "" || item.Company === company) &&
            (location === "" || item.Location === location) &&
            (branch === "" || item.Branch === branch) &&
            (department === "" || item.Department === department)
        );
    });
    displayEmployeeRecords(filteredRecords);
}


// Function to display employee records in a table
function displayEmployeeTable(records) {
    const tableBody = document.querySelector("#employeeTable tbody");
    tableBody.innerHTML = '';

    records.forEach(function (record) {
        const employeeNumber = (record.EmployeeNumber || '').toString().toUpperCase();
        const employeeName = (record.EmployeeName || '').toString().toUpperCase();
        const contactNo = (record.ContactNo || '').toString().toUpperCase();
        const paddedEmployeeNumber = (employeeNumber || '').toString().toUpperCase().padStart(5, '0');

        tableBody.innerHTML += `<tr><td>${paddedEmployeeNumber}</td><td>${employeeName}</td><td>${contactNo}</td></tr>`;
    });
}


// Function to display employee records in the table
function displayEmployeeRecords(records) {
    displayEmployeeTable(records);
    displayedRecords = records;
}

// Function to display searched employee records in the table with highlighted matches
function displaySearchedEmployeeRecords(records) {
    const tableBody = document.querySelector("#employeeTable tbody");
    tableBody.innerHTML = '';

    const searchInput = document.getElementById("searchInput").value.trim().toUpperCase();

    records.forEach(function (record) {
        const employeeNumber = (record.EmployeeNumber || '').toString().toUpperCase();
        const employeeName = (record.EmployeeName || '').toString().toUpperCase();
        const department = (record.Department || '').toString().toUpperCase();
        const location = (record.Location || '').toString().toUpperCase();
        const branch = (record.Branch || '').toString().toUpperCase();
        const contactNo = record.ContactNo || '';


        const EmployeeNumberHighlighted = employeeNumber.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);
        const empNameHighlighted = employeeName.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);
        //const  departmentHighlighted = department.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);
        //const  locationHighlighted = location.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);
        //const  branchHighlighted = branch.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);
        const contactHighlighted = contactNo.replace(new RegExp(searchInput, "gi"), (match) => `<span style="background-color: yellow">${match}</span>`);

        const row = `<tr><td>${EmployeeNumberHighlighted}</td><td>${empNameHighlighted}</td><td>${contactHighlighted}</td></tr>`;
        tableBody.innerHTML += row;
    });
}


// Function to handle company selection
function onCompanySelect() {
    const company = document.getElementById("companySelect").value;
    const locations = employeeContactList.filter(function (item) {
        return item.Company === company;
    }).map(function (item) {
        return item.Location;
    });
    populateSelectBox("locationSelect", [...new Set(locations)]);
    //filterEmployeeRecords(company, "", "", "");

    if (company === '') {
        document.getElementById("locationSelect").disabled = true;
        document.getElementById("branchSelect").disabled = true;
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;
    }
    else {
        document.getElementById("locationSelect").disabled = false;
        document.getElementById("branchSelect").disabled = true;
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;
    }


}
// Function to handle location selection
function onLocationSelect() {
    const company = document.getElementById("companySelect").value;
    const location = document.getElementById("locationSelect").value;
    const branches = employeeContactList.filter(function (item) {
        return item.Company === company && item.Location === location;
    }).map(function (item) {
        return item.Branch;
    });
    populateSelectBox("branchSelect", branches);
    filterEmployeeRecords(company, location, "", "");

    if (location === '') {
        document.getElementById("branchSelect").disabled = true;
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;
    }
    else {
        document.getElementById("branchSelect").disabled = false;
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;
    }
}

// Function to handle branch selection
function onBranchSelect() {
    const company = document.getElementById("companySelect").value;
    const location = document.getElementById("locationSelect").value;
    const branch = document.getElementById("branchSelect").value;
    const departments = employeeContactList.filter(function (item) {
        return item.Company === company && item.Location === location && item.Branch === branch;
    }).map(function (item) {
        return item.Department;
    });
    populateSelectBox("departmentSelect", departments);
    filterEmployeeRecords(company, location, branch, "");

    if (branch === '') {
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;
    }
    else {
        document.getElementById("departmentSelect").disabled = false;
        document.getElementById("searchInput").disabled = true;
    }
}

// Function to handle department selection
function onDepartmentSelect() {
    const company = document.getElementById("companySelect").value;
    const location = document.getElementById("locationSelect").value;
    const branch = document.getElementById("branchSelect").value;
    const department = document.getElementById("departmentSelect").value;
    filterEmployeeRecords(company, location, branch, department);

    if (department === '') {
        document.getElementById("searchInput").disabled = true;
    }
    else {
        document.getElementById("searchInput").disabled = false;
    }
}

// Initially populate location select box with unique locations
const locations = employeeContactList.map(function (item) {
    return item.Location;
});

function displayEmployeeContactFunction() {
    if (employeeContactList.length > 0) {

        document.getElementById("locationSelect").disabled = true;
        document.getElementById("branchSelect").disabled = true;
        document.getElementById("departmentSelect").disabled = true;
        document.getElementById("searchInput").disabled = true;

        populateSelectBox("companySelect", [...new Set(employeeContactList.map(item => item.Company))]);

    } else {
        console.log('No data available for Employee Contact.');
    }
}

// Expose displayEmployeeFunction to the global scope
window.displayEmployeeContactFunction = displayEmployeeContactFunction;
document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Report.js loaded");
  var climberId = 0; //change to whatever current climber logged in
  getHangTestRecords(climberId);
}

async function getHangTestRecords(climberId) {
  var response = await fetch('/api/FitnessDataEntryApi/HangTest/' + climberId);
  var data = await response.json();
  console.log(JSON.stringify(data));
  createClimberHangTestTable(data);
}

function createClimberHangTestTable(data) {
  // Create table
  var table = document.createElement('table');
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = 'table table-striped table-bordered';
  // Create table header
  var tableHead = document.createElement('thead');
  var headRow = document.createElement('tr');
  var headers = ['Date', 'Body Weight', 'Added Weight',];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement('th');
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement('tbody');
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement('tr');
    var date = document.createElement('td');
    var bodyWeight = document.createElement('td');
    var addedWeight = document.createElement('td');
    date.appendChild(document.createTextNode(data[i].entryDate));
    bodyWeight.appendChild(document.createTextNode(data[i].bodyWeight));
    addedWeight.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(bodyWeight);
    row.appendChild(addedWeight);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  // Add table to page
  var tableDiv = document.getElementById('hang-test-table');
  tableDiv.appendChild(table);
  
}
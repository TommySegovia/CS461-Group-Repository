document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Report.js loaded");
  getHangTestRecords();
}

async function getHangTestRecords() {
  var response = await fetch('/api/FitnessDataEntryApi/HangTest/Results');
  var data = await response.json();
  console.log(JSON.stringify(data));
  if (response.ok){
    createClimberHangTestTable(data);
  }
  else{
    createButtonToRecordPage();
  }
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

function createButtonToRecordPage() {
  //let the user know they have no current results
  var noResults = document.createElement('p');
  noResults.appendChild(document.createTextNode('No Hang Test Results Found'));
  var noResultsDiv = document.getElementById('hang-test-table');
  noResultsDiv.appendChild(noResults);
  //create button to record page
  var button = document.createElement('button');
  button.className = 'btn btn-primary';
  button.appendChild(document.createTextNode('Record Test'));
  button.addEventListener('click', function() {
    window.location.href = '/Record/Record';
  });
  var buttonDiv = document.getElementById('hang-test-table');
  buttonDiv.appendChild(button);

  //apply box2 class to button
  button.classList.add('box2');
}
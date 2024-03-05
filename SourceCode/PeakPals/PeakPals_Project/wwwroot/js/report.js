document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Report.js loaded");
  var tableDiv = document.getElementById('hang-test-table');
  getTestRecords(0, tableDiv); //get hang test records
  tableDiv = document.getElementById('pull-test-table');
  getTestRecords(1, tableDiv); //get pull up test records

  var hangTestAverageDiv = document.getElementById('hang-test-average');
  getTestAverage(0, hangTestAverageDiv); //get hang test average
  var pullTestAverageDiv = document.getElementById('pull-test-average');
  getTestAverage(1, pullTestAverageDiv); //get pull up test average

}

async function getTestRecords(testId, tableDiv) {
  var response = await fetch('/api/FitnessDataEntryApi/Test/Results/' + testId);
  var data = await response.json();
  console.log(JSON.stringify(data));
  if (response.ok && data.length > 0) {
    createClimberTestTable(data, tableDiv);
    if (data.length > 1) {
      addGraphToResults(testId);
    }
  }
  else {
    createButtonToRecordPage(tableDiv);
  }
}

// adds graph created by FitnessDataEntryService to the analytics history page
async function addGraphToResults(testId) {
  // add each individual graph html tag
  var graphHangDiv = document.getElementById('hang-test-graph');
  var graphPullDiv = document.getElementById('pull-test-graph');

  // add graph images from wwwroot/images, but first check which test it is
  switch (testId) {
    case 0:
      graphHangDiv.innerHTML = '<img src="/images/Test_0_ResultsOverTime.png" alt="Hang Test Graph" />';
      break;
    case 1:
      graphPullDiv.innerHTML = '<img src="/images/Test_1_ResultsOverTime.png" alt="Pull Up Test Graph" />';
      break;
  }
}

async function getTestAverage(testId, averageDiv) {
  //get the most recent user test result
  try {
    var recentResponse = await fetch('/api/FitnessDataEntryApi/Test/Results/MostRecent/' + testId);
    var recentData = await recentResponse.json();
    console.log(JSON.stringify(recentData));
    if (recentResponse.ok) {
      var recentResult = recentData.result;
      var recentWeight = recentData.bodyWeight;
      var recentText = document.createElement('p');

      var resultText = 'Recent Test: ' + recentResult + ' lbs';
      var weightText = 'Bodyweight: ' + recentWeight + ' lbs';
      var ratioText = (((recentResult / recentWeight).toFixed(2) * 100)+100) + '% of total body weight';

      var resultNode = document.createTextNode(resultText);
      var br1 = document.createElement('br');
      var weightNode = document.createTextNode(weightText);
      var br2 = document.createElement('br');
      var ratioNode = document.createTextNode(ratioText);

      recentText.appendChild(resultNode);
      recentText.appendChild(br1);
      recentText.appendChild(weightNode);
      recentText.appendChild(br2);
      recentText.appendChild(ratioNode);
      averageDiv.appendChild(recentText);
    }
    else {
      var noResults = document.createElement('p');
      noResults.appendChild(document.createTextNode('No Recent Test Found'));
      averageDiv.appendChild(noResults);
    }
  }
  catch (error) {
    console.log(error);
    averageDiv.appendChild(document.createTextNode('No recent test found'));
  }

  try {
    var averageResponse = await fetch('/api/FitnessDataEntryApi/Test/Results/Average/All/' + testId);
    var averageData = await averageResponse.json();
    console.log(JSON.stringify(averageData));
    if (averageResponse.ok) {
      var average = averageData;
      var averageText = document.createElement('p');
      averageText.appendChild(document.createTextNode('The overall average for this test is: ' + ((average * 100)+100).toFixed(2) + '% of total body weight'));
      averageDiv.appendChild(averageText);
      //your score is x% higher/lower than the average
      var percentDifference = ((recentResult / recentWeight) / average * 100 - 100).toFixed(0);
      var percentDifferenceText = document.createElement('p');
      if (percentDifference > 0) {
        percentDifferenceText.appendChild(document.createTextNode('Your score is ' + percentDifference + '% higher than the average'));
      }
      else if (percentDifference < 0) {
        percentDifferenceText.appendChild(document.createTextNode('Your score is ' + Math.abs(percentDifference) + '% lower than the average'));
      }
      else if (percentDifference == 0) {
        percentDifferenceText.appendChild(document.createTextNode('Your score is exactly the same as the average'));
      }
      else {
        percentDifferenceText.appendChild(document.createTextNode('No average found'));
      }
      averageDiv.appendChild(percentDifferenceText);

    }
    else {
      var noResults = document.createElement('p');
      noResults.appendChild(document.createTextNode('No Results Found'));
      averageDiv.appendChild(noResults);
    }
  }
  catch (error) {
    console.log('No average found');
    averageDiv.appendChild(document.createTextNode('No average found'));
  }
}

function createClimberTestTable(data, tableDiv) {
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
    date.appendChild(document.createTextNode(new Date(data[i].entryDate).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' })));
    bodyWeight.appendChild(document.createTextNode(data[i].bodyWeight));
    addedWeight.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(bodyWeight);
    row.appendChild(addedWeight);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  // Add table to page
  tableDiv.appendChild(table);
}

function createButtonToRecordPage(tableDiv) {
  //let the user know they have no current results
  var noResults = document.createElement('p');
  noResults.appendChild(document.createTextNode('No Test Results Found'));
  var noResultsDiv = tableDiv;
  noResultsDiv.appendChild(noResults);
  //create button to record page
  var button = document.createElement('button');
  button.className = 'btn btn-primary';
  button.appendChild(document.createTextNode('Record Test'));
  button.addEventListener('click', function () {
    window.location.href = '/Record/Record';
  });
  var buttonDiv = tableDiv;
  buttonDiv.appendChild(button);

  //apply box2 class to button
  button.classList.add('box2');
}

$('#testTabs a').on('click', function (e) {
  e.preventDefault();
  $(this).tab('show');
});
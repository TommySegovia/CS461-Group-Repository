document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Report.js loaded");

  const testData = [
    { index: 0, id: 'hang-test-table', averageId: 'hang-test-average', resultsId: 'hang-test-results'},
    { index: 1, id: 'pull-test-table', averageId: 'pull-test-average', resultsId: 'pull-test-results'},
    { index: 2, id: 'hammerCurl-test-table', averageId: 'hammerCurl-test-average', resultsId: 'hammerCurl-test-results'},
    { index: 3, id: 'hipFlexibility-test-table', averageId: 'hipFlexibility-test-average', resultsId: 'hipFlexibility-test-results'},
    { index: 4, id: 'hamstringFlexibility-test-table', averageId: 'hamstringFlexibility-test-average', resultsId: 'hamstringFlexibility-test-results'},
    { index: 5, id: 'repeater-test-table', averageId: 'repeater-test-average', resultsId: 'repeater-test-results'},
    { index: 6, id: 'smallestEdge-test-table', averageId: 'smallestEdge-test-average', resultsId: 'smallestEdge-test-results'},
    { index: 7, id: 'campusBoard-test-table', averageId: 'campusBoard-test-average', resultsId: 'campusBoard-test-results'}
  ];

  testData.forEach(test => {
    var tableDiv = document.getElementById(test.id);
    var averageDiv = document.getElementById(test.averageId);
    var resultsDiv = document.getElementById(test.resultsId);
    getTestRecords(test.index, tableDiv, resultsDiv);
    getTestAverage(test.index, averageDiv);
  });
}


async function getTestRecords(testId, tableDiv, resultsDiv) {
  var response = await fetch('/api/FitnessDataEntryApi/Test/Results/' + testId);
  var data = await response.json();
  console.log(JSON.stringify(data));
  if (response.ok && data.length > 0) {
    if (testId === 3 || testId === 4) { //if the test is flexibility, generate a table for flexibility tests
      createClimberFlexibilityTestTable(data, tableDiv);
    }
    else if (testId === 5) { //if the test is a repeater test, generate a table for repeater tests
      createClimberRepeaterTestTable(data, tableDiv);
    }
    else if (testId === 6) { //if the test is a smallest edge test, generate a table for smallest edge tests
      createClimberSmallestEdgeTestTable(data, tableDiv);
    }
    else if (testId === 7) { //if the test is a campus board test, generate a table for campus board tests
      createClimberCampusBoardTestTable(data, tableDiv);
    }
    else { //if the test is a strength test, generate a table for strength tests
      createClimberStrengthTestTable(data, tableDiv);
    }

    if (data.length > 1) {
      addGraphToResults(testId);
    }
  }
  else {
    createButtonToRecordPage(resultsDiv);
  }
}

// adds graph created by FitnessDataEntryService to the analytics history page
async function addGraphToResults(testId) {
  // add each individual graph html tag
  var graphHangDiv = document.getElementById('hang-test-graph');
  var graphPullDiv = document.getElementById('pull-test-graph');
  var graphHammerCurlDiv = document.getElementById('hammerCurl-test-graph');
  var graphHipFlexibilityDiv = document.getElementById('hipFlexibility-test-graph');
  var graphHamstringFlexibilityDiv = document.getElementById('hamstringFlexibility-test-graph');
  var graphRepeaterDiv = document.getElementById('repeater-test-graph');
  var graphSmallestEdgeDiv = document.getElementById('smallestEdge-test-graph');

  // add graph images from wwwroot/images, but first check which test it is
  switch (testId) {
    case 0:
      graphHangDiv.innerHTML = '<img src="/images/Test_0_ResultsOverTime.png" alt="Hang Test Graph" />';
      break;
    case 1:
      graphPullDiv.innerHTML = '<img src="/images/Test_1_ResultsOverTime.png" alt="Pull Up Test Graph" />';
      break;
    case 2:
      graphHammerCurlDiv.innerHTML = '<img src="/images/Test_2_ResultsOverTime.png" alt="Hammer Curl Test Graph" />';
      break;
    case 3:
      graphHipFlexibilityDiv.innerHTML = '<img src="/images/Test_3_ResultsOverTime.png" alt="Hip Flexibility Test Graph" />';
      break;
    case 4:
      graphHamstringFlexibilityDiv.innerHTML = '<img src="/images/Test_4_ResultsOverTime.png" alt="Hamstring Flexibility Test Graph" />';
      break;
    case 5:
      graphRepeaterDiv.innerHTML = '<img src="/images/Test_5_ResultsOverTime.png" alt="Repeater Test Graph" />';
      break;
    case 6:
      graphSmallestEdgeDiv.innerHTML = '<img src="/images/Test_6_ResultsOverTime.png" alt="Smallest Edge Test Graph" />';
      break;
    case 7:
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

      //flexibility test
      if (testId === 3 || testId === 4) {
        var resultText = 'Recent Test: ' + recentResult + ' inches';

        var resultNode = document.createTextNode(resultText);
        var br1 = document.createElement('br');

        recentText.appendChild(resultNode);
        recentText.appendChild(br1);

        averageDiv.appendChild(recentText);
      }
       //repeater test
      else if (testId === 5){
        var resultText = 'Recent Test: ' + recentResult + ' seconds';

        var resultNode = document.createTextNode(resultText);
        var br1 = document.createElement('br');

        recentText.appendChild(resultNode);
        recentText.appendChild(br1);

        averageDiv.appendChild(recentText);
      }
      //smallest edge test
      else if (testId === 6){
        var resultText = 'Recent Test: ' + recentResult + ' mm';

        var resultNode = document.createTextNode(resultText);
        var br1 = document.createElement('br');

        recentText.appendChild(resultNode);
        recentText.appendChild(br1);

        averageDiv.appendChild(recentText);
      }
      //campus board test
      else if (testId === 7){
        var resultText = 'Recent Test: ' + (recentResult.toString().split("").join("-"));

        var resultNode = document.createTextNode(resultText);
        var br1 = document.createElement('br');

        recentText.appendChild(resultNode);
        recentText.appendChild(br1);

        averageDiv.appendChild(recentText);
      }
      else {
        var resultText = 'Recent Test: ' + recentResult + ' lbs';
        var weightText = 'Bodyweight: ' + recentWeight + ' lbs';
        var ratioText = (((recentResult / recentWeight).toFixed(2) * 100) + 100) + '% of total body weight';

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
    if (testId === 0 || testId === 1 || testId === 2) { //if the test is strength, get the average of all strength tests
      var averageResponse = await fetch('/api/FitnessDataEntryApi/Test/Results/Average/All/PercentageOfBodyweight/' + testId);
    }
    else if (testId >= 3 && testId <= 6) {
      var averageResponse = await fetch('/api/FitnessDataEntryApi/Test/Results/Average/All/' + testId);
    }
    else if (testId === 7) { //if the test is campus board, get the average of all campus board tests
      var averageResponse = await fetch('/api/FitnessDataEntryApi/Test/Results/MostCommon/All/CampusBoard/' + testId);
    }

    var averageData = await averageResponse.json();
    console.log(JSON.stringify(averageData));
    if (averageResponse.ok) {
      var average = averageData;
      var averageText = document.createElement('p');
      if (testId === 0 || testId === 1 || testId === 2) {
        averageText.appendChild(document.createTextNode('The overall average for this test is: ' + ((average * 100) + 100).toFixed(2) + '% of total body weight'));
      }
      else if (testId === 3 || testId === 4) {
        averageText.appendChild(document.createTextNode('The overall average for this test is: ' + average.toFixed(2) + ' inches'));
      }
      else if (testId === 5) {
        averageText.appendChild(document.createTextNode('The overall average for this test is: ' + average.toFixed(2) + ' seconds'));
      }
      else if (testId === 6) {
        averageText.appendChild(document.createTextNode('The overall average for this test is: ' + average.toFixed(2) + ' mm'));
      }
      else if (testId === 7) {
        averageText.appendChild(document.createTextNode('The overall most common result for this test is: ' + (average.toString().split("").join("-"))));
      }

      averageDiv.appendChild(averageText);
      //your score is x% higher/lower than the average
      if (testId === 0 || testId === 1 || testId ===2) { //if the test is not a flexibility test, calculate the percent difference based on the weight
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
      else if (testId === 3 || testId === 4) { //if the test is a flexibility test, calculate the percent difference based on the flexibility
        var percentDifference = ((recentResult / average) * 100 - 100).toFixed(0);
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
      else if (testId === 5) { //if the test is a repeater test, calculate the percent difference based on the repeater time
        var percentDifference = ((recentResult / average) * 100 - 100).toFixed(0);
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
      else if (testId === 6) { //if the test is a smallest edge test, calculate the percent difference based on the smallest edge
        var percentDifference = ((recentResult / average) * 100 - 100).toFixed(0);
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
      else if (testId === 7) { //if the test is a campus board test, calculate the percent difference based on the campus board
      }
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

function createClimberStrengthTestTable(data, tableDiv) {
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

function createClimberFlexibilityTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement('table');
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = 'table table-striped table-bordered';
  // Create table header
  var tableHead = document.createElement('thead');
  var headRow = document.createElement('tr');
  var headers = ['Date', 'Distance (inches)'];
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
    var result = document.createElement('td');
    date.appendChild(document.createTextNode(new Date(data[i].entryDate).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' })));
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberRepeaterTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement('table');
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = 'table table-striped table-bordered';
  // Create table header
  var tableHead = document.createElement('thead');
  var headRow = document.createElement('tr');
  var headers = ['Date', 'Time (s)'];
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
    var result = document.createElement('td');
    date.appendChild(document.createTextNode(new Date(data[i].entryDate).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' })));
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberSmallestEdgeTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement('table');
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = 'table table-striped table-bordered';
  // Create table header
  var tableHead = document.createElement('thead');
  var headRow = document.createElement('tr');
  var headers = ['Date', 'Edge Size (mm)'];
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
    var result = document.createElement('td');
    date.appendChild(document.createTextNode(new Date(data[i].entryDate).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' })));
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberCampusBoardTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement('table');
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = 'table table-striped table-bordered';
  // Create table header
  var tableHead = document.createElement('thead');
  var headRow = document.createElement('tr');
  var headers = ['Date', 'Result'];
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
    var result = document.createElement('td');
    date.appendChild(document.createTextNode(new Date(data[i].entryDate).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' })));
    result.appendChild(document.createTextNode(data[i].result.toString().split("").join("-")));
    row.appendChild(date);
    row.appendChild(result);
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
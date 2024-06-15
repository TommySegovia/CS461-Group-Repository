document.addEventListener("DOMContentLoaded", initializePage);

const testData = [
  {
    index: 0,
    id: "hang-test-table",
    recentId: "hang-test-recent",
    averageId: "hang-test-average",
    resultsId: "hang-test-results",
  },
  {
    index: 1,
    id: "pull-test-table",
    recentId: "pull-test-recent",
    averageId: "pull-test-average",
    resultsId: "pull-test-results",
  },
  {
    index: 2,
    id: "hammerCurl-test-table",
    recentId: "hammerCurl-test-recent",
    averageId: "hammerCurl-test-average",
    resultsId: "hammerCurl-test-results",
  },
  {
    index: 3,
    id: "hipFlexibility-test-table",
    recentId: "hipFlexibility-test-recent",
    averageId: "hipFlexibility-test-average",
    resultsId: "hipFlexibility-test-results",
  },
  {
    index: 4,
    id: "hamstringFlexibility-test-table",
    recentId: "hamstringFlexibility-test-recent",
    averageId: "hamstringFlexibility-test-average",
    resultsId: "hamstringFlexibility-test-results",
  },
  {
    index: 5,
    id: "repeater-test-table",
    recentId: "repeater-test-recent",
    averageId: "repeater-test-average",
    resultsId: "repeater-test-results",
  },
  {
    index: 6,
    id: "smallestEdge-test-table",
    recentId: "smallestEdge-test-recent",
    averageId: "smallestEdge-test-average",
    resultsId: "smallestEdge-test-results",
  },
  {
    index: 7,
    id: "campusBoard-test-table",
    recentId: "campusBoard-test-recent",
    averageId: "campusBoard-test-average",
    resultsId: "campusBoard-test-results",
  },
];

function initializePage() {
  console.log("Report.js loaded");

  const testDetails = document.getElementById("test-details-div");
  if (testDetails) {
    console.log(testDetails.dataset.test + "-test-table");
    var testName = testDetails.dataset.test + "-test-table";
    const test = testData.find(test => test.id === testName);
    var tableDiv = document.getElementById(test.id); 
    var resultsDiv = document.getElementById(test.resultsId);
    getTestRecords(test.index, tableDiv, resultsDiv);
  }

  //filter form
    var filterForm = document.getElementById("filter-form");
    if ( filterForm) {
      filterForm.addEventListener("submit", function (event) {
        event.preventDefault();
        //console.log("Filter button clicked");
        var minAge = document.getElementById("minAge").value;
        var maxAge = document.getElementById("maxAge").value;
        var gender = document.getElementById("gender").value;
        var climbingExperience =
          document.getElementById("climbingExperience").value;
        var minimumClimbingGrade = document.getElementById(
          "minimumClimbingGrade"
        ).value;
        var maximumClimbingGrade = document.getElementById(
          "maximumClimbingGrade"
        ).value;
        //create a object to store the filter data
        var filterData = {
          minAge: minAge,
          maxAge: maxAge,
          gender: gender,
          climbingExperience: climbingExperience,
          minimumClimbingGrade: minimumClimbingGrade,
          maximumClimbingGrade: maximumClimbingGrade,
        };
        try {
          updateClimberData(testData, filterData);
        } catch (error) {
          console.log(error);
        }
      });
    

    testData.forEach((test) => {
      var tableDiv = document.getElementById(test.id);
      var averageDiv = document.getElementById(test.averageId);
      var recentDiv = document.getElementById(test.recentId);
      var resultsDiv = document.getElementById(test.resultsId);
      getTestRecords(test.index, tableDiv, resultsDiv);
      getTestAverage(test.index, averageDiv, recentDiv, {
        minAge: 0,
        maxAge: 100,
        gender: "All",
        climbingExperience: "All",
        minimumClimbingGrade: 0,
        maximumClimbingGrade: 100,
        });
      });
  }
}

//update data
function updateClimberData(testData, filterData) {
  try {
    //console.log(filterData);
    //console.log(testData);
    testData.forEach((test) => {
      var tableDiv = document.getElementById(test.id);
      var averageDiv = document.getElementById(test.averageId);
      var recentDiv = document.getElementById(test.recentId);
      var resultsDiv = document.getElementById(test.resultsId);
      //getTestRecords(test.index, tableDiv, resultsDiv);
      getTestAverage(test.index, averageDiv, recentDiv, filterData);
    });
  } catch (error) {
    console.log(error);
  }
}

async function getTestRecords(testId, tableDiv, resultsDiv) {
  var response = await fetch("/api/FitnessDataEntryApi/Test/Results/" + testId);
  var data = await response.json();
  if (response.ok && data.length > 0) {
    if (testId === 3 || testId === 4) {
      //if the test is flexibility, generate a table for flexibility tests
      createClimberFlexibilityTestTable(data, tableDiv);
    } else if (testId === 5) {
      //if the test is a repeater test, generate a table for repeater tests
      createClimberRepeaterTestTable(data, tableDiv);
    } else if (testId === 6) {
      //if the test is a smallest edge test, generate a table for smallest edge tests
      createClimberSmallestEdgeTestTable(data, tableDiv);
    } else if (testId === 7) {
      //if the test is a campus board test, generate a table for campus board tests
      createClimberCampusBoardTestTable(data, tableDiv);
    } else {
      //if the test is a strength test, generate a table for strength tests
      createClimberStrengthTestTable(data, tableDiv);
    }

    if (data.length > 1) {
      addGraphToResults(testId);
    }
    else {
      var graphDiv = document.getElementById(testId === 0 ? "hang-test-graph" : testId === 1 ? "pull-test-graph" : testId === 2 ? "hammerCurl-test-graph" : testId === 3 ? "hipFlexibility-test-graph" : testId === 4 ? "hamstringFlexibility-test-graph" : testId === 5 ? "repeater-test-graph" : testId === 6 ? "smallestEdge-test-graph" : "");
      var missingDataMessage = document.createElement("p");
      missingDataMessage.appendChild(document.createTextNode("Need more data to generate a graph"));
      missingDataMessage.classList.add("missing-data-message");
      if (graphDiv)
        {
          graphDiv.appendChild(missingDataMessage);
        }
      createButtonToRecordPage(resultsDiv);
    }

  }
  else {
    var graphDiv = document.getElementById(testId === 0 ? "hang-test-graph" : testId === 1 ? "pull-test-graph" : testId === 2 ? "hammerCurl-test-graph" : testId === 3 ? "hipFlexibility-test-graph" : testId === 4 ? "hamstringFlexibility-test-graph" : testId === 5 ? "repeater-test-graph" : testId === 6 ? "smallestEdge-test-graph" : "");
    var missingDataMessage = document.createElement("p");
    missingDataMessage.appendChild(document.createTextNode("Need more data to generate a graph"));
    missingDataMessage.classList.add("missing-data-message");
    graphDiv.appendChild(missingDataMessage);

    var noResults = document.createElement("p");
    noResults.appendChild(document.createTextNode("No Test Results Found"));
    var noResultsDiv = tableDiv;
    noResultsDiv.appendChild(noResults);

    createButtonToRecordPage(resultsDiv);

  }
}

// adds graph created by FitnessDataEntryService to the analytics history page
async function addGraphToResults(testId) {
  // add each individual graph html tag
  var graphHangDiv = document.getElementById("hang-test-graph");
  var graphPullDiv = document.getElementById("pull-test-graph");
  var graphHammerCurlDiv = document.getElementById("hammerCurl-test-graph");
  var graphHipFlexibilityDiv = document.getElementById("hipFlexibility-test-graph");
  var graphHamstringFlexibilityDiv = document.getElementById("hamstringFlexibility-test-graph");
  var graphRepeaterDiv = document.getElementById("repeater-test-graph");
  var graphSmallestEdgeDiv = document.getElementById("smallestEdge-test-graph");

  if (graphHangDiv) { graphHangDiv.classList.add("history-graph-div"); }
  if (graphPullDiv) { graphPullDiv.classList.add("history-graph-div"); }
  if (graphHammerCurlDiv) { graphHammerCurlDiv.classList.add("history-graph-div"); }
  if (graphHipFlexibilityDiv) { graphHipFlexibilityDiv.classList.add("history-graph-div"); }
  if (graphHamstringFlexibilityDiv) { graphHamstringFlexibilityDiv.classList.add("history-graph-div"); }
  if (graphRepeaterDiv) { graphRepeaterDiv.classList.add("history-graph-div"); }
  if (graphSmallestEdgeDiv) { graphSmallestEdgeDiv.classList.add("history-graph-div"); }

  // add graph images from wwwroot/images, but first check which test it is
  switch (testId) {
    case 0:
      graphHangDiv.innerHTML =
        '<img src="/images/Test_0_ResultsOverTime.png" alt="Hang Test Graph" />';
      break;
    case 1:
      graphPullDiv.innerHTML =
        '<img src="/images/Test_1_ResultsOverTime.png" alt="Pull Up Test Graph" />';
      break;
    case 2:
      graphHammerCurlDiv.innerHTML =
        '<img src="/images/Test_2_ResultsOverTime.png" alt="Hammer Curl Test Graph" />';
      break;
    case 3:
      graphHipFlexibilityDiv.innerHTML =
        '<img src="/images/Test_3_ResultsOverTime.png" alt="Hip Flexibility Test Graph" />';
      break;
    case 4:
      graphHamstringFlexibilityDiv.innerHTML =
        '<img src="/images/Test_4_ResultsOverTime.png" alt="Hamstring Flexibility Test Graph" />';
      break;
    case 5:
      graphRepeaterDiv.innerHTML =
        '<img src="/images/Test_5_ResultsOverTime.png" alt="Repeater Test Graph" />';
      break;
    case 6:
      graphSmallestEdgeDiv.innerHTML =
        '<img src="/images/Test_6_ResultsOverTime.png" alt="Smallest Edge Test Graph" />';
      break;
    case 7:

      break;
  }
}

async function getTestAverage(testId, averageDiv, recentDiv, filterData) {
  //get the most recent user test result

  try {
    var recentResponse = await fetch(
      "/api/FitnessDataEntryApi/Test/Results/MostRecent/" + testId
    );
    var recentData = await recentResponse.json();

    //console.log(JSON.stringify(recentData));

    //clear the average div
    recentDiv.innerHTML = "";
    averageDiv.innerHTML = "";

    if (recentResponse.ok) {
      var recentResult = recentData.result;
      var recentWeight = recentData.bodyWeight;
      var recentText = document.createElement("p");

      //strength test
      if (testId === 0 || testId === 1) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult + '</span>' + " lbs";
        var weightText = "Bodyweight: " + '<span class="orange-text">' + recentWeight + '</span>' + " lbs";
        var ratioText = '<span class="orange-text">' + ((recentResult / recentWeight) * 100 + 100).toFixed(2) + '%</span>' + " of total body weight";

        recentText.innerHTML = resultText + "<br>" + weightText + "<br>" + ratioText;

        recentDiv.appendChild(recentText);
      }
      //hammercurl test
      else if (testId === 2) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult + '</span>' + " lbs";
        var weightText = "Bodyweight: " + '<span class="orange-text">' + recentWeight + '</span>' + " lbs";
        var ratioText = '<span class="orange-text">' + ((recentResult / recentWeight) * 100).toFixed(2) + '%</span>' + " of total body weight";

        recentText.innerHTML = resultText + "<br>" + weightText + "<br>" + ratioText;

        recentDiv.appendChild(recentText);
      }
      //flexibility test
      else if (testId === 3 || testId === 4) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult + '</span>' + " inches";

        recentText.innerHTML = resultText;
        recentDiv.appendChild(recentText);
      }
      //repeater test
      else if (testId === 5) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult + '</span>' + " seconds";

        recentText.innerHTML = resultText;
        recentDiv.appendChild(recentText);
      }
      //smallest edge test
      else if (testId === 6) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult + '</span>' + " mm";

        recentText.innerHTML = resultText;
        recentDiv.appendChild(recentText);
      }
      //campus board test
      else if (testId === 7) {
        var resultText = "Recent Test: " + '<span class="orange-text">' + recentResult.toString().split("").join("-") + '</span>';
        recentText.innerHTML = resultText;
        var br1 = document.createElement("br");
        recentText.appendChild(br1);
        recentDiv.appendChild(recentText);
      } else {
        var noResults = document.createElement("p");
        noResults.appendChild(document.createTextNode("No Recent Test Found"));
        recentDiv.appendChild(noResults);
      }
    }
  } catch (error) {
    console.log(error);
    recentDiv.appendChild(document.createTextNode("No recent test found"));
  }

  try {
    if (testId === 0 || testId === 1 || testId === 2) {
      var endpointString = "Average/All/PercentageOfBodyweight/";
      //if the test is strength, get the average of all strength tests
    } else if (testId >= 3 && testId <= 6) {
      var endpointString = "Average/All/";
    } else if (testId === 7) {
      var endpointString = "MostCommon/All/CampusBoard/";
      //if the test is campus board, get the average of all campus board tests
    }
    var averageResponse = await fetch("/api/FitnessDataEntryApi/Test/Results/" + endpointString + testId + "/" + filterData.minAge + "/" + filterData.maxAge + "/" + filterData.gender +
      "/" + filterData.climbingExperience + "/" + filterData.minimumClimbingGrade + "/" + filterData.maximumClimbingGrade);

    var averageData = await averageResponse.json();
    //console.log("average: " + JSON.stringify(averageData));
    function createPercentDifferenceText(percentDifference) {
      var percentDifferenceText = document.createElement("p");
      if (percentDifference < 0) {
        percentDifferenceText.innerHTML = "Your score is <span id='higher-score'>" + Math.abs(percentDifference) + "%</span> higher than the average";
      } else if (percentDifference > 0) {
        percentDifferenceText.innerHTML = "Your score is <span id='lower-score'>" + Math.abs(percentDifference) + "%</span> lower than the average";
      } else if (percentDifference == 0) {
        var percentDifferenceText = document.createElement("p");
        percentDifferenceText.innerHTML = "Your score is exactly the <span id='same-score'>same</span> as the average";
      } else {
        var noAverageFoundText = document.createElement("p");
        noAverageFoundText.appendChild(document.createTextNode("No average found"));
        percentDifferenceText.appendChild(noAverageFoundText);
      }
      return percentDifferenceText;
    }

    if (averageResponse.ok) {
      var average = averageData;
      var averageText = document.createElement("p");
      var testDescriptions = {
        0: "<span class='orange-text'>%</span> of total body weight",
        1: "<span class='orange-text'>%</span> of total body weight",
        2: "<span class='orange-text'>%</span> of total body weight",
        3: " inches",
        4: " inches",
        5: " seconds",
        6: " mm",
        7: " " + average.toString().split("").join("-")
      };
      var testDescription = testDescriptions[testId];
      var averageValue = (testId === 0 || testId === 1 || testId === 2) ? (average * 100 + 100).toFixed(2) : average.toFixed(2);
      if (testId === 7) {
        averageText.innerHTML = "The overall average for this test is: " + '<span class="orange-text">' + testDescription + '</span>';
      }
      else if (testId === 2) {
        averageText.innerHTML = "The overall average for this test is: " + '<span class="orange-text">' + (averageValue - 100).toFixed(2) + '</span>' + testDescription;
      }
      else {
        averageText.innerHTML = "The overall average for this test is: " + '<span class="orange-text">' + averageValue + '</span>' + testDescription;
      }

      averageDiv.appendChild(averageText);

      if (testId === 0 || testId === 1 || testId === 2) {
        var percentDifference = ((average -(recentResult / (recentWeight))) * 100).toFixed(1);
        var percentDifferenceText = createPercentDifferenceText(percentDifference);
        averageDiv.appendChild(percentDifferenceText);
      }
      else if (testId === 3 || testId === 5) {
        var percentDifference = (100-((recentResult / average) * 100)).toFixed(0);
        var percentDifferenceText = createPercentDifferenceText(percentDifference);
        averageDiv.appendChild(percentDifferenceText);
      }
      else if (testId === 4) {
        recentResult += Math.abs(average) + 10;
        average = 10;
        var percentDifference = (100-((recentResult / average) * 100)).toFixed(0);
        var percentDifferenceText = createPercentDifferenceText(percentDifference);
        averageDiv.appendChild(percentDifferenceText);
      }
      else if (testId !== 7) {
        var percentDifference = -(100-((recentResult / average) * 100)).toFixed(0);
        var percentDifferenceText = createPercentDifferenceText(percentDifference);
        averageDiv.appendChild(percentDifferenceText);
      }
    } else {
      var noResults = document.createElement("p");
      noResults.appendChild(document.createTextNode("No Results Found"));
      averageDiv.appendChild(noResults);
    }
  } catch (error) {
    console.log(error);
    var noAverageFoundText = document.createElement("p");
    noAverageFoundText.appendChild(document.createTextNode("No average found"));
    averageDiv.appendChild(noAverageFoundText);
  }
}

function createClimberStrengthTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement("table");
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = "table table-striped table-bordered";
  // Create table header
  var tableHead = document.createElement("thead");
  var headRow = document.createElement("tr");
  var headers = ["Date", "Body Weight", "Added Weight", ""];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement("th");
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement("tbody");
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement("tr");
    var date = document.createElement("td");
    var bodyWeight = document.createElement("td");
    var addedWeight = document.createElement("td");

    var deleteCell = document.createElement("td");
    var deleteButton = document.createElement("button");
    deleteButton.style.backgroundColor = "red";
    deleteButton.classList.add("deleteButton");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = deleteTest(data[i].id, data[i].testId);
    deleteCell.appendChild(deleteButton);

    date.appendChild(
      document.createTextNode(
        new Date(data[i].entryDate).toLocaleString([], {
          year: "numeric",
          month: "2-digit",
          day: "2-digit",
          hour: "2-digit",
          minute: "2-digit",
        })
      )
    );
    bodyWeight.appendChild(document.createTextNode(data[i].bodyWeight));
    addedWeight.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(bodyWeight);
    row.appendChild(addedWeight);
    row.appendChild(deleteCell);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  table.classList.add("history-table-div");
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberFlexibilityTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement("table");
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = "table table-striped table-bordered";
  // Create table header
  var tableHead = document.createElement("thead");
  var headRow = document.createElement("tr");
  var headers = ["Date", "Distance (inches)"];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement("th");
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement("tbody");
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement("tr");
    var date = document.createElement("td");
    var result = document.createElement("td");

    var deleteCell = document.createElement("td");
    var deleteButton = document.createElement("button");
    deleteButton.style.backgroundColor = "red";
    deleteButton.classList.add("deleteButton");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = deleteTest(data[i].id, data[i].testId);
    deleteCell.appendChild(deleteButton);

    date.appendChild(
      document.createTextNode(
        new Date(data[i].entryDate).toLocaleString([], {
          year: "numeric",
          month: "2-digit",
          day: "2-digit",
          hour: "2-digit",
          minute: "2-digit",
        })
      )
    );
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    row.appendChild(deleteCell);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  table.classList.add("history-table-div");
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberRepeaterTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement("table");
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = "table table-striped table-bordered";
  // Create table header
  var tableHead = document.createElement("thead");
  var headRow = document.createElement("tr");
  var headers = ["Date", "Time (s)"];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement("th");
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement("tbody");
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement("tr");
    var date = document.createElement("td");
    var result = document.createElement("td");

    var deleteCell = document.createElement("td");
    var deleteButton = document.createElement("button");
    deleteButton.style.backgroundColor = "red";
    deleteButton.classList.add("deleteButton");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = deleteTest(data[i].id, data[i].testId);
    deleteCell.appendChild(deleteButton);

    date.appendChild(
      document.createTextNode(
        new Date(data[i].entryDate).toLocaleString([], {
          year: "numeric",
          month: "2-digit",
          day: "2-digit",
          hour: "2-digit",
          minute: "2-digit",
        })
      )
    );
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    row.appendChild(deleteCell);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  table.classList.add("history-table-div");
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberSmallestEdgeTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement("table");
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = "table table-striped table-bordered";
  // Create table header
  var tableHead = document.createElement("thead");
  var headRow = document.createElement("tr");
  var headers = ["Date", "Edge Size (mm)"];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement("th");
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement("tbody");
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement("tr");
    var date = document.createElement("td");
    var result = document.createElement("td");

    var deleteCell = document.createElement("td");
    var deleteButton = document.createElement("button");
    deleteButton.style.backgroundColor = "red";
    deleteButton.classList.add("deleteButton");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = deleteTest(data[i].id, data[i].testId);
    deleteCell.appendChild(deleteButton);

    date.appendChild(
      document.createTextNode(
        new Date(data[i].entryDate).toLocaleString([], {
          year: "numeric",
          month: "2-digit",
          day: "2-digit",
          hour: "2-digit",
          minute: "2-digit",
        })
      )
    );
    result.appendChild(document.createTextNode(data[i].result));
    row.appendChild(date);
    row.appendChild(result);
    row.appendChild(deleteCell);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  table.classList.add("history-table-div");
  // Add table to page
  tableDiv.appendChild(table);
}

function createClimberCampusBoardTestTable(data, tableDiv) {
  // Create table
  var table = document.createElement("table");
  //make background of table white
  table.style.backgroundColor = "white";
  table.className = "table table-striped table-bordered";
  // Create table header
  var tableHead = document.createElement("thead");
  var headRow = document.createElement("tr");
  var headers = ["Date", "Result"];
  for (var i = 0; i < headers.length; i++) {
    var th = document.createElement("th");
    th.appendChild(document.createTextNode(headers[i]));
    headRow.appendChild(th);
  }
  tableHead.appendChild(headRow);
  table.appendChild(tableHead);
  // Create table body
  var tableBody = document.createElement("tbody");
  for (var i = 0; i < data.length; i++) {
    var row = document.createElement("tr");
    var date = document.createElement("td");
    var result = document.createElement("td");

    var deleteCell = document.createElement("td");
    var deleteButton = document.createElement("button");
    deleteButton.style.backgroundColor = "red";
    deleteButton.classList.add("deleteButton");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = deleteTest(data[i].id, data[i].testId);
    deleteCell.appendChild(deleteButton);

    date.appendChild(
      document.createTextNode(
        new Date(data[i].entryDate).toLocaleString([], {
          year: "numeric",
          month: "2-digit",
          day: "2-digit",
          hour: "2-digit",
          minute: "2-digit",
        })
      )
    );
    result.appendChild(
      document.createTextNode(data[i].result.toString().split("").join("-"))
    );
    row.appendChild(date);
    row.appendChild(result);
    row.appendChild(deleteCell);
    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  table.classList.add("history-table-div");
  // Add table to page
  tableDiv.appendChild(table);
}

function deleteTest(id, testId) {
  //console.log("ID: " + id);
  //console.log("TESTID: " + testId);
  return async function () {
    var response = await fetch(
      "/api/FitnessDataEntryApi/Test/Results/Delete/" + id + "/" + testId,
      {
        method: "DELETE",
      }
    );
    if (response.ok) {
      console.log("Test deleted");
      location.reload();
    } else {
      console.log("Test not deleted");
    }
  };
}

function createButtonToRecordPage(tableDiv) {
  //let the user know they have no current results
  //create button to record page
  var button = document.createElement("button");
  button.className = "btn btn-primary";
  button.appendChild(document.createTextNode("Record Test"));
  button.addEventListener("click", function () {
    window.location.href = "/Record/Record";
  });
  var buttonDiv = tableDiv;
  buttonDiv.appendChild(button);

  //apply box2 class to button
  button.classList.add("record-button");
}

$(document).ready(function () {
  // Show 'test-history' and hide others when 'history-tab' is clicked
  $("#history-tab").click(function (event) {
    event.preventDefault();
    $("#test-history").show();
    $("#test-analysis, #test-recommendations").hide();

    // Remove the active state from all tabs
    $(".nav-buttons").removeClass("active-tab");
    // Add the active state to the clicked tab
    $("#history-button").addClass("active-tab");
  });

  // Show 'test-analysis' and hide others when 'analysis-tab' is clicked
  $("#analysis-tab").click(function (event) {
    event.preventDefault();
    $("#test-analysis").show();
    $("#test-history, #test-recommendations").hide();

    // Remove the active state from all tabs
    $(".nav-buttons").removeClass("active-tab");
    // Add the active state to the clicked tab
    $("#analysis-button").addClass("active-tab");
  });

  // Show 'test-recommendations' and hide others when 'recommendations-tab' is clicked
  $("#recommendations-tab").click(function (event) {
    event.preventDefault();
    $("#test-recommendations").show();
    $("#test-history, #test-analysis").hide();

    // Remove the active state from all tabs
    $(".nav-buttons").removeClass("active-tab");
    // Add the active state to the clicked tab
    $("#recommendations-button").addClass("active-tab");
  });
});

$(document).ready(function () {
  // Show the modal when the 'filter-button' is clicked
  $("#filter-button").click(function () {
    $("#filterModal").modal('show');
  });

  // Hide the modal when the 'close-button' is clicked
  $("#close-button").click(function () {
    $("#filterModal").modal('hide');
  });

  // Hide the modal when the 'submit-button' is clicked
  $("#filterButtonSubmit").click(function () {
    $("#filterModal").modal('hide');
  });

});
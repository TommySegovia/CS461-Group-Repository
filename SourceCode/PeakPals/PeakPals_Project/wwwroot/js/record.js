document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Record.js loaded");

}

function handleFormSubmit(formId, testId, testType) {
  document.getElementById(formId).addEventListener("submit", function (event) {
    event.preventDefault();
    var result = document.getElementById(`${testType}-test-input`).value;
    //set bodyweight to 0 if the input is null
    if (document.getElementById(`${testType}-test-bodyweight-input`) === null) {
      var bodyWeight = 0;
    }
    else{
      var bodyWeight = document.getElementById(`${testType}-test-bodyweight-input`).value;
    }
    
    recordTest(testId, result, bodyWeight);
    document.getElementById(`${testType}-test-input`).value = "";
    if (bodyWeight !== 0){
      document.getElementById(`${testType}-test-bodyweight-input`).value = "";
    }
    
  });
}

handleFormSubmit("hang-test-form", 0, 'hang');
handleFormSubmit("pull-test-form", 1, 'pull');
handleFormSubmit("hammerCurl-test-form", 2, 'hammerCurl');
handleFormSubmit("hipFlexibility-test-form", 3, 'hipFlexibility');
handleFormSubmit("hamstringFlexibility-test-form", 4, 'hamstringFlexibility');


function recordTest(testId, result, bodyWeight) {

  if (result === "" || bodyWeight === "") {
    console.log("No input");
    return;
  } else {
    console.log("Test Result: " + result);
    console.log("Body Weight: " + bodyWeight);
    postTestRecord(testId, result, bodyWeight);
  }
}

async function postTestRecord(testId, testResult, bodyWeight) {
  var url = '/api/FitnessDataEntryApi/RecordTestResult';
  var data = {
    "id": 0,
    "testId": testId,
    "result": parseInt(testResult), // Convert to integer
    "bodyWeight": parseInt(bodyWeight), // Convert to integer
    "entryDate": new Date().toISOString()
  };
  var response = await fetch(url, {
    method: 'POST',
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });
  var result = await response.json();

  console.log(result);
  confirmationPopup();
}

function confirmationPopup() {
  var popup = document.getElementById("record-confirmation-popup");
  popup.style.display = "block";
  setTimeout(function () {
    popup.style.display = "none";
  }, 3000);
}


document.addEventListener('DOMContentLoaded', initializePage);

function initializePage() {
  console.log("Record.js loaded");

}

document.getElementById("hang-test-form").addEventListener("submit", function (event) {
  event.preventDefault();
  recordHangTest();
});

function recordHangTest() {
  var result = document.getElementById("hang-test-input").value;
  var bodyWeight = document.getElementById("hang-test-bodyweight-input").value;
  if (result === "" || bodyWeight === "") {
    console.log("No input");
    return;
  } else {
    console.log("Hang Test: " + result + " lbs");
    console.log("Body Weight: " + bodyWeight + " lbs");
    postHangTestRecord(result, bodyWeight);
  }
}

async function postHangTestRecord(hangTest, bodyWeight) {
  var url = "http://localhost:5044/api/FitnessDataEntryApi/RecordHangTestResult";
  var data = {
    "id": 0,
    "testId": 0,
    "result": parseInt(hangTest), // Convert to integer
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
  document.getElementById("hang-test-input").value = "";
  document.getElementById("hang-test-bodyweight-input").value = "";
  confirmationPopup();
}

function confirmationPopup() {
  var popup = document.getElementById("record-confirmation-popup");
  popup.style.display = "block";
  setTimeout(function () {
    popup.style.display = "none";
  }, 3000);
}


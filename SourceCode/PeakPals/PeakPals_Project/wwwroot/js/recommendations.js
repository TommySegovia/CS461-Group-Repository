document.addEventListener("DOMContentLoaded", initializePage);

async function initializePage() {
  console.log("Recommendations.js loaded");
  await GenerateRadarChart();
  await AddRadarChart();
}

async function GenerateRadarChart() {
  var url = "/api/FitnessDataEntryApi/Test/Results/All/RadarChart";
  var response = await fetch(url);
  var data = await response.json();
  console.log(data);
}

async function AddRadarChart() {
  var url = "/api/community/currentUserId";
  var response = await fetch(url);
  var userId = await response.json();

  var radarChartDiv = document.getElementById("radar-chart-div");
  radarChartDiv.innerHTML = `<img src="/images/User_${userId}_RadarChart.png" alt="User Stat Radar Chart" />`;
}

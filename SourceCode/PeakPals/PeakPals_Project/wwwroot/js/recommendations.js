document.addEventListener("DOMContentLoaded", initializePage);

async function initializePage() {
  console.log("Recommendations.js loaded");
  await GenerateRadarChart();
  await AddRadarChart();
  var stats = await GetStrongestStats();
  var tags = await ConvertStatsToTags(stats);
  var climbs = await GetRecommendedClimbs(tags);
  await PopulateRecommendedClimbs(climbs, stats);

  var refreshButton = document.getElementById('refresh-button');
  refreshButton.addEventListener('click', function() {
    PopulateRecommendedClimbs(climbs, stats);
  });
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
  radarChartDiv.innerHTML = `<img id="radar-chart" src="/images/User_${userId}_RadarChart.png" alt="User Stat Radar Chart" />`;
}

async function GetStrongestStats() {
  console.log("Getting Strongest Stats");
  var url = "/api/FitnessDataEntryApi/Test/Results/All/StrongestStats";
  var response = await fetch(url);
  var data = await response.json();
  console.log(data);
  return data;
}

async function ConvertStatsToTags(stats) {
  console.log("Converting Stats to Tags");
  var firstStatName = stats[0].split(":")[0];
  var secondStatName = stats[1].split(":")[0];

  var tags = [];
  tags = tags.concat(GetTagsFromStat(firstStatName));
  tags = tags.concat(GetTagsFromStat(secondStatName));

  tags = [...new Set(tags)];

  console.log(tags);

  return tags;
}

function GetTagsFromStat(stat) {
  switch (stat) {
    case "Finger Strength":
      return ["Crimpy", "Tension", "Classic", "Pinches"];
    case "Pull Strength":
      return ["Powerful", "Compression", "Tension"];
    case "Power":
      return ["Powerful", "Dyno", "Compression"];
    case "Endurance":
      return ["Pumpy", "Tension", "Slab", "Highball"];
    case "Flexibility":
      return ["Technical", "Slab", "Unique"];
    default:
      break;
  }
}

async function GetRecommendedClimbs(tags) {
  console.log("Recommending Climbs");
  var url = "/api/ClimbTagEntryApi/log/search/" + tags.join(",");
  var response = await fetch(url);
  var climbs = await response.json();

  console.log(climbs);

  return climbs;
}

async function PopulateRecommendedClimbs(climbs, stats) {
    var recommendedClimbsDiv = document.getElementById("recommended-climbs-div");
    var recommendedClimbTemplate = document.getElementById("recommended-climb-template").content;
  
    // Clear the previous recommended climbs
    recommendedClimbsDiv.innerHTML = '';
  
    //shuffle the climbs
    for (let i = climbs.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * i);
      const temp = climbs[i];
      climbs[i] = climbs[j];
      climbs[j] = temp;
    }
  
    var row;
    climbs.slice(0, 3).forEach(async (climb, index) => {
      // Create a new row for every 3 climbs
      if (index % 3 === 0) {
        row = document.createElement('div');
        row.className = 'row';
        recommendedClimbsDiv.appendChild(row);
      }
  
      // Clone the template
      var clone = document.importNode(recommendedClimbTemplate, true);
  
      // Populate the clone with the climb's data
      clone.querySelector('.recommended-climb-header-name').textContent = climb.climbName;
      clone.querySelector('.recommended-climb-header-grade').textContent = climb.suggestedGrade;

      var climbTags = await GetClimbTags(climb.id);
      var climbDescription = await GetClimbDescription(climbTags, stats);
      clone.querySelector('.recommended-climb-tag-div').textContent = climbTags.join(', ');
      clone.querySelector('.recommended-climb-description-div').textContent = climbDescription;
      clone.querySelector('.recommend-climb-button').addEventListener('click', function() {
        window.location.href = "/Locations/Climbs/" + climb.climbId;
      });
  
      // Create a column div and append the clone to it
      var col = document.createElement('div');
      col.className = 'col-4';
      col.appendChild(clone);
  
      // Append the column to the current row
      row.appendChild(col);
    });
  }

    async function GetClimbTags(climbId) {
        var url = "/api/ClimbTagEntryApi/log/view/" + climbId;
        var response = await fetch(url);
        var tags = await response.json();
        console.log(tags);
        return tags;
    }

    async function GetClimbDescription(climbTags, stats) {
        console.log("Getting Climb Description");
        var climbDescription = "This climb will likely require ";
        var attributes = [];
        for (var stat of stats) {
            console.log(stat);
            var statName = stat.split(":")[0].trim(); // Extract the stat name
            console.log(statName);
            switch (statName) {
                case ("Finger Strength"):
                    if (["Crimpy", "Tension", "Classic", "Pinches"].some(val => climbTags.includes(val))) {
                        
                        attributes.push("strong fingers");
                    }
                    break;
                case ("Pull Strength"):
                    if (["Powerful", "Compression", "Tension"].some(val => climbTags.includes(val))) {
                        attributes.push("strong pulling muscles");
                    }
                    break;
                case ("Power"):
                    if (["Powerful", "Dyno", "Compression"].some(val => climbTags.includes(val))) {
                        attributes.push("powerful moves");
                    }
                    break;
                case ("Endurance"):
                    if (["Pumpy", "Tension", "Slab", "Highball"].some(val => climbTags.includes(val))) {
                        attributes.push("good endurance");
                    }
                    break;
                case ("Flexibility"):
                    if (["Technical", "Slab", "Unique"].some(val => climbTags.includes(val))) {
                        attributes.push("flexibility");
                    }
                    break;
            
                default:
                    break;
            }
        }
        console.log(stats);
        console.log(attributes);
        climbDescription += attributes.join(" and ");
        climbDescription += attributes.length > 1 ? " which are some of your strengths." : " which is one of your strengths.";
        return (climbDescription);
    }

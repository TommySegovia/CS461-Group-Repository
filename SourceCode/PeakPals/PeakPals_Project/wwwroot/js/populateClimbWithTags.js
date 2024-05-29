document.addEventListener("DOMContentLoaded", initializePage);

async function initializePage() {
  var climbAttempts = await GetAllClimbAttemptsForCurrentClimb();
  var climbTags = await GetAllTagsForCurrentClimb(climbAttempts);
  climbTags = climbTags.flat();
  climbTags = [...new Set(climbTags)];
  console.log(climbTags);
  AddTagsToClimb(climbTags);
}

async function GetAllClimbAttemptsForCurrentClimb() {
  //get the climbId from the url
  const urlParams = new URLSearchParams(window.location.search);
  //split the url to get the climbId after Climbs/
  const climbId = window.location.pathname.split("/")[3];
  const response = await fetch(`/api/climb/log/view/climb/${climbId}`);
  const climbAttempts = await response.json();
  return climbAttempts;
}

async function GetAllTagsForCurrentClimb(climbAttempts) {
  //get the id for each climb attempt
  var climbTags = [];
  for (let i = 0; i < climbAttempts.length; i++) {
    var climbAttemptId = climbAttempts[i].id;
    var tags = await GetTagsForClimb(climbAttemptId);
    climbTags.push(tags);
  }
  return climbTags;
}

async function GetTagsForClimb(climbAttemptId) {
  const response = await fetch(
    `/api/ClimbTagEntryApi/log/view/${climbAttemptId}`
  );
  const tags = await response.json();
  return tags;
}

function AddTagsToClimb(climbTags) {
  var climbTagsDiv = document.getElementById("climb-tags-div");
  //loop through the climbTags array and add each tag to the climbTagsDiv
  if (climbTags.length === 0) {
    var noTagsDiv = document.createElement("span");
    noTagsDiv.innerHTML = "No tags for this climb yet!";
    climbTagsDiv.appendChild(noTagsDiv);
  } else {
    var tagsTitle = document.createElement("h3");
    tagsTitle.innerHTML = "Tags:";
    tagsTitle.classList.add("tags-title");
    climbTagsDiv.appendChild(tagsTitle);
  }
  for (let i = 0; i < climbTags.length; i++) {
    var tag = climbTags[i];
    var tagDiv = document.createElement("span");
    tagDiv.classList.add("tag");
    tagDiv.innerHTML = tag;
    climbTagsDiv.appendChild(tagDiv);
    switch (tag) {
      case "Crimpy":
        tagDiv.style.backgroundColor = "#ff0000";
        break;
      case "Slopers":
        tagDiv.style.backgroundColor = "#ff4000";
        break;
      case "Pockets":
        tagDiv.style.backgroundColor = "#ff8000";
        break;
      case "Juggy":
        tagDiv.style.backgroundColor = "#ffbf00";
        break;
      case "Pinches":
        tagDiv.style.backgroundColor = "#80ff00";
        break;
      case "Technical":
        tagDiv.style.backgroundColor = "#ff00ff";
        break;
      case "Powerful":
        tagDiv.style.backgroundColor = "#00ff00";
        break;
      case "Compression":
        tagDiv.style.backgroundColor = "#00ff80";
        break;
      case "Highball":
        tagDiv.style.backgroundColor = "#8000ff";
        break;
      case "Slab":
        tagDiv.style.backgroundColor = "#0000ff";
        break;
      case "Tension":
        tagDiv.style.backgroundColor = "#ff8000";
        break;
      case "Pumpy":
        tagDiv.style.backgroundColor = "#0080ff";
        break;
      case "Dyno":
        tagDiv.style.backgroundColor = "#00ffff";
        break;
      case "Classic":
        tagDiv.style.backgroundColor = "#aaaaaa";
        break;
      case "Unique":
        tagDiv.style.backgroundColor = "#ff0080";
        break;

      default:
        break;
    }
  }
}

import { getAllMessagesApiCall } from "/js/api.js";
import { populateGroupComments } from "/js/eventhandlers.js";


document.addEventListener("DOMContentLoaded", function () {
  const communityGroupButton = document.getElementById(
    "community-group-button"
  );
  const groupId = communityGroupButton.getAttribute("data-group-id");
  updateMemberCount(groupId);
  getGroupMessages(groupId);

  const memberModal = document.getElementById("memberModal");
  memberModal.addEventListener("show.bs.modal", function () {
    populateGroupMemberList(groupId);
  });

  function updateMembershipStatus() {
    updateMemberCount(groupId);
    checkUserGroupMembership(groupId).then((isMember) => {
      if (isMember) {
        communityGroupButton.textContent = "Leave Group";
        communityGroupButton.onclick = function () {
          leaveGroup(groupId).then((success) => {
            if (success) {
              updateMembershipStatus();
            }
          });
        };
      } else {
        communityGroupButton.textContent = "Join Group";
        communityGroupButton.onclick = function () {
          joinGroup(groupId).then((success) => {
            if (success) {
              updateMembershipStatus();
            }
          });
        };
      }
    });
  }
  updateMembershipStatus();
});

async function checkUserGroupMembership(groupId) {
  //checks if the user is a member of the group
  const url = `/api/community/check/group/${groupId}`;
  const response = await fetch(url);
  if (!response.ok) {
    return false;
  }
  const membership = await response.json();
  return membership;
}

async function leaveGroup(groupId) {
  //removes the user from the group
  const url = `/api/community/leave/group/${groupId}`;
  const response = await fetch(url, { method: "POST" });
  if (!response.ok) {
    return false;
  }
  return true;
}

async function joinGroup(groupId) {
  //adds the user to the group
  const url = `/api/community/join/group/${groupId}`;
  const response = await fetch(url, { method: "POST" });
  if (!response.ok) {
    return false;
  }
  return true;
}

async function getGroupMemberCount(groupId) {
  //gets the number of members in the group
  const url = `/api/community/members/group/${groupId}`;
  const response = await fetch(url);
  if (!response.ok) {
    return 0;
  }
  const memberCount = await response.json();
  console.log(memberCount);
  return memberCount;
}

async function updateMemberCount(groupId) {
  //updates the number of members in the group
  const memberCountSpan = document.getElementById("member-count-span");
  const memberCount = await getGroupMemberCount(groupId);
  memberCountSpan.innerHTML = memberCount;
}

//populate the group member list modal
async function populateGroupMemberList(groupId) {
  const memberListModalDiv = document.getElementById("member-list");
  memberListModalDiv.innerHTML = "";
  const table = document.createElement("table");
  memberListModalDiv.appendChild(table);
  const url = `/api/community/members/group/${groupId}/list`;
  const response = await fetch(url);
  if (!response.ok) {
    return;
  }
  const members = await response.json();
  const currentUserId = await getCurrentUserId(); // Function to get the current user
  members.forEach((member) => {
    const row = document.createElement("tr");

    const nameCell = document.createElement("td");
    nameCell.textContent = member.userName;
    row.appendChild(nameCell);

    if (member.id == currentUserId) {
      const buttonCell = document.createElement("td");
      buttonCell.classList.add("text-muted");
      buttonCell.textContent = "(You)";
      row.appendChild(buttonCell);
    }

    table.appendChild(row);
  });
}

async function getCurrentUserId() {
  const url = "/api/community/currentUserId";
  const response = await fetch(url);
  if (!response.ok) {
    return null;
  }
  const user = await response.json();
  return user;
}

// community message functionality

function getGroupMessages(groupId)
{
  
  const messages = getAllMessagesApiCall(groupId);
  console.log(messages);
  if (messages != null  && messages.length > 0) {
    populateGroupComments(messages);
  }
  else {
    const elseTemplate = document.getElementById("else-template");
    const elseArea = document.getElementById("comment-else");
    const clone = elseTemplate.content.cloneNode(true);
    elseArea.appendChild(clone);
  }

}

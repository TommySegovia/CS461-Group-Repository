import { getAllMessagesApiCall } from "/js/api.js";
import { populateGroupComments } from "/js/eventhandlers.js";
import { postMessage } from "/js/api.js";
import { getCommunityClimbingLog } from "/js/api.js";
import { getClimbersFromGroupId } from "/js/api.js";
import { displayGroupClimbingLog } from "/js/eventhandlers.js";

document.addEventListener("DOMContentLoaded", function () {
  const communityGroupButton = document.getElementById("community-group-button");
  const groupId = communityGroupButton.getAttribute("data-group-id");
  updateMemberCount(groupId);
  getGroupMessages(groupId);
  getClimbingLogsForGroup(groupId);

  if (document.querySelector("#createMessageModal")) {
    document.getElementById('createMessageModal').addEventListener('shown.bs.modal', function (e) {
      console.log("modal shown!");
      handleCommentFormSubmit(groupId);
    });
  }
  

  // Event listener for modal cancel button
  document.getElementById("modalCancelButton").addEventListener("click", closeModal);

  // Event listener for modal confirm button
  document.getElementById("modalConfirmButton").addEventListener("click", () => {
    confirmNewOwner(groupId);
  });

  const memberModal = document.getElementById("memberModal");
  memberModal.addEventListener("show.bs.modal", function () {
    populateGroupMemberList(groupId);
  });

  function updateMembershipStatus() {
    updateMemberCount(groupId)
    checkUserGroupMembership(groupId).then((isMember) => {
      if (isMember) {
        communityGroupButton.textContent = "Leave Group";
        communityGroupButton.onclick = function () {

          getGroupMemberCount(groupId).then((memberCount) => {
            console.log(memberCount);
            if (memberCount > 1) {
              openNewOwnerModal(groupId).then((success) => {
                if (success) {
                  updateMembershipStatus();
                }
              });
            }
            else {
              leaveGroup(groupId).then((success) => {
                if (success) {
                  deleteGroup(groupId).then((success) => {
                    if (success) {
                      window.location.href = "/Community";
                    }
                  });
                }
              });
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

// Function to open the new owner modal and populate the member list
async function openNewOwnerModal(groupId) {
  const newOwnerSelect = document.getElementById("new-owner-select");
  newOwnerSelect.innerHTML = ""; // Clear the select element

  // Fetch the list of members
  const url = `/api/community/members/group/${groupId}/list`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error("Failed to fetch members");
    }
    const members = await response.json();

    // Fetch the current owner's ID
    const ownerId = await getCurrentUserId();
    if (!ownerId) {
      throw new Error("Failed to fetch owner's ID");
    }

    // Filter the members to exclude the owner
    const nonOwnerMembers = members.filter(member => member.id !== ownerId);

    nonOwnerMembers.forEach((member) => {
      const option = document.createElement("option");
      option.value = member.id;
      option.textContent = member.userName;
      newOwnerSelect.appendChild(option);
    });

    $('#newOwnerModal').modal('show');
  } catch (error) {
    console.error("Failed to populate new owner modal: ", error);
  }
}

// Function to close the modal
function closeModal() {
  $('#newOwnerModal').modal('hide');
}

// Function to handle confirming the new owner selection
function confirmNewOwner(groupId) {
  const newOwnerId = document.getElementById("new-owner-select").value;
  if (!newOwnerId) {
    return;
  }

  // Set the new owner
  const setOwnerUrl = `/api/community/setOwner/group/${groupId}/user/${newOwnerId}`;
  fetch(setOwnerUrl, { method: "POST" })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to set new owner");
      }
      // Leave the group
      leaveGroup(groupId);
      //redirect to communityGroup page
      window.location.href = `/Community/Group/${groupId}`;
    })
    .catch((error) => {
      console.error("Failed to confirm new owner: ", error);
    });
}

async function deleteGroup(groupId) {
  // Deletes the group
  const url = `/api/community/delete/group/${groupId}`;
  const response = await fetch(url, { method: "POST" });
  if (!response.ok) {
    return false;
  }
  return true;
}

async function leaveGroup(groupId) {
  // Removes the user from the group
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

    if (member.id !== currentUserId) {
      const buttonCell = document.createElement("td");
      const removeButton = document.createElement("button");
      removeButton.classList.add("btn", "btn-danger");
      removeButton.textContent = "Remove from group";
      removeButton.onclick = function () {
        removeMemberFromGroup(groupId, member.id).then((success) => {
          if (success) {
            populateGroupMemberList(groupId);
            updateMemberCount(groupId);
          }
        });
      };
      buttonCell.appendChild(removeButton);
      row.appendChild(buttonCell);
    }
    else {
      const buttonCell = document.createElement("td");
      buttonCell.classList.add("text-muted");
      buttonCell.textContent = "(You)";
      row.appendChild(buttonCell);
    }

    table.appendChild(row);
  });
}

async function removeMemberFromGroup(groupId, memberId) {
  const url = `/api/community/remove/group/${groupId}/member/${memberId}`;
  const response = await fetch(url, { method: "POST" });
  if (!response.ok) {
    return false;
  }
  return true;
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

async function getGroupMessages(groupId) {
  
  const messages = await getAllMessagesApiCall(groupId);
  console.log(messages);
  if (messages != null && messages.length > 0) {
    populateGroupComments(messages);
  }
  else {
    const elseTemplate = document.getElementById("else-template");
    const elseArea = document.getElementById("comment-else");
    const clone = elseTemplate.content.cloneNode(true);
    elseArea.appendChild(clone);
  }

}

async function handleCommentFormSubmit(groupId) {

  const form = document.getElementById("comment-form");

  form.addEventListener("submit", async function (event) {
      event.preventDefault();
      const comment = document.getElementById("comment-textarea").value;


      console.log('Before postComment');
      postMessage(comment, groupId);
      console.log('After postComment: ');


      localStorage.setItem('formSubmitted', 'true');

      location.reload();
  });
}

async function getClimbingLogsForGroup(groupId) {
  const climbers = await getClimbersFromGroupId(groupId);
  console.log(climbers);
  const logs = await getCommunityClimbingLog(climbers);
  displayGroupClimbingLog(logs);
}

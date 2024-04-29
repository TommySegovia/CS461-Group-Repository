document.addEventListener("DOMContentLoaded", function () {
    const communityGroupButton = document.getElementById("community-group-button");
    const groupId = communityGroupButton.getAttribute("data-group-id");
  
    function updateMembershipStatus() {
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

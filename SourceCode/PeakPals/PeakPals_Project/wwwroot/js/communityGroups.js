document.addEventListener("DOMContentLoaded", function () {
    const communityGroupButton = document.getElementById("community-group-button");
    const groupId = communityGroupButton.getAttribute("data-group-id");
    console.log(groupId);
    updateMemberCount(groupId);
    
  
    function updateMembershipStatus() {
        updateMemberCount(groupId)
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
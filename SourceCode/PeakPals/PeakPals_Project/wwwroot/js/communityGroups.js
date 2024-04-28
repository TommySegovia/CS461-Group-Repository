document.addEventListener("DOMContentLoaded", function () {
  console.log("Community page created.");
  const communityGroupButton = document.getElementById(
    "community-group-button"
  );
  //on page load, check if the user is a member of the group, if they are, change the button text to "Leave Group", if they are not, change the button text to "Join Group"
  const groupId = communityGroupButton.getAttribute("data-group-id");
  console.log(groupId);
  checkUserGroupMembership(groupId).then((isMember) => {
    if (isMember) {
      communityGroupButton.textContent = "Leave Group";
      console.log("User is a member of the group.");
    } else {
      communityGroupButton.textContent = "Join Group";
      console.log("User is not a member of the group.");
    }
  });


});




async function checkUserGroupMembership(groupId)
{
    //checks if the user is a member of the group
    const url = `/api/community/check/group/${groupId}`;
    const response = await fetch(url);
    if (!response.ok)
    {
        return false;
    }
    const membership = await response.json();
    return membership;
}
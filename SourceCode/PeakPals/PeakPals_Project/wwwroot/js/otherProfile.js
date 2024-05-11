document.addEventListener("DOMContentLoaded", function () {
    console.log("Other Profile page created.");
        
    populateJoinedGroups();
});

async function populateJoinedGroups() {
    // Get userName id of the climber
    const pageUrl = window.location.href;
    const userName = pageUrl.substring(pageUrl.lastIndexOf('/') + 1);

    const url = `/api/community/joinedGroups/${userName}`;
    const response = await fetch(url);
    const groups = await response.json();
    const joinedCommunityGroupsDiv = document.getElementById("joinedCommunityGroupsDiv");
    if (groups.length > 0) {
        const joinedCommunityGroupsHeader = document.getElementById("joinedCommunityGroupsHeader");
        joinedCommunityGroupsHeader.innerHTML = "Joined Community Groups";
        joinedCommunityGroupsDiv.style.display = "block";
    }
    groups.forEach(group => {
        const groupDiv = document.createElement("div");
        groupDiv.className = "joinedCommunityGroup";
        groupDiv.innerHTML = `<a class="joinedGroupLink" href="/Community/Group/${group.id}">${group.name}</a>`;
        joinedCommunityGroupsDiv.appendChild(groupDiv);
    });
}
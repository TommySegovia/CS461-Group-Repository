document.addEventListener("DOMContentLoaded", function () {
    console.log("User Profile page created.");
    populateJoinedGroups();
    populateLoggedClimbs();
});

async function populateJoinedGroups() {
    const url = `/api/community/joinedGroups`;
    const response = await fetch(url);
    const groups = await response.json();
    const joinedCommunityGroupsDiv = document.getElementById("joinedCommunityGroupsDiv");
    if (groups.length > 0) {
        const joinedCommunityGroupsHeader = document.getElementById("joinedCommunityGroupsHeader");
        joinedCommunityGroupsHeader.innerHTML = "Joined Groups:";
        joinedCommunityGroupsDiv.style.display = "block";
    }
    groups.forEach(group => {
        const groupDiv = document.createElement("div");
        groupDiv.className = "joinedCommunityGroup";
        groupDiv.innerHTML = `<a class="joinedGroupLink" href="/Community/Group/${group.id}">${group.name}</a>`;
        joinedCommunityGroupsDiv.appendChild(groupDiv);
    });
}

async function populateLoggedClimbs() {
    const loggedClimbUrl = `/api/climb/log/view`;
    const loggedClimbResponse = await fetch(loggedClimbUrl);
    const climbs = await loggedClimbResponse.json();
    const loggedClimbsDiv = document.getElementById("loggedClimbsDiv");
    console.log(climbs);
    console.log(climbs.length);

    

    if (climbs.length > 0) {
        const loggedClimbsHeader = document.getElementById("loggedClimbsHeader");
        loggedClimbsHeader.innerHTML = "Logged Climbs";
        loggedClimbsDiv.style.display = "block";
    }
    climbs.forEach(async climb => {
        const climbDiv = document.createElement("div");
        climbDiv.className = "loggedClimb";

        const climbNameLink = document.createElement("a");
        climbNameLink.innerHTML = climb.climbName + " - Grade: " + climb.suggestedGrade;
        climbNameLink.className = "loggedClimbName";
        climbNameLink.href = "/Locations/Climbs/" + climb.climbId;
        climbDiv.appendChild(climbNameLink);

        const climbBody = document.createElement("div");
        climbBody.className = "loggedClimbBody container";

        const climbBodyRow1 = document.createElement("div");
        climbBodyRow1.className = "row";
        const climbBodyRow2 = document.createElement("div");
        climbBodyRow2.className = "row";
        
        const climbBodyCol1 = document.createElement("div");
        climbBodyCol1.className = "col";
        let date = new Date(climb.entryDate);
        let formattedDate = (date.getMonth()+1).toString().padStart(2, '0') + '/' + 
                        date.getDate().toString().padStart(2, '0') + '/' + 
                        date.getFullYear().toString().substr(-2);
        climbBodyCol1.innerHTML = formattedDate;

        const climbBodyCol2 = document.createElement("div");
        climbBodyCol2.className = "col";
            const starRating = climb.rating;
            for (let i = 0; i < starRating; i++) {
                let img = document.createElement('img');
                img.src = "/images/star.svg";
                img.width = 40;
                img.height = 40;
                climbBodyCol2.appendChild(img);
            }

        const climbBodyCol3 = document.createElement("div");
        climbBodyCol3.className = "col";
        climbBodyCol3.innerHTML = "Attempts: " + climb.attempts;
        
        const tagsUrl = `/api/ClimbTagEntryApi/log/view/${climb.id}`;
        const tagsResponse = await fetch(tagsUrl);
        const tags = await tagsResponse.json();
        console.log(tags);

        const tagsDiv = document.createElement("div");
        tagsDiv.className = "tagsDiv";
        tags.forEach(tag => {
            const tagDiv = document.createElement("div");
            tagDiv.className = "tag";
            tagDiv.innerHTML = tag;
            tagsDiv.appendChild(tagDiv);
        });
        climbBodyRow2.appendChild(tagsDiv);
        
        climbBodyRow1.appendChild(climbBodyCol1);
        climbBodyRow1.appendChild(climbBodyCol2);
        climbBodyRow1.appendChild(climbBodyCol3);
        climbBody.appendChild(climbBodyRow1);
        climbBody.appendChild(climbBodyRow2);
        climbDiv.appendChild(climbBody);
        loggedClimbsDiv.appendChild(climbDiv);
    }
    );
}


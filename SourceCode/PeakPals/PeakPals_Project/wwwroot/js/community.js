
document.addEventListener("DOMContentLoaded", function() 
{
    console.log("Index page created.")
    const searchButton = document.getElementById("search-button");
    searchButton.addEventListener("click", searchButtonClicked, false);

    const communityGroupSearchButton = document.getElementById("community-group-search-button");
    communityGroupSearchButton.addEventListener("click", communityGroupSearchButtonClicked, false);

    const createGroupForm = document.getElementById("createGroupForm");
    createGroupForm.addEventListener("submit", createGroupButtonClicked, false);

    populateJoinedGroups();
});

async function searchButtonClicked(e)
{
    console.log("search button clicked.")
    const searchResultsDiv = document.getElementById("search-results");
    searchResultsDiv.textContent = "";
    const errorMessage = document.createElement("p");
    const validationWarning = document.getElementById("community-validation-warning");
    validationWarning.textContent = "";

    const searchInput = document.getElementById("search-input");
    const username = searchInput.value.trim();
    const isValid = /^[a-z0-9 ,!?]+$/i.test(username);

    if(!isValid)
    {
        errorMessage.textContent = "Invalid input. Please enter a search term with valid characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }


    if (username === "")
    { 
        errorMessage.textContent = "Invalid input. Please enter a search term.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const url = `/api/community/search/${username}`;
    console.log(url);
    const response = await fetch(url);
    console.log(response);
    if (!response.ok)
    {
        errorMessage.textContent = "Sorry, the response returned an error."
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    const climbers = await response.json();
    if (climbers.length === 0) {
        errorMessage.textContent = "Sorry, no user exists with that name."
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    console.log(climbers);
    
    const resultTemplate = document.getElementById("results-template");

    climbers.forEach(climber => 
    {
        const clone = resultTemplate.content.cloneNode(true);
        const profileLink = clone.getElementById('profile-link');
        profileLink.href = `/Profile/${climber.userName}`;
        profileLink.textContent = climber.userName;

        const userImage = clone.getElementById('userImage');
        if (climber.imageLink !== null)
        {
            userImage.src = climber.imageLink;
        }
        else 
        {
            userImage.src = "/images/blank_profile.png";
            console.log(userImage.src);
        }
        searchResultsDiv.appendChild(clone);
        console.log("created a new movie object to show.");

    })
    
}

async function communityGroupSearchButtonClicked(e){
    //searches for community groups and populates the search results div with the results by building the template for each group using the groups name and description
    console.log("community group search button clicked.")
    const searchResultsDiv = document.getElementById("community-group-search-results");
    searchResultsDiv.textContent = "";
    const errorMessage = document.createElement("p");
    const validationWarning = document.getElementById("community-group-validation-warning");
    validationWarning.textContent = "";

    const searchInput = document.getElementById("community-group-search-input");
    const groupName = searchInput.value.trim();
    const isValid = /^[a-z0-9 ,!?]+$/i.test(groupName);

    if(!isValid)
    {
        errorMessage.textContent = "Invalid input. Please enter a search term with valid characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    if (groupName === "")
    { 
        errorMessage.textContent = "Invalid input. Please enter a search term.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const url = `/api/community/search/group/${groupName}`;
    console.log(url);
    const response = await fetch(url);
    console.log(response);
    if (!response.ok)
    {
        errorMessage.textContent = "Sorry, the response returned an error."
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    const communityGroups = await response.json();
    if (communityGroups.length === 0) {
        errorMessage.textContent = "Sorry, no community group exists with that name."
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    console.log(communityGroups);

    const resultTemplate = document.getElementById("community-group-template");
    const searchResultsText = document.getElementById("group-results-text");
    searchResultsText.textContent = "Search Results:";

    communityGroups.forEach(communityGroup => 
    {
        //creates a new clone of the template for each community group and populates the template with the groups name and description
        const clone = resultTemplate.content.cloneNode(true);
        const groupLink = clone.getElementById('group-link-name');
        groupLink.href = `/Community/Group/${communityGroup.id}`;
        groupLink.textContent = communityGroup.name;

        const groupDescription = clone.getElementById('group-description');
        groupDescription.href = `/Community/Group/${communityGroup.id}`;
        groupDescription.textContent = communityGroup.description;

        searchResultsDiv.appendChild(clone);
        console.log("created a new community group object to show.");

    })
}

async function createGroupButtonClicked(e){
    //creates a new community group and adds it to the database
    console.log("create group button clicked.")
    e.preventDefault();
    const errorMessage = document.createElement("p");
    const validationWarning = document.getElementById("createGroupValidation");
    validationWarning.textContent = "";

    const groupName = document.getElementById("groupName").value;
    const groupDescription = document.getElementById("groupDescription").value;

    const nameIsValid = /^[a-z0-9 ,!?]+$/i.test(groupName);
    const descriptionIsValid = /^[a-z0-9 .,!?]+$/i.test(groupDescription);

    if(!nameIsValid)
    {
        errorMessage.textContent = "Invalid input. Please enter a group name with valid characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    if(!descriptionIsValid)
    {
        errorMessage.textContent = "Invalid input. Please enter a group description with valid characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }


    if (groupName === "")
    {
        errorMessage.textContent = "Invalid input. Please enter a group name.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    if (groupDescription === "")
    {
        errorMessage.textContent = "Invalid input. Please enter a group description.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const url = `/api/community/create/group`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({name: groupName, description: groupDescription})
    });
    console.log(response);
    if (!response.ok)
    {
        errorMessage.textContent = "Sorry, the response returned an error."
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    const communityGroup = await response.json();
    console.log(communityGroup);
    window.location.href = `/Community/Group/${communityGroup.id}`;
}

async function populateJoinedGroups()
{
    //populates the joined groups div with the users joined groups
    console.log("populate joined groups.")
    const url = `/api/community/joinedGroups`;
    console.log(url);
    const response = await fetch(url);
    console.log(response);
    if (!response.ok)
    {
        console.log("error")
        return;
    }
    const communityGroups = await response.json();
    console.log(communityGroups);
    const joinedGroupsDiv = document.getElementById("joined-groups");
    const joinedGroupsText = document.getElementById("joined-groups-text");
    joinedGroupsText.textContent = "Joined Groups:";
    const resultTemplate = document.getElementById("community-group-template");

    communityGroups.forEach(communityGroup => 
    {
        //creates a new clone of the template for each community group and populates the template with the groups name and description
        const clone = resultTemplate.content.cloneNode(true);
        const groupLink = clone.getElementById('group-link-name');
        groupLink.href = `/Community/Group/${communityGroup.id}`;
        groupLink.textContent = communityGroup.name;

        const groupDescription = clone.getElementById('group-description');
        groupDescription.href = `/Community/Group/${communityGroup.id}`;
        groupDescription.textContent = communityGroup.description;

        joinedGroupsDiv.appendChild(clone);
        console.log("created a new community group object to show.");

    }

    )
}

document.addEventListener("DOMContentLoaded", function() 
{
    console.log("Index page created.")
    const searchButton = document.getElementById("search-button");
    searchButton.addEventListener("click", searchButtonClicked, false);
});

async function searchButtonClicked(e)
{
    console.log("search button clicked.")
    const searchResultsDiv = document.getElementById("search-results");
    searchResultsDiv.innerHTML = "";
    const errorMessage = document.createElement("p");
    const validationWarning = document.getElementById("community-validation-warning");
    validationWarning.innerHTML = "";

    const searchInput = document.getElementById("search-input");
    const username = searchInput.value;
    const isValid = /^[a-z0-9]+$/i.test(username);

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
        profileLink.innerHTML = climber.userName;

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

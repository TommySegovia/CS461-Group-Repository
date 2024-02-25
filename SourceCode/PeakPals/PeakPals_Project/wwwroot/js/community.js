
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
    const searchInput = document.getElementById("search-input");
    const username = searchInput.value;

    const validationWarning = document.getElementById("community-validation-warning");
    validationWarning.innerHTML = "";

    if (username === "")
    {
        const errorMessage = document.createElement("p");
        errorMessage.textContent = "Invalid input. Please enter a search term.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const url = `/api/community/search/${username}`;
    console.log(url);
    const response = await fetch(url);
    if (!response.ok)
    {
        const errorMessageNull = document.createElement("p");
        errorMessageNull.textContent = "Sorry, no user exists with that name."
        errorMessageNull.classList.add("error-message");
        validationWarning.appendChild(errorMessageNull);
        return;
    }
    const climber = await response.json();
    // const climbers = await response.json();
    console.log(climber);
    console.log(climber.userName);
    

    const resultTemplate = document.getElementById("results-template");

    //climbers.forEach(climber => {})

    const clone = resultTemplate.content.cloneNode(true);
    const profileLink = clone.getElementById('profile-link');
    profileLink.href = `/Profile/${climber.userName}`;
    profileLink.innerHTML = climber.userName;

    const userImage = clone.getElementById('userImage');
    if (userImage !== "")
    {
        userImage.src = climber.imageLink;
    }

    searchResultsDiv.appendChild(clone);
    console.log("created a new movie object to show.");

   

}




document.addEventListener("DOMContentLoaded", function() 
{
    console.log("Index page created.")
    const searchButton = document.getElementById("search-button");
    searchButton.addEventListener("click", searchButtonClicked, false);
});

async function searchButtonClicked(e)
{
    console.log("Search button clicked");
    const searchResultsDiv = document.getElementById("search-results");
    searchResultsDiv.innerHTML = "";
    const errorMessage = document.createElement("p");
    const validationWarning = document.getElementById("locations-validation-warning")
    validationWarning.innerHTML = "";

    const searchInput = document.getElementById("search-input");
    const query = searchInput.value;
    const isValidLength = query.length >= 3;
    const isValid = /^[a-z0-9 ]+$/i.test(query);

    if (!isValid)
    {
        errorMessage.textContent = "Invalid input. Please enter a search term with valid characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    if (!isValidLength)
    {
        errorMessage.textContent = "Invalid input. Please enter at least 3 characters.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }
    if (query === "")
    {
        errorMessage.textContent = "Invalid input. Please enter a search term.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const loadingSpinner = document.getElementById('loading-spinner');
    loadingSpinner.style.display = 'block';

    const url = `/api/locations/search/${query}`
    console.log(url);
    const response = await fetch(url);
    console.log(response);
    const result = await response.json();
    console.log(result);
    const areas = result.value.areas;
    console.log(areas);

    loadingSpinner.style.display = 'none';

    if (areas === null || areas.length === 0)
    {
        errorMessage.textContent = "No results found.";
        errorMessage.classList.add("error-message");
        validationWarning.appendChild(errorMessage);
        return;
    }

    const areaTemplate = document.getElementById("area-template")
    

    areas.forEach(area => {
        
        const clone = areaTemplate.content.cloneNode(true);
        const areaName = clone.getElementById('area-name');

        // refactor possibly to remove template use
        /*
        const ancestorsTemplate = document.getElementById("ancestors-template");
        const ancestorsList = clone.getElementById("ancestors-list");
        const ancestorClone = ancestorsTemplate.content.cloneNode(true);
        area.ancestors.forEach(ancestor => {
            
            const areaAncestors = ancestorClone.getElementById('area-ancestors');
            areaAncestors.textContent += ancestor + " > ";    
        })
        ancestorsList.appendChild(ancestorClone);
        */

        areaName.textContent = area.area_Name;
        searchResultsDiv.appendChild(clone);
        console.log("created a new location to show.");

    })

}


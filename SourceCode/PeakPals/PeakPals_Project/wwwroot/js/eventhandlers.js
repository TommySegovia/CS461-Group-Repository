import { toggleLoadingSpinner } from "/js/ui.js";
import { displayErrorMessage } from "/js/ui.js";
import { fetchAreas } from "/js/api.js";
import { fetchAreaAncestors } from "/js/api.js";

export async function locationsSearchButtonClicked(e)
{
    console.log("Search button clicked");
    const searchResultsDiv = document.getElementById("search-results");
    searchResultsDiv.innerHTML = "";
    
    const validationWarning = document.getElementById("locations-validation-warning")
    validationWarning.innerHTML = "";

    const searchInput = document.getElementById("search-input");
    const query = searchInput.value;
    const isValidLength = query.length >= 3;
    const isValid = /^[a-z0-9 ]+$/i.test(query);

    if (!isValid){
        displayErrorMessage("Invalid input. Please enter a search term with valid characters.", validationWarning);
        return;
    }
    if (!isValidLength) {
        displayErrorMessage("Invalid input. Please enter at least 3 characters.", validationWarning);
        return;
    }
    if (query === "") {
        displayErrorMessage("Invalid input. Please enter a search term.", validationWarning);
        return;
    }

    const loadingSpinner = document.getElementById('loading-spinner');
    toggleLoadingSpinner(loadingSpinner);

    const areas = await fetchAreas(query, loadingSpinner, validationWarning);

    const areaTemplate = document.getElementById("area-template");

    
    const areasPromise = areas.map(async area => {

        console.log(areaTemplate);
        const clone = areaTemplate.content.cloneNode(true);
        const areaName = clone.getElementById('area-name');

        // refactor possibly to remove template use
        let ancestors = await fetchAreaAncestors(area.ancestors);
        ancestors = ancestors.slice(0, -1);
        
        const ancestorsTemplate = document.getElementById("ancestors-template");
        const ancestorsList = clone.getElementById("ancestors-list");
        const ancestorClone = ancestorsTemplate.content.cloneNode(true);
        ancestors.forEach(ancestor => {
            
            const areaAncestors = ancestorClone.getElementById('area-ancestors');
            areaAncestors.textContent += ancestor.area.area_Name + "  >  ";    
        })
        ancestorsList.appendChild(ancestorClone);
        

        areaName.textContent = area.area_Name;

        const areaDiv = clone.querySelector('#areas-div');
        areaDiv.style.cursor = "pointer";
        areaDiv.addEventListener("click", async function() 
        { 
            console.log("created a new location to show.");
            areaCardClicked(area.uuid)
        });

        searchResultsDiv.appendChild(clone);

    })

    await Promise.all(areasPromise);
    toggleLoadingSpinner(loadingSpinner);
}


export async function areaCardClicked(uuid)
{
    console.log("Area card clicked: " + uuid);

    window.location.href = `/Locations/Areas/${uuid}`;
 
}
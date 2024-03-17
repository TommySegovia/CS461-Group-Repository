
import { fetchAreaAncestors } from "/js/api.js";
import { locationsSearchButtonClicked } from "/js/eventhandlers.js";


document.addEventListener("DOMContentLoaded", async function() 
{
    console.log("Index page created.")

    // locations/search.cshtml 
    const searchButton = document.getElementById("search-button");
    if (searchButton) {
        searchButton.addEventListener("click", locationsSearchButtonClicked, false);
    }

    // locations/areas.cshtml
    const ancestorLinksDiv = document.getElementById("areas-ancestor-links");
    if (ancestorLinksDiv) {
        const areaAncestorsList = await fetchAreaAncestors(areaPageAncestors);
        areaAncestorsList.forEach(areaAncestor => {
            const ancestorLink = document.createElement("a");
            ancestorLink.href = areaAncestor.area.uuid;
            ancestorLink.textContent = areaAncestor.area.area_Name + "  >  ";
            ancestorLinksDiv.appendChild(ancestorLink);
        });
    }
    
});







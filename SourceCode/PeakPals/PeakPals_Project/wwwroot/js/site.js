
import { fetchAreaAncestors } from "/js/api.js";
import { locationsSearchButtonClicked } from "/js/eventhandlers.js";


document.addEventListener("DOMContentLoaded", async function() 
{
    console.log("Index page created.")

    // locations/search.cshtml 
    const searchButton = document.getElementById("search-button");
    if (searchButton) {
        let searchType;
        document.addEventListener('click', function(e) {
            if (e.target.matches('.category-button')) {
                console.log("Category button clicked: " + e.target.textContent.trim());
                searchType = e.target.textContent.trim();
            }
        });
        searchButton.addEventListener("click", function(e) {
            locationsSearchButtonClicked(e, searchType);
        }, false);
        
    }
    
    // locations/areas.cshtml
    const ancestorLinksDiv = document.getElementById("areas-ancestor-links");
    if (ancestorLinksDiv) {
        const areaAncestorsList = await fetchAreaAncestors(areaPageAncestors);
        areaAncestorsList.forEach(areaAncestor => {
            const ancestorLink = document.createElement("a");
            ancestorLink.href = "/locations/areas/" + areaAncestor.area.uuid;
            ancestorLink.textContent = areaAncestor.area.area_Name + "  >  ";
            ancestorLinksDiv.appendChild(ancestorLink);
        });
    }
    const climbsAncestorLinksDiv = document.getElementById("climbs-ancestor-links");
    if (climbsAncestorLinksDiv) {
        const climbAncestorsList = await fetchAreaAncestors(climbPageAncestors);
        climbAncestorsList.forEach(climbAncestor => {
            const ancestorLink = document.createElement("a");
            ancestorLink.href = "/locations/areas/" + climbAncestor.area.uuid;
            ancestorLink.textContent = climbAncestor.area.area_Name + "  >  ";
            climbsAncestorLinksDiv.appendChild(ancestorLink);
        });
    }
    
});








import { handleClimbAttemptFormSubmit } from "/js/eventhandlers.js";
import { fetchAreaAncestors } from "/js/api.js";
import { locationsSearchButtonClicked } from "/js/eventhandlers.js";
import { initializeDynamicMapArea, initializeDynamicMapClimb } from "/js/map.js";
import { displayClimbingLog } from "/js/eventhandlers.js";

document.addEventListener("DOMContentLoaded", async function() 
{
    console.log("Index page created.")
    
    // search.cshtml 
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
    const climbingLog = document.getElementById("climb-and-map");
    if (climbingLog) {
        const user = climbingLog.dataset.user;
        displayClimbingLog(user);
    }
    const searchMap = document.getElementById("dynamic-map-search")
    if (searchMap) {
        const mapElement = document.querySelector("#dynamic-map")
        if (mapElement) {
            const lat = mapElement.dataset.latitude;
            const lng = mapElement.dataset.longitude;
            const name = "";
            const mode = mapElement.dataset.mode;
            const id = mapElement.dataset.id;
            initializeDynamicMapArea(lng, lat, name, id, mode);
        }
    }
    
    // areas.cshtml
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
    if (document.querySelector("#dynamic-map-box")){
        const mapElement = document.querySelector('#dynamic-map');
        const buttonElement = document.querySelector('#dimension-button')
        if (mapElement) {
            const lat = mapElement.dataset.latitude;
            const lng = mapElement.dataset.longitude;
            const name = mapElement.dataset.name;
            const id = mapElement.dataset.id;
            let mode = mapElement.dataset.mode;
            initializeDynamicMapArea(lng, lat, name, id, mode);

            buttonElement.addEventListener('click', function() {
                mode = mapElement.dataset.mode === "2d" ? "3d" : "2d";
                mapElement.dataset.mode = mode;
                initializeDynamicMapArea(lng, lat, name, id, mode);
            });
        }
    }
    
    //climbs.cshtml
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
    if (document.querySelector("#dynamic-map-climb")){
        const mapElement = document.querySelector("#dynamic-map-climb");
        const buttonElement = document.querySelector('#dimension-button')
        if (mapElement) {
            const lat = mapElement.dataset.latitude;
            const lng = mapElement.dataset.longitude;
            const name = mapElement.dataset.name;
            const id = mapElement.dataset.id;
            const ancestors = mapElement.dataset.ancestors;
            let mode = false;
            initializeDynamicMapClimb(lng, lat, name, id, ancestors);

            buttonElement.addEventListener('click', function() {
                mode = mapElement.dataset.mode === "2d" ? "3d" : "2d";
                mapElement.dataset.mode = mode;
                initializeDynamicMapClimb(lng, lat, name, id, ancestors, mode);
            });
        }
    }
    if (document.querySelector("#climbAttemptModal")) {
        document.getElementById('climbAttemptModal').addEventListener('shown.bs.modal', function (e) {
            console.log("logAttemptButton exists!");
            $('#climbAttemptModal').on('hidden.bs.modal', function (e) {
                $(this).find('form').trigger('reset');
            });
            handleClimbAttemptFormSubmit();
        })
    }
    
    // community groups
    if (document.querySelector("#createMessageModal")) {
        document.getElementById('createMessageModal').addEventListener('shown.bs.modal', function (e) {
            $('#createMessageModal').on('hidden.bs.modal', function (e) {
                $(this).find('form').trigger('reset');
            });
            handleCommentFormSubmit();
        })
    }
});



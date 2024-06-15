import { toggleLoadingSpinner } from "/js/ui.js";
import { displayErrorMessage } from "/js/ui.js";
import { fetchAreas } from "/js/api.js";
import { fetchClimbs } from "/js/api.js";
import { fetchAreaAncestors } from "/js/api.js";
import { getClimbAttempts, postClimbAttempt } from "/js/api.js";
import { fetchClimbDataById } from "/js/api.js";

export async function locationsSearchButtonClicked(e, searchType) {
    console.log("Search button clicked");
    const searchResultsAreaDiv = document.getElementById("search-results-areas");
    searchResultsAreaDiv.textContent = "";
    const searchResultsClimbDiv = document.getElementById("search-results-climbs");
    searchResultsClimbDiv.textContent = "";

    const validationWarning = document.getElementById("locations-validation-warning")
    validationWarning.textContent = "";

    const searchInput = document.getElementById("search-input");
    const query = searchInput.value.trim();
    const isValidLength = query.length >= 3;
    const isValid = /^[a-z0-9 ]+$/i.test(query);

    if (!isValid) {
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

    let areas, climbs, areasPromise, climbsPromise;

    // api call
    switch (searchType) {
        case 'All':
            areasPromise = await fetchAreas(query, loadingSpinner, validationWarning);
            climbsPromise = await fetchClimbs(query, loadingSpinner, validationWarning);
            break;
        case 'Areas':
            areasPromise = await fetchAreas(query, loadingSpinner, validationWarning);
            break;
        case 'Climbs':
            climbsPromise = await fetchClimbs(query, loadingSpinner, validationWarning);
            break;
        default:
            areasPromise = await fetchAreas(query, loadingSpinner, validationWarning);
            climbsPromise = await fetchClimbs(query, loadingSpinner, validationWarning);
            break;
    }
    [areas, climbs] = await Promise.all([areasPromise, climbsPromise]);
    console.log(climbs);

    const areaTemplate = document.getElementById("area-template");

    if (areas != null && areas.length > 0) {
        const areasPromise = areas.map(async area => {

            const clone = areaTemplate.content.cloneNode(true);
            const areaName = clone.getElementById('area-name');

            // refactor possibly to remove template use
            //let ancestors = await fetchAreaAncestors(area.ancestors);
            //ancestors = ancestors.slice(0, -1);
            console.log(area);
            let ancestors = area.pathTokens;

            const ancestorsTemplate = document.getElementById("ancestors-template");
            const ancestorsList = clone.getElementById("ancestors-list");
            const ancestorClone = ancestorsTemplate.content.cloneNode(true);
            ancestors.forEach(ancestor => {

                const areaAncestors = ancestorClone.getElementById('area-ancestors');
                areaAncestors.textContent += ancestor + "  >  ";
            })
            ancestorsList.appendChild(ancestorClone);

            areaName.textContent = area.area_Name;

            const areaDiv = clone.querySelector('#areas-div');
            areaDiv.style.cursor = "pointer";
            areaDiv.addEventListener("click", async function () {
                console.log("created a new area location to show.");
                areaCardClicked(area.uuid)
            });

            searchResultsAreaDiv.appendChild(clone);

        })
        await Promise.all(areasPromise);
    }
    if (climbs != null && climbs.length > 0) {
        const climbsPromise = climbs.map(async climb => {
            const clone = areaTemplate.content.cloneNode(true);
            const climbName = clone.getElementById('area-name');

            const returnedClimb = await fetchClimbDataById(climb.uuid);
            console.log(returnedClimb);
            console.log(returnedClimb.climb);
            const ancestors = returnedClimb.climb.pathTokens;
            console.log(ancestors);

            const ancestorsTemplate = document.getElementById("ancestors-template");
            const ancestorsList = clone.getElementById("ancestors-list");
            const ancestorClone = ancestorsTemplate.content.cloneNode(true);
            ancestors.forEach(ancestor => {

                const areaAncestors = ancestorClone.getElementById('area-ancestors');
                areaAncestors.textContent += ancestor + "  >  ";
            });
            ancestorsList.appendChild(ancestorClone);

            climbName.textContent = climb.name;

            const areaDiv = clone.querySelector("#areas-div");
            areaDiv.style.cursor = "pointer";
            areaDiv.addEventListener("click", async function () {
                console.log("created a new climb location to show.");
                climbCardClicked(climb.uuid)
            });

            searchResultsClimbDiv.appendChild(clone);
        });
        await Promise.all(climbsPromise);
    }

    toggleLoadingSpinner(loadingSpinner);
}


export async function areaCardClicked(uuid) {
    console.log("Area card clicked: " + uuid);

    window.location.href = `/locations/areas/${uuid}`;

}

export async function climbCardClicked(uuid) {
    console.log("Climb card clicked: " + uuid);

    window.location.href = `/locations/climbs/${uuid}`;

}

// ClimbAttempt Logging

export async function handleClimbAttemptFormSubmit() {

    const form = document.getElementById("climb-attempt-form");
    var climbAttemptModalElement = document.getElementById('climbAttemptModal');

    


    document.getElementById("climb-attempt-form").addEventListener("submit", async function (event) {
        event.preventDefault();
        const attempts = document.getElementById("attempts").value;
        const rating = document.getElementById("rating").value;
        const suggestedGrade = document.getElementById("suggested-grade").value;
        const climbId = document.getElementById("climb-id").dataset.id;
        const climbName = document.getElementById("climb-id").dataset.name;

        
        if (suggestedGrade.length < 1 || suggestedGrade.length > 9) {
            alert("Please enter a valid grade");
            return;
        }

        console.log('Before postClimbAttempt');
        try {
            await postClimbAttempt(climbId, climbName, suggestedGrade, attempts, rating);
            localStorage.setItem('formSubmitted', 'true');

            location.reload();
            console.log("log submitted!");
        }
        catch (error) {
            console.error(error);
            
        }
        console.log('After postClimbAttempt');


        
    });
}


// Climbing Log Display

export async function displayClimbingLog(user) {

    let logs = await getClimbAttempts();
    console.log(logs);
    const climbLogTemplate = document.getElementById("climb-log-template");
    const climbLogDisplayArea = document.getElementById("display-log-list");
    const paginationArea = document.getElementById("pagination-area");
    const itemsPerPage = 6;
    let currentPage = 1;

    function renderItems() {
        if (logs.length === 0) {
            return;
        }
        climbLogDisplayArea.textContent = '';
        const startIndex = (currentPage - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;
        const itemsToDisplay = logs.slice(startIndex, endIndex);

        itemsToDisplay.forEach(async log => {
            //Setup
            const clone = climbLogTemplate.content.cloneNode(true);
            console.log(log.rating);

            //ClimbName
            const attemptNameElement = clone.getElementById("climb-attempt-name");
            attemptNameElement.textContent = log.climbName;

            // EntryDate
            const attemptDateElement = clone.getElementById("climb-attempt-date");
            let options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' };
            attemptDateElement.textContent = new Date(log.entryDate).toLocaleString('en-US', options);

            // SuggestedGrade
            const attemptGradeElement = clone.getElementById("climb-attempt-grade");
            attemptGradeElement.textContent = log.suggestedGrade;

            // Climbing Attempts
            const attemptsNumElement = clone.getElementById("climb-attempt-attempts");
            attemptsNumElement.textContent = log.attempts;

            // Rating
            const attemptRatingElement = clone.getElementById("climb-attempt-rating");
            const starRating = log.rating;
            for (let i = 0; i < starRating; i++) {
                let img = document.createElement('img');
                img.src = "/images/star.svg";
                img.width = 40;
                img.height = 40;
                attemptRatingElement.appendChild(img);
            }

            // Link 4 Button
            const attemptLinkElement = clone.getElementById("climb-attempt-link-button");
            attemptLinkElement.href = `/Locations/Climbs/${log.climbId}`;

            

            // Tags
            const tags = await getClimbTags(log.id);
            console.log('tags',tags);
            const tagsElement = clone.getElementById("climb-attempt-tags");

            // Check if tagsElement is not null before proceeding
            if (tagsElement) {
                if (tags.length === 0) {
                    const noTagsElement = document.createElement('span');
                    noTagsElement.className = "badge climbTagBadge";
                    noTagsElement.textContent = "No Tags";
                    tagsElement.appendChild(noTagsElement);
                }
                else{
                    tags.forEach(tag => {
                        console.log(tag);
                        const tagElement = document.createElement('span');
                        tagElement.className = "badge climbTagBadge";
                        tagElement.textContent = tag;
                        switch (tag) {
                            case "Crimpy":
                                tagElement.style.backgroundColor = "#ff0000";
                                break;
                            case "Slopers":
                                tagElement.style.backgroundColor = "#ff4000";
                                break;
                            case "Pockets":
                                tagElement.style.backgroundColor = "#ff8000";
                                break;
                            case "Juggy":
                                tagElement.style.backgroundColor = "#ffbf00";
                                break;
                            case "Pinches":
                                tagElement.style.backgroundColor = "#80ff00";
                                break;
                            case "Technical":
                                tagElement.style.backgroundColor = "#ff00ff";
                                break;
                            case "Powerful":
                                tagElement.style.backgroundColor = "#00ff00";
                                break;
                            case "Compression":
                                tagElement.style.backgroundColor = "#00ff80";
                                break;
                            case "Highball":
                                tagElement.style.backgroundColor = "#8000ff";
                                break;
                            case "Slab":
                                tagElement.style.backgroundColor = "#0000ff";
                                break;
                            case "Tension":
                                tagElement.style.backgroundColor = "#ff8000";
                                break;
                            case "Pumpy":
                                tagElement.style.backgroundColor = "#0080ff";
                                break;
                            case "Dyno":
                                tagElement.style.backgroundColor = "#00ffff";
                                break;
                            case "Classic":
                                tagElement.style.backgroundColor = "#aaaaaa";
                                break;
                            case "Unique":
                                tagElement.style.backgroundColor = "#ff0080";
                                break;
                        
                            default:
                                break;
                        }
                        tagsElement.appendChild(tagElement);
                    });
                }
            } else {
                console.error('Element with id "climb-attempt-tags" not found');
            }
            climbLogDisplayArea.append(clone);

        })
    }

    function renderPagination() {
        const totalPages = Math.ceil(logs.length / itemsPerPage);
        paginationArea.textContent = ''; // Clear the pagination area

        const prevButton = document.createElement('button');
        prevButton.id = 'prev-button';
        prevButton.textContent = '<';
        prevButton.style.width = '50px'; 
        prevButton.style.height = '50px';
        prevButton.addEventListener('click', () => {
            if (currentPage > 1) {
                currentPage--;
                renderItems();
                renderPagination();
            }
        });
        paginationArea.appendChild(prevButton);

        const currentButton = document.createElement('button');
        currentButton.id = 'current-button';
        currentButton.textContent = currentPage;
        currentButton.style.width = '50px'; 
        currentButton.style.height = '50px'; 
        paginationArea.appendChild(currentButton);

        const nextButton = document.createElement('button');
        nextButton.id = 'next-button';
        nextButton.textContent = '>';
        nextButton.style.width = '50px';
        nextButton.style.height = '50px';
        nextButton.addEventListener('click', () => {
            if (currentPage < totalPages) {
                currentPage++;
                renderItems();
                renderPagination();
            }
        });
        paginationArea.appendChild(nextButton);
    }

    if (user == "true" && logs != null && logs.length > 0) {
        renderItems();
        renderPagination();
    }
    else {
        const emptyLogMessage = document.createElement('div');
        emptyLogMessage.style.width = '622px';
        emptyLogMessage.style.height = '600px';
        emptyLogMessage.style.backgroundColor = 'white';
        emptyLogMessage.style.display = 'flex';
        emptyLogMessage.style.justifyContent = 'center';
        emptyLogMessage.style.alignItems = 'flex-start';
        emptyLogMessage.style.paddingTop = '60px';
        emptyLogMessage.style.margin = 'auto';
        emptyLogMessage.style.textAlign = 'center';
        emptyLogMessage.style.fontSize = '22px';
        emptyLogMessage.style.fontWeight = 'bold';
        emptyLogMessage.textContent = 'Attempt some climbs to see this fill up!';
        climbLogDisplayArea.append(emptyLogMessage);
    }
}

async function getClimbTags(climbAttemptId){
    console.log("getting tags");
    const tags = fetchClimbTags(climbAttemptId);
    console.log(tags);
    return tags;
}

async function fetchClimbTags(climbAttemptId){
    var url = `/api/ClimbTagEntryApi/log/view/${climbAttemptId}`;
    var response = await fetch(url);
    var result = await response.json();
    console.log('fetchclimbtags: ', result);
    return result;
}

// group page -- message submission

export async function populateGroupComments(messages) {

    const templateId = document.getElementById("comment-template");
    const commentArea = document.getElementById("comment-area");
    
    messages.forEach(message => {
        const clone = templateId.content.cloneNode(true);
        const commentName = clone.getElementById("comment-name");
        const commentMessage = clone.getElementById("comment-message");

        commentName.textContent = message.displayName;
        commentMessage.textContent = message.message;

        commentArea.appendChild(clone);
    });
}

export async function displayGroupClimbingLog(logs) {

    console.log(logs);
    const climbLogTemplate = document.getElementById("climb-log-template");
    const climbLogDisplayArea = document.getElementById("display-log-list");
    const paginationArea = document.getElementById("pagination-area");
    const itemsPerPage = 6;
    let currentPage = 1;

    function renderItems() {
        if (logs.length === 0 || logs == null || logs.message == "No climb attempts logged or found so far.") {
            return;
        }
        climbLogDisplayArea.textContent = '';
        const startIndex = (currentPage - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;
        const itemsToDisplay = logs.slice(startIndex, endIndex);

        itemsToDisplay.forEach(async log => {
            //Setup
            const clone = climbLogTemplate.content.cloneNode(true);
            console.log(log.rating);

            //ClimbName
            const attemptNameElement = clone.getElementById("climb-attempt-name");
            attemptNameElement.textContent = log.climbName;

            const attemptClimberNameElement = clone.getElementById("climb-attempt-climber-name");
            attemptClimberNameElement.textContent = log.climberName;

            // EntryDate
            const attemptDateElement = clone.getElementById("climb-attempt-date");
            let options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' };
            attemptDateElement.textContent = new Date(log.entryDate).toLocaleString('en-US', options);

            // SuggestedGrade
            const attemptGradeElement = clone.getElementById("climb-attempt-grade");
            attemptGradeElement.textContent = log.suggestedGrade;

            // Climbing Attempts
            const attemptsNumElement = clone.getElementById("climb-attempt-attempts");
            attemptsNumElement.textContent = log.attempts;

            // Rating
            const attemptRatingElement = clone.getElementById("climb-attempt-rating");
            const starRating = log.rating;
            for (let i = 0; i < starRating; i++) {
                let img = document.createElement('img');
                img.src = "/images/star.svg";
                img.width = 40;
                img.height = 40;
                attemptRatingElement.appendChild(img);
            }

            // Link 4 Button
            const attemptLinkElement = clone.getElementById("climb-attempt-link-button");
            attemptLinkElement.href = `/Locations/Climbs/${log.climbId}`;

            

            // Tags
            const tags = await getClimbTags(log.id);
            console.log('tags',tags);
            const tagsElement = clone.getElementById("climb-attempt-tags");

            // Check if tagsElement is not null before proceeding
            if (tagsElement) {
                if (tags.length === 0) {
                    const noTagsElement = document.createElement('span');
                    noTagsElement.className = "badge climbTagBadge";
                    noTagsElement.textContent = "No Tags";
                    tagsElement.appendChild(noTagsElement);
                }
                else{
                    tags.forEach(tag => {
                        console.log(tag);
                        const tagElement = document.createElement('span');
                        tagElement.className = "badge climbTagBadge";
                        tagElement.textContent = tag;
                        switch (tag) {
                            case "Crimpy":
                                tagElement.style.backgroundColor = "#ff0000";
                                break;
                            case "Slopers":
                                tagElement.style.backgroundColor = "#ff4000";
                                break;
                            case "Pockets":
                                tagElement.style.backgroundColor = "#ff8000";
                                break;
                            case "Juggy":
                                tagElement.style.backgroundColor = "#ffbf00";
                                break;
                            case "Pinches":
                                tagElement.style.backgroundColor = "#80ff00";
                                break;
                            case "Technical":
                                tagElement.style.backgroundColor = "#ff00ff";
                                break;
                            case "Powerful":
                                tagElement.style.backgroundColor = "#00ff00";
                                break;
                            case "Compression":
                                tagElement.style.backgroundColor = "#00ff80";
                                break;
                            case "Highball":
                                tagElement.style.backgroundColor = "#8000ff";
                                break;
                            case "Slab":
                                tagElement.style.backgroundColor = "#0000ff";
                                break;
                            case "Tension":
                                tagElement.style.backgroundColor = "#ff8000";
                                break;
                            case "Pumpy":
                                tagElement.style.backgroundColor = "#0080ff";
                                break;
                            case "Dyno":
                                tagElement.style.backgroundColor = "#00ffff";
                                break;
                            case "Classic":
                                tagElement.style.backgroundColor = "#aaaaaa";
                                break;
                            case "Unique":
                                tagElement.style.backgroundColor = "#ff0080";
                                break;
                        
                            default:
                                break;
                        }
                        tagsElement.appendChild(tagElement);
                    });
                }
            } else {
                console.error('Element with id "climb-attempt-tags" not found');
            }
            climbLogDisplayArea.append(clone);

        })
    }

    function renderPagination() {
        const totalPages = Math.ceil(logs.length / itemsPerPage);
        paginationArea.textContent = ''; // Clear the pagination area

        const prevButton = document.createElement('button');
        prevButton.id = 'prev-button';
        prevButton.textContent = '<';
        prevButton.style.width = '50px'; 
        prevButton.style.height = '50px';
        prevButton.addEventListener('click', () => {
            if (currentPage > 1) {
                currentPage--;
                renderItems();
                renderPagination();
            }
        });
        paginationArea.appendChild(prevButton);

        const currentButton = document.createElement('button');
        currentButton.id = 'current-button';
        currentButton.textContent = currentPage;
        currentButton.style.width = '50px'; 
        currentButton.style.height = '50px'; 
        paginationArea.appendChild(currentButton);

        const nextButton = document.createElement('button');
        nextButton.id = 'next-button';
        nextButton.textContent = '>';
        nextButton.style.width = '50px';
        nextButton.style.height = '50px';
        nextButton.addEventListener('click', () => {
            if (currentPage < totalPages) {
                currentPage++;
                renderItems();
                renderPagination();
            }
        });
        paginationArea.appendChild(nextButton);
    }

    if (logs && logs.message != "No climb attempts logged or found so far.") {
        renderItems();
        renderPagination();
    }
    else {
        
        const emptyLogMessage = document.createElement('div');
        emptyLogMessage.style.width = '622px';
        emptyLogMessage.style.height = '600px';
        emptyLogMessage.style.backgroundColor = 'white';
        emptyLogMessage.style.display = 'flex';
        emptyLogMessage.style.justifyContent = 'center';
        emptyLogMessage.style.alignItems = 'flex-start';
        emptyLogMessage.style.paddingTop = '60px';
        emptyLogMessage.style.margin = 'auto';
        emptyLogMessage.style.textAlign = 'center';
        emptyLogMessage.style.fontSize = '22px';
        emptyLogMessage.style.fontWeight = 'bold';
        emptyLogMessage.textContent = 'No climbs found!';
        emptyLogMessage.style.overflowY = 'hidden';
        climbLogDisplayArea.append(emptyLogMessage);
        
    }
}


import { toggleLoadingSpinner } from "/js/ui.js";
import { displayErrorMessage } from "/js/ui.js";
import { SubmitTags } from "/js/climbsTags.js";


// location pages for areas and climbs functionality
export async function fetchAreas(query, loadingSpinner, validationWarning)
{
    const url = `/api/locations/search/${query}`
    console.log(url);

    const TIMEOUT = 12000;
    const timeoutPromise = new Promise((resolve, reject) => 
    {
        setTimeout(() => 
        {
            reject(new Error("Request timed out"));
        }, TIMEOUT);

    });

    let response;
    try {
        response = await Promise.race([fetch(url), timeoutPromise]);
    }
    catch (error) {
        console.error(error);
        displayErrorMessage("Request timed out. Please try again.", validationWarning);
        toggleLoadingSpinner(loadingSpinner);
        return;
    }
    
    console.log(response);
    const result = await response.json();
    console.log(result);
    const areas = result.areas;
    console.log(areas);

    if (areas === null || areas.length === 0) {
        displayErrorMessage("No results found.", validationWarning, loadingSpinner);
        return;
    }

    return areas;
}
export async function getAreaData(id)
{
    const url = `/api/locations/search/area/${id}`
    
    const response = await fetch(url);
    const result = await response.json();

    if (result === null)
    {
        displayErrorMessage("No results found.");
        return;
    }
    return result;
}
export async function fetchClimbs(query, loadingSpinner, validationWarning)
{
    const url = `/api/locations/search/climbs/${query}`
    console.log(url);

    const TIMEOUT = 12000;
    const timeoutPromise = new Promise((resolve, reject) => 
    {
        setTimeout(() => 
        {
            reject(new Error("Request timed out"));
        }, TIMEOUT);

    });

    let response;
    try {
        response = await Promise.race([fetch(url), timeoutPromise]);
    }
    catch (error) {
        console.error(error);
        displayErrorMessage("Request timed out. Please try again.", validationWarning);
        //toggleLoadingSpinner(loadingSpinner);
        return;
    }
    
    console.log(response);
    const result = await response.json();
    console.log(result);
    const areas = result.areas;
    console.log(areas);

    if (areas === null || areas.length === 0) {
        displayErrorMessage("No results found.", validationWarning, loadingSpinner);
        return;
    }

    const allClimbs = areas.flatMap(area => area.climbs);
    const matchingClimbs = allClimbs.filter(climb => climb.name.includes(query));
    console.log(matchingClimbs);

    let climbs = matchingClimbs.slice(0, 12);

    // make an api to get full climb data for each climb
    climbs = await fetchClimbData(climbs);

    return climbs;
}
export async function fetchAreaAncestors(ancestors)
{
    if (ancestors === null || ancestors.length === 0)
    {
        return [];
    }

    const ancestorsPromises = ancestors.map(async ancestor => { 
        const id = ancestor;
        const url = `/api/locations/search/area/ancestors/${id}`;
        const response = await fetch(url);
        const result = await response.json();
        return result;
    });

    const ancestorsAreas = await Promise.all(ancestorsPromises);
    console.log(ancestorsAreas);
    
    return ancestorsAreas;
}
export async function fetchClimbData(climbs)
{
    if (climbs === null || climbs.length === 0)
    {
        return [];
    }

    const climbsPromise = climbs.map(async climb => {
        const id = climb.uuid;
        const url = `/api/locations/climb/${id}`;
        const response = await fetch(url);
        const result = await response.json(); 
        console.log(result);
        return result.climb
    });
    const updatedClimbs = await Promise.all(climbsPromise);
    console.log(climbs);
    return updatedClimbs;
}

export async function fetchClimbDataById(id)
{
    const url = `/api/locations/climb/${id}`;
    const response = await fetch(url);
    const result = await response.json();
    return result;

}

// climb attempts functionality
export async function postClimbAttempt(climbId, climbName, suggestedGrade, attempts, rating) 
{
    console.log("api.js postClimbAttempt");
    var url = '/api/climb/log/record';
    var data = {
        "id": 0,
        "climbId": climbId,
        "climbName": climbName,
        "suggestedGrade": suggestedGrade,
        "attempts": parseInt(attempts), // Convert to integer
        "rating": parseInt(rating),
        "entryDate": new Date().toISOString()
      };
    
    console.log(data);
        try {
            var response = await fetch(url, {
                method: 'POST',
                headers: {
                  'accept': '*/*',
                  'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
              });
      
          console.log("Request sent with data:", data); // Log data before sending
      
          if (response.ok) {
            const responseData = await response.json();
            console.log("api.js response:", responseData.id);
            if (responseData.id) {
              SubmitTags(responseData.id);
            } else {
              console.error('Climb attempt has not been submitted yet');
            }
          } else {
            console.error('Failed to post climb attempt', response);
            // Try to extract more details from the response if possible
            try {
              const errorData = await response.json();
              console.error('Server error details:', errorData);
            } catch (error) {
              console.error('Could not parse server error response');
            }
          }
        } catch (error) {
          console.error('Error during fetch:', error);
        }
}
export async function getClimbAttempts()
{
    const url = '/api/climb/log/view';
    const response = await fetch(url);
    const result = response.json();
    return result;
}

// community messages functionality
export async function getAllMessagesApiCall(groupId)
{
    const url = `/api/community/group/messages/${groupId}`;
    const response = await fetch(url);
    const result = response.json();
    return result;
}
export async function postMessage(comment, groupId)
{
    const url = `/api/community/group/${groupId}/messages/${comment}`;
    const response = await fetch(url, { method: "POST" });
    const result = response.json();
    return result;
}

// community climbing log functionality
export async function getClimbersFromGroupId(groupID)
{
    // get climbers from group id
    const url = `/api/community/members/group/${groupID}/list`;
    const response = await fetch(url);
    const result = response.json();
    return result;
}

export async function getCommunityClimbingLog(climbers)
{
    // get climbing log for climbers
    const url = `/api/climb/log/view/list/${climbers}`;
    const response = await fetch(url, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(climbers)
    });
    const result = response.json();
    return result;
}
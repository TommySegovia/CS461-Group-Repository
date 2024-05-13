document.addEventListener("DOMContentLoaded", async function () {
    console.log("climbsTags.js loaded");
    const addTagButton = document.getElementById("addTagButton");
    if (addTagButton === null) {
        return;
    }
    addTagButton.addEventListener("click", AddTag);
});


function AddTag() {
    console.log("adding tag");
    // Create a new select element
    var select = document.createElement('select');

    select.className = "climbTags";
    
    // Define the options
    var options = [
        { value: 1, text: 'Crimpy' },
        { value: 2, text: 'Slopers' },
        { value: 3, text: 'Pockets' },
        { value: 4, text: 'Juggy' },
        { value: 5, text: 'Pinches' },
        { value: 6, text: 'Technical' },
        { value: 7, text: 'Powerful' },
        { value: 8, text: 'Compression' },
        { value: 9, text: 'Highball' },
        { value: 10, text: 'Slab' },
        { value: 11, text: 'Tension' },
        { value: 12, text: 'Pumpy' },
        { value: 13, text: 'Dyno' },
        { value: 14, text: 'Classic' },
        { value: 15, text: 'Unique' }
    ];

    // Add the options to the select element
    for (var i = 0; i < options.length; i++) {
        var option = document.createElement('option');
        option.value = options[i].value;
        option.text = options[i].text;
        select.appendChild(option);
    }

    // Append the select element to the form
    document.getElementById('climb-tag-div').appendChild(select);
}

async function SubmitTags(climbAttemptId) {
    console.log("submitting tags");
    var tags = [];
    var tagElements = document.getElementsByClassName("climbTags");
    for (var i = 0; i < tagElements.length; i++) {
        tags.push(tagElements[i].value);
    }
    console.log(tags);
    await postClimbTags(climbAttemptId, tags);
}

async function postClimbTags(climbAttemptId, tags) {
    var url = '/api/ClimbTagEntryApi/log/record';
    await climbAttemptId;
    if (climbAttemptId)
        {
            for (var i = 0; i < tags.length; i++) {
                var data = {
                    "id": 0,
                    "climbAttemptID": climbAttemptId,
                    "tagID": tags[i]
                };
                console.log(data);
                var response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
                console.log(response);
                if (response.ok) {
                    console.log("Tag added successfully");
                } else {
                    console.log("Tag failed to add");
                }
            }
        }
    
}

export { SubmitTags };
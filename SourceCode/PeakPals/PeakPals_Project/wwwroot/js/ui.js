

export function displayErrorMessage(message, validationWarning, loadingSpinner)
{
    const errorMessage = document.createElement("p");
    errorMessage.textContent = message;
    errorMessage.classList.add("error-message");
    validationWarning.appendChild(errorMessage);
    //toggleLoadingSpinner(loadingSpinner);
    return;
}

export function toggleLoadingSpinner(loadingSpinner)
{
    if (loadingSpinner.style.display == 'none')
    {
        loadingSpinner.style.display = 'block';
    }
    else if (loadingSpinner.style.display == 'block')
    {
        loadingSpinner.style.display = 'none';
    }
}
function getRequest(uri) {
    fetch(uri)
        .then(response => {
            return response.json();
        })
        .then(data => {
            return data;
        })
        .catch(error => console.error('Unable to get data.', error));
}

function postRequest(uri, item) {
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            return response.json();
        })
        .catch(error => console.error('Unable to get data.', error));
}

function putRequest(uri, item) {
    fetch(uri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            return response.json();
        })
        .catch(error => console.error('Unable to get data.', error));
}

function deleteRequest(uri) {
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => {
            return response.json();
        })
        .catch(error => console.error('Unable to get data.', error));
}

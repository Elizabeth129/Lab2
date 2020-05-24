const uri = 'api/Performances';
let categories = [];

const uri2 = 'api/TypeOfPerformanceCollections';
let types = [];

const uri3 = 'api/Theaters';
let theaters = [];

function getCategories() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCategories(data))
        .catch(error => console.error('Unable to get categories.', error));
}

function getTypes() {
    fetch(uri2)
        .then(response => response.json())
        .then(data => displayTypes(data))
        .catch(error => console.error('Unable to get categories.', error));
}

function getTheaters() {
    fetch(uri3)
        .then(response => response.json())
        .then(data => displayTheater(data))
        .catch(error => console.error('Unable to get categories.', error));
}

function addCategory() {
    const addNameTextbox = document.getElementById('add-name');
    const addTypeTextbox = document.getElementById('add-type');
    const addTheaterTextbox = document.getElementById('add-theater');

    const category = {
        name: addNameTextbox.value.trim(),
        typeOfPerformanceId: parseInt(addTypeTextbox.value.trim(), 10),
        theaterId: parseInt(addTheaterTextbox.value.trim(), 10)
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
      //  .then(response => response.json())
        .then(() => {
            getCategories();
            addNameTextbox.value = '';
            addTypeTextbox.value = '';
            addTheaterTextbox.value = '';
        })
        .catch(error => console.error('Unable to add category.', error));
}


function deleteCategory(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to delete category.', error));
}

function displayEditForm(id) {
    const category = categories.find(category => category.id === id);

    document.getElementById('edit-id').value = category.id;
    document.getElementById('edit-name').value = category.name;
    document.getElementById('edit-type').value = category.typeOfPerformanceId;
    document.getElementById('edit-theater').value = category.theaterId;



    document.getElementById('editForm').style.display = 'block';
}

function updateCategory() {
    const categoryId = document.getElementById('edit-id').value;
    const category = {
        id: parseInt(categoryId, 10),
        name: document.getElementById('edit-name').value.trim(),
        theaterId: parseInt( document.getElementById('edit-type').value.trim(), 10),
        typeOfPerformanceId: parseInt( document.getElementById('edit-theater').value.trim(), 10)
       
    };

    fetch(`${uri}/${categoryId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to update category.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayCategories(data) {
    const tBody = document.getElementById('categories');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(category => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${category.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCategory(${category.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNodeName = document.createTextNode(category.name);
        td1.appendChild(textNodeName);

        let td2 = tr.insertCell(1);
        let textNodeSurname = document.createTextNode(category.typeOfPerformanceId);
        td2.appendChild(textNodeSurname);

        let td3 = tr.insertCell(2);
        let textNodeDate = document.createTextNode(category.theaterId);
        td3.appendChild(textNodeDate);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    categories = data;
}

function displayTypes(data) {
    const tBody = document.getElementById('types');
    tBody.innerHTML = '';
    data.forEach(type => {
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(type.id);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(type.typeOfPerformanceName);
        td2.appendChild(textNodeInfo);
    });
    types = data;
}


function displayTheater(data) {
    const tBody = document.getElementById('theater');
    tBody.innerHTML = '';
    data.forEach(type => {
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(type.id);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(type.address);
        td2.appendChild(textNodeInfo);
    });
    theaters = data;
}
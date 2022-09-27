var a = 0;
var b = 0;

function inowner() {
    window.location.href = 'http://localserver33114sea:8080/EndProject/ChangeRole?Role=owner';
    a = 0;
    b = 0;
}

function outowner() {
    if(a === 3 && b === 2)
        window.location.href = 'http://localserver33114sea:8080/EndProject/ChangeRole';
    a = 0;
    b = 0;
}

function getWSREFS() {
    let url, filter;
    document.getElementById("tableWSREF").innerHTML = "";
    url = 'http://localserver33114sea:8080/EndProject/ManagmentWSREF';
    if(document.getElementById("filterDescription") != null)
        filter = document.getElementById("filterDescription").value;

    let role, sessionId;
    fetch(url, {
        method: 'GET',
        headers:{
            'Filter': filter
        }
    }).then(res => {
        sessionId = res.headers.get('sessionId');
        role = res.headers.get('Role');
        return res.json();
    }).then(res => {
        if (res) {
            res.forEach(element => {
                let wsrefs = `<tr><td>`;
                if (role != null && role == 'owner')
                    wsrefs += `<input type="button" value="delete" onclick="deleteContentVisibility('${element.id}', '${element.description}') ">
                                 <input type="button" value="update" onclick="updateContentVisibility('${element.id}','${element.url}','${element.description}')">`
                let links = '';
                let tagsarray = element.description.split(',');
                for (let i = 0; i < tagsarray.length; i++) {
                    links+=`<a href="${element.url}" onclick="searchTag('${tagsarray[i]}')">${tagsarray[i]}</a> `
                };

                wsrefs += `<input type="button" value="+${element.plus}" onclick="updateReference('${element.id}', '${element.url}','${element.description}', '${element.plus + 1}', '${element.minus}')">
                             <input type="button" value="-${element.minus}" onclick="updateReference('${element.id}', '${element.url}','${element.description}','${element.plus}', '${element.minus + 1}')">
                             <input type="button" value="comments" onclick="getCommentForReference(${element.id})">
                             [${element.id}]                             
                             ${links}
                             </td></tr>
                             <tr><td id="${element.id}">
                             </td></tr>`;
                document.getElementById("tableWSREF").innerHTML += wsrefs;
            });
        }
    });

}

function deleteContentVisibility(id, description) {
    let attention = `<table>
                        <tr><td>
                            ${description}
                            <br>
                            <input type="button" value="Yes" onclick="deleteReference(${id})">
                            <input type="button" value="Cancel" onclick="clearContent(${id})">
                        </td></tr>
                     </table>`;
    document.getElementById(id).innerHTML = attention;
}

function clearContent(id) {
    document.getElementById(id).innerHTML = "";
}

function deleteReference(id) {
    fetch('/EndProject/ManagmentWSREF?id='+id,{
        method : 'DELETE'
    }).then(()=>{
        getWSREFS();
    })
}

function updateContentVisibility(id,url,description, plus, minus) {
    let form = `<table>
                    <tr><td>
                        <p>Url:<input type="text" id="editUrl" value="${url}" ></p>
                        <p>Description:<input type="text" id="editDescription" value="${description}"></p>
                        <p>
                            <input type="button" onclick="updateReference(${id}, ${plus}, ${minus})" value="Update Reference">
                            <input type="button" value="Cancel" onclick="clearContent(${id})">
                        </p>
                    </td></tr>
            </table>`
    document.getElementById(id).innerHTML = form;
}

function updateReference(id, urlold,descriptionold, plus, minus) {

    let url, description;

    if (document.getElementById('editUrl') != null)
        url = document.getElementById('editUrl').value;
    else
        url = urlold;
    if (document.getElementById('editDescription') != null)
        description =  document.getElementById('editDescription').value;
    else
        description = descriptionold;
    fetch('/EndProject/ManagmentWSREF', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            id: id, url: url, description: description, plus : plus, minus: minus
        })
    }).then(()=>{
        getWSREFS();
    })
}

//insert data
function visibilityInsertForm() {
    let form = `<p>Url:<input type="text" id="url"></p>
            <p>Description:<input type="text" id="description"></p>
            <p>
                <input type="button" onclick="addReference()" value="Add Reference">
                <input type="button" onclick="clearContent('form')" value="Cancel">
            </p>`
    document.getElementById("form").innerHTML = form;
}

function visibilityFilterForm() {
    b = b + 1;
    let form = `<p>Description:<input type="text" id="filterDescription"></p>
                <p>
                    <input type="button" onclick="getWSREFS()" value="Filter">
                    <input type="button" onclick="clearContent('form')" value="Cancel">
                </p>`
    document.getElementById("form").innerHTML = form;
}

function addReference() {
    if (document.getElementById('url').value != "" && document.getElementById('description').value != "")
        fetch('/EndProject/ManagmentWSREF', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                url: document.getElementById('url').value,
                description: document.getElementById('description').value
            })
        }).then(()=>{
            clearContent("form")
            getWSREFS();
        })
    else
        alert('Input fields must be filled')
}


function getCommentForReference(id) {
    a = a  + 1;
    let url = 'http://localserver33114sea:8080/EndProject/ManagmentComments?refId='+id;
    let role, sessionId;
    fetch(url, {
        method: 'GET',
        headers: {
            'refId': id
        }
    }).then(res => {
        sessionId = res.headers.get('sessionId');
        role = res.headers.get('Role');
        return res.json();
    }).then(res => {
        let data = `<table>
                    <tr><td>
                        <h4>--UWSR COMMENTS/${id}--</h4>
                        <input type="button" onclick="visibilityCommentsInsertForm(${id})" value="insert">
                        <input type="button" onclick="clearContent(${id})" value="Close comments">
                        <div id="insertComment${id}"></div>
                    </td></tr>
                </table><br>`
        if (res) {
            res.forEach(element =>{
                let comments = `<table><tr><td>[${element.stamp}]`
                if(sessionId != null && element.sessionId == sessionId || role == 'owner')
                    comments += `<input type="button" value="delete" onclick="deleteComment(${element.id},${element.refId})">
                                 <input type="button" value="update" onclick="updateComment(${element.id},${element.refId})"><br>
                                 <textarea id="txt${element.id}">${element.comment}</textarea>`
                else
                    comments+= `<br><textarea readonly id="txt${element.id}">${element.comment}</textarea>`
                comments+= `</td></tr></table><br>`
                data += comments;
            })
        }
        document.getElementById(id).innerHTML = data;
    })
}

function visibilityCommentsInsertForm(id) {
    let needId = "insertComment"+id;
    let form = `<p>Comment:<input type="text" id="comment"></p>
                <p>
                    <input type="button" onclick="addComment(${id})" value="Add comment">
                    <input type="button" value="Cancel" onclick="clearContent('insertComment${id}')">
                </p>`
    document.getElementById(needId).innerHTML = form;
}

function deleteComment(id,refId) {
    fetch('http://localserver33114sea:8080/EndProject/ManagmentComments?id='+id,{
        method : 'DELETE'
    }).then(()=>{
        getCommentForReference(refId);
    })
}

function updateComment(id,refId) {
    let needId = 'txt'+id;
    fetch('http://localserver33114sea:8080/EndProject/ManagmentComments', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            id: id,
            comment: document.getElementById(needId).value
        })
    }).then(()=>{
        getCommentForReference(refId);
    })
}


function addComment(id) {
    fetch('http://localserver33114sea:8080/EndProject/ManagmentComments', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            comment: document.getElementById('comment').value,
            refId: id.toString(),
            sessionId : ""
        })
    }).then(()=>{
        clearContent(id);
        getCommentForReference(id);
    })
}
function CreateNewDir(val) {

    $.ajax({
        url: 'http://localserver33114sea:8080/WebDAV/FileManager',
        type: 'GET',
        dataType: 'json',
        headers: {
            'TypeAction' : "CreateNewDirectory",
            'NewDir': val.value
        }
    }).done(response => {
        UpdateFilesAndDirectorys("");
    });
    setTimeout(() => UpdateFilesAndDirectorys(""), 300);
}

function DownLevelDirectory() {
    $.ajax({
        url: 'http://localserver33114sea:8080/WebDAV/FileManager',
        type: 'GET',
        dataType: 'json',
        headers: {
            'TypeAction': "DownLevelDirectory",
        }
    });
    setTimeout(() => UpdateFilesAndDirectorys(""), 300);
}

function UpdateFilesAndDirectorys(val) {
    console.log(val.toString());
    if(val.toString().lastIndexOf("/") != val.toString().length - 1){
        Downloadfile(val.toString());
        console.log(val.toString());
        return;
    }

    $("#fileAndDirectory").empty();

    $.ajax({
        url: 'http://localserver33114sea:8080/WebDAV/FileManager',
        type: 'GET',
        dataType: 'json',
        headers: {
            'TypeAction': "UpLevelOrOpen",
            'File': val.toString()
        }
    }).done(response => {
        var firstitem = ' ';
        var counter = 0;
        response.numbers.forEach((item) => {
            if(counter === 0){
                firstitem = item.toString();
                counter++;
            }
            else{
                if (item.toString().lastIndexOf('/') === item.toString().length - 1)
                    document.getElementById("fileAndDirectory").innerHTML += '<li style="background: transparent;" > ' +
                        '<button style="width: 150px; height: 150px" ondblclick="UpdateFilesAndDirectorys(value)"  value="'+ item  +'">' +
                        '<figure class="sign"><p>' +
                        '<img style="width: 70px; height: 70px; " src="https://png.pngtree.com/png-vector/20190916/ourlarge/pngtree-folder-icon-for-your-project-png-image_1731108.jpg"/>' +
                        '</p><figcaption><input type="checkbox" id="dirorfile_check" value="' + item.toString() + '">'+ item.toString().replace(firstitem, '').replace('/', '')  +'</figcaption>' +
                        '  </figure>' +
                        '</button></li>';
                else
                    document.getElementById("fileAndDirectory").innerHTML += '<li style="background: transparent;" > ' +
                        '<button style="width: 150px; height: 150px" ondblclick="UpdateFilesAndDirectorys(value)"  value="'+ item.toString()  +'">' +
                        '<figure class="sign"><p>' +
                        '<img style="width: 60px; height: 60px; " src="https://img.icons8.com/ios/452/file--v1.png"/>' +
                        '</p><figcaption><input type="checkbox" id="dirorfile_check" value="' + item.toString() + '"/>'+ item.toString().replace(firstitem, '')  +'</figcaption>' +
                        '  </figure>' +
                        '</button></li>';

            }

        });
    });
}

function RemoveFileOrDir() {

    var removeElement=[]; // initialise an empty array

    $('#fileAndDirectory input:checked').each(function() {
        removeElement.push($(this).attr('value').toString());  //
    });

    const output = removeElement.join(', ');

    $.ajax({
        url: 'http://localserver33114sea:8080/WebDAV/FileManager',
        type: 'POST',
        dataType: 'json',
        headers: {
            'TypeAction': 'RemoveElement',
            'RemoveElements': output
        },
        async: false
    }).done(response => {
        setTimeout(() => UpdateFilesAndDirectorys(""), 1000);
    });


}

function DownloadFileOrDir() {

    $('#fileAndDirectory input:checked').each(function() {
        setTimeout(() => Downloadfile($(this).attr('value').toString()), 1000);
    });

}

function Downloadfile(file) {

    window.location.href = "http://localserver33114sea:8080/WebDAV/FileManager?type=download-file&FileName=" + file.toString();

}

var copyElement=[];

function saveCopyFiles() {

    copyElement = [];

    $('#fileAndDirectory input:checked').each(function() {
        copyElement.push($(this).attr('value').toString());
    });
    setTimeout(() => alert("Files copied in buffer"), 10);

}


function CopyFiles() {
    if(copyElement.length != 0)
        var output = copyElement.join(', ');
    else
        return;

    $.ajax({
        url: 'http://localserver33114sea:8080/WebDAV/FileManager',
        type: 'POST',
        dataType: 'json',
        headers: {
            'TypeAction': 'CopyFile',
            'CopyElements': output
        },
        async: false
    }).done(response => {
        setTimeout(() => alert(response.mess.toString()), 1000);
        setTimeout(() => UpdateFilesAndDirectorys(''), 10);

    });

}

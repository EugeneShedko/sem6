function JQuery(n) {
    document.getElementById("jsonn").innerHTML = "";
    $.ajax({
        url: 'http://localserver33114sea:8080/AS_33114SEA/SssJSON',
        type: 'POST',
        dataType: 'json',
        headers: {
            'XRand-N': n,
        }
    }).done(response => {
        response.X.forEach((item) => {
            document.getElementById("jsonn").innerHTML += item.rand + " ";
        });
    });
}
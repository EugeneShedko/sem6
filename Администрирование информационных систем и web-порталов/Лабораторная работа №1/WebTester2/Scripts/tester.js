// Write your Javascript code.
function refreshSum() {
    location.href = "/Home/refreshSum?src=" + encodeURI(document.getElementById("address").value);
}
function addSummand() {
    location.href = "/Home/addSummand?src=" + encodeURI(document.getElementById("address").value) + "&summand=" + document.getElementById("summand").value;
}
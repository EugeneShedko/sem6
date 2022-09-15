// Write your Javascript code.
function refreshSum() {
    document.getElementById("sumIframe").src = document.getElementById("address").value;
}
function addSummand() {
    document.getElementById("addIframe").src = document.getElementById("address").value + "add?summand=" + document.getElementById("summand").value;
}
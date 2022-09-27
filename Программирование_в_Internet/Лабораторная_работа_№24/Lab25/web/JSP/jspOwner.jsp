<%--
  Created by IntelliJ IDEA.
  User: Jake
  Date: 29.05.2022
  Time: 16:48
  To change this template use File | Settings | File Templates.
--%>

<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html>
<head>
    <title>--UWSR--</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
    <script src="JS/UsingDataMSREF.js"></script>
    <script src="JS/UsingDataMSREFComments.js"></script>
    <link rel="icon" href="data:,">
</head>
<%
    String role = (String)session.getAttribute("Role");
    if (role == null || role.equals("") ||  !role.equals("owner")) {
        response.sendRedirect("http://localserver33114sea:8080/EndProject/Uwsref");
    }
    else{
%>
<body onload="getWSREFS()">
<h1>--UWSR--</h1>
<form method="post" action="ChangeRole">
    <input type="hidden" name="Role" value="guest">
    <input class="button" type="submit" value="Reset" >
</form>
<br>
<input type="button" value="insert" onclick="visibilityInsertForm()">
<input type="button" value="filter" onclick="visibilityFilterForm()">
<br>
<div id="form"></div>
<br>
<br>
<table id="tableWSREF">

</table>
</body>
<%
    }
%>
</html>

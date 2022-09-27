<%--
  Created by IntelliJ IDEA.
  User: Jake
  Date: 30.05.2022
  Time: 0:57
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
    <style>
    #container>div{
        display:inline-block;
    }
    #block{
        width: 200px;
        height: 150px;
        border:2px white solid;
        z-index : 10;
        position: absolute;
        margin-left: 85%;
        background: green;
    }
    </style>
</head>
<body onload="getWSREFS()">
<div id="block" onclick="inowner()"> </div>
<h1>--UWSR--</h1>
</div>
<!-- <div id="Search">
    <input class="button" type="button" value="Set2" onclick="inowner()" >
    <form method="post" action="ChangeRole">
        <input type="hidden" name="Role" value="guest">
    </form>
</div>
-->
<br>
<input type="button" value="filter" onclick="visibilityFilterForm()">
<br>
<div id="form"></div><br>
<br>
<table id="tableWSREF">

</table>

</body>
</html>

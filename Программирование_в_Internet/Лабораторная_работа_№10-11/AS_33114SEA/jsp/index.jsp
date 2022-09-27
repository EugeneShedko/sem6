<%@ page language = "java" contentType = "text/html" %>
<%@ page import="lab4.*" %>
<%@ page import = "java.util.Calendar" %>
<!DOCTYPE html>
<html>
<head>
<style>
table {
    border: 2px solid black;
    border-collapse: collapse;
    text-align: center;
}
td{
    border: 2px solid black;
}
</style>
<script>
    function InsertJSP(hour)
    {
        if(hour < 6 )
            document.write('<%@ include file ="night.jsp"%>');
        else if(hour >= 6 && hour < 9)
            document.write('<%@ include file ="morning.jsp"%>');
        else if(hour >= 9 && hour < 21)
                document.write('<%@ include file ="afternoon.jsp"%>');
            else
                document.write('<%@ include file ="evening.jsp"%>');
    }
    function SInsertJSP(hour)
    {
        if(hour < 6 )
            document.write('<jsp:include page ="night.jsp"/>');
        else if(hour >= 6 && hour < 9)
            document.write('<jsp:include page ="morning.jsp"/>');
        else if(hour >= 9 && hour < 21)
             {
                document.write('<jsp:include page ="../Afternoon"/>');
             }
            else
                document.write('<jsp:include page ="evening.jsp"/>');
    }
</script>
</head>
<body>
<%!
public String PrintMessage(Integer hour)
{
    if(hour < 6)
        return "<h1>Good night!</h1>";
    if(hour < 9)
        return "<h1>Good morning!</h1>";
    if(hour < 21)
        return "<h1>Good afternoon!</h1>";
    else
        return "<h1>Good evening!</h1>";
}
 %>
<%= PrintMessage(new Time().GetHour()) %>
<table>
<tr>
    <td>Date</td><td>DayNumber</td>
</tr>
<!-- Таблица! -->
<%
    Calendar calendar = Calendar.getInstance();
    Integer dayNumber;
    String day;
    String month;
    for(int i = 0; i < 7; i++)
    {
        dayNumber = calendar.get(Calendar.DAY_OF_WEEK) == 1? 7 : calendar.get(Calendar.DAY_OF_WEEK) - 1;
        month = Integer.toString(calendar.get(Calendar.MONTH) + 1).length() == 1? "0" + Integer.toString(calendar.get(Calendar.MONTH)):Integer.toString(calendar.get(Calendar.MONTH));
        day = Integer.toString(calendar.get(Calendar.DAY_OF_MONTH) + 1).length() == 1? "0" + Integer.toString(calendar.get(Calendar.DAY_OF_MONTH)):Integer.toString(calendar.get(Calendar.DAY_OF_MONTH));
%>
    <tr>
    <td> <%= day %>.<%= month%>.<%= calendar.get(Calendar.YEAR) %> </td><td><%= dayNumber %></td>
    </tr>
<%
    calendar.roll(Calendar.DAY_OF_YEAR, 1);
} %>
</table>
<br><br>
<button onclick = "InsertJSP(<%= new Time().GetHour()%>)">Press</button>
<br><br>
<button onclick = "SInsertJSP(<%= new Time().GetHour()%>)">Press</button>
<br><br>
<a href = "Ttt.jsp" > NextLab </a>
</body>
</html>
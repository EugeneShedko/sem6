<%@ page language = "java" contentType = "text/html"  import= "java.util.*" %>
<!DOCTYPE html>
<html>
<head>
</head>
<body>
<%
ServletContext context = getServletContext();
Enumeration en = context.getInitParameterNames();
String parameterName;
while(en.hasMoreElements())
{
    parameterName = (String)en.nextElement(); %>
    <%=parameterName + ":"%><%= context.getInitParameter(parameterName) %><br>
<%}%>
</body>
</html>
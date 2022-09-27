<%@ page language = "java" contentType = "text/html"%>
<%@ page import = "java.util.*" %>
<%@ page import = "lab6.*" %>
<%
    CBean object = (CBean)getServletContext().getAttribute("atrCBean");
%>
<%=object.getValue1()%>
<br>
<%=object.getValue2()%>
<br>
<%=object.getValue3()%>
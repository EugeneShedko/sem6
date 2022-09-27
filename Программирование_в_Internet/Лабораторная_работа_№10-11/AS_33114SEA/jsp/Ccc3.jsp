<%@ page language = "java" contentType = "text/html"%>
<%@ page import = "java.util.*" %>
<%@ page import = "lab6.*" %>
<%
        CBean object = (CBean)request.getSession().getAttribute(request.getSession().getId());
%>
Session id: <%=request.getSession().getId() %>
<br>
<%=object.getValue1()%>
<br>
<%=object.getValue2()%>
<br>
<%=object.getValue3()%>
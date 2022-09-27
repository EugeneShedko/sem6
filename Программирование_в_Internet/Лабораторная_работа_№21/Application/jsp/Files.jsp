<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1" import= "lab22.*" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Insert title here</title>
</head>
<body>
<%
	String d = (String)getServletContext().getInitParameter("FileDirectory");
	ChoiseXXX ch = new ChoiseXXX(d, "docx");
	String ll = null;
	for (int i = 0; i < ch.listxxx.length; i++) {
	   ll = ch.listxxx[i];
%>
<br />
<a href="../Sss2?ilfe=<%=ll%>"> <%=ll%> </a>
<%}%>
<br/><br/>
<form method="post" action="../Sss2" enctype="multipart/form-data">
    <input type="file" name="file"/>
    <button type="submit">Upload</button>
</form>

</body>
</html>

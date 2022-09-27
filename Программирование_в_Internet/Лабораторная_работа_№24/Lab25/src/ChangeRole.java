import DbUse.DBConnection;
import Model.WSREF;

import javax.servlet.Servlet;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;

@WebServlet(name = "ChangeRole")
public class ChangeRole extends HttpServlet implements Servlet {
    @Override
    public void init() throws ServletException {

    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        session.setAttribute("Role", "guest");
        response.sendRedirect("http://localserver33114sea:8080/EndProject/Uwsref");

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        session.getAttribute("Role");
        session.setAttribute("Role", "owner");
        response.sendRedirect("http://localserver33114sea:8080/EndProject/Uwsref");
    }


}

import DbUse.DBConnection;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.SQLException;

@WebServlet(name = "Uwsref")
public class Uwsref extends HttpServlet {
    @Override
    public void init() throws ServletException {
        try {
            DBConnection.connectToDB();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String role = (String)request.getSession().getAttribute("Role");
        if (role != null && !role.equals("") && role.equals("owner"))
            //что за переопределение?
            getServletContext().getRequestDispatcher("/JSP/jspOwner.jsp").forward(request, response);
        else
            getServletContext().getRequestDispatcher("/JSP/jspGuest.jsp").forward(request, response);

    }
}

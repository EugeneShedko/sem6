package lab3;

import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;
import java.io.PrintWriter;

public class Ggg extends HttpServlet {
    public Ggg()
    {
        super();
    }

    public  void doGet(HttpServletRequest req, HttpServletResponse res)  throws ServletException, IOException
    {
        System.out.println("Ggg: doGet");
        res.getWriter().println("<html><body>firstname: " + req.getParameter("firstname") + "<br>secondname: " +
                req.getParameter("secondname") + "</body></html>");
        String stringParameter = "firstname=" + req.getParameter("firstname") + "&" + "secondname=" + req.getParameter("secondname");
        res.sendRedirect("http://localserver33114sea:8080/AS_33114SEA/html/adress.html?" + stringParameter);
        //req.getRequestDispatcher("html/adress.html").forward(req, res);
    }

    public void doPost(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException
    {
        System.out.println("Ggg: doPost");
        res.getWriter().println("<html><body>firstname: " + req.getParameter("firstname") + "<br>secondname: " +
                req.getParameter("secondname") + "</body></html>");
        String stringParameter = "firstname=" + req.getParameter("firstname") + "&" + "secondname=" + req.getParameter("secondname");
        res.sendRedirect("http://localserver33114sea:8080/AS_33114SEA/html/adress.html?" + stringParameter);
        //req.getRequestDispatcher("html/adress.html").forward(req, res);
    }
}

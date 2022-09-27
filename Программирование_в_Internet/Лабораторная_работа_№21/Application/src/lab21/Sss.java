package lab21;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Sss extends HttpServlet {
    public Sss()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        res.getWriter().println("Servlet:SSS");
    }
}

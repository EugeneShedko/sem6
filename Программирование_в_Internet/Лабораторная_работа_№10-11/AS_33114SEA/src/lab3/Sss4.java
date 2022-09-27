package lab3;

import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;

public class Sss4 extends HttpServlet {
    public Sss4()
    {
        super();
        System.out.println("Sss: constructor");
    }

    public void init(ServletConfig sc) throws ServletException  {
        super.init();
        System.out.println("Sss: init");
    }

    public void destroy() {
        super.destroy();
        System.out.println("Sss: destroy");
    }

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        if(rq.getMethod().equals("GET")) {
            doGet(rq,rs);
        }
        if(rq.getMethod().equals("POST")){
            doPost(rq,rs);
        }
    }
    protected void doPost(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException{
        System.out.println("Sss2: doPost");
        String stringParameter = "firstname=" + req.getParameter("firstname") + "&" + "secondname=" + req.getParameter("secondname");
        res.getWriter().println("<html><body>firstname: " + req.getParameter("firstname") + "<br>secondname: " +
                req.getParameter("secondname") + "</body></html>");
        res.sendRedirect("http://remoteserver33114sea:8080/AS_33114SEA/Ggg?" + stringParameter);
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        System.out.println("Sss2: doGet");
        String parameterString = "firstname="+ req.getParameter("firstname") + "&" +"secondname=" +
                req.getParameter("secondname");
        res.sendRedirect("http://remoteserver33114sea:8080/AS_33114SEA/Ggg?" + parameterString);
    }
}
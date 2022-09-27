package lab3;
import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;

public class Sss5 extends HttpServlet {
    public Sss5()
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
        System.out.println("Sss: doPost");
        res.getWriter().println("<html><body>firstname: " + req.getParameter("firstname") + "<br>secondname: " +
                req.getParameter("secondname") + "</body></html>");
        req.getRequestDispatcher("http://remoteserver33114sea:8080/AS_33114SEA/Ggg").forward(req, res);
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        System.out.println("Sss: doGet");
        String parameterString = "firstname="+ req.getParameter("firstname") + "&" +"secondname=" +
                req.getParameter("secondname");
        RequestDispatcher rd = req.getRequestDispatcher("http://remoteserver33114sea:8080/AS_33114SEA/Ggg?"  + parameterString);
        rd.forward(req, res);
    }
}

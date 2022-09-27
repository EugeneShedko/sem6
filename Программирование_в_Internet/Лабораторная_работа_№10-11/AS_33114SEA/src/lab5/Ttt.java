package lab5;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Ttt extends HttpServlet {
    public  Ttt()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    protected void service(HttpServletRequest req, HttpServletResponse res) throws IOException
    {
        String parameterString  = "surname: " + req.getParameter("surname") + "\n" + "lastname: " + req.getParameter("lastname") +
                "\n" + "sex: " + req.getParameter("sex");
        PrintWriter writer = res.getWriter();
        writer.println(parameterString);
    }
}

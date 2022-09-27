package lab8;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Enumeration;

public class Bbb extends HttpServlet {
    public Bbb()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    protected void service(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String parameterName = "";
        String headerName = "";
        int i = 0;

        resp.setHeader("header1", "value1");
        resp.setHeader("header2", "value2");

        PrintWriter writer = resp.getWriter();
        Enumeration parameterNames = req.getParameterNames();
        Enumeration headerNames = req.getHeaderNames();
        writer.println("PARAMETERS");

        while (parameterNames.hasMoreElements())
        {
            parameterName = (String)parameterNames.nextElement();
            writer.println(parameterName + ": " + req.getParameter(parameterName));
        }
        writer.println("HEADERS");
        while (i < 3)
        {
            headerName = (String)headerNames.nextElement();
            writer.println(headerName + ": " + req.getHeader(headerName));
            i++;
        }
    }
}

package lab6;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.methods.GetMethod;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Mmm extends HttpServlet {
    public Mmm()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    public void service(HttpServletRequest req, HttpServletResponse res) throws IOException
    {
        String initParameter = null;
        PrintWriter writer = res.getWriter();
        String stringParameter = req.getParameter("urln");
        switch (stringParameter)
        {
            case "1": initParameter = getServletContext().getInitParameter("URL1");break;
            case "2": initParameter = getServletContext().getInitParameter("URL2");break;
        }
        if(initParameter != null)
        {
            HttpClient client = new HttpClient();
            GetMethod method = new GetMethod(initParameter);
            client.executeMethod(method);
            writer.println(method.getResponseBodyAsString());
        }
        else
        {
            writer.println("parameter URL" + stringParameter + "not found");
        }

    }
}

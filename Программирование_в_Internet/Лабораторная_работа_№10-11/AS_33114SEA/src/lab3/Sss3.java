package lab3;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.methods.PostMethod;

import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;

public class Sss3  extends HttpServlet {
    public Sss3()
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
        System.out.println("Sss3: doPost");
        String stringParameter = "firstname="+ req.getParameter("firstname") + "&" + "secondname=" + req.getParameter("secondname");
        HttpClient client = new HttpClient();
        PostMethod method = new PostMethod("http://172.16.193.69:8080/AS_33114SEA/Ggg?" + stringParameter);
        client.executeMethod(method);
        res.getWriter().println(method.getResponseBodyAsString());
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        System.out.println("Sss3: doGet");
        String parameterString = "firstname=" + req.getParameter("firstname") + "&" + "secondname=" +
                req.getParameter("secondname");
        HttpClient cl = new HttpClient();
        GetMethod method = new GetMethod("http://172.16.193.69:8080/AS_33114SEA/Ggg?" + parameterString);
        cl.executeMethod(method);
        res.getWriter().println(method.getResponseBodyAsString());
    }
}

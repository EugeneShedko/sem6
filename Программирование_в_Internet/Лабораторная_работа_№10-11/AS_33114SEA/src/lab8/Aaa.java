package lab8;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.PostMethod;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Collection;
import java.util.Enumeration;

public class Aaa extends HttpServlet {
    public Aaa()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    protected void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        PrintWriter writer = res.getWriter();
        String value1 = "value1";
        String value2 = "value2";
        String value3 = "value3";
        String name1 = "parameter1";
        String name2 = "parameter2";
        String name3 = "parameter3";


        HttpClient client = new HttpClient();
        PostMethod postMethod = new PostMethod("http://172.16.193.69:8080/AS_33114SEA/Bbb");

        postMethod.addRequestHeader(name1, value1);
        postMethod.addRequestHeader(name2, value2);
        postMethod.addRequestHeader(name3, value3);

        NameValuePair[] parameters = { new NameValuePair(name1, value1), new NameValuePair(name2, value2), new NameValuePair(name3, value3)};
        postMethod.addParameters(parameters);
        client.executeMethod(postMethod);
        writer.println(postMethod.getResponseBodyAsString());

        writer.println("RESPONSEHEADERSVALUES");
        writer.print(postMethod.getResponseHeader("header1"));
        writer.print(postMethod.getResponseHeader("header2"));
    }
}

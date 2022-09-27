package lab31;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class SssXml extends HttpServlet {
    public SssXml()
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
        try
        {
            Thread.currentThread().sleep(5000);
        }
        catch (Exception ex)
        {
            System.out.println(ex);
        }
        PrintWriter writer = res.getWriter();
        int n = Integer.parseInt(req.getHeader("XRand-N"));
        XXRand num = new XXRand(n);
        String s = "<?xml version=\"1.0\"  encoding = \"utf-8\" ?><rand>";
        for(int i = 0; i < 10; i++)
        {
            s += "<num>"+num.Get().toString()+"</num>";
        }
        s += "</rand>";
        writer.println(s);
    }
}

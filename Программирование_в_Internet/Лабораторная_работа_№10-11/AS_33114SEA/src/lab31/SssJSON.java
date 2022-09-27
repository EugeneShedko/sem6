package lab31;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class SssJSON extends HttpServlet {
    public SssJSON()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }


    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException
    {
        try
        {
            Thread.currentThread().sleep(1000);
        }
        catch (Exception ex)
        {
            System.out.println(ex);
        }
        PrintWriter writer = res.getWriter();
        Integer n = Integer.parseInt(req.getHeader("XRand-N"));
        XXRand num = new XXRand(n);
        String s ="{\"X\":[" ;
        for (int i = 0; i < 10; i++)
        {
            s += ("{\"rand\":"+ num.Get().toString()+"}"+
                    ((i < 9)?",":" "));
        }
        s+="]}";
        System.out.println(s);
        writer.println(s);

    }
}

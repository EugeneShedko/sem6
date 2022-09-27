package lab31;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class SssHeader extends HttpServlet {
    public SssHeader()
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
            Thread.currentThread().sleep(10000);
        }
        catch (Exception ex)
        {
            System.out.println(ex);
        }
        Float z = Float.parseFloat(req.getHeader("Value-X")) + Float.parseFloat(req.getHeader("Value-Y"));
        res.setHeader("Value-Z", z.toString());
    }
}

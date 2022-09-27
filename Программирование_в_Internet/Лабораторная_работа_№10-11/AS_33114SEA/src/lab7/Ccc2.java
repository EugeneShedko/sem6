package lab7;
import lab6.CBean;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Enumeration;

public class Ccc2 extends HttpServlet {
    public Ccc2()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }
    protected void service(HttpServletRequest req, HttpServletResponse res) throws IOException, ServletException {
        req.setAttribute("atrCBean", new CBean());
        Enumeration parameterNames = req.getParameterNames();
        String CBean;
        String value1;
        String value2;
        String value3;
        while(parameterNames.hasMoreElements())
        {
            switch(parameterNames.nextElement().toString())
            {
                case "CBean":
                    CBean = req.getParameter("CBean");
                    if(CBean != null)
                    {
                        if (CBean.equals("new")) {
                            req.setAttribute("atrCBean", new CBean());
                        }
                    }
                    else
                    {
                        req.setAttribute("atrCBean", new CBean());
                    }
                    break;
                case "Value1":
                    value1 = req.getParameter("Value1");
                    if(value1 != null)
                    {
                        ((CBean)req.getAttribute("atrCBean")).setValue1(Integer.parseInt(req.getParameter("Value1")));
                    }
                    break;
                case "Value2":
                    value2 = req.getParameter("Value2");
                    if(value2 != null)
                    {
                        ((CBean)req.getAttribute("atrCBean")).setValue2(Integer.parseInt(req.getParameter("Value2")));
                    }
                    break;
                case "Value3":
                    value3 = req.getParameter("Value3");
                    if(value3 != null)
                    {
                        ((CBean)req.getAttribute("atrCBean")).setValue3(Integer.parseInt(req.getParameter("Value3")));
                    }
                    break;
            }
        }
        req.getRequestDispatcher("jsp/Ccc2.jsp").forward(req, res);
    }
}

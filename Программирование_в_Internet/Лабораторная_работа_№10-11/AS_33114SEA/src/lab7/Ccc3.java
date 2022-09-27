package lab7;
import lab6.CBean;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.util.Enumeration;

public class Ccc3 extends HttpServlet {
    public Ccc3()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
        ServletContext context = getServletContext();
        context.setAttribute("atrCBean", new CBean());
    }

    public void destroy() {
        super.destroy();
    }
    protected void service(HttpServletRequest req, HttpServletResponse res) throws IOException, ServletException {
        HttpSession session = req.getSession();
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
                            session.setAttribute(session.getId(), new CBean());
                        }
                        else
                        {
                            if(session.getAttribute(session.getId()) == null)
                            {
                                session.setAttribute(session.getId(), new CBean());
                            }
                        }
                    }
                    else
                    {
                        session.setAttribute(session.getId(), new CBean());
                    }
                    break;
                case "Value1":
                    value1 = req.getParameter("Value1");
                    if(value1 != null)
                    {
                        ((CBean)session.getAttribute(session.getId())).setValue1(Integer.parseInt(req.getParameter("Value1")));
                    }
                    break;
                case "Value2":
                    value2 = req.getParameter("Value2");
                    if(value2 != null)
                    {
                        ((CBean)session.getAttribute(session.getId())).setValue2(Integer.parseInt(req.getParameter("Value2")));
                    }
                    break;
                case "Value3":
                    value3 = req.getParameter("Value3");
                    if(value3 != null)
                    {
                        ((CBean)session.getAttribute(session.getId())).setValue3(Integer.parseInt(req.getParameter("Value3")));
                    }
                    break;
            }
        }
        req.getRequestDispatcher("jsp/Ccc3.jsp").forward(req, res);
    }
}

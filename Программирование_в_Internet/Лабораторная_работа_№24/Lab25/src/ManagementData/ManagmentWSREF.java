package ManagementData;

import DbUse.DataWSREF;
import Model.WSREF;
import com.google.gson.Gson;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.*;
import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;

@WebServlet(name = "ManagmentWSREF")
public class ManagmentWSREF extends HttpServlet {
    @Override
    public void init() throws ServletException {
        wsrefs.add(new WSREF(wsrefs.size(), "https://www.google.com/", "google,my", -1, 1));
        wsrefs.add(new WSREF(wsrefs.size(), "https://www.google.com/", "google,my", 0, 0));
        wsrefs.add(new WSREF(wsrefs.size(), "https://www.google.com/", "google,my", 0, 0));
        wsrefs.add(new WSREF(wsrefs.size(), "https://www.google.com/", "google,my", 0, 0));
        wsrefs.add(new WSREF(wsrefs.size(), "https://www.google.com/", "google,my", 0, 0));
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            String json = request.getReader().readLine();
            WSREF wsref = new Gson().fromJson(json,WSREF.class);
           /* wsref.setId(wsrefs.size());
            wsrefs.add(wsref);*/
            DataWSREF.addWSREF(wsref);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    ArrayList<WSREF> wsrefs = new ArrayList<>();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String filter = request.getHeader("Filter");

        try {
            HttpSession session = request.getSession();
            Object roleObj = session.getAttribute("Role");
            String role = roleObj != null ? roleObj.toString() : null;
            String json = new Gson().toJson(DataWSREF.getWSREF(filter));
            response.setContentType("application/json");
            session.setAttribute("sessionId", session.getId());
            response.setHeader("Role", role);
            response.setCharacterEncoding("UTF-8");
            response.getWriter().write(json);
        }
        catch (Exception e){
            System.out.println(e.getMessage());
        }

    }

    @Override
    protected void doDelete(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            int id = Integer.parseInt(request.getParameter("id"));
            DataWSREF.delete(id);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void doPut(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            String json = request.getReader().readLine();
            WSREF wsref = new Gson().fromJson(json, WSREF.class);
            DataWSREF.update(wsref);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

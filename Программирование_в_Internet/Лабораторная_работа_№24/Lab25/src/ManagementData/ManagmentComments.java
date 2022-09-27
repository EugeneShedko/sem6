package ManagementData;

import DbUse.DataWSREFComments;
import Model.WSREFCOMMENT;
import com.google.gson.Gson;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.sql.Date;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import static java.util.stream.Collectors.toList;

@WebServlet(name = "ManagmentComments")
public class ManagmentComments extends HttpServlet {
    private ArrayList<WSREFCOMMENT> wsrefcomment;
    private int id;
    private int refId;
    private String sessionId;
    private Date stamp;
    private String comment;


    @Override
    public void init() {
        try {
            wsrefcomment = new ArrayList<WSREFCOMMENT>();
            wsrefcomment.add(new WSREFCOMMENT(wsrefcomment.size(), ((int)Math.random())%6,
                    "djkknkdgn", new java.sql.Date(Calendar.getInstance().getTime().getTime()), "comment"));
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            String refId = request.getHeader("refId");
            if(refId != null){
                List<WSREFCOMMENT> selectComments = DataWSREFComments.getWSREFCOMMENT(Integer.parseInt(refId));
                String json = new Gson().toJson(selectComments);
                HttpSession session = request.getSession();
                Object roleObj = session.getAttribute("Role");
                String role = roleObj != null ? roleObj.toString() : null;
                String sessionId = session.getId();
                response.setHeader("role", role);
                response.setHeader("sessionId", sessionId);
                response.setContentType("application/json");
                response.setCharacterEncoding("UTF-8");
                response.getWriter().write(json);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            String json = request.getReader().readLine();
            WSREFCOMMENT comment = new Gson().fromJson(json,WSREFCOMMENT.class);
            HttpSession session = request.getSession();
            String sessionId = session.getId();
            comment.setSessionId(sessionId);
            comment.setStamp(new java.sql.Date(Calendar.getInstance().getTime().getTime()));
            DataWSREFComments.add(comment);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    @Override
    protected void doPut(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            String json = request.getReader().readLine();
            WSREFCOMMENT comment = new Gson().fromJson(json,WSREFCOMMENT.class);
            DataWSREFComments.update(comment);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void doDelete(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        try {
            DataWSREFComments.delete(Integer.parseInt(request.getParameter("id")));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

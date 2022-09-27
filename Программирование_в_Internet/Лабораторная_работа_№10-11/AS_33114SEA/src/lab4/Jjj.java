package lab4;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.methods.PostMethod;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Jjj extends HttpServlet {
    public  Jjj()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    //Переопределение!!!!!!!!!!!!!!!!!!!!!!!!!!!!

//    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
//        Integer hourParameter = new Time().GetHour();
//        RequestDispatcher rd = null;
//        if(hourParameter < 6) {
//            rd = req.getRequestDispatcher("jsp/night.jsp");
//        }else if(hourParameter >= 6 && hourParameter < 9){
//            rd = req.getRequestDispatcher("jsp/morning.jsp");
//        }else if(hourParameter >= 9 && hourParameter < 21) {
//            rd = req.getRequestDispatcher("jsp/afternoon.jsp");
//        }else
//            rd = req.getRequestDispatcher("jsp/evening.jsp");
//        rd.forward(req, res);
//    }

     //Get запрос!!!!!!!!!!!!!!!!!!!!
//    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
//        HttpClient cl = new HttpClient();
//        GetMethod method = null;
//        Integer hourParameter = new Time().GetHour();
//        if(hourParameter < 6) {
//            method = new GetMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/night.jsp");
//        }else if(hourParameter >= 6 && hourParameter < 9){
//            method = new GetMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/morning.jsp");
//        }else if(hourParameter >= 9 && hourParameter < 21) {
//            method = new GetMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/afternoon.jsp");
//        }else
//            method = new GetMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/evening.jsp");
//        cl.executeMethod(method);
//        PrintWriter writer = res.getWriter();
//        writer.println(method.getResponseBodyAsString());
//    }

    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        HttpClient cl = new HttpClient();
        PostMethod method = null;
        Integer hourParameter = new Time().GetHour();
        if(hourParameter < 6) {
            method = new PostMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/night.jsp");
        }else if(hourParameter >= 6 && hourParameter < 9){
            method = new PostMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/morning.jsp");
        }else if(hourParameter >= 9 && hourParameter < 21) {
            method = new PostMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/afternoon.jsp");
        }else
            method = new PostMethod("http://127.0.0.1:8080/AS_33114SEA/jsp/evening.jsp");
        cl.executeMethod(method);
        PrintWriter writer = res.getWriter();
        writer.println(method.getResponseBodyAsString());
    }
}

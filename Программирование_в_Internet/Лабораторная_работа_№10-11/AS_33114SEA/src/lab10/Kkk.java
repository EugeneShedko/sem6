package lab10;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import com.microsoft.sqlserver.jdbc.SQLServerException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.*;

public class Kkk extends HttpServlet {
    public Kkk()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    public void service(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException{
        PrintWriter writer = res.getWriter();
        String getallrow = "select * from lab10.justtable";
        try {
            SQLServerDataSource ds = new SQLServerDataSource();
            ds.setUser("lab10");
            ds.setPassword("lab10");
            ds.setServerName("DESKTOP-H66HRKQ");
            ds.setPortNumber(1433);
            ds.setDatabaseName("databaseforlab10");
            ds.setTrustServerCertificate(true);
            Connection connection = ds.getConnection();

            PreparedStatement stm = connection.prepareStatement(getallrow);
            ResultSet result = stm.executeQuery();
            writer.println("<table>");
            writer.println("<tr>");
            writer.println("<td>id</td><td>name</td><td>price</td>");
            while (result.next())
            {
                writer.println("<tr>");
                writer.println("<td>" + result.getString(1) + "</td><td>" + result.getString(2) + "</td><td>"
                        + result.getString(3) + "</td>");
                writer.println("</tr>");
            }
            writer.println("</table><br/>");

            String getOneRow = "select * from lab10.justtable where lab10.justtable.id = ?";
            PreparedStatement stm2 = connection.prepareStatement(getOneRow);
            stm2.setString(1, req.getParameter("Value1"));
            ResultSet result2 = stm2.executeQuery();

            writer.println("<table>");
            while (result2.next())
            {
                writer.println("<tr>");
                writer.println("<td>" + result2.getString(1) + "</td><td>" + result2.getString(2) + "</td><td>"
                        + result2.getString(3) + "</td>");
                writer.println("</tr>");
            }
            writer.println("</table><br/>");

            String execPrcedure = "{call lab10.lab10procedure(?,?)}";
            CallableStatement stm3 = connection.prepareCall(execPrcedure);
            stm3.setString(1, req.getParameter("Value2"));
            stm3.registerOutParameter(2, Types.INTEGER);
            stm3.execute();

            writer.println("PROCEDURERESULT: ");
            writer.println(stm3.getInt(2));

        }catch(SQLException ex)
        {
            System.out.println("!!!!!!!!!!!!Error: " +  ex);
        }
    }
}

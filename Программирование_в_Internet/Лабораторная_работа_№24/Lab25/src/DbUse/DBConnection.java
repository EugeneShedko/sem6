package DbUse;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DBConnection {
    public static Connection connection;

    public static void connectToDB() throws SQLException, ClassNotFoundException {
        Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
        String URL = "jdbc:sqlserver://DESKTOP-H66HRKQ\\DESKTOP-H66HRKQ:1433;DatabaseName=java_ee_lab;encrypt=false";
        connection = DriverManager.getConnection(URL, "lab24", "lab24");
        if (connection != null) {
            System.out.println("Connect to db has been performed successfully");
        }
        else {
            System.out.println("dont Connect to db");
        }
    }
}
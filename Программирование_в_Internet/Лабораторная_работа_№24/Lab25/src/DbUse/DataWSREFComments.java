package DbUse;

import Model.WSREF;
import Model.WSREFCOMMENT;

import java.io.IOException;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

public class DataWSREFComments {
    public static void add(WSREFCOMMENT wsrefcomment) throws SQLException {
        Connection connection = DBConnection.connection;
        Statement statement = connection.createStatement();

        statement.execute(String.format("INSERT INTO WSREFCOMMENT(refId, sessionId, stamp, comment)" +
                        "VALUES (\'%d\',\'%s\',  \'%s\',  \'%s\')", wsrefcomment.getRefId(), wsrefcomment.getSessionId(),
                wsrefcomment.getStamp().toString(),  wsrefcomment.getComment()));

        statement.close();
    }

    public static ArrayList<WSREFCOMMENT> getWSREFCOMMENT(int id) throws SQLException, ClassNotFoundException {

        String request = "SELECT * FROM WSREFCOMMENT WHERE refId = " + id + ";";

        Statement statement = DBConnection.connection.createStatement();

        ResultSet comments = statement.executeQuery(request);
        ArrayList<WSREFCOMMENT> findedLinks = new ArrayList<>();

        while(comments.next()) {
            WSREFCOMMENT wsrefcomment = new WSREFCOMMENT(comments.getInt(1), comments.getInt(2),
                    comments.getString(3), comments.getDate(4), comments.getString("comment"));
            findedLinks.add(wsrefcomment);
        }
        return findedLinks;
    }

    public static void update(WSREFCOMMENT wsrefcomment) throws SQLException {
        Statement statement = DBConnection.connection.createStatement();
        statement.execute(String.format("UPDATE WSREFCOMMENT SET comment = \'" + wsrefcomment.getComment() + "\'  WHERE id = " + wsrefcomment.getId()));
        statement.close();
    }

    public static void delete(int id) throws SQLException {
        try {
            Connection connection = DBConnection.connection;
            Statement statement = connection.createStatement();
            statement.execute("DELETE FROM WSREFCOMMENT WHERE id = " + id);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }


}

package DbUse;

import Model.WSREF;

import java.sql.*;
import java.util.ArrayList;

public class DataWSREF {
    private static ArrayList<String> GetElementsString(String str){
        if(str == null)
            return null;
        ArrayList<String> arrayList = new ArrayList<>();
        for (int i = 0; i < str.length(); i++){
            String tempstr = "";
            while(str.length() != i && str.charAt(i) != ','){
                if(str.charAt(i) != ' ')
                    tempstr += str.charAt(i);
                i++;
            }
            arrayList.add(tempstr);
        }
        return arrayList;
    }

    public static ArrayList<WSREF> getWSREF(String filter) throws SQLException, ClassNotFoundException {
        ArrayList<String> filters = null;
        String request = "SELECT * FROM WSREF;";
        if(filter != null && !filter.equals("") && !filter.equals("undefined")){
            filters = GetElementsString(filter);
            request = "SELECT * FROM WSREF WHERE ";
            for(int i = 0; i < filters.size(); i++){
                request += "Description LIKE \'%" + filters.get(i) + "%\'";
                if(i != filters.size() - 1){
                    request += " or ";
                }
                else request += ";";
            }
        }


        Statement statement = DBConnection.connection.createStatement();

        ResultSet listOfLinks = statement.executeQuery(request.toString());
        ArrayList<WSREF> findedLinks = new ArrayList<>();

        while(listOfLinks.next()) {
            WSREF wsref = new WSREF(listOfLinks.getInt(1), listOfLinks.getString(2),
                    listOfLinks.getString(3), listOfLinks.getInt(4), listOfLinks.getInt(5));
            findedLinks.add(wsref);
        }
        return findedLinks;
    }

    public static void addWSREF(WSREF wsref) throws SQLException, ClassNotFoundException {

        DBConnection.connectToDB();
        Statement statement = DBConnection.connection.createStatement();
        statement.execute(String.format("INSERT INTO WSREF(url, description, plus, minus) VALUES (\'%s\',\'%s\',\'%d\',\'%d\')", wsref.getUrl(), wsref.getDescription(), 0,0));
        statement.close();
    }

    public static void update(WSREF wsref) throws SQLException {
        Statement statement = DBConnection.connection.createStatement();
        statement.execute(String.format("UPDATE WSREF SET url = \'" + wsref.getUrl() + "\' , description = \'" +
                wsref.getDescription() + "\', plus = \'" + wsref.getPlus() + "\', minus = \'" + wsref.getMinus() + "\'  WHERE id = " + wsref.getId()));
        statement.close();
    }

    public static void delete(int refid) throws SQLException {
        Connection connection = DBConnection.connection;
        connection.setAutoCommit(false);
        System.out.println("PODKL DB");

        Savepoint savepointOne = connection.setSavepoint("SavepointOne");
        Statement statement = connection.createStatement();
        System.out.println("start");
        try {
            String deleteLinks = "DELETE FROM WSREFCOMMENT WHERE refId ="+refid;
            PreparedStatement stmt = connection.prepareStatement(deleteLinks);
            stmt.executeUpdate();

            //statement.executeUpdate(deleteLinks);
            System.out.println("1 delete");

            deleteLinks = "DELETE FROM WSREF WHERE id =" + refid;
            statement.executeUpdate(deleteLinks);
            System.out.println("2 delete");

            connection.commit();
            connection.setAutoCommit(true);
        }
        catch (SQLException var112) {
            System.out.println(var112);
            connection.rollback(savepointOne);
        }
    }
}

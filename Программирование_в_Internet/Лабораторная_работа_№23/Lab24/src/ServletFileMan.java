import com.github.sardine.DavResource;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import javax.servlet.ServletException;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

import static java.lang.Thread.sleep;

@WebServlet(name = "ServletFileMan")
@MultipartConfig
public class ServletFileMan extends HttpServlet {
    private ArrayList<String> GetElementsRemove(String str){
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

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String TypeAction = request.getHeader("TypeAction");
        String type = request.getParameter("type");
        switch (TypeAction == null ? type : TypeAction){
            case "RemoveElement":{
                String Mess = "Files removed";
                try{
                    String elementsT = request.getHeader("RemoveElements");
                    ArrayList<String> removeElements = GetElementsRemove(elementsT);
                    if(removeElements != null)
                        for (String str : removeElements){
                            this.fileManager.RemmoveEl(str);
                        }
                }catch (Exception e){
                    Mess = e.getMessage();
                }
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("mess", Mess);

                response.setContentType("application/json");
                response.getWriter().write(jsonObject.toJSONString());
                break;
            }
            case "UploadFile":{
                String file = request.getHeader("File");
                fileManager.UploadFile(request.getPart("file"));
                response.sendRedirect("http://localserver33114sea:8080/WebDAV/");
                break;
            }
            case "CopyFile":{
                String Mess = "Files copied";
                try{
                    String elementsT = request.getHeader("CopyElements");
                    ArrayList<String> moveElements = GetElementsRemove(elementsT);
                    if(moveElements != null || moveElements.size() != 0)
                        for (String str : moveElements){
                            String file = str.substring(str.lastIndexOf("/") + 1, str.length() - 1);
                            this.fileManager.CopyFile(str, file);
                        }
                }catch (Exception e){
                    Mess = e.getMessage();
                }

                JSONObject jsonObject = new JSONObject();
                jsonObject.put("mess", Mess);

                response.setContentType("application/json");
                response.getWriter().write(jsonObject.toJSONString());
                break;
            }
        }
    }

    FileManager fileManager = new FileManager("eugeni.shedko", "pmzorjlhzkjgdjni", "https://webdav.yandex.ru/", "https://webdav.yandex.ru/");

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String TypeAction = request.getHeader("TypeAction");
        String type = request.getParameter("type");
        switch (TypeAction == null ? type : TypeAction){
            case "UpLevelOrOpen":{
                String file = request.getHeader("File");
                String lastSymbol = "";
                if(file.lastIndexOf("/") == file.length() -1 || file == ""){
                    if(file != "")
                        fileManager.UpLevelCurentDir(file);
                    List<DavResource> resources = fileManager.GetDirAndFiles();
                    JSONArray arrayFiles = new JSONArray();
                    for (int i = 0; i < resources.size(); i++) {
                        arrayFiles.add(resources.get(i).toString());
                    }
                    JSONObject jsonObject = new JSONObject();
                    jsonObject.put("numbers", arrayFiles);

                    response.setContentType("application/json");
                    response.getWriter().write(jsonObject.toJSONString());
                    break;
                }

            }
            case "download-file": {
                String fileName = request.getHeader("FileName");
                fileName = fileName == null ? request.getParameter("FileName") : fileName;
                BufferedInputStream bufferedStream = fileManager.DowloadFile(fileName);
                String FileNameShort = fileName.substring(fileName.lastIndexOf("/") + 1);
                response.setContentType("application/octet-stream");
                response.addHeader("Content-Disposition", "attachment; FileName=" + FileNameShort);
                //response.setContentLength(inputStream.readAllBytes().length);
                int readBytes = 0;
                while ((readBytes = bufferedStream.read()) != -1) {
                    response.getWriter().write(readBytes);
                }
                bufferedStream.close();
                break;
            }
            case "DownLevelDirectory":{
                fileManager.DownLevelCurentDir();
                break;
            }
            case "CreateNewDirectory":{
                String newDir = request.getHeader("NewDir");
                if(newDir != "") {
                    FileManager fileManager = new FileManager();
                    fileManager.CreateNewDirectory(newDir);
                }
                break;
            }

        }

    }
}

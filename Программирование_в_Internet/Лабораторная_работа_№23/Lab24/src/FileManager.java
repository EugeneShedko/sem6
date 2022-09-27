import com.github.sardine.DavResource;
import com.github.sardine.Sardine;
import com.github.sardine.SardineFactory;

import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import javax.swing.*;
import java.io.*;
import java.nio.file.Paths;
import java.util.List;

@MultipartConfig
public class FileManager {
    static Sardine sardine;
    static String PATH = "";
    static String CURRENTPATH = "";
    String userName = "";
    String password = "";
    //password: iszspdkjdpfpfshj
    public FileManager(){}
    public FileManager(String userName, String password, String path, String currentpath){
        sardine = SardineFactory.begin(userName, password);
        this.userName = userName;
        this.password = password;
        PATH = path;
        CURRENTPATH = currentpath;
    }

    public void UpLevelCurentDir(String directory){
        if(PATH.lastIndexOf('/') == PATH.length() - 1)
            PATH = PATH.substring(0, PATH.lastIndexOf('/'));
        CURRENTPATH = PATH + directory;
    }

    public void DownLevelCurentDir(){
        if(CURRENTPATH.lastIndexOf('/') == CURRENTPATH.length() - 1)
            CURRENTPATH = CURRENTPATH.substring(0, CURRENTPATH.lastIndexOf('/'));
        CURRENTPATH = CURRENTPATH.substring(0, CURRENTPATH.lastIndexOf('/'));
    }

    public List<DavResource> GetDirAndFiles() throws IOException {
        return sardine.list(CURRENTPATH);
    }

    public void CreateNewDirectory(String nameDir) throws IOException {
        if(CURRENTPATH.lastIndexOf('/') == CURRENTPATH.length() - 1)
            CURRENTPATH = CURRENTPATH.substring(0, CURRENTPATH.lastIndexOf('/'));
        sardine.createDirectory(CURRENTPATH + "/" + nameDir);
    }

    public void RemmoveEl(String name) throws IOException {
        if(PATH.lastIndexOf('/') == PATH.length() - 1)
            PATH = PATH.substring(0, PATH.lastIndexOf('/'));
        this.sardine.delete(PATH + name);
    }

    public BufferedInputStream DowloadFile(String name) throws IOException {
        InputStream inputStream = sardine.get(PATH + name);
        BufferedInputStream bufferedStream = new BufferedInputStream(inputStream);
        return bufferedStream;
    }

    public void UploadFile(Part partFile) throws IOException {
        String fileFullName = Paths.get(partFile.getSubmittedFileName()).getFileName().toString();
        InputStream fileContent = partFile.getInputStream();
        if(CURRENTPATH.lastIndexOf('/') != CURRENTPATH.length() - 1)
            this.sardine.put(CURRENTPATH + "/" + fileFullName, fileContent);
        else
            this.sardine.put(CURRENTPATH + fileFullName, fileContent);
    }

    public void CopyFile(String filePath, String file) throws IOException {
        if(PATH.lastIndexOf('/') == PATH.length() - 1)
            PATH = PATH.substring(0, PATH.lastIndexOf('/'));
        if(CURRENTPATH.lastIndexOf('/') != CURRENTPATH.length() - 1)
            CURRENTPATH += "/";
        this.sardine.copy(PATH + filePath, CURRENTPATH + file);
    }
}

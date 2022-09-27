package lab22;

import org.apache.commons.io.FileUtils;

import javax.servlet.ServletException;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import java.io.*;
import java.nio.file.Paths;

@MultipartConfig
public class Sss2 extends HttpServlet {
    public Sss2()
    {
        super();
    }

    public void init() throws ServletException {
        super.init();
    }

    public void destroy() {
        super.destroy();
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        String filename = req.getParameter("file");
        String fileDirectory = getServletContext().getInitParameter("FileDirectory");
        res.getWriter().println(fileDirectory);
        if(filename != null) {
            File direcory = new File(fileDirectory);
            File needFile = direcory.listFiles(((dir, name) -> name.equals(filename)))[0];
            if(needFile != null) {
                res.setContentType("application/msword");
                res.addHeader("Content-Disposition", "attachment; filename=" + filename);
                res.setContentLength((int)needFile.length());

                FileInputStream inputStream = new FileInputStream(needFile);
                BufferedInputStream bufferedStream = new BufferedInputStream(inputStream);
                int readBytes = 0;
                while ((readBytes = bufferedStream.read()) != -1) {
                    res.getWriter().write(readBytes);
                }
            }else {
                res.getWriter().write("Unexpected error happened...");
            }
        }
    }

    protected void doPost(HttpServletRequest req, HttpServletResponse res) throws ServletException, IOException {
        Part partFile = req.getPart("file");
        String fileName = Paths.get(partFile.getSubmittedFileName()).getFileName().toString();
        InputStream fileContent = partFile.getInputStream();

        File file = new File(getServletContext().getInitParameter("FileDirectory") + "/" + fileName);
        FileUtils.copyInputStreamToFile(fileContent, file);

        res.getWriter().write("File has been saved");
    }
}

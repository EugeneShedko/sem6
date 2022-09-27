package lab5;

import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Submit extends TagSupport {
    public int doStartTag()
    {
        String okButton = "<input type = \"submit\" name = \"submit\" value = \"OK\" />";
        String cancelButton = "<input type = \"submit\"  name = \"submit\" value = \"Cancel\"/>";
        String tag = "<p>" +  okButton+ " " + cancelButton + "</p>";
        try
        {
            JspWriter writer = pageContext.getOut();
            writer.print(tag);
        }
        catch (IOException ex)
        {
            System.out.println(ex);
        }
        return  SKIP_BODY;
    }
}

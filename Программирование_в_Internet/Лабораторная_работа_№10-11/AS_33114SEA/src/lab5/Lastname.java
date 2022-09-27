package lab5;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Lastname extends TagSupport {
    private  String value = "";
    public  void setValue(String value)
    {
        this.value = value;
    }
    public String getValue()
    {
        return this.value;
    }
    public int doStartTag() throws JspException
    {
        String tag = "<p><label> Lastname </label> <input type = \"text\" name = \"lastname\" value = \"" + this.value + "\"/></p>";
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

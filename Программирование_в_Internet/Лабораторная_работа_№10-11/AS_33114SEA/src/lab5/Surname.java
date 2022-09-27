package lab5;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Surname extends TagSupport {
    private String value = "";

    public  void setValue(String value)
    {
        this.value = value;
    }
    public  String getValue()
    {
        return  this.value;
    }
    public int doStartTag() throws JspException
    {
        String tag = "<p><label> Surname </label><input name = \"surname\" type =\"text\" value = \"" + this.value + "\"/></p>";
        JspWriter writer = pageContext.getOut();
        try
        {
            writer.print(tag);
        }
        catch (IOException ex)
        {
            System.out.println(ex);
        }
        return  SKIP_BODY;
    }
}

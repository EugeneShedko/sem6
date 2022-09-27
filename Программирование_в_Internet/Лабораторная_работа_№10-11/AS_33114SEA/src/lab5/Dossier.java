package lab5;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Dossier extends TagSupport {
    public String action = "";
    public String getAction()
    {
        return  this.action;
    }
    public void setAction(String action)
    {
        this.action = action;
    }
    public int doStartTag() throws JspException
    {
        String in = "<form method = \"POST\" action = \"" + this.action + "\">";
        JspWriter writer = pageContext.getOut();
        try
        {
            writer.print(in);
        }
        catch (IOException ex)
        {
            System.out.println(ex);
        }
        return EVAL_BODY_INCLUDE;
    }
    public int doEndTag() throws  JspException
    {
        String in = "</form>";
        JspWriter writer = pageContext.getOut();
        try
        {
            writer.print(in);
        }
        catch (IOException ex)
        {
            System.out.println(ex);
        }
        return EVAL_BODY_INCLUDE;
    }
}

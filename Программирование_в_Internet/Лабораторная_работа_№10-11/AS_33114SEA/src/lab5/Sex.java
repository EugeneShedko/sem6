package lab5;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Sex extends TagSupport {
    public int doStartTag() throws JspException
    {
        String label = "<label> Sex:</label>";
        String maleRadio = "<p><input name = \"sex\" type = \"radio\" value = \"male\"/> <label> male </label></p>";
        String femaleRadio = "<p><input name = \"sex\" type = \"radio\" value = \"female\"/> <label> female </label></p>";
        String tag = label + maleRadio + femaleRadio;
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

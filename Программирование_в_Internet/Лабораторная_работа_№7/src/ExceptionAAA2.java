import java.io.PrintStream;

public class ExceptionAAA2 extends Exception{

    public ExceptionAAA2(String message){
        super(message);
    }

    public String getMessage()
    {
        return super.getMessage();
    }

    public void printStackTrace()
    {
        super.printStackTrace();
    }

    public String toString()
    {
        return super.toString();
    }
}

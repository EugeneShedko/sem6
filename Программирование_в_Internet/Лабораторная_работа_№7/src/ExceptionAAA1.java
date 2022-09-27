public class ExceptionAAA1 extends Exception {

    public  ExceptionAAA1(String message){
        super(message);
    }

    public String getMessage()
    {
        return super.getMessage();
    }

    public  void PrintStackTrace()
    {
        super.printStackTrace();
    }

    public String toString()
    {
        return  super.toString();
    }

}

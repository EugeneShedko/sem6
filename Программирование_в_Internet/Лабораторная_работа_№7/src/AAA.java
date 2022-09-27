public class AAA {

    public AAA(Integer x) throws ExceptionAAA1
    {
        if(x == null)
            throw new ExceptionAAA1("the value of the variable is null");
    }

    public  AAA () {}

    public void method(int z) throws ExceptionAAA2, ExceptionAAA3
    {
        if(z == 0)
            throw  new ExceptionAAA2("the values of the variable is 0");
        if(z < 0)
            throw  new ExceptionAAA3("the values of the variable is less than 0");
    }
}

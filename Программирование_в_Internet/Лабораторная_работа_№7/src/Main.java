public class Main {

    public static void main(String[] args) {
        try
        {
            AAA bbb = new AAA(null);
//            AAA aaa = new AAA();
//            aaa.method(-5);
        }
        catch(ExceptionAAA1 ex)
        {
            System.out.println(ex.getMessage());
            ex.printStackTrace();
            System.out.println(ex.toString());
        }
//        catch (ExceptionAAA2 ex)
//        {
//            System.out.println(ex.getMessage());
//            ex.printStackTrace();
//            System.out.println(ex.toString());
//        }
//        catch (ExceptionAAA3 ex)
//        {
//            System.out.println(ex.getMessage());
//            ex.printStackTrace();
//            System.out.println(ex.toString());
//        }
    }

}
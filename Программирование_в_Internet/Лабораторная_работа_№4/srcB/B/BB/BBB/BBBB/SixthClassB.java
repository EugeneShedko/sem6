package B.BB.BBB.BBBB;
import A.*;

public class SixthClassB {
    public FirstClassA firstClassA;
    public SixthClassB()
    {
        this.firstClassA = new FirstClassA(70);
    }
    public void print()
    {
        firstClassA.print();
    }
}

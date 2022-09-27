package B.BB.BBB.BBBB;

public class FourthClassB {
    public int valueB4;

    public FourthClassB(int valueB4) {
        this.valueB4 = valueB4;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueB4 = " + this.valueB4);
    }
}

package B.BB.BBB.BBBB;

public class FifthClassB {
    public int valueB5;

    public FifthClassB(int valueB5) {
        this.valueB5 = valueB5;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueB5 = " + this.valueB5);
    }
}

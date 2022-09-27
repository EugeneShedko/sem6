package B.BB;

public class SecondClassB {
    public int valueB2;

    public SecondClassB(int valueB2) {
        this.valueB2 = valueB2;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueB2 = " + this.valueB2);
    }
}

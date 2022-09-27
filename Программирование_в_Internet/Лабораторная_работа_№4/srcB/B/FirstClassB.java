package B;

public class FirstClassB {
    public int valueB1;

    public FirstClassB(int valueB1) {
        this.valueB1 = valueB1;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueB1 = " + this.valueB1);
    }
}

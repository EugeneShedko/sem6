package B.BB;

public class ThirdClassB {
    public int valueB3;

    public ThirdClassB(int valueB3) {
        this.valueB3 = valueB3;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueB3 = " + this.valueB3);
    }
}

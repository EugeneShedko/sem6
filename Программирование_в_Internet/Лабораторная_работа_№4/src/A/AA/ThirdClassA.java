package A.AA;

public class ThirdClassA {
    public int valueA3;

    public ThirdClassA(int valueA3) {
        this.valueA3 = valueA3;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueA3 = " + this.valueA3);
    }
}

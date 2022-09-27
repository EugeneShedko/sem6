package A.AA;

public class SecondClassA {
    public int valueA2;

    public SecondClassA(int valueA2) {
        this.valueA2 = valueA2;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueA2 = " + this.valueA2);
    }
}

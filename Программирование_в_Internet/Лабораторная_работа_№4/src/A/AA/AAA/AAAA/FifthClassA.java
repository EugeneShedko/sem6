package A.AA.AAA.AAAA;

public class FifthClassA {
    public int valueA5;

    public FifthClassA(int valueA5) {
        this.valueA5 = valueA5;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueA5 = " + this.valueA5);
    }
}

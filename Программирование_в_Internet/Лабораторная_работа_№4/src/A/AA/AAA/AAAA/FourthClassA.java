package A.AA.AAA.AAAA;

public class FourthClassA {
    public int valueA4;

    public FourthClassA(int valueA4) {
        this.valueA4 = valueA4;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueA4 = " + this.valueA4);
    }
}

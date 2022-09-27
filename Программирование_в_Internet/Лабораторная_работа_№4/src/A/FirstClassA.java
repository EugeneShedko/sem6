package A;

public class FirstClassA{
    public int valueA1;

    public FirstClassA(int valueA1) {
        this.valueA1 = valueA1;
    }

    public void print() {
        System.out.println(this.getClass().getName());
        System.out.println("valueA1 = " + this.valueA1);
    }
}

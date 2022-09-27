import A.*;
import A.AA.*;
import A.AA.AAA.AAAA.*;
import B.*;
import B.BB.*;
import B.BB.BBB.BBBB.*;

public class Main {

    public static void main(String[] args) {
        FirstClassA firstClass = new FirstClassA(5);
        firstClass.print();

        SecondClassA secondClass = new SecondClassA(10);
        secondClass.print();

        ThirdClassA thirdClass = new ThirdClassA(15);
        thirdClass.print();

        FourthClassA fourthClass = new FourthClassA(20);
        fourthClass.print();

        FifthClassA fifthClass = new FifthClassA(25);
        fifthClass.print();

        FirstClassB firstClassB = new FirstClassB(30);
        firstClassB.print();

        SecondClassB secondClassB = new SecondClassB(35);
        secondClassB.print();

        ThirdClassB thirdClassB = new ThirdClassB(40);
        thirdClassB.print();

        FourthClassB fourthClassB = new FourthClassB(45);
        fourthClassB.print();

        FifthClassB fifthClassB = new FifthClassB(50);
        fifthClassB.print();

        SixthClassB sixthClassB = new SixthClassB();
        sixthClassB.print();

        SixthClassA sixthClassA = new SixthClassA();
        sixthClassA.print();

        SixthClassA sss = new SixthClassA();
        sixthClassA.print();

    }
}

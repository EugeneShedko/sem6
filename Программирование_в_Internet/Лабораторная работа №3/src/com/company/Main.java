package com.company;

public class Main {

    public static void main(String[] args) {
        //Task 1
        CCC var1 = new CCC();
        System.out.println(var1.sum());
        System.out.println(var1.ssub(10 , 5));

        CCC var2 = new CCC(20, 25);
        System.out.println(var2.sum());
        System.out.println(var1.ssub(50 , 30));

        //Task 2
        DDD var3 = new DDD();
        System.out.println(var3.ssub(78, 34));

        DDD var4 = new DDD(10 , 10);
        System.out.println(var4.ssub(46, 12));
    }
}

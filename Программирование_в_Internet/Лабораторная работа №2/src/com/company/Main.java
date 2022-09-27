package com.company;

import java.net.Inet4Address;

public class Main {

    public static void main(String[] args) {
        System.out.println("\nTask 1");
        int[] xx = new int[10];
        for(int i = 0; i < xx.length; i++) {
            xx[i] = i;
        }
        for(int i =0; i < xx.length; i++)
        {
            System.out.println(xx[i]);
        }
        System.out.println("\nTask 2");
        int [][] xxx = new int[5][5];
        for(int i =0; i < xxx.length; i++)
        {
            for(int j = 0; j < xxx[i].length; j++)
            {
                xxx[i][j] = i + j;
            }
        }
        for(int i = 0; i < xxx.length; i++)
        {
            System.out.println(" ");
            for(int j = 0; j < xxx[i].length; j++)
            {
                System.out.println(xxx[i][j]);
            }
        }
        System.out.println("\nTask 3");
        System.out.println("~");
        int a = 500;
        System.out.println(Integer.toBinaryString(a));
        System.out.println(Integer.toBinaryString(~a));

        System.out.println("&");
        int b = 154, c = 200;
        System.out.println(Integer.toBinaryString(b) + "\n" + Integer.toBinaryString(c));
        System.out.println(Integer.toBinaryString(b & c));

        System.out.println("^=");
        System.out.println(Integer.toBinaryString(b));
        System.out.println(Integer.toBinaryString(c));
        System.out.println(Integer.toBinaryString( b ^= c));

        System.out.println("<<=");
        System.out.println(Integer.toBinaryString(b));
        System.out.println(Integer.toBinaryString(b <<= 4));

        System.out.println(">>>");
        System.out.println(Integer.toBinaryString(b));
        System.out.println(Integer.toBinaryString(b >>> 4));

        System.out.println("\nTask 4");
        boolean d = true || false;
        System.out.println(d);
        boolean e = false && false;
        System.out.println(e);
        boolean f =  (e == false) ? false : true;
        System.out.println(f);

        System.out.println("\nTask 5");
        if(!f)
        {
            System.out.println("false");
        }
        for(int i = 0; i < xxx.length; i++)
        {
            System.out.println(" ");
            for(int j = 0; j < xxx[i].length; j++)
            {
                System.out.println(xxx[i][j]);
            }
        }
        int  i = 0;
        while (i < xxx[0].length)
        {
            System.out.println(xxx[0][i++]);
        }

        i = 0;
        do {
            System.out.println(xxx[0][i++]);
        } while (i < xxx[0].length);

        int g = 2;
        switch (g)
        {
            case 1: System.out.println("g = 1");break;
            case 2: System.out.println("g = 2");break;
            case 3: System.out.println("g = 3");break;
        }

        System.out.println("\n Task6");
        JustClass.PrintValueOfVariable();
        JustClass.x = 10;
        JustClass.PrintValueOfVariable();
    }
}

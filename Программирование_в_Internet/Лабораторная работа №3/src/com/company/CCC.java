package com.company;

public class CCC {
    public int x;
    private int y;

    public int gety()
    {
        return  this.y;
    }

    public  CCC()
    {
        x = 0;
        y = 0;
    }
    public  CCC(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int sum()
    {
        return  this.x + this.y;
    }
    public  int ssub(int x , int y)
    {
        return  x - y;
    }
}

package com.company;

public class DDD extends CCC{
    public int x;
    private  int y;

    public  DDD()
    {
        super();
        this.x = 1;
        this.y = 1;
    }
    public  DDD(int x, int y)
    {
        super(x , y);
        this.x = x;
        this.y = y;
    }
    public  int calc()
    {
        return this.x * this.y + super.x * super.gety();
    }

    @Override
    public  int sum()
    {
        return this.x + this.y + super.sum();
    }
}

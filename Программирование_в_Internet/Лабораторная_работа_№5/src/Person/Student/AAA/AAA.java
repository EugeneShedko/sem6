package Person.Student.AAA;

import Person.Student.*;

import java.util.Date;
public class AAA implements Student {
    private String name;
    private String surname;
    private  String fathername;
    private Date birthday;
    private Date firstDate = new Date();
    private  String university;

    public void setName(String name)
    {
        this.name = name;
    }
    public String getName()
    {
        return this.name;
    }
    public void setSurname(String surname)
    {
        this.surname = surname;
    }
    public String getSurname()
    {
        return  this.surname;
    }
    public  void setFathername(String fathername)
    {
        this.fathername = fathername;
    }
    public String getFathername()
    {
        return  this.fathername;
    }
    public void setBirthday(int yyy, int mm, int ddd)
    {
        if(Limityyyy < yyy)
            this.birthday = new Date(yyy, mm, ddd);
    }
    public  Date getBirthday()
    {
        return  this.birthday;
    }
    public void setFirstDate(Date d)
    {
        this.firstDate = d;
    }
    public Date getFirstDate()
    {
        return  this.firstDate;
    }
    public void setUniversity(String u)
    {
        if(u.length() < LimitUniversityLength)
            this.university = u;
    }
    public String getUniversity()
    {
        return  this.university;
    }
}

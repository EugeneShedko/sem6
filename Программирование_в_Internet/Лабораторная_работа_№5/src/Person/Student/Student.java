package Person.Student;

import Person.*;

import java.util.Date;

public interface Student extends Person {
    public void setFirstDate(Date d);
    public Date getFirstDate();
    public void setUniversity(String u);
    public String getUniversity();
    public int LimitUniversityLength = 50;
}

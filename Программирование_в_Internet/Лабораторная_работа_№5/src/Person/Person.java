package Person;

import java.util.Date;

public interface Person {
    public int Limityyyy = 1900;
    public void setSurname(String surname);
    public  void setName(String name);
    public void setFathername(String fathername);
    public void setBirthday(int yyyy, int mm, int ddd);
    public String getSurname();
    public String getName();
    public String getFathername();
    public Date getBirthday();
}

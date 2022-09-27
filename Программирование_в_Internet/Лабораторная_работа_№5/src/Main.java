import java.util.Date;
import Person.Student.AAA.*;

public class Main {

    public static void main(String[] args) {
        AAA aaa = new AAA();
        aaa.setName("Eugene");
        aaa.setSurname("Shedko");
        aaa.setFathername("Aleksandrovich");
        aaa.setBirthday(2002, 5, 18);
        aaa.setFirstDate(new Date(2019 , 8 , 23));
        aaa.setUniversity("BSTU");

        System.out.println("Name: " + aaa.getName());
        System.out.println("Surname: " + aaa.getSurname());
        System.out.println("Fathername: " + aaa.getFathername());
        System.out.println("Birthday: " + aaa.getBirthday().getDate() + "." + aaa.getBirthday().getMonth() + "." + aaa.getBirthday().getYear());
        System.out.println("FirstDate: " + aaa.getFirstDate().getDate() + "." + aaa.getFirstDate().getMonth() + "." + aaa.getFirstDate().getYear());
        System.out.println("University: " + aaa.getUniversity());
    }
}

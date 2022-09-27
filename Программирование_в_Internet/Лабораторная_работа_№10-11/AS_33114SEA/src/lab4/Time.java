package lab4;
import java.util.Calendar;
import java.util.GregorianCalendar;

public class Time {
    private Calendar calendar;
    public Time()
    {
        calendar = Calendar.getInstance();
    }
    public int GetHour()
    {
        return calendar.get(Calendar.HOUR_OF_DAY);
    }
}

import java.util.Locale;

public class Main {

    public static void main(String[] args) {
        //Example 1
        String s01 = new String();
        System.out.println("s01.length = " + s01.length());

        char[] cs = {'0','1', '2', '3', '4', '5', '6', '7'};
        String s02 = new String(cs);
        System.out.println("s02.length = " + s02.length() + " s02 = " + s02);

        String s03 = new String(cs , 3 ,5);
        System.out.println("s03.length = " + s02.length() + " s03 = " + s03);

        String s04 = "01234567";
        System.out.println("s04.length = " + s04.length() + " s04 = " + s04);

        String s05 = new String("01234567");
        System.out.println("s05.length = " + s05.length() + " s05 = " + s05);
//
//        //Example2
        System.out.println("\"01234567\".length = " + "01234567".length());

        //Example3
        String s06 = new String("01234567");
        for(int i = 0; i < s06.length(); i++)
        {
            System.out.print(s06.charAt(i) + ((i < s06.length() - 1) ? "-" : ""));
        }
        System.out.println("");
        char[] cs1 = new char[6 - 2];
        s06.getChars(2, 6 , cs1, 0);
        for(int i = 0; i < cs1.length; i++)
        {
            System.out.print(cs1[i] + (( i < cs1.length - 1) ? "-" : ""));
        }

        //Example4
        String s07 = new String("01234567");
        String s08 = new String("01234567");
        System.out.println("(s07 == s08) = " + (s07 == s08));
        System.out.println("(s07 == \"01234567\" = " + (s07 == "01234567"));
        System.out.println("s07.equals(s08) = " + s07.equals(s08));
        String s09 = s08;
        System.out.println("(s08 == s09) = " + (s08 == s09));

        //Example5
        String s10 = new String("Человека невозможно чему-нибудь научить, его можно убедить");
        System.out.println(s10.indexOf("j"));
        System.out.println(s10.indexOf("о"));
        System.out.println(s10.lastIndexOf("о"));
        System.out.println(s10.indexOf('о', 5));
        System.out.println(s10.lastIndexOf('о' , 5));
        System.out.println(s10.lastIndexOf('о' , 14));

        //Example6
        String s11 = new String("Лучший вид на этот город, если сесть в бомбардировщик");
        System.out.println(s11.substring(26));
        System.out.println(s11.substring(26, 30));

        //Example7
        String s12 = new String("Оффтопик - любое сетевое сообщение выходящее за рамки ранее установленной темы");
        System.out.println(s12.replace('ф', 'f'));
        System.out.println(s12.toUpperCase());
        System.out.println(s12.toLowerCase());

        //Example8
        StringBuffer s15 = new StringBuffer(100);
        s15.append("В городе Сочи темные ночи");
        System.out.println("s15.length() = " + s15.length());
        System.out.println("s15.capacity() = " + s15.capacity());
        s15.insert(21, "и теплые");
        System.out.println(s15);
        System.out.println("s15.length() = " + s15.length());
        System.out.println("s15.capacity() = " + s15.capacity());
        s15.delete(21 , 30);
        System.out.println(s15);
        s15.ensureCapacity(200);
        System.out.println("s15.capacity() = " + s15.capacity());
        String s16 = s15.toString();

        //Example9
        Byte x1 = 3;
        Short x2 = 256;
        Integer x3 = 1000;
        Float x4 = 3.14f;
        Double x5 = 2.9e10;
        Long x6 = 200000L;
        Character x7 = 'f';
        Boolean x8 = true;

        //Example10
        Planet planet = Planet.EART;
        switch (planet)
        {
            case MERCURY: System.out.println("MERCURY");break;
            case JUPITER: System.out.println("JUPITER");break;
            default: System.out.println("default");
        }

        //Example11
        Teams team = Teams.MU;
        System.out.println(team.winings);
        System.out.println(team.losses);
        System.out.println(team.draws);
    }
    public enum Planet
    {
        MERCURY,
        VENUS,
        EART,
        MARS,
        JUPITER,
        SATURN,
        URANUS,
        NEPTUN;
    }
    public enum Teams
    {
        MC(12 , 5 , 2),
        MU(15 , 0 , 0),
        Сhelsea(10 , 8 , 7),
        Arsenal(8, 4 , 6),
        Lester(4 , 12 , 4);

        private final int winings;
        private final int losses;
        private final int draws;
        Teams(int winings, int losses, int draws)
        {
            this.winings = winings;
            this.losses = losses;
            this.draws = draws;
        }
    }
}

package lab9;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

public class L1 implements ServletContextListener {
    public void contextInitialized(ServletContextEvent servletContextEvent) {
        System.out.println("L1:init");
    }

    public void contextDestroyed(ServletContextEvent servletContextEvent) {
        System.out.println("L1:destroy");
    }
}

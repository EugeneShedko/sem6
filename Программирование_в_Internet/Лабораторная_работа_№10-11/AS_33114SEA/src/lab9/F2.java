package lab9;

import javax.servlet.*;
import java.io.IOException;

public class F2 implements Filter {
    public void init(FilterConfig filterConfig) throws ServletException {
        System.out.println("F2:init");
    }

    public void destroy() {
        System.out.println("F2:destroy");
    }

    public void doFilter(ServletRequest req, ServletResponse res, FilterChain filterChain) throws IOException, ServletException {
        System.out.println("F2:doFilter");
        //net doFilter
    }
}

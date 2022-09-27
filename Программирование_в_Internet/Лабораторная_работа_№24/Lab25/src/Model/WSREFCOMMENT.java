package Model;

import java.sql.Date;

public class WSREFCOMMENT {
    private int id;
    private int refId;
    private String sessionId;
    private Date stamp;
    private String comment;

    public WSREFCOMMENT(int id1, int refId1, String sessionId1, Date stamp1, String comment1) {
        this.id = id1;
        this.refId = refId1;
        this.sessionId = sessionId1;
        this.stamp = stamp1;
        this.comment = comment1;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getSessionId() {
        return sessionId;
    }

    public void setSessionId(String sessionId) {
        this.sessionId = sessionId;
    }

    public int getRefId() {
        return refId;
    }

    public void setRefId(int refId) {
        this.refId = refId;
    }

    public Date getStamp() {
        return stamp;
    }

    public void setStamp(Date stamp) {
        this.stamp = stamp;
    }

    public String getComment() {
        return comment;
    }

    public void setComment(String comment) {
        this.comment = comment;
    }
}

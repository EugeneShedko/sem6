
alter session set "_ORACLE_SCRIPT"=true;


create tablespace TS_AES
  datafile 'C:\New folder\TS_AES.dbf'  --???
  size 7m                       --??? ??????
  autoextend on next 5m         --???? ??????????
  maxsize 20m;                  --???? ??????
  
create temporary tablespace TS_AES_TEMP
  tempfile 'C:\New folder\TS_AES_TEMP.dbf'
  size 5m
  autoextend on next 3m
  maxsize 30m;
  
select *from SYS.DBA_TABLESPACES;

select  TABLESPACE_name, file_name from dba_data_files;

create role RL_AESCORE;

grant create session, create table, create view, create procedure to RL_AESCORE;


SELECT *FROM dba_roles WHERE ROLE = 'RL_AESCORE';
SELECT * FROM DBA_SYS_PRIVS where grantee='RL_AESCORE';

CREATE PROFILE PF_AESCORE;

--TASK6
CREATE PROFILE PF_AESCORE LIMIT
  PASSWORD_LIFE_TIME unlimited        --???-?? ???? ????? ??????
  SESSIONS_PER_USER unlimited           --???-?? ?????? ??? ?????
  failed_login_attempts unlimited       --???-?? ??????? ?????
  password_lock_time unlimited          --???-?? ???? ???????????? ????? ??????
  password_reuse_time unlimited        --????? ?? ???? ????? ????????? ??????
  password_grace_time unlimited   --???-?? ???? ?????????????? ? ????? ??????
  connect_time unlimited              --????? ???? ? ???
  idle_time unlimited;   

select * from DBA_PROFILES;
select * from DBA_PROFILES where profile='PF_CJACORE';  
select * from DBA_PROFILES where profile='DEFAULT';

--TASK8
CREATE USER AES2 identified by Pa$$w0rd --Pa$$w0rd
DEFAULT TABLESPACE TS_AES
TEMPORARY TABLESPACE TS_AES_TEMP
PROFILE PF_AESCORE
ACCOUNT UNLOCK;

DROP USER AES;

GRANT CREATE DATABASE LINK TO AESCORE;
GRANT CREATE PUBLIC DATABASE LINK TO AESCORE;


GRANT CREATE DATABASE LINK TO AES2;
GRANT CREATE PUBLIC DATABASE LINK TO AES2;
GRANT CREATE SESSION TO AES2 


--connect
CREATE DATABASE LINK con1
  CONNECT TO AES1
  IDENTIFIED BY Pa$$w0rd   
  USING 'OracleServer:1521/orcl';    
  
  
alter session set "_ORACLE_SCRIPT"=true;


create tablespace TS_AES
  datafile 'C:\New folder\TS_AES.dbf'  --???
  size 7m                       --??? ??????
  autoextend on next 5m         --???? ??????????
  maxsize 20m;                  --???? ??????
  
create temporary tablespace TS_AES_TEMP
  tempfile 'C:\New folder\TS_AES_TEMP.dbf'
  size 5m
  autoextend on next 3m
  maxsize 30m;
  
select *from SYS.DBA_TABLESPACES;

select  TABLESPACE_name, file_name from dba_data_files;

create role RL_AESCORE;

grant create session, create table, create view, create procedure to RL_AESCORE;


SELECT *FROM dba_roles WHERE ROLE = 'RL_AESCORE';
SELECT * FROM DBA_SYS_PRIVS where grantee='RL_AESCORE';

CREATE PROFILE PF_AESCORE;

--TASK6
CREATE PROFILE PF_AESCORE LIMIT
  PASSWORD_LIFE_TIME unlimited        --???-?? ???? ????? ??????
  SESSIONS_PER_USER unlimited           --???-?? ?????? ??? ?????
  failed_login_attempts unlimited       --???-?? ??????? ?????
  password_lock_time unlimited          --???-?? ???? ???????????? ????? ??????
  password_reuse_time unlimited        --????? ?? ???? ????? ????????? ??????
  password_grace_time unlimited   --???-?? ???? ?????????????? ? ????? ??????
  connect_time unlimited              --????? ???? ? ???
  idle_time unlimited;   

select * from DBA_PROFILES;
select * from DBA_PROFILES where profile='PF_CJACORE';  
select * from DBA_PROFILES where profile='DEFAULT';

--TASK8
CREATE USER AES2 identified by Pa$$w0rd --Pa$$w0rd
DEFAULT TABLESPACE TS_AES
TEMPORARY TABLESPACE TS_AES_TEMP
PROFILE PF_AESCORE
ACCOUNT UNLOCK;

DROP USER AES;

GRANT CREATE DATABASE LINK TO AESCORE;
GRANT CREATE PUBLIC DATABASE LINK TO AESCORE;


GRANT CREATE DATABASE LINK TO AES2;
GRANT CREATE PUBLIC DATABASE LINK TO AES2;
GRANT CREATE SESSION TO AES2 


--connect
    
ALTER SESSION CLOSE DATABASE LINK con2;

DBA_DB_LINKS
USER_DB_LINKS
V$DBLINKS


insert into b values(1, '2');
insert into b values(1, '2');
insert into b values(1, '2');
insert into b values(1, '2');


CREATE TABLE b
     (MFR_ID number(5, 2),
 DESCRIPTION VARCHAR(20) 
     );
     
     drop table b;
     
  grant create table to aes2;
  grant INSERT ANY TABLE to aes2;
  GRANT UNLIMITED TABLESPACE TO aes2;

CREATE DATABASE LINK dblink_AES1one1
CONNECT TO AES1 IDENTIFIED BY Pa$$w0rd
USING
'(DESCRIPTION=
(ADDRESS=
(PROTOCOL=TCP)
(HOST=OracleServer)
(PORT=1521))
(CONNECT_DATA=
(SID=orcl)))';


ALTER SYSTEM SET resource_limit=true;


ALTER SESSION OPEN DATABASE LINK dblink_AES1one1;

CREATE DATABASE LINK dblink_AES1
CONNECT TO AES1 IDENTIFIED BY Pa$$w0rd
USING 'OracleServer'

drop DATABASE LINK dblink_AES1one

    ALTER SESSION CLOSE DATABASE LINK dblink_AES1;
    EXECUTE DBMS_SESSION.CLOSE_DATABASE_LINK(‘dblink_AES1‘);


  select * from B@dblink_AES1;
  
 commit
rollback

insert into B@dblink_AES1 values(1, '8');
insert into B@dblink_AES1 values(1, '7');
commit;

insert into B@dblink_AES1 values(1, '9');
update B@dblink_AES1 set DESCRIPTION='111111'
where DESCRIPTION='7';
commit;


update B@dblink_AES1 set DESCRIPTION='7'
where DESCRIPTION='111111';
insert into B@dblink_AES1 values(1, '10');
commit;

insert into B@dblink_AES1 values('1', '10');


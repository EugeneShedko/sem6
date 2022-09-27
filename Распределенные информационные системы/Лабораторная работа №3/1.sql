
alter session set "_ORACLE_SCRIPT"=true;

select *from b

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
CREATE USER AES12 identified by Pa$$w0rd --Pa$$w0rd
DEFAULT TABLESPACE TS_AES
TEMPORARY TABLESPACE TS_AES_TEMP
PROFILE PF_AESCORE
ACCOUNT UNLOCK;

DROP USER AES11;

GRANT CREATE DATABASE LINK TO AES12;
GRANT CREATE PUBLIC DATABASE LINK TO AES12;
GRANT CREATE SESSION TO AES12


GRANT SELECT, insert, update ON b TO aes12;

select *from b;
SELECT * FROM aes1.b;

commit

insert into aes1.b values(1, '9');
insert into aes1.b values(1, '2');
insert into aes1.b values(1, '2');
ALTER SYSTEM SET resource_limit=true;



SELECT default_tablespace, temporary_tablespace
 FROM dba_users
 WHERE username='AES11' or username='AES12'

CREATE TABLE b
     (MFR_ID number(5, 2) unique,
 DESCRIPTION VARCHAR(20) 
     );
     
     drop table b;
     
  grant create table to aes12;
  grant INSERT ANY TABLE to aes12;
  GRANT UNLIMITED TABLESPACE TO aes12;
  
  
  insert into b values(6, '2');
  
  
update b set GENDER='111111'
where DESCRIPTION='7';
  
  
CREATE TABLE customer
(
id_customer RAW(16) DEFAULT SYS_GUID() NOT NULL ,
login VARCHAR2 (10) ,
pass VARCHAR2 (10) ,
customer_name VARCHAR2 (20) ,
customer_surename VARCHAR2 (30) ,
city VARCHAR2 (30) ,
street VARCHAR2 (30) ,
accounttype_id_account_type RAW(16) NOT NULL
) ;
ALTER TABLE customer ADD CONSTRAINT customer_PK PRIMARY KEY ( id_customer ) ;
CREATE TABLE accounttype
(
id_account_type RAW(16) DEFAULT SYS_GUID() NOT NULL ,
account_name VARCHAR2 (20) ,
discount INTEGER
) ;
ALTER TABLE accounttype ADD CONSTRAINT accounttype_PK PRIMARY KEY ( id_account_type ) ;
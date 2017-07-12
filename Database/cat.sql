CREATE TABLE cat
(
id_category RAW(16) DEFAULT SYS_GUID() NOT NULL ,
category_name VARCHAR2 (20)
) ;
ALTER TABLE cat ADD CONSTRAINT cat_PK PRIMARY KEY ( id_category ) ;
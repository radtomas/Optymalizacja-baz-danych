CREATE TABLE item
(
id_item RAW(16) DEFAULT SYS_GUID() NOT NULL ,
item_name VARCHAR2 (20) ,
price INTEGER ,
weight INTEGER ,
cat_id_category RAW(16) NOT NULL
) ;
ALTER TABLE item ADD CONSTRAINT item_PK PRIMARY KEY ( id_item ) ;
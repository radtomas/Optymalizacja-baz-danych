CREATE TABLE trans
(
id_trans RAW(16) DEFAULT SYS_GUID() NOT NULL ,
trans_date DATE ,
customer_id_customer RAW(16) NOT NULL ,
item_id_item RAW(16) NOT NULL
) ;
ALTER TABLE trans ADD CONSTRAINT trans_PK PRIMARY KEY ( id_trans ) ;
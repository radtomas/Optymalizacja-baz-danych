ALTER TABLE customer ADD CONSTRAINT customer_accounttype_FK FOREIGN KEY
( accounttype_id_account_type ) REFERENCES accounttype ( id_account_type ) ;

ALTER TABLE item ADD CONSTRAINT item_cat_FK FOREIGN KEY ( cat_id_category )
REFERENCES cat ( id_category ) ;

ALTER TABLE trans ADD CONSTRAINT trans_customer_FK FOREIGN KEY ( customer_id_customer )
REFERENCES customer ( id_customer ) ;

ALTER TABLE trans ADD CONSTRAINT trans_item_FK FOREIGN KEY ( item_id_item ) REFERENCES
item ( id_item ) ;
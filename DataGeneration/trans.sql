CREATE OR REPLACE PROCEDURE generate_trans
AS
l_seed BINARY_INTEGER;
l_number INTEGER(10);

i INTEGER(10);

temp_date DATE;

TYPE uid_type IS VARRAY(7000) OF RAW(16);

cus_uid uid_type := uid_type();
item_uid uid_type := uid_type();

cus CUSTOMER.ID_CUSTOMER%TYPE;
it ITEM.ID_ITEM%TYPE;

CURSOR cust_c IS
  SELECT id_customer FROM customer;
CURSOR item_c IS
  SELECT id_item FROM item;

BEGIN 
  l_seed := TO_NUMBER(TO_CHAR(SYSDATE,'YYYYDDMMSS'));
  DBMS_RANDOM.initialize (val => l_seed);

  OPEN cust_c;
  i := 1;
  LOOP
    FETCH cust_c INTO cus;
    EXIT WHEN cust_c%NOTFOUND;
    
    cus_uid.extend;
    cus_uid(i) := cus;
   
    i := i + 1;
  END LOOP;
  CLOSE cust_c;
  
  OPEN item_c;
  i := 1;
  LOOP
    FETCH item_c INTO it;
    EXIT WHEN item_c%NOTFOUND;
    
    item_uid.extend;
    item_uid(i) := it;
   
    i := i + 1;
  END LOOP;
  CLOSE item_c;
  
  
  FOR i IN 1..5000 LOOP
    temp_date := TO_DATE( TRUNC( DBMS_RANDOM.VALUE(TO_CHAR(DATE '2000-01-01','J'), TO_CHAR(DATE '2020-12-31','J'))), 'J');
    
    l_number := DBMS_RANDOM.VALUE(1,3500) + DBMS_RANDOM.VALUE(1,3500);
    cus := cus_uid(l_number);
    
    l_number := DBMS_RANDOM.VALUE(1,63) + DBMS_RANDOM.VALUE(1,63);
    it := item_uid(l_number);
    
    INSERT INTO trans(id_trans, trans_date, customer_id_customer, item_id_item) VALUES(SYS_GUID(), temp_date, cus, it);
  END LOOP;
  
  DBMS_RANDOM.terminate;
END;
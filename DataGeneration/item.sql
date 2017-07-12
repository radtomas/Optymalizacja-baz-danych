create or replace PROCEDURE generate_item 
AS 
l_seed BINARY_INTEGER;
l_number INTEGER(10);

r_number INTEGER(10);
str_name VARCHAR(20);
l_price NUMBER(38);
l_weight NUMBER(38);

ic CAT.ID_CATEGORY%TYPE;

CURSOR item_category IS
  SELECT id_category FROM cat;
BEGIN 
  l_seed := TO_NUMBER(TO_CHAR(SYSDATE,'YYYYDDMMSS'));
  DBMS_RANDOM.initialize (val => l_seed);
  
  OPEN item_category;
  
  LOOP
    FETCH item_category INTO ic;
    EXIT WHEN item_category%NOTFOUND;
    
    l_number := DBMS_RANDOM.VALUE(1,18);
    FOR i IN 0..l_number LOOP
      r_number := DBMS_RANDOM.VALUE(1,20);
      str_name := DBMS_RANDOM.STRING('X', r_number);
      l_price := DBMS_RANDOM.VALUE(1,2000);
      l_weight := DBMS_RANDOM.VALUE(1,300);
      
      dbms_output.put_line('CAT is: '|| ic || 'and NUMBER is' || i || ' and NAME is: ' || str_name); 
      INSERT INTO item(id_item, item_name, price, weight, cat_id_category) VALUES(SYS_GUID(), str_name, l_price, l_weight, ic);
    END LOOP;

    
  END LOOP;
  
  DBMS_RANDOM.terminate;
  CLOSE item_category;
END;
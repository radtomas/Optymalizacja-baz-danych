CREATE OR REPLACE PROCEDURE generate_cat 
AS 
l_seed BINARY_INTEGER;
l_number INTEGER(10);

i INTEGER(1);
str VARCHAR(24);

BEGIN 
  l_seed := TO_NUMBER(TO_CHAR(SYSDATE,'YYYYDDMMSS'));
  DBMS_RANDOM.initialize (val => l_seed);
  
  FOR i IN 1..10 LOOP
    l_number := DBMS_RANDOM.VALUE(1,10);
    str := DBMS_RANDOM.STRING('X', l_number);

    dbms_output.put_line('NUMBER is: '||l_number || ' and STR is: ' || str); 
    INSERT INTO cat(id_category, category_name) VALUES(SYS_GUID(), str);
  END LOOP;
  
  DBMS_RANDOM.terminate;
END;
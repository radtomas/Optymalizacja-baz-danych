create or replace PROCEDURE generate_users 
AS
l_seed BINARY_INTEGER;
l_number INTEGER(10);

i INTEGER(1);

TYPE names_array IS VARRAY(150) OF VARCHAR(20);
TYPE surenames_array IS VARRAY(150) OF VARCHAR(30);
TYPE addres IS VARRAY(150) OF VARCHAR(30);
TYPE acc_uid IS VARRAY(3) OF RAW(16);

names names_array := names_array();
surenames surenames_array := surenames_array();
cities addres := addres();
streets addres := addres();
act_array acc_uid := acc_uid();

temp_name VARCHAR(20);
temp_surename VARCHAR(30);
temp_city VARCHAR(30);
temp_street VARCHAR(30);

act ACCOUNTTYPE.ID_ACCOUNT_TYPE%TYPE;

CURSOR acctype_c IS
  SELECT id_account_type FROM accounttype;

BEGIN 
  l_seed := TO_NUMBER(TO_CHAR(SYSDATE,'YYYYDDMMSS'));
  DBMS_RANDOM.initialize (val => l_seed);
  
  FOR i IN 1..150 LOOP
    l_number := DBMS_RANDOM.VALUE(5,15);
    names.extend;
    names(i) := DBMS_RANDOM.STRING('X', l_number);
    
    l_number := DBMS_RANDOM.VALUE(5,25);
    surenames.extend;
    surenames(i) := DBMS_RANDOM.STRING('X', l_number);
    
    l_number := DBMS_RANDOM.VALUE(5,15);
    cities.extend;
    cities(i) := DBMS_RANDOM.STRING('X', l_number);
    
    l_number := DBMS_RANDOM.VALUE(5,15);
    streets.extend;
    streets(i) := DBMS_RANDOM.STRING('X', l_number);
  END LOOP;
  
  OPEN acctype_c;
  i := 1;
  LOOP
    FETCH acctype_c INTO act;
    EXIT WHEN acctype_c%NOTFOUND;
    
    act_array.extend;
    act_array(i) := act;
   
    i := i + 1;
  END LOOP;
  CLOSE acctype_c;
  
  FOR i IN 1..5000 LOOP
    l_number := DBMS_RANDOM.VALUE(1,75) + DBMS_RANDOM.VALUE(1,75);
    temp_name := names(l_number);
    
    l_number := DBMS_RANDOM.VALUE(1,75) + DBMS_RANDOM.VALUE(1,75);
    temp_surename := surenames(l_number);
    
    l_number := DBMS_RANDOM.VALUE(1,75) + DBMS_RANDOM.VALUE(1,75);
    temp_city := cities(l_number);
    
    l_number := DBMS_RANDOM.VALUE(1,75) + DBMS_RANDOM.VALUE(1,75);
    temp_street := streets(l_number);
    
    IF(l_number < 3000) THEN
      l_number := 1;
    ELSIF(l_number < 4500) THEN
      l_number := 2;
    ELSE
      l_number := 3;
    END IF;
    act := act_array(l_number);
    
     INSERT INTO customer(id_customer, login, pass, customer_name, customer_surename, city, street, accounttype_id_account_type) VALUES(SYS_GUID(), CONCAT('user', i), '12345678', temp_name, temp_surename, temp_city, temp_street, act);
  END LOOP;
  
  DBMS_RANDOM.terminate;
END;

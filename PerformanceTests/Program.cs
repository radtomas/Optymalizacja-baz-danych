using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnTest
{
    using Oracle.DataAccess.Client;
    using System.IO;
    using System.Threading;

    class Program
    {
        static string cs = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                        (HOST=)(PORT=)))(CONNECT_DATA=(SERVER=DEDICATED)
                        (SERVICE_NAME=)));User Id=;Password=;";
        static void user_A()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name); 

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_item) as \"count\" from item";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_items = new string[rows];

                    sql = "select rawtohex(id_item) from ITEM";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        i++;
                    }

                    //uzytkownik poszedl na kawe
                    var rand = new Random(id);
                    int temp = rand.Next(1, 1800) * 1000;
                    Thread.Sleep(temp);

                    //zlozenie zamowienie
                    temp = rand.Next(0, rows);

                    sql = "insert into trans values(SYS_GUID(), current_date, hextoraw(:param1), hextoraw(:param2))";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("param1", id_cus));
                    cmd.Parameters.Add(new OracleParameter("param2", id_items[temp]));

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }
        static void user_B()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //uzytkownik poszedl na kawe
                    var rand = new Random(id);
                    int temp = rand.Next(1, 1800) * 1000;
                    Thread.Sleep(temp);

                    //zmiana danych
                    temp = rand.Next(0, rows);//wylosowanie jednego przedmiotu

                    sql = "update customer set city = :param1 where id_customer = :param2";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("param1", "city" + id));
                    cmd.Parameters.Add(new OracleParameter("param2", id_cus));

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }
        static void user_C()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_item) as \"count\" from item";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_items = new string[rows];

                    sql = "select rawtohex(id_item) from ITEM";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        i++;
                    }

                    //uzytkownik poszedl na kawe
                    var rand = new Random(id);
                    int temp = rand.Next(1, 1800) * 1000;
                    Thread.Sleep(temp);

                    //zmiana danych
                    temp = rand.Next(0, rows);//wylosowanie jednego przedmiotu

                    sql = "update item set item_name = :param1 where id_item = :param2";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("param1", "kotlet" + id));
                    cmd.Parameters.Add(new OracleParameter("param2", id_items[temp]));

                    stmt = cmd.ExecuteReader();//wykonanie zapytania
                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }
        static void user_D()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_item) as \"count\" from item";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_items = new string[rows];

                    sql = "select rawtohex(id_item) from ITEM";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        i++;
                    }

                    //uzytkownik poszedl na kawe
                    var rand = new Random(id);
                    int temp = rand.Next(1, 1800) * 1000;
                    Thread.Sleep(temp);

                    //zlozenie zamowienie
                    temp = rand.Next(0, rows);

                    sql = "delete from trans where id_trans = :param1";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("param1", id_items[temp]));

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }
        static void user_E()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_trans) as \"count\" from trans";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_trans = new string[rows];

                    sql = "select rawtohex(id_trans) from trans";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_trans[i] = stmt["rawtohex(ID_trans)"].ToString();
                        i++;
                    }

                    //uzytkownik poszedl na kawe
                    var rand = new Random(id);
                    int temp = rand.Next(1, 1800) * 1000;
                    //Thread.Sleep(temp);

                    //zlozenie zamowienie
                    temp = rand.Next(0, rows);

                    sql = "delete from trans where id_trans = :param1";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("param1", id_trans[temp]));

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }

        static void fuzzy_a()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_item) as \"count\" from item where item_name = 'kotlet1'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_items = new string[rows];
                    string[] name = new string[rows];
                    string[] price = new string[rows];
                    string[] weight = new string[rows];

                    sql = "select rawtohex(id_item), item_name, price, weight from ITEM where item_name = 'kotlet1'";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        name[i] = stmt["item_name"].ToString();
                        price[i] = stmt["price"].ToString();
                        weight[i] = stmt["weight"].ToString();

                        Console.WriteLine("ID: {0}, Name: {1}, Price: {2}, Weight: {3}", id, name[i], price[i], weight[i]);

                        i++;
                    }

                    //uzytkownik poszedl na kawe
                    int temp = 120000;
                    Thread.Sleep(temp);

                    sql = "select rawtohex(id_item), item_name, price, weight from ITEM where item_name = 'kotlet1'";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        name[i] = stmt["item_name"].ToString();
                        price[i] = stmt["price"].ToString();
                        weight[i] = stmt["weight"].ToString();

                        Console.WriteLine("ID: {0}, Name: {1}, Price: {2}, Weight: {3}", id, name[i], price[i], weight[i]);

                        i++;
                    }

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }

        static void fuzzy_b()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //uzytkownik poszedl na kawe
                    int temp = 60000;
                    Thread.Sleep(temp);

                    sql = "update item set price = 200 where item_name = 'kotlet1'";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania
                    Console.WriteLine("ID: {0} UPDATE", id);

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }

        static void phantom_a()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //pobranie listy przedmiotow
                    //pobranie ilosci
                    sql = "select count(id_item) as \"count\" from item where price = 200";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());

                    string[] id_items = new string[rows];
                    string[] name = new string[rows];
                    string[] price = new string[rows];
                    string[] weight = new string[rows];

                    sql = "select rawtohex(id_item), item_name, price, weight from ITEM where price = 200";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    int i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        name[i] = stmt["item_name"].ToString();
                        price[i] = stmt["price"].ToString();
                        weight[i] = stmt["weight"].ToString();


                        i++;
                    }
                    for(i = 0; i < 2; i++)
                        Console.WriteLine("ID: {0}, Name: {1}, Price: {2}, Weight: {3}", id, name[i], price[i], weight[i]);

                    //uzytkownik poszedl na kawe
                    //int temp = 120000;
                    //Thread.Sleep(temp);
                    Console.WriteLine("ID: 20 INSERT");
                    //ponowne pobranie
                    sql = "select count(id_item) as \"count\" from item where price = 200";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();
                    rows = Int32.Parse(stmt["count"].ToString());
                    
                    sql = "select rawtohex(id_item), item_name, price, weight from ITEM where price = 200";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania

                    i = 0;
                    while (stmt.Read())//zapis do tablicy
                    {
                        id_items[i] = stmt["rawtohex(ID_ITEM)"].ToString();
                        name[i] = stmt["item_name"].ToString();
                        price[i] = stmt["price"].ToString();
                        weight[i] = stmt["weight"].ToString();

                        Console.WriteLine("ID: {0}, Name: {1}, Price: {2}, Weight: {3}", id, name[i], price[i], weight[i]);

                        i++;
                    }

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }

        static void phantom_b()
        {
            OracleConnection oraconn = new OracleConnection(cs);

            int id = Int32.Parse(Thread.CurrentThread.Name);

            try
            {
                oraconn.Open();

                //logowanie
                string sql = "select count(id_customer) as \"count\" from customer where login = 'user" + id + "'";
                OracleCommand cmd = new OracleCommand(sql, oraconn);//zapytanie
                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader stmt = cmd.ExecuteReader();//wykonanie zapytania
                stmt.Read();
                int rows = Int32.Parse(stmt["count"].ToString());

                if (rows == 1)
                {
                    //pobranie id uzytkownika
                    sql = "select rawtohex(id_customer) from customer where login = 'user" + id + "'";
                    cmd = new OracleCommand(sql, oraconn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();
                    stmt.Read();

                    string id_cus = stmt["rawtohex(id_customer)"].ToString();

                    //uzytkownik poszedl na kawe
                    int temp = 60000;
                    Thread.Sleep(temp);

                    sql = "update item set price = 200 where item_name = 'kotlet41'";
                    cmd = new OracleCommand(sql, oraconn);//zapytanie
                    cmd.CommandType = System.Data.CommandType.Text;

                    stmt = cmd.ExecuteReader();//wykonanie zapytania
                    Console.WriteLine("ID: {0} INSERT", id);

                }
                else
                {
                    throw new Exception();
                }

                oraconn.Close();
                cmd.Dispose();
                stmt.Dispose();
            }
            catch (OracleException ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine("Oracle Exception Message");
                    writer.WriteLine("Exception Message: " + ex.Message);
                    writer.WriteLine("Exception Source: " + ex.Source);
                }
            }
            catch (Exception ex)
            {
                string loc = "log\\{0}.txt";
                loc = String.Format(loc, Thread.CurrentThread.Name);

                using (StreamWriter writer = new StreamWriter(loc, true))
                {
                    writer.WriteLine(Thread.CurrentThread.Name);
                    writer.WriteLine(ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            /*
            int maxThread = 1000;
            
            var threads = new List<Thread>();
            for (int i = 1; i <= maxThread; i++)
            {
                Thread nova;
                if(i <= 500)
                {
                    nova = new Thread(user_A);
                }
                else if(i <= 600)
                {
                    nova = new Thread(user_B);
                }
                else if(i <= 700)
                {
                    nova = new Thread(user_C);
                }
                else if(i <= 800)
                {
                    nova = new Thread(user_D);
                }
                else
                {
                    nova = new Thread(user_E);
                }
                nova.Name = i.ToString();
                nova.Start();
                threads.Add(nova);
            }
            foreach (var thread in threads)
                thread.Join();
            */


            //fuzzy
            /*
            int maxThread = 2;

            var threads = new List<Thread>();
            for (int i = 1; i <= maxThread; i++)
            {
                Thread nova;
                if (i == 1)
                {
                    nova = new Thread(fuzzy_a);
                }else
                {
                    nova = new Thread(fuzzy_b);
                }
                nova.Name = i.ToString();
                nova.Start();
                threads.Add(nova);
            }
            foreach (var thread in threads)
                thread.Join();
           */

            int maxThread = 1;

            var threads = new List<Thread>();
            for (int i = 1; i <= maxThread; i++)
            {
                Thread nova;
                /*
                if (i == 1)
                {
                    //nova = new Thread(phantom_a);
                }
                else
                {
                    nova = new Thread(phantom_a);
                }
                */
                nova = new Thread(phantom_a);
                nova.Name = i.ToString();
                nova.Start();
                threads.Add(nova);
            }
            foreach (var thread in threads)
                thread.Join();

            Console.WriteLine("DONE");
            Console.ReadKey();
        }

    }
}

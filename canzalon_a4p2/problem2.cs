/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p2
 * File/Module: problem2.cs 
 * Author: Christopher Anzalone 
 * 
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace canzalon_a4p2
{
    class problem2
    {
        public static void debugOutput(char letter)
        {
            Console.WriteLine("--------------");
            Console.WriteLine("--"+letter+"--");
            Console.WriteLine("--------------");
        }

        static void Main()
        {
            Console.WriteLine(@"Please enter database name, table name, and number of inputs separated by blank spaces.");
            string line = Console.ReadLine();
            Regex r = new Regex("[ ]+");
            string[] user_inputs = r.Split(line);

            string databaseName = user_inputs[0];
            string tableName = user_inputs[1];
            int numInputs = int.Parse(user_inputs[2]);

            /*TESTING: Printing user_inputs*/
            for (int x = 0; x < 3; x++)
            {
                Console.Write(user_inputs[x]);
                Console.WriteLine("\n");
            }

                problem2.debugOutput('A');

            string connString = @"
                server = dbw.cse.fau.edu;
                integrated security = false;
                database = " + databaseName + @"; 
                user id = canzalon;
                password = spiritedaway;
                MultipleActiveResultSets = True;";

            problem2.debugOutput('B');

            SqlConnection conn = null;
            SqlDataReader reader_one = null,
                reader_two = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                string user_query_one = "SELECT * FROM " + tableName;
                string user_insert_to_table = @"insert into " + tableName + " values(";

                problem2.debugOutput('C');

                Console.WriteLine();
                Console.WriteLine(user_query_one);
                Console.WriteLine();

                problem2.debugOutput('D');

                SqlCommand cmd_one = new SqlCommand(user_query_one, conn);

                problem2.debugOutput('E');

                reader_two = cmd_one.ExecuteReader(CommandBehavior.SchemaOnly);

                problem2.debugOutput('F');

                int num_of_fields = reader_two.FieldCount;

                string[] names = new string[num_of_fields];
                string[] types = new string[num_of_fields];

                for (int i = 0; i < num_of_fields; i++)
                {
                 //   user_insert_to_table = user_insert_to_table + "@inputs" + i;
                    user_insert_to_table += "@inputs" + i;

                    if ((i + 1) < num_of_fields)
                    {
                        user_insert_to_table = user_insert_to_table + ",";
                    }

                    names[i] = reader_two.GetName(i);
                    types[i] = reader_two.GetDataTypeName(i);
                }

                //user_insert_to_table = user_insert_to_table + ")";
                user_insert_to_table += ")";

                reader_two.Close();

                Console.WriteLine(user_insert_to_table);
                problem2.debugOutput('G');
                SqlCommand cmd_two = new SqlCommand(user_insert_to_table, conn);
                problem2.debugOutput('H');
                for (int i = 0; i < num_of_fields; i++)
                {
                    if (types[i] == "int")
                    {
                        cmd_two.Parameters.Add("@inputs" + i, SqlDbType.Int);
                    }
                    else if (types[i] == "float")
                    {
                        cmd_two.Parameters.Add("@inputs" + i, SqlDbType.Float);
                    }
                    else
                    {
                        cmd_two.Parameters.Add("@inputs" + i, SqlDbType.VarChar, 20);
                    }
                }
                problem2.debugOutput('I');
                cmd_two.Prepare();
                problem2.debugOutput('J');
                /*Print table header*/
                for (int j = 0; j < num_of_fields; j++)
                {
                    Console.Write("{0,-8}", names[j]);
                }

                Console.WriteLine();
                Console.WriteLine();

                /*Execute query_one and print the associated table*/
                reader_one = cmd_one.ExecuteReader();
                while (reader_one.Read())
                {
                    for (int j = 0; j < num_of_fields; j++)
                    {
                        /* Since we are only getting the fields to print them,
                        we can get them as strings. */
                        Console.Write("{0,-8}", reader_one[j]);
                    }
                    Console.WriteLine();
                }
                reader_one.Close();

                /*Loop for new entry inputss to desired table and database*/
                for (int i = 0; i < /*num_of_fields*/numInputs; i++)
                {
                    try
                    {
                        string insertq = "Please enter ";
                        for (int j = 0; j < num_of_fields; j++)
                        {
                            insertq += names[j];
                            insertq += " ";
                        }
                        Console.WriteLine(insertq);

                        string line2 = Console.ReadLine();
                        Regex r2 = new Regex("[ ]+");
                        string[] inputs2 = r2.Split(line2);

                        /*Printing input for testing purposes*/
                        for (int k = 0; k < num_of_fields; k++)
                        {
                            cmd_two.Parameters[k].Value = inputs2[k];
                            Console.WriteLine(inputs2[k]);
                        }

                        cmd_two.ExecuteNonQuery();
                    }
                    catch (Exception d)
                    {
                        Console.WriteLine(d);
                    }

                    /* Print table header. */
                    for (int j = 0; j < num_of_fields; j++)
                        Console.Write("{0,-8}", names[j]);
                    Console.WriteLine();
                    Console.WriteLine();

                    /*Execute query_one and print its table*/
                    reader_one = cmd_one.ExecuteReader();

                    while (reader_one.Read())
                    {
                        for (int j = 0; j < num_of_fields; j++)
                        {
                            /* Since we are only getting the fields to print them,
                            we can get them as strings. */
                            Console.Write("{0,-8}", reader_one[j]);
                        }
                        Console.WriteLine();
                    }

                    reader_one.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Bad table name or field name, retrieval terminated." + e);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

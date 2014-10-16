/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p1
 * File/Module: problem1.cs
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
//using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace canzalon_a4p1
{
    class problem1
    {
        static void Main(string[] args)
        {
            string connString = @"
          server = dbw.cse.fau.edu;
          integrated security = false;
          database = canzalon_spjdatabase4;
          user id = canzalon;
          password = pass;
          ";

            string stmt_one = "insertj";
            string stmt_two = @"select * from jsp(@snum, @pnum)";
            string stmt_three = @"select * from j";
            string transaction = "BEGIN TRAN";
            string rollback = "ROLLBACK TRAN";

            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                SqlDataReader reader_one = null;
                SqlDataReader reader_two = null;

                SqlCommand cmd_one = new SqlCommand(stmt_one, conn);
                SqlCommand cmd_two = new SqlCommand(stmt_two, conn);
                SqlCommand cmd_three = new SqlCommand(stmt_three, conn);
                SqlCommand cmd_trans = new SqlCommand(transaction, conn);
                SqlCommand cmd_roll = new SqlCommand(rollback, conn);

                cmd_one.CommandType = CommandType.StoredProcedure;
                cmd_two.CommandType = CommandType.Text;

                cmd_one.Parameters.Add("@jnum", SqlDbType.VarChar, 5);
                cmd_one.Parameters.Add("@jname", SqlDbType.VarChar, 20);
                cmd_one.Parameters.Add("@city", SqlDbType.VarChar, 10);

                SqlParameter ret = cmd_one.Parameters.Add("@code", SqlDbType.Int);
                ret.Direction = ParameterDirection.ReturnValue;
                cmd_one.Prepare();

                cmd_two.Parameters.Add("@snum", SqlDbType.VarChar, 5);
                cmd_two.Parameters.Add("@pnum", SqlDbType.VarChar, 5);
                cmd_two.Prepare();

                string user_input = "";

                cmd_trans.ExecuteNonQuery();

                Console.WriteLine("J#  Jname  City");

                reader_one = cmd_three.ExecuteReader();
                while (reader_one.Read())
                {
                    Console.WriteLine(
                        reader_one[0].ToString() + "  "
                        + reader_one[1].ToString() + "  "
                        + reader_one[2].ToString());
                }
                reader_one.Close();

                while (user_input != "exit")
                {
                    Console.WriteLine(@"Please enter insertj, jsp, or exit.");
                    user_input = Console.ReadLine();

                    if (user_input == "insertj")
                    {
                        Console.WriteLine(@"Please enter J# JName City seperated by blank spaces, or exit to terminate.");
                        string user_input_JStuff = Console.ReadLine();

                        Regex r = new Regex("[ ]+");
                        string[] fields = r.Split(user_input_JStuff);

                        if (fields[0] == "exit") break;

                        cmd_one.Parameters[0].Value = fields[0];
                        cmd_one.Parameters[1].Value = fields[1];
                        cmd_one.Parameters[2].Value = fields[2];

                        cmd_one.ExecuteNonQuery();

                        int n = (int)ret.Value;
                        if (n == 0) Console.WriteLine
                            ("Insert for " + fields[0] + " complete.");
                        else Console.WriteLine
                            ("Insert for " + fields[0] + " failed.");

                        Console.WriteLine(" ");
                        Console.WriteLine("J#  Jname  City");

                        reader_one = cmd_three.ExecuteReader();
                        while (reader_one.Read())
                        {
                            Console.WriteLine(reader_one[0].ToString() + "  "
                                              + reader_one[1].ToString() + "  "
                                              + reader_one[2].ToString());
                        }
                        reader_one.Close();
                    }
                    else if (user_input == "jsp")
                    {
                        Console.WriteLine(@"Please enter S# and P#, separated by blank spaces.");
                        string line = Console.ReadLine();
                        Regex r = new Regex("[ ]+");
                        string[] fields = r.Split(line);

                        cmd_two.Parameters[0].Value = fields[0];
                        cmd_two.Parameters[1].Value = fields[1];

                        reader_two = cmd_two.ExecuteReader();
                        while (reader_two.Read())
                        {
                            Console.WriteLine(reader_two[0].ToString() + "  "
                                              + reader_two[1].ToString() + "  "
                                              + reader_two[2].ToString());
                        }
                        reader_two.Close();

                        Console.WriteLine(" ");
                        Console.WriteLine("J#  Jname  City");

                        reader_one = cmd_three.ExecuteReader();
                        while (reader_one.Read())
                        {
                            Console.WriteLine(reader_one[0].ToString() + "  "
                                              + reader_one[1].ToString() + "  "
                                              + reader_one[2].ToString());
                        }
                        reader_one.Close();
                    }
                    else
                    {
                        if (user_input != "exit")
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine(@"Incorrect input.");
                            Console.WriteLine(" ");
                        }
                    }
                }

                cmd_roll.ExecuteNonQuery();

                Console.WriteLine(" ");
                Console.WriteLine("J#  Jname  City ");

                reader_one = cmd_three.ExecuteReader();

                while (reader_one.Read())
                {
                    Console.WriteLine(
                        reader_one[0].ToString() + "  "
                        + reader_one[1].ToString() + "  "
                        + reader_one[2].ToString());
                }

                reader_one.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Program aborted: " + e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
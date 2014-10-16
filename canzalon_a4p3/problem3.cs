/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p3
 * File/Module: problem3.cs
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

namespace canzalon_a4p3
{
    class problem3
    {
        static void Main(string[] args)
        {
            string connString = @"
            server = dbw.cse.fau.edu;
            integrated security = false;
            database = canzalon_spjdatabase4;
            user id  = canzalon;
            password = spiritedaway;
            MultipleActiveResultSets = True";

            string query = "select * from j";
            SqlDataAdapter sad = new SqlDataAdapter(query, connString);
            DataSet ds = new DataSet();
            sad.Fill(ds, "j");
            //ds.Tables["j"].ColumnChanged += new DataColumnChangeEventHandler(MyHandler);

            SqlDataAdapter spjad = new SqlDataAdapter("select * from spj", connString);
            spjad.Fill(ds, "spj");

            DataColumn parentcol = ds.Tables["j"].Columns["j#"];
            DataColumn childcol = ds.Tables["spj"].Columns["j#"];

            DataRelation dr = new DataRelation("spj_j", parentcol, childcol);
            ds.Relations.Add(dr);
            DataTable spjtable = ds.Tables["spj"];

            string ask = "yay";

            DataTable jtable = ds.Tables["j"];

            while (ask != "exit")
            {
                    Console.WriteLine("Please enter insert, getp, dumpxml, insertxml, or exit");
                    ask = Console.ReadLine();

                    if (ask == "insert")
                    {

                        /* Insert new record in DataTable. */
                        string insertq = "Please enter J# JNAME CITY seperated by blanks";
                   
                        Console.WriteLine(insertq);
                        string line = Console.ReadLine();
                        Regex r= new Regex("[ ]+");
                        string[] inputs = r.Split(line);
                        string jnum = inputs[0];
                        string jname = inputs[1];
                        string city = inputs[2];

                        DataRow nr = jtable.NewRow();
                        nr.BeginEdit();
                        nr["j#"] = jnum;
                        nr["jname"] = jname;
                        nr["city"] = city;
                        nr.EndEdit();
                        jtable.Rows.Add(nr);

                        SqlCommandBuilder b = new SqlCommandBuilder(sad);
                        /* Propagate above insert, delete, and update to the database. */
                        sad.Update(jtable);
                    }
                    else if (ask == "getp")
                    {
                        Console.WriteLine("Please enter a JNAME");
                        string jname = Console.ReadLine();
                        DataRow[] rows = spjtable.Select("Parent.jname = '"+jname + "'");
                        foreach (DataRow row in rows)
                        { Console.WriteLine("P# = " + row["P#"]); }
                    }
                    else if (ask == "dumpxml")
                    {
                        Console.WriteLine("Please enter a xml file name such as jspj.xml");
                        string filename = Console.ReadLine();
                        ds.Relations["spj_j"].Nested = true;
                        /* Write XML representation of DataSet to a file. */
                        ds.WriteXml(filename); 
                    }
                    else if (ask == "insertxml")
                    {
                        /* Use XML file to insert into DataTables, and propagate to 
                    database. */
                        Console.WriteLine("Please enter a xml file name such as jspj.xml");
                        string filename = Console.ReadLine();
                        ds.ReadXml(filename);/* In a Visual Studio Console  
                          Application, newstuff.xml should be located 
                          in the bin\Debug folder. */
                        SqlCommandBuilder b2 = new SqlCommandBuilder(sad);
                        SqlCommandBuilder b3 = new SqlCommandBuilder(spjad);
                        sad.Update(jtable);
                        spjad.Update(spjtable);
                }           
            }
        }
    }
}
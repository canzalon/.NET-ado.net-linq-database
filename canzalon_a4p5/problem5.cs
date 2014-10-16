/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p5
 * File/Module: problem5.cs
 * Author: Christopher Anzalone 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;

namespace canzalon_a4p5
{
    class Program
    {
        static void Main(string[] args)
        {

            /* Create DataContext object. */
            DataClasses1DataContext db = new DataClasses1DataContext();

            Console.WriteLine("A. Print the s#s and snames of suppliers who supply a bolt");
            var supps5 =
                from sx in db.S
                where sx.SPJs.Any(spjx => spjx.P.pname == "Bolt")
                select new { sx.snum, sx.sname };
            foreach (var s in supps5) Console.WriteLine(s);
            Console.WriteLine();

            Console.WriteLine("B.Same query as above, done with join clauses, only containing one clause, and without using any relationships");
            var supps6 =
                (from sx in db.S
                 join spjx in db.SPJs on sx.snum equals spjx.snum
                 where spjx.P.pname == "Bolt"
                 select new { sx.snum, sx.sname }).Distinct();
            foreach (var s in supps6) Console.WriteLine("S# = " + s.snum + " SNAME = " + s.sname);
            Console.WriteLine();

            Console.WriteLine
            ("C.Same query as above, but done with two from operators:");
            var supps7 =
                (from sx in db.S
                 from spjx in db.SPJs
                 where spjx.pnum == "P1" && spjx.snum == sx.snum
                 select new { sx.snum, sx.sname }).Distinct();
            foreach (var s in supps7) Console.WriteLine("S# = " + s.snum + " SNAME = " + s.sname);
            Console.WriteLine();

            Console.WriteLine("D.For each supplier having avg > S3, print the s#");
            var savg2 = (from spjx in db.SPJs
                         group spjx by spjx.snum into gs
                         where gs.Max(spjx => spjx.qty) > (from spjy in db.SPJs
                                                           where spjy.snum == "S3"
                                                           select spjy.qty).Max()
                         select new
                         {
                             gs.Key,
                             max =
                                 gs.Max(spjx => spjx.qty)
                         });
            foreach (var x in savg2)
                Console.WriteLine("s# = " + x.Key);

            Console.WriteLine("E.s# and max quantity for suppliers with avg qty > avg qty of S3.");
            var savg4 = (from spjx in db.SPJs
                         group spjx by spjx.snum into gs
                         let max = gs.Max(spjx => spjx.qty)
                         where max > (from spjy in db.SPJs
                                      where spjy.snum == "S3"
                                      select spjy.qty).Max()
                         select new { gs.Key, max });
            foreach (var x in savg4)
                Console.WriteLine("s# = " + x.Key + " max = " + x.max);

            Console.WriteLine("F.snums of each suppliers that supply some part in an avg qty > 320.");
            Console.WriteLine("Previous query, but using let.");
            var savg9 = (from spjx in db.SPJs
                         group spjx by new { spjx.snum, spjx.pnum } into gs
                         let avg = gs.Average(spjx => spjx.qty)
                         where avg > 320
                         select new { gs.Key.snum }).Distinct();
            foreach (var x in savg9)
                Console.WriteLine("s# = " + x.snum);






        }

    }
}

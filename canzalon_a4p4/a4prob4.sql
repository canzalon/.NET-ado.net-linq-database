/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p4
 * File/Module: a4prob4.sql
 * Author: Christopher Anzalone 
 * 
 */

use canzalon_spjdatabase4;
go

BEGIN TRAN
SELECT * from S;

SELECT  dbo.VARI(status) AS "VARI of Status" FROM S
go

/* Rollback Transactions */

print 'Rollingback Transactions';
ROLLBACK TRAN
go

SELECT * from S;
go

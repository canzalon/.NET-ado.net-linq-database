/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p1
 * File/Module: problem2a.sql (file from .NET-transact-sql-database)
 * Author: Christopher Anzalone 
 * 
 */

use canzalon_spjdatabase4;
go

create procedure insertj @jnum varchar(5), @jname varchar(20), @city varchar(10) as
	declare @insCount integer;
	SELECT @insCount = count(*) FROM J WHERE j# = @jnum;
	if (@insCount = 1)
		begin
			return -1;
		end;
	INSERT INTO J VALUES(@jnum, @jname, @city);
	return 0;
go

create procedure deletej @jnum varchar(5) as
	declare @delCount integer; 
	SELECT @delCount = count(*) FROM J WHERE j# = @jnum;
	if (@delCount = 0)
		begin
			return -1;
		end
	DELETE FROM J WHERE j# = @jnum;
	return 0;
go

create procedure updatej @jnum varchar(5), @newCity varchar(10) as 
	declare @updCount integer;
	SELECT @updCount = count(*) FROM J WHERE j# = @jnum;
	if (@updCount = 0)
		begin
			return -1;
		end
	UPDATE J SET city = @newCity WHERE j# = @jnum;
	return 0;
go

create function jsp (@snum varchar(10), @pnum varchar(10)) returns table as 
	return (
		SELECT j.* 
		FROM J, SPJ 
		WHERE SPJ.s# = @snum AND SPJ.p# = @pnum AND SPJ.j# = J.j#
			)
go


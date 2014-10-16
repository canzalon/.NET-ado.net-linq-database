/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p4
 * File/Module: createnum.sql
 * Author: Christopher Anzalone 
 * 
 */

use canzalon_spjnum;

create table S(snum varchar(5), sname varchar(20), 
  status integer, city varchar(10), primary key(snum));
create table P(pnum varchar(5), pname varchar(20), 
  color varchar(10), 
  weight integer, city varchar(10), primary key(pnum));
create table J(jnum varchar(5), jname varchar(20), 
  city varchar(10), primary key(jnum));
create table SPJ(snum varchar(5), pnum varchar(5), 
  jnum varchar(5), qty integer, primary key(snum, pnum, jnum), 
  foreign key(snum) references S, 
  foreign key(pnum) references P, 
  foreign key(jnum) references J);
go

insert into S values('S1', 'Smith', 20, 'London');
insert into S values('S2', 'Jones', 10, 'Paris');
insert into S values('S3', 'Blake', 30, 'Paris');
insert into S values('S4', 'Clark', 20, 'London');
insert into S values('S5', 'Adams', 30, 'Athens');
go

insert into P values('P1', 'Nut', 'Red', 12, 'London');
insert into P values('P2', 'Bolt', 'Green', 17, 'Paris');
insert into P values('P3', 'Screw', 'Blue', 17, 'Rome');
insert into P values('P4', 'Screw', 'Red', 14, 'London');
insert into P values('P5', 'Cam', 'Blue', 12, 'Paris');
insert into P values('P6', 'Cog', 'Red', 19, 'London');
go

insert into J values('J1', 'Sorter', 'Paris');
insert into J values('J2', 'Punch', 'Rome');
insert into J values('J3', 'Reader', 'Athens');
insert into J values('J4', 'Console', 'Athens');
insert into J values('J5', 'Collator', 'London');
insert into J values('J6', 'Terminal', 'Oslo');
insert into J values('J7', 'Tape', 'London');
go

insert into SPJ values('S1', 'P1', 'J1', 200);
insert into SPJ values('S1', 'P1', 'J4', 700);
insert into SPJ values('S2', 'P3', 'J1', 400);
insert into SPJ values('S2', 'P3', 'J2', 200);
insert into SPJ values('S2', 'P3', 'J3', 200);
insert into SPJ values('S2', 'P3', 'J4', 500);
insert into SPJ values('S2', 'P3', 'J5', 600);
insert into SPJ values('S2', 'P3', 'J6', 400);
insert into SPJ values('S2', 'P3', 'J7', 800);
insert into SPJ values('S2', 'P5', 'J2', 100);
insert into SPJ values('S3', 'P3', 'J1', 200);
insert into SPJ values('S3', 'P4', 'J2', 500);
insert into SPJ values('S4', 'P6', 'J3', 300);
insert into SPJ values('S4', 'P6', 'J7', 300);
insert into SPJ values('S5', 'P2', 'J2', 200);
insert into SPJ values('S5', 'P2', 'J4', 100);
insert into SPJ values('S5', 'P5', 'J5', 500);
insert into SPJ values('S5', 'P5', 'J7', 100);
insert into SPJ values('S5', 'P6', 'J2', 200);
insert into SPJ values('S5', 'P1', 'J4', 100);
insert into SPJ values('S5', 'P3', 'J4', 200);
insert into SPJ values('S5', 'P4', 'J4', 800);
insert into SPJ values('S5', 'P5', 'J4', 400);
insert into SPJ values('S5', 'P6', 'J4', 500);
go

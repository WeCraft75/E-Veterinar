/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     23.11.2021 21:15:17                          */
/*==============================================================*/

-- Create a new database called 'eveterinar'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
   SELECT name
FROM sys.databases
WHERE name = N'eveterinar'
)
CREATE DATABASE eveterinar
GO

USE eveterinar
GO

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('EVIDENCA') and o.name = 'FK_EVIDENCA_JE_BILO_O_NAROCILO')
alter table EVIDENCA
   drop constraint FK_EVIDENCA_JE_BILO_O_NAROCILO
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('EVIDENCA') and o.name = 'FK_EVIDENCA_JE_ZABELE_TERMIN')
alter table EVIDENCA
   drop constraint FK_EVIDENCA_JE_ZABELE_TERMIN
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('JE_BILA_OPRAVLJENA') and o.name = 'FK_JE_BILA__JE_BILA_O_STORITEV')
alter table JE_BILA_OPRAVLJENA
   drop constraint FK_JE_BILA__JE_BILA_O_STORITEV
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('JE_BILA_OPRAVLJENA') and o.name = 'FK_JE_BILA__JE_BILA_O_EVIDENCA')
alter table JE_BILA_OPRAVLJENA
   drop constraint FK_JE_BILA__JE_BILA_O_EVIDENCA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('NAROCILO') and o.name = 'FK_NAROCILO_JE_NAROCI_STRANKA')
alter table NAROCILO
   drop constraint FK_NAROCILO_JE_NAROCI_STRANKA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('STRANKA') and o.name = 'FK_STRANKA_JE_NA_POSTA')
alter table STRANKA
   drop constraint FK_STRANKA_JE_NA_POSTA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('TERMIN') and o.name = 'FK_TERMIN_JE_PREVZE_STRANKA')
alter table TERMIN
   drop constraint FK_TERMIN_JE_PREVZE_STRANKA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('TERMIN') and o.name = 'FK_TERMIN_JE_RAZPIS_VETERINA')
alter table TERMIN
   drop constraint FK_TERMIN_JE_RAZPIS_VETERINA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('VETERINAR') and o.name = 'FK_VETERINA_IMA_VETER_POSTA')
alter table VETERINAR
   drop constraint FK_VETERINA_IMA_VETER_POSTA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('ZAHTEVA') and o.name = 'FK_ZAHTEVA_ZAHTEVA_NAROCILO')
alter table ZAHTEVA
   drop constraint FK_ZAHTEVA_ZAHTEVA_NAROCILO
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('ZAHTEVA') and o.name = 'FK_ZAHTEVA_ZAHTEVA2_ZALOGA')
alter table ZAHTEVA
   drop constraint FK_ZAHTEVA_ZAHTEVA2_ZALOGA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('ZALOGA') and o.name = 'FK_ZALOGA_IMA_VETERINA')
alter table ZALOGA
   drop constraint FK_ZALOGA_IMA_VETERINA
go

if exists (select 1
from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
where r.fkeyid = object_id('ZALOGA') and o.name = 'FK_ZALOGA_JE_OD_IZDELEK')
alter table ZALOGA
   drop constraint FK_ZALOGA_JE_OD_IZDELEK
go

if exists (select 1
from sysindexes
where  id    = object_id('EVIDENCA')
   and name  = 'JE_BILO_OPRAVLJENO_FK'
   and indid > 0
   and indid < 255)
   drop index EVIDENCA.JE_BILO_OPRAVLJENO_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('EVIDENCA')
   and name  = 'JE_ZABELEZENO_V_FK'
   and indid > 0
   and indid < 255)
   drop index EVIDENCA.JE_ZABELEZENO_V_FK
go

if exists (select 1
from sysobjects
where  id = object_id('EVIDENCA')
   and type = 'U')
   drop table EVIDENCA
go

if exists (select 1
from sysobjects
where  id = object_id('IZDELEK')
   and type = 'U')
   drop table IZDELEK
go

if exists (select 1
from sysindexes
where  id    = object_id('JE_BILA_OPRAVLJENA')
   and name  = 'JE_BILA_OPRAVLJENA2_FK'
   and indid > 0
   and indid < 255)
   drop index JE_BILA_OPRAVLJENA.JE_BILA_OPRAVLJENA2_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('JE_BILA_OPRAVLJENA')
   and name  = 'JE_BILA_OPRAVLJENA_FK'
   and indid > 0
   and indid < 255)
   drop index JE_BILA_OPRAVLJENA.JE_BILA_OPRAVLJENA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('JE_BILA_OPRAVLJENA')
   and type = 'U')
   drop table JE_BILA_OPRAVLJENA
go

if exists (select 1
from sysindexes
where  id    = object_id('NAROCILO')
   and name  = 'JE_NAROCILA_FK'
   and indid > 0
   and indid < 255)
   drop index NAROCILO.JE_NAROCILA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('NAROCILO')
   and type = 'U')
   drop table NAROCILO
go

if exists (select 1
from sysobjects
where  id = object_id('POSTA')
   and type = 'U')
   drop table POSTA
go

if exists (select 1
from sysobjects
where  id = object_id('STORITEV')
   and type = 'U')
   drop table STORITEV
go

if exists (select 1
from sysindexes
where  id    = object_id('STRANKA')
   and name  = 'JE_NA_FK'
   and indid > 0
   and indid < 255)
   drop index STRANKA.JE_NA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('STRANKA')
   and type = 'U')
   drop table STRANKA
go

if exists (select 1
from sysindexes
where  id    = object_id('TERMIN')
   and name  = 'JE_RAZPISAL_FK'
   and indid > 0
   and indid < 255)
   drop index TERMIN.JE_RAZPISAL_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('TERMIN')
   and name  = 'JE_PREVZELA_FK'
   and indid > 0
   and indid < 255)
   drop index TERMIN.JE_PREVZELA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('TERMIN')
   and type = 'U')
   drop table TERMIN
go

if exists (select 1
from sysindexes
where  id    = object_id('VETERINAR')
   and name  = 'IMA_VETERINO_NA_FK'
   and indid > 0
   and indid < 255)
   drop index VETERINAR.IMA_VETERINO_NA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('VETERINAR')
   and type = 'U')
   drop table VETERINAR
go

if exists (select 1
from sysindexes
where  id    = object_id('ZAHTEVA')
   and name  = 'ZAHTEVA2_FK'
   and indid > 0
   and indid < 255)
   drop index ZAHTEVA.ZAHTEVA2_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('ZAHTEVA')
   and name  = 'ZAHTEVA_FK'
   and indid > 0
   and indid < 255)
   drop index ZAHTEVA.ZAHTEVA_FK
go

if exists (select 1
from sysobjects
where  id = object_id('ZAHTEVA')
   and type = 'U')
   drop table ZAHTEVA
go

if exists (select 1
from sysindexes
where  id    = object_id('ZALOGA')
   and name  = 'IMA_FK'
   and indid > 0
   and indid < 255)
   drop index ZALOGA.IMA_FK
go

if exists (select 1
from sysindexes
where  id    = object_id('ZALOGA')
   and name  = 'JE_OD_FK'
   and indid > 0
   and indid < 255)
   drop index ZALOGA.JE_OD_FK
go

if exists (select 1
from sysobjects
where  id = object_id('ZALOGA')
   and type = 'U')
   drop table ZALOGA
go

/*==============================================================*/
/* Table: EVIDENCA                                              */
/*==============================================================*/
create table EVIDENCA
(
   ID_EVIDENCE numeric not null,
   ID_VETERINAR numeric null,
   DATUM_ZACETKA datetime null,
   DATUM_KONCA datetime null,
   ID_NAROCILO numeric not null,
   CENA money not null,
   constraint PK_EVIDENCA primary key nonclustered (ID_EVIDENCE)
)
go

/*==============================================================*/
/* Index: JE_ZABELEZENO_V_FK                                    */
/*==============================================================*/
create index JE_ZABELEZENO_V_FK on EVIDENCA (
ID_VETERINAR ASC,
DATUM_ZACETKA ASC,
DATUM_KONCA ASC
)
go

/*==============================================================*/
/* Index: JE_BILO_OPRAVLJENO_FK                                 */
/*==============================================================*/
create index JE_BILO_OPRAVLJENO_FK on EVIDENCA (
ID_NAROCILO ASC
)
go

/*==============================================================*/
/* Table: IZDELEK                                               */
/*==============================================================*/
create table IZDELEK
(
   ID_IZDELEK numeric not null,
   IME varchar(100) not null,
   CENA money not null,
   OPIS varchar(1024) null,
   constraint PK_IZDELEK primary key nonclustered (ID_IZDELEK)
)
go

/*==============================================================*/
/* Table: JE_BILA_OPRAVLJENA                                    */
/*==============================================================*/
create table JE_BILA_OPRAVLJENA
(
   ID_STORITEV numeric not null,
   ID_EVIDENCE numeric not null,
   constraint PK_JE_BILA_OPRAVLJENA primary key (ID_STORITEV, ID_EVIDENCE)
)
go

/*==============================================================*/
/* Index: JE_BILA_OPRAVLJENA_FK                                 */
/*==============================================================*/
create index JE_BILA_OPRAVLJENA_FK on JE_BILA_OPRAVLJENA (
ID_STORITEV ASC
)
go

/*==============================================================*/
/* Index: JE_BILA_OPRAVLJENA2_FK                                */
/*==============================================================*/
create index JE_BILA_OPRAVLJENA2_FK on JE_BILA_OPRAVLJENA (
ID_EVIDENCE ASC
)
go

/*==============================================================*/
/* Table: NAROCILO                                              */
/*==============================================================*/
create table NAROCILO
(
   ID_NAROCILO numeric not null,
   ID_STRANKA numeric null,
   ZAHTEVANA_KOLICINA numeric not null,
   DATUM_NAROCILA datetime null,
   constraint PK_NAROCILO primary key nonclustered (ID_NAROCILO)
)
go

/*==============================================================*/
/* Index: JE_NAROCILA_FK                                        */
/*==============================================================*/
create index JE_NAROCILA_FK on NAROCILO (
ID_STRANKA ASC
)
go

/*==============================================================*/
/* Table: POSTA                                                 */
/*==============================================================*/
create table POSTA
(
   STEVILKA numeric not null,
   NAZIV varchar(50) not null,
   constraint PK_POSTA primary key nonclustered (STEVILKA)
)
go

/*==============================================================*/
/* Table: STORITEV                                              */
/*==============================================================*/
create table STORITEV
(
   ID_STORITEV numeric not null,
   OPIS_STORITVE varchar(1024) null,
   constraint PK_STORITEV primary key nonclustered (ID_STORITEV)
)
go

/*==============================================================*/
/* Table: STRANKA                                               */
/*==============================================================*/
create table STRANKA
(
   ID_STRANKA numeric not null,
   STEVILKA numeric not null,
   IME varchar(100) not null,
   PRIIMEK varchar(100) not null,
   NASLOV varchar(100) not null,
   KRAJ varchar(150) not null,
   constraint PK_STRANKA primary key nonclustered (ID_STRANKA)
)
go

/*==============================================================*/
/* Index: JE_NA_FK                                              */
/*==============================================================*/
create index JE_NA_FK on STRANKA (
STEVILKA ASC
)
go

/*==============================================================*/
/* Table: TERMIN                                                */
/*==============================================================*/
create table TERMIN
(
   ID_VETERINAR numeric not null,
   DATUM_ZACETKA datetime not null,
   DATUM_KONCA datetime not null,
   ID_STRANKA numeric not null,
   JE_ZASEDEN bit not null,
   JE_POTRJEN bit not null,
   constraint PK_TERMIN primary key nonclustered (ID_VETERINAR, DATUM_ZACETKA, DATUM_KONCA)
)
go

/*==============================================================*/
/* Index: JE_PREVZELA_FK                                        */
/*==============================================================*/
create index JE_PREVZELA_FK on TERMIN (
ID_STRANKA ASC
)
go

/*==============================================================*/
/* Index: JE_RAZPISAL_FK                                        */
/*==============================================================*/
create index JE_RAZPISAL_FK on TERMIN (
ID_VETERINAR ASC
)
go

/*==============================================================*/
/* Table: VETERINAR                                             */
/*==============================================================*/
create table VETERINAR
(
   ID_VETERINAR numeric not null,
   STEVILKA numeric not null,
   IME varchar(100) not null,
   PRIIMEK varchar(100) not null,
   KRAJ varchar(150) not null,
   NA_DOM bit not null,
   constraint PK_VETERINAR primary key nonclustered (ID_VETERINAR)
)
go

/*==============================================================*/
/* Index: IMA_VETERINO_NA_FK                                    */
/*==============================================================*/
create index IMA_VETERINO_NA_FK on VETERINAR (
STEVILKA ASC
)
go

/*==============================================================*/
/* Table: ZAHTEVA                                               */
/*==============================================================*/
create table ZAHTEVA
(
   ID_NAROCILO numeric not null,
   ID_IZDELEK numeric not null,
   ID_VETERINAR numeric not null,
   constraint PK_ZAHTEVA primary key (ID_IZDELEK, ID_NAROCILO, ID_VETERINAR)
)
go

/*==============================================================*/
/* Index: ZAHTEVA_FK                                            */
/*==============================================================*/
create index ZAHTEVA_FK on ZAHTEVA (
ID_NAROCILO ASC
)
go

/*==============================================================*/
/* Index: ZAHTEVA2_FK                                           */
/*==============================================================*/
create index ZAHTEVA2_FK on ZAHTEVA (
ID_IZDELEK ASC,
ID_VETERINAR ASC
)
go

/*==============================================================*/
/* Table: ZALOGA                                                */
/*==============================================================*/
create table ZALOGA
(
   ID_IZDELEK numeric not null,
   ID_VETERINAR numeric not null,
   KOLICINA numeric not null,
   constraint PK_ZALOGA primary key (ID_IZDELEK, ID_VETERINAR)
)
go

/*==============================================================*/
/* Index: JE_OD_FK                                              */
/*==============================================================*/
create index JE_OD_FK on ZALOGA (
ID_IZDELEK ASC
)
go

/*==============================================================*/
/* Index: IMA_FK                                                */
/*==============================================================*/
create index IMA_FK on ZALOGA (
ID_VETERINAR ASC
)
go

alter table EVIDENCA
   add constraint FK_EVIDENCA_JE_BILO_O_NAROCILO foreign key (ID_NAROCILO)
      references NAROCILO (ID_NAROCILO)
go

alter table EVIDENCA
   add constraint FK_EVIDENCA_JE_ZABELE_TERMIN foreign key (ID_VETERINAR, DATUM_ZACETKA, DATUM_KONCA)
      references TERMIN (ID_VETERINAR, DATUM_ZACETKA, DATUM_KONCA)
go

alter table JE_BILA_OPRAVLJENA
   add constraint FK_JE_BILA__JE_BILA_O_STORITEV foreign key (ID_STORITEV)
      references STORITEV (ID_STORITEV)
go

alter table JE_BILA_OPRAVLJENA
   add constraint FK_JE_BILA__JE_BILA_O_EVIDENCA foreign key (ID_EVIDENCE)
      references EVIDENCA (ID_EVIDENCE)
go

alter table NAROCILO
   add constraint FK_NAROCILO_JE_NAROCI_STRANKA foreign key (ID_STRANKA)
      references STRANKA (ID_STRANKA)
go

alter table STRANKA
   add constraint FK_STRANKA_JE_NA_POSTA foreign key (STEVILKA)
      references POSTA (STEVILKA)
go

alter table TERMIN
   add constraint FK_TERMIN_JE_PREVZE_STRANKA foreign key (ID_STRANKA)
      references STRANKA (ID_STRANKA)
go

alter table TERMIN
   add constraint FK_TERMIN_JE_RAZPIS_VETERINA foreign key (ID_VETERINAR)
      references VETERINAR (ID_VETERINAR)
go

alter table VETERINAR
   add constraint FK_VETERINA_IMA_VETER_POSTA foreign key (STEVILKA)
      references POSTA (STEVILKA)
go

alter table ZAHTEVA
   add constraint FK_ZAHTEVA_ZAHTEVA_NAROCILO foreign key (ID_NAROCILO)
      references NAROCILO (ID_NAROCILO)
go

alter table ZAHTEVA
   add constraint FK_ZAHTEVA_ZAHTEVA2_ZALOGA foreign key (ID_IZDELEK, ID_VETERINAR)
      references ZALOGA (ID_IZDELEK, ID_VETERINAR)
go

alter table ZALOGA
   add constraint FK_ZALOGA_IMA_VETERINA foreign key (ID_VETERINAR)
      references VETERINAR (ID_VETERINAR)
go

alter table ZALOGA
   add constraint FK_ZALOGA_JE_OD_IZDELEK foreign key (ID_IZDELEK)
      references IZDELEK (ID_IZDELEK)
go


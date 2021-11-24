use eveterinar
go

/*
to use different parts this file,
copy paste the * and / to the end of the topside comment and press run
*/

/* delete all rows from databases */
delete from storitev
DELETE FROM evidenca
DELETE FROM termin
delete from zaloga
delete from izdelek
DELETE FROM veterinar
DELETE FROM stranka
DELETE FROM narocilo
DELETE FROM posta
GO


/* Get a list of tables and views in the current database
SELECT table_catalog [database], table_schema [schema], table_name name, table_type type
FROM INFORMATION_SCHEMA.TABLES
GO
*/
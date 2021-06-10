create table dbo.Hasta(
	HastaId int identity(1,1),
	HastaName varchar(500),
	Doktor varchar(500),
	Bolum varchar(500),
	Information varchar(1000)
)
insert into dbo.Hasta values
('Hasan','Dr.Mehmet','Kardiyoloji','Normal Hasta')

select *from dbo.Hasta
--Base de datos de FARMASALUD
if db_id('FARMASALUD') is not null 
begin 
	use master 
	drop database FARMASALUD
end

create database FARMASALUD
go

use FARMASALUD
GO

--creando tablas

create table tb_proveedor
(
idProveedor char(4) not null primary key,
nomProveedor varchar(40) not null,
dirProveedor varchar(50) not null,
telefono varchar(9) not null,
)
go

create table tb_productos
(
idProducto char(4) not null primary key,
nomProducto varchar(30) not null,
fechVencimiento varchar(30) not null,
idProveedor char(4) not null,
precio decimal(10,1) not null,
stock int not null,
constraint fk_produc foreign key (idProveedor) references tb_proveedor (idProveedor)
)
go

create table tb_tipoUsuario
(
id_tipo char(1) not null primary key,
desc_tipo varchar (50) not null
)
go

create table tb_usuarios
(
id_Usuario char(4) not null primary key,
nom_Usuario varchar(40) not null primary key,
correo_Usuario varchar(40) not null,
contra varchar(20) not null,
dirUsuario varchar(50) not null,
id_tipo char(1) not null default('2')        /*adm y cli */
foreign key (id_tipo) references tb_tipoUsuario (id_tipo)
)
go


create table tb_Boleta
(
idBoleta int identity(1, 1) primary key,
nom_Usuario varchar(40),
fechaOrden datetime not null,
Total decimal(10,1) not null
)
go

create table tb_DetalleBoleta
(
Id int identity(1, 1) primary key,
idBoleta int not null,
idProducto char(4) not null,
nomProducto varchar(30) not null,
precio decimal(10,1) not null,
cantidad int not null,
Monto decimal(10,1) not null,
foreign key(idBoleta) references tb_Boleta (idBoleta),
foreign key(idProducto) references tb_productos(idProducto)
)
Go

--insertando datos en las tablas


--proveedor
insert into tb_proveedor values('PR01','Sebal Farma Distribuciones S.A.C.','SAN MIGUEL - LIMA','014059716')
insert into tb_proveedor values('PR02','P y G Distribuidores S.R.L.','SAN MIGUEL - LIMA','016527106')
insert into tb_proveedor values('PR03','B. Braun','ATE - LIMA','013261825')
insert into tb_proveedor values('PR04','F y a Representaciones S.A.C.','SAN MIGUEL - LIMA','012809631')


--productos
insert into tb_productos values('P001','Doloral Suspensión Oral','15/05/2025','PR01',13.2,60)
insert into tb_productos values('P002','Dolo-Quimagésico Aerosol','25/06/2025','PR02',15.1,50)
insert into tb_productos values('P003','Multi Frost Pomada','09/05/2025','PR01',15.2,60)
insert into tb_productos values('P004','Voltaren 1 % Emulgel','01/01/2023','PR03',39.8,40)
insert into tb_productos values('P005','Reuma Plus Ungüento','12/02/2025','PR03',21.3,25)
insert into tb_productos values('P006','Hep.52 Tabletas Recubiertas','01/05/2025','PR04',48.5,80)
insert into tb_productos values('P007','Nonpiron Suspensión Oral','10/07/2023','PR04',09.7,20)
insert into tb_productos values('P008','Bengay Gel','08/04/2023','PR01',15.7,40)
insert into tb_productos values('P009','Koflet Jarabe','08/05/2025','PR02',21.6,70)
insert into tb_productos values('P010','Reuma plus Ungüento','15/05/2025','PR03',14.2,15)

 --tipoUsuario
insert into tb_tipoUsuario values (1, 'Administrador')
insert into tb_tipoUsuario values (2, 'Cliente')

  --usuarios
insert into tb_usuarios values('C001','Albert Tello','albert01@hotmail.com','albert01','Príncipe de Vergara 36','2')
insert into tb_usuarios values('ADM1','Admin1','adm123@hotmail.com','adm123','Calle Cristobal Bordiú','1')
insert into tb_usuarios values('C003','Erick Chavez','erick03@hotmail.com','erick03','Avenida de Roma','2')
insert into tb_usuarios values('C004','Gonzalo Cachuy','gonzalo04@hotmail.com','gonzalo04','orge Basadre 498','2')
insert into tb_usuarios values('C005','Brenda Vargas','brenda05@hotmail.com','brenda05','Calle Los Pinos,','2')

insert into tb_usuarios values('C006','Brenda Vargas','brenda05@gmail.com','brenda05','Calle Los Pinos,','1')
insert into tb_usuarios values('C007','Carlos Vargas','carlos01@gmail.com','carlos01','Calle Los Pinos,','2')
insert into tb_usuarios values('K007','Carlos Vegas','carlos01@gmail.com','carlos01','Calle Los Pinos,',default)



select * from tb_proveedor
select * from tb_productos
select * from tb_usuarios
select * from tb_ordenPedido
select * from tb_tipoUsuario
go


--------------------------------------------------------------------------------
CREATE OR ALTER PROC usp_listarTiposUser
AS
BEGIN
	SELECT * FROM tb_tipoUsuario
End
Go


CREATE OR ALTER PROC usp_Usuario_Merge
@id_Usuario char(4),
@nom_Usuario varchar(40),
@correo_Usuario varchar(40),
@contra varchar(20),
@dirUsuario varchar(50),
@id_tipo char(1)
AS
BEGIN
	MERGE tb_usuarios AS TARGET
	USING (SELECT @id_Usuario, @nom_Usuario, @correo_Usuario, @contra, @dirUsuario,@id_tipo)
		AS SOURCE(id_Usuario,nom_Usuario, correo_Usuario,contra,dirUsuario, id_tipo)
		on target.id_Usuario = source.id_Usuario
	WHEN MATCHED then 
	 Update Set target.nom_Usuario=source.nom_Usuario, target.correo_Usuario=source.correo_Usuario,
	 target.contra=source.contra, target.dirUsuario=source.dirUsuario, target.id_tipo= source.id_tipo
	WHEN NOT MATCHED THEN
	 Insert Values(source.id_Usuario, source.nom_Usuario, source.correo_Usuario,source.contra, source.dirUsuario, source.id_tipo);
END
GO

--------------------------------------------------------------------------------

--PROCEDIMIENTO PARA LISTAR PRODUCTOS
create or alter proc usp_ListarProductos
as
begin
	select p.idProducto,p.nomProducto,p.fechVencimiento,e.nomProveedor,p.precio,p.stock
	from tb_productos p inner join tb_proveedor e
	on p.idProveedor=e.idProveedor
end
go

exec usp_ListarProductos
go

--PROCEDIMIENTO PARA LISTAR PRODUCTOS POR NOMBRES
create or alter proc usp_ProductoNombre
@nomproducto varchar(30)
as
begin
	select  p.idProducto, p.nomProducto,p.fechVencimiento,pr.nomProveedor,
	p.precio,p.stock
	from tb_productos p inner join
	tb_proveedor pr on p.idProveedor=pr.idProveedor
	where p.nomProducto like '%' + @nomproducto + '%'
end
go

exec usp_ProductoNombre 'p'
go


--PROCEDIMIENTO PARA LISTAR PROVEEDORES
create or alter proc usp_ListarProveedor
as
begin
	select * from tb_proveedor
end
go


--Merge de productos
create or alter proc usp_Merge_InsertUpd
@idproducto char(4),@nomproducto varchar(30),
@fechvencimiento varchar(30),@idproveedor char(4),
@precio decimal(10,1),@stock int
as
begin
	Merge tb_productos as target
	using (select @idproducto,@nomproducto,@fechvencimiento,@idproveedor,
			@precio,@stock)
		as source(idProducto,nomProducto,fechVencimiento,idProveedor,precio,stock)
		on target.idProducto=source.idProducto
	when matched then
		update set target.nomProducto=source.nomProducto,target.fechVencimiento=source.fechVencimiento,
		target.idProveedor=source.idProveedor,target.precio=source.precio,target.stock=source.stock
	when not matched then
		insert values(source.idProducto,source.nomProducto,source.fechVencimiento,source.idProveedor,source.precio,
		source.stock);
end
go

-----------------------------------------------------------------
----USP PARA VALIDAR LOGIN 

CREATE or ALTER PROC usp_seg_UserSesion
@correo_Usuario varchar(40),
@contra varchar(20)
AS
BEGIN
	Select nom_Usuario, correo_Usuario, contra, id_tipo from tb_usuarios
	Where correo_Usuario = @correo_Usuario And contra=@contra
END
GO

-------Procedures para Ventas
CREATE or ALTER PROC usp_NuevaBoleta
@nom_Usuario varchar(40),
@fechaOrden datetime,
@Total decimal(10,1)
AS
BEGIN
	INSERT INTO tb_Boleta VALUES (@nom_Usuario, @fechaOrden, @Total)
END
GO

CREATE or ALTER PROC usp_DetalleBoleta
@idProducto char(4),
@nomProducto varchar(30),
@precio decimal(10,1),
@cantidad int,
@Monto decimal(10,1)
AS
BEGIN
	Declare @idBoleta int = (select top 1 idBoleta from tb_Boleta order by idBoleta DESC);
	INSERT INTO tb_DetalleBoleta VALUES (@idBoleta, @idProducto, @nomProducto, @precio,@cantidad,@Monto);
END
GO
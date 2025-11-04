--* CODIGO DDL *--

CREATE DATABASE AIR_CNR_PII;
GO
USE AIR_CNR_PII;
GO

CREATE TABLE Provincias(
id_provincia int IDENTITY(5,10),
provincia varchar(100) not null,
CONSTRAINT PK_Provinces PRIMARY KEY (id_provincia));
GO

CREATE TABLE Localidades(
id_localidad int IDENTITY(5,10),
localidad varchar(100) not null,
id_provincia int not null,
CONSTRAINT PK_Localidades PRIMARY KEY (id_localidad),
CONSTRAINT FK_Localidades_Provincias FOREIGN KEY (id_provincia)
REFERENCES Provincias (id_provincia));
GO

CREATE TABLE Barrios(
id_barrio int IDENTITY(5,10),
barrio varchar(100) not null,
id_localidad int not null,
CONSTRAINT PK_Barrios PRIMARY KEY (id_barrio),
CONSTRAINT FK_Barrios_Localidades FOREIGN KEY (id_localidad)
REFERENCES Localidades (id_localidad));
GO

CREATE TABLE Sucursales(
id_sucursal int IDENTITY(5,10),
descripcion varchar(100),
id_barrio int not null,
CONSTRAINT PK_Sucursales PRIMARY KEY (id_sucursal),
CONSTRAINT FK_Sucursales_Barrios FOREIGN KEY (id_barrio)
REFERENCES Barrios (id_barrio));
GO

CREATE TABLE Tipos_Documentos( --Dni, Cedula, Pasaporte, Cuil
id_tipo_documento int IDENTITY(5,10),
tipo varchar(100) not null,
CONSTRAINT PK_Tipos_Documentos PRIMARY KEY (id_tipo_documento));
GO

CREATE TABLE Usuarios_Empleados(
id_usuario int IDENTITY(5,10),
usuario varchar(200) not null,
contraseña varchar(200) not null,
CONSTRAINT PK_Usuarios_Empleados PRIMARY KEY (id_usuario));

CREATE TABLE Empleados(
id_empleado int IDENTITY(5,10),
legajo int not null,
nombre varchar(100) not null,
documento varchar(50) not null,
id_tipo_documento int not null,
id_sucursal int not null,
id_usuario int null,
CONSTRAINT PK_Empleados PRIMARY KEY (id_empleado),
CONSTRAINT FK_Empleados_Tipos_Documentos FOREIGN KEY (id_tipo_documento)
REFERENCES Tipos_Documentos (id_tipo_documento),
CONSTRAINT FK_Empleados_Sucursales FOREIGN KEY (id_sucursal)
REFERENCES Sucursales (id_sucursal),
CONSTRAINT FK_Empleados_Usuarios_Empleados FOREIGN KEY (id_usuario)
REFERENCES Usuarios_Empleados (id_usuario));
GO

CREATE TABLE Clientes(
id_cliente int IDENTITY(5,10),
nombre varchar(100) not null,
documento varchar(50) not null,
id_tipo_documento int not null,
CONSTRAINT PK_Clientes PRIMARY KEY (id_cliente),
CONSTRAINT FK_Clientes_Tipos_Documentos FOREIGN KEY (id_tipo_documento)
REFERENCES Tipos_Documentos (id_tipo_documento));
GO

CREATE TABLE Tipos_Contactos(
id_tipo_contacto int IDENTITY(5,10),
tipo varchar(100) not null,
CONSTRAINT PK_Tipos_Contactos PRIMARY KEY (id_tipo_contacto));
GO

CREATE TABLE Contactos(
id_contacto int IDENTITY(5,10),
id_tipo_contacto int not null,
contacto varchar(150) not null,
id_cliente int null,
CONSTRAINT PK_Contactos PRIMARY KEY (id_contacto),
CONSTRAINT FK_Contactos_Tipos_Contactos FOREIGN KEY (id_tipo_contacto)
REFERENCES Tipos_Contactos (id_tipo_contacto),
CONSTRAINT FK_Contactos_Clientes FOREIGN KEY (id_cliente)
REFERENCES Clientes (id_cliente));
GO

CREATE TABLE Marcas(
id_marca int IDENTITY(5,10),
marca varchar(100) not null,
CONSTRAINT PK_Marcas PRIMARY KEY (id_marca));
GO

CREATE TABLE Tipos_Vehiculos(
id_tipo_vehiculo int IDENTITY(5,10),
tipo varchar(100) not null,
CONSTRAINT PK_Tipos_Vehiculos PRIMARY KEY (id_tipo_vehiculo));
GO

CREATE TABLE Estados_Vehiculos( 
id_estado_vehiculo int IDENTITY(5,10),
estado_vehiculo varchar(100) not null,
CONSTRAINT PK_Estados_Vehiculos PRIMARY KEY (id_estado_vehiculo));
GO

CREATE TABLE Vehiculos(
id_vehiculo int IDENTITY(5,10),
patente varchar(20) not null,
id_tipo_vehiculo int not null,
id_marca int not null,
modelo varchar(100) not null,
valor_tasado decimal (12,2) not null,
id_estado_vehiculo int not null,
id_sucursal int not null,
CONSTRAINT PK_Vehiculos PRIMARY KEY (id_vehiculo),
CONSTRAINT FK_Vehiculos_Tipos_Vehiculos FOREIGN KEY (id_tipo_vehiculo)
REFERENCES Tipos_Vehiculos (id_tipo_vehiculo),
CONSTRAINT FK_Vehiculos_Marcas FOREIGN KEY (id_marca)
REFERENCES Marcas (id_marca),
CONSTRAINT FK_Vehiculos_Estados_Vehiculos FOREIGN KEY (id_estado_vehiculo)
REFERENCES Estados_Vehiculos (id_estado_vehiculo),
CONSTRAINT FK_Vehiculos_Sucursales FOREIGN KEY (id_sucursal)
REFERENCES Sucursales (id_sucursal));
GO

CREATE TABLE Estados_Alquileres(
id_estado_alquiler int IDENTITY(5,10),
estado varchar(100),
CONSTRAINT PK_Estados_Alquileres PRIMARY KEY (id_estado_alquiler));
GO


CREATE TABLE Alquileres( 
id_alquiler int IDENTITY(5,10),
id_cliente int not null,
id_estado_alquiler int not null,
monto decimal(12,2) not null,
fecha_inicio datetime not null,
fecha_fin datetime null,
id_vehiculo int not null,
id_sucursal int not null
CONSTRAINT PK_Alquileres PRIMARY KEY (id_alquiler),
CONSTRAINT FK_Alquileres_Clientes FOREIGN KEY (id_cliente)
REFERENCES Clientes (id_cliente),
CONSTRAINT FK_Alquileres_Estados_Alquileres FOREIGN KEY (id_estado_alquiler)
REFERENCES Estados_Alquileres (id_estado_alquiler),
CONSTRAINT FK_Alquileres_Vehiculos FOREIGN KEY (id_vehiculo)
REFERENCES Vehiculos (id_vehiculo),
CONSTRAINT FK_Alquileres_Sucursales FOREIGN KEY (id_sucursal)
REFERENCES Sucursales (id_sucursal));
GO

--* CODIGO DML *--

INSERT INTO Tipos_Documentos (tipo)
VALUES ('Dni'),
('Cedula'),
('Pasaporte'),
('Cuil'),
('Cuit');

INSERT INTO Tipos_Vehiculos (tipo)
VALUES ('Sedan'),
('Descapotable'),
('SUV'),
('Deportivo');

INSERT INTO Estados_Vehiculos (estado_vehiculo)
VALUES ('Disponible'),
('Alquilado'),
('En mantenimiento'),
('Fuera de servicio');

INSERT INTO Estados_Alquileres (estado)
VALUES ('Confirmado'),
('Cancelado'),
('Pendiente'),
('Finalizado');

INSERT INTO Provincias (provincia)
VALUES 
('Buenos Aires'),
('Córdoba');

INSERT INTO Localidades (localidad, id_provincia)
VALUES
('Ezeiza', 5),
('Palermo', 5),
('Capital', 15);

INSERT INTO Barrios (barrio, id_localidad)
VALUES
-- Ezeiza (Buenos Aires)
('Centro Ezeiza', 5),
( 'La Unión', 5),
('Tristán Suárez', 5),
('Canning', 5),
('José María Ezeiza', 5),

-- Palermo (Buenos Aires)
('Palermo Chico', 15),
('Palermo Soho', 15),
('Palermo Hollywood', 15),
('Las Cañitas', 15),

-- Capital (Córdoba)
('Centro', 25),
('Nueva Córdoba', 25),
('Alta Córdoba', 25),
('Cerro de las Rosas', 25),
('Aeropuerto Internacional Ing. A. Taravella', 25);

INSERT INTO Sucursales (id_barrio, descripcion)
VALUES
(15, 'Sucursal La Union (BS.AS)'),   -- La Unión, Ezeiza (Buenos Aires)
(65, 'Sucursal Palermo Soho (BS.AS)'),   -- Palermo Soho, Palermo (Buenos Aires)
(135, 'Sucursal del Aeropuerto (CBA)'),  -- Aeropuerto de Córdoba
(105, 'Sucursal Nueva Cordoba (CBA)');  -- Nueva Córdoba

INSERT INTO Tipos_Contactos (tipo)
VALUES ('Mail'),
('Telefono');

INSERT INTO Marcas (marca)
VALUES
('Toyota'),
('BMW'),
('Ford'),
('Ferrari');

INSERT INTO Empleados (nombre, legajo, documento, id_sucursal, id_tipo_documento)
VALUES ('Mateo Carballo Juarez', 114324, '44272598', 5, 5),
('Valentina Busceni', 412073, '48124188', 25, 5),
('Marco Ticiano Barabino', 412013, '41762189', 15, 5),
('Aylen Milena Garcia Maestri', 421410, '41349105', 35, 5);

INSERT INTO Usuarios_Empleados (usuario, contraseña)
VALUES ('Matu4091', '54D5CB2D332DBDB4850293CAAE4559CE88B65163F1EA5D4E4B3AC49D772DED14') --Contraseña: asd123

UPDATE Empleados 
SET id_usuario = 5
WHERE id_empleado = 5



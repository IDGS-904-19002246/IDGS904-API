-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         Microsoft SQL Server 2019 (RTM-GDR) (KB5021125) - 15.0.2101.7
-- SO del servidor:              Windows 10 Home Single Language 10.0 <X64> (Build 19045: ) (Hypervisor)
-- HeidiSQL Versión:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para IDGS904_API
CREATE DATABASE IF NOT EXISTS "IDGS904_API";
USE "IDGS904_API";

-- Volcando estructura para tabla IDGS904_API.tbl_insumos
CREATE TABLE IF NOT EXISTS "tbl_insumos" (
	"id_insumo" INT NOT NULL,
	"nombre" VARCHAR(32) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"cantidad" INT NOT NULL,
	"cantidad_min" INT NOT NULL,
	PRIMARY KEY ("id_insumo")
);

-- Volcando datos para la tabla IDGS904_API.tbl_insumos: -1 rows
/*!40000 ALTER TABLE "tbl_insumos" DISABLE KEYS */;
INSERT INTO "tbl_insumos" ("id_insumo", "nombre", "cantidad", "cantidad_min") VALUES
	(1, 'clavo', 5, 2);
/*!40000 ALTER TABLE "tbl_insumos" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_insumo_producto
CREATE TABLE IF NOT EXISTS "tbl_insumo_producto" (
	"fk_id_insumo" INT NOT NULL,
	"fk_id_producto" INT NOT NULL,
	"cantidad" INT NOT NULL,
	"precio" INT NOT NULL,
	FOREIGN KEY INDEX "FK__tbl_insum__fk_id__778AC167" ("fk_id_insumo"),
	FOREIGN KEY INDEX "FK__tbl_insum__fk_id__787EE5A0" ("fk_id_producto"),
	CONSTRAINT "FK__tbl_insum__fk_id__787EE5A0" FOREIGN KEY ("fk_id_producto") REFERENCES "tbl_productos" ("id_producto") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK__tbl_insum__fk_id__778AC167" FOREIGN KEY ("fk_id_insumo") REFERENCES "tbl_insumos" ("id_insumo") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Volcando datos para la tabla IDGS904_API.tbl_insumo_producto: -1 rows
/*!40000 ALTER TABLE "tbl_insumo_producto" DISABLE KEYS */;
/*!40000 ALTER TABLE "tbl_insumo_producto" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_insumo_proveedor
CREATE TABLE IF NOT EXISTS "tbl_insumo_proveedor" (
	"fk_id_insumo" INT NOT NULL,
	"fk_id_proveedor" INT NOT NULL,
	"cantidad" INT NOT NULL,
	"precio" INT NOT NULL,
	"fecha" DATE NOT NULL,
	FOREIGN KEY INDEX "FK__tbl_insum__fk_id__74AE54BC" ("fk_id_insumo"),
	FOREIGN KEY INDEX "FK__tbl_insum__fk_id__75A278F5" ("fk_id_proveedor"),
	CONSTRAINT "FK__tbl_insum__fk_id__74AE54BC" FOREIGN KEY ("fk_id_insumo") REFERENCES "tbl_insumos" ("id_insumo") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK__tbl_insum__fk_id__75A278F5" FOREIGN KEY ("fk_id_proveedor") REFERENCES "tbl_proveedores" ("id_proveedor") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Volcando datos para la tabla IDGS904_API.tbl_insumo_proveedor: -1 rows
/*!40000 ALTER TABLE "tbl_insumo_proveedor" DISABLE KEYS */;
/*!40000 ALTER TABLE "tbl_insumo_proveedor" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_productos
CREATE TABLE IF NOT EXISTS "tbl_productos" (
	"id_producto" INT NOT NULL,
	"nombre" VARCHAR(32) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"precio" INT NOT NULL,
	"cantidad" INT NOT NULL DEFAULT '(0)',
	"cantidad_min" INT NOT NULL DEFAULT '(0)',
	"img" NVARCHAR(max) NULL DEFAULT NULL COLLATE 'Modern_Spanish_CI_AS',
	"descripcion" VARCHAR(64) NULL DEFAULT NULL COLLATE 'Modern_Spanish_CI_AS',
	"estado" VARCHAR(4) NULL DEFAULT NULL COLLATE 'Modern_Spanish_CI_AS',
	"pendientes" INT NULL DEFAULT NULL,
	PRIMARY KEY ("id_producto")
);

-- Volcando datos para la tabla IDGS904_API.tbl_productos: -1 rows
/*!40000 ALTER TABLE "tbl_productos" DISABLE KEYS */;
INSERT INTO "tbl_productos" ("id_producto", "nombre", "precio", "cantidad", "cantidad_min", "img", "descripcion", "estado", "pendientes") VALUES
	(1, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(2, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1002, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1003, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1004, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1005, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1006, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2),
	(1007, 'modificado con put :)', 3, 3, 3, 'd', 'd', 'ok', 2);
/*!40000 ALTER TABLE "tbl_productos" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_proveedores
CREATE TABLE IF NOT EXISTS "tbl_proveedores" (
	"id_proveedor" INT NOT NULL,
	"nombre" VARCHAR(32) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"correo" VARCHAR(32) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"telefono" VARCHAR(15) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"direccion" NVARCHAR(max) NULL DEFAULT NULL COLLATE 'Modern_Spanish_CI_AS',
	PRIMARY KEY ("id_proveedor")
);

-- Volcando datos para la tabla IDGS904_API.tbl_proveedores: -1 rows
/*!40000 ALTER TABLE "tbl_proveedores" DISABLE KEYS */;
INSERT INTO "tbl_proveedores" ("id_proveedor", "nombre", "correo", "telefono", "direccion") VALUES
	(2, 'NU', 'NU', '477', 'NU');
/*!40000 ALTER TABLE "tbl_proveedores" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_usuarios
CREATE TABLE IF NOT EXISTS "tbl_usuarios" (
	"id_usuario" INT NOT NULL,
	"nombre" VARCHAR(64) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"apellidoP" VARCHAR(64) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"apellidoM" VARCHAR(64) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"correo" VARCHAR(64) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"contrasena" VARCHAR(64) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	"rol" INT NOT NULL,
	PRIMARY KEY ("id_usuario")
);

-- Volcando datos para la tabla IDGS904_API.tbl_usuarios: -1 rows
/*!40000 ALTER TABLE "tbl_usuarios" DISABLE KEYS */;
INSERT INTO "tbl_usuarios" ("id_usuario", "nombre", "apellidoP", "apellidoM", "correo", "contrasena", "rol") VALUES
	(1, 'xd', 'xd', 'xd', 'xd@gmail.com', 'xd', 2),
	(2, 'sw', 'sw', 'sw', 'sw', 'sw', 2);
/*!40000 ALTER TABLE "tbl_usuarios" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_ventas
CREATE TABLE IF NOT EXISTS "tbl_ventas" (
	"id_venta" INT NOT NULL,
	"fk_id_usuario" INT NOT NULL,
	"fecha_compra" DATE NOT NULL,
	"status" VARCHAR(32) NOT NULL COLLATE 'Modern_Spanish_CI_AS',
	PRIMARY KEY ("id_venta"),
	FOREIGN KEY INDEX "FK__tbl_venta__fk_id__6477ECF3" ("fk_id_usuario"),
	CONSTRAINT "FK__tbl_venta__fk_id__6477ECF3" FOREIGN KEY ("fk_id_usuario") REFERENCES "tbl_usuarios" ("id_usuario") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Volcando datos para la tabla IDGS904_API.tbl_ventas: -1 rows
/*!40000 ALTER TABLE "tbl_ventas" DISABLE KEYS */;
INSERT INTO "tbl_ventas" ("id_venta", "fk_id_usuario", "fecha_compra", "status") VALUES
	(5, 1, '2023-01-01', 'entregado'),
	(7, 2, '2023-02-02', 'en camino'),
	(8, 1, '2023-05-05', 'entregado'),
	(9, 1, '2023-06-06', 'pago pendiente'),
	(10, 1, '2023-06-06', 'pago pendiente'),
	(21, 1, '2023-06-06', 'pago pendiente');
/*!40000 ALTER TABLE "tbl_ventas" ENABLE KEYS */;

-- Volcando estructura para tabla IDGS904_API.tbl_venta_producto
CREATE TABLE IF NOT EXISTS "tbl_venta_producto" (
	"id_venta_producto" INT NOT NULL,
	"fk_id_venta" INT NOT NULL,
	"fk_id_producto" INT NOT NULL,
	"cantidad" INT NOT NULL,
	"precio" INT NOT NULL,
	PRIMARY KEY ("id_venta_producto"),
	FOREIGN KEY INDEX "FK__tbl_venta__fk_id__7B5B524B" ("fk_id_venta"),
	FOREIGN KEY INDEX "FK__tbl_venta__fk_id__7C4F7684" ("fk_id_producto"),
	CONSTRAINT "FK__tbl_venta__fk_id__7C4F7684" FOREIGN KEY ("fk_id_producto") REFERENCES "tbl_productos" ("id_producto") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK__tbl_venta__fk_id__7B5B524B" FOREIGN KEY ("fk_id_venta") REFERENCES "tbl_ventas" ("id_venta") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Volcando datos para la tabla IDGS904_API.tbl_venta_producto: -1 rows
/*!40000 ALTER TABLE "tbl_venta_producto" DISABLE KEYS */;
INSERT INTO "tbl_venta_producto" ("id_venta_producto", "fk_id_venta", "fk_id_producto", "cantidad", "precio") VALUES
	(1, 10, 1, 2, 20),
	(2, 5, 2, 2, 3),
	(3, 5, 2, 2, 3),
	(4, 5, 2, 8, 8),
	(5, 21, 2, 8, 8),
	(6, 21, 1, 10, 10);
/*!40000 ALTER TABLE "tbl_venta_producto" ENABLE KEYS */;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

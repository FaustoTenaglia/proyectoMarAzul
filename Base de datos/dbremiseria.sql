-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.28-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para dbremiseria
DROP DATABASE IF EXISTS `dbremiseria`;
CREATE DATABASE IF NOT EXISTS `dbremiseria` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `dbremiseria`;

-- Volcando estructura para tabla dbremiseria.abono
DROP TABLE IF EXISTS `abono`;
CREATE TABLE IF NOT EXISTS `abono` (
  `tipo` char(9) NOT NULL,
  `cuotas` tinyint(3) unsigned NOT NULL,
  `importe` decimal(10,2) NOT NULL,
  PRIMARY KEY (`tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.abono: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.caja
DROP TABLE IF EXISTS `caja`;
CREATE TABLE IF NOT EXISTS `caja` (
  `turno` int(10) unsigned NOT NULL DEFAULT 1,
  `fecha` date NOT NULL DEFAULT curdate(),
  `total` decimal(11,2) DEFAULT 0.00,
  PRIMARY KEY (`turno`,`fecha`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.caja: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.cartel
DROP TABLE IF EXISTS `cartel`;
CREATE TABLE IF NOT EXISTS `cartel` (
  `numero_movil` int(10) unsigned NOT NULL,
  `fecha_desde` date NOT NULL,
  `fecha_hasta` date DEFAULT NULL,
  `observacion` varchar(255) DEFAULT NULL,
  `estado` char(1) DEFAULT 'A' CHECK (`estado` in ('A','B')),
  PRIMARY KEY (`numero_movil`,`fecha_desde`),
  CONSTRAINT `Cartel_fk_Movil` FOREIGN KEY (`numero_movil`) REFERENCES `movil` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.cartel: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.chofer
DROP TABLE IF EXISTS `chofer`;
CREATE TABLE IF NOT EXISTS `chofer` (
  `id_persona` int(10) unsigned NOT NULL,
  `numero_movil` int(10) unsigned NOT NULL,
  `fecha_desde` date NOT NULL,
  `fecha_hasta` date DEFAULT NULL,
  `observacion` varchar(255) DEFAULT NULL,
  `estado` char(1) DEFAULT 'A' CHECK (`estado` in ('A','B')),
  PRIMARY KEY (`id_persona`,`numero_movil`,`fecha_desde`),
  KEY `Chofer_fk_Movil` (`numero_movil`),
  CONSTRAINT `Chofer_fk_Movil` FOREIGN KEY (`numero_movil`) REFERENCES `movil` (`id`),
  CONSTRAINT `Chofer_fk_Persona` FOREIGN KEY (`id_persona`) REFERENCES `persona` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.chofer: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.cobro
DROP TABLE IF EXISTS `cobro`;
CREATE TABLE IF NOT EXISTS `cobro` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `periodo` date DEFAULT curdate(),
  `importe` decimal(10,2) DEFAULT 0.00,
  `cuotas` int(10) unsigned DEFAULT 1 CHECK (`cuotas` in (1,2,4)),
  `numero_movil` int(10) unsigned NOT NULL,
  `id_servicio` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Cobro_fk_Servicio` (`id_servicio`),
  KEY `Cobro_fk_Movil` (`numero_movil`),
  CONSTRAINT `Cobro_fk_Movil` FOREIGN KEY (`numero_movil`) REFERENCES `movil` (`id`),
  CONSTRAINT `Cobro_fk_Servicio` FOREIGN KEY (`id_servicio`) REFERENCES `servicio` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.cobro: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.gasto
DROP TABLE IF EXISTS `gasto`;
CREATE TABLE IF NOT EXISTS `gasto` (
  `fecha` date DEFAULT curdate(),
  `importe` decimal(10,2) DEFAULT 0.00,
  `numero` int(10) unsigned DEFAULT 1,
  `id_servicio` int(10) unsigned NOT NULL,
  KEY `Gasto_fk_Servicio` (`id_servicio`),
  CONSTRAINT `Gasto_fk_Servicio` FOREIGN KEY (`id_servicio`) REFERENCES `servicio` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.gasto: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.localidad
DROP TABLE IF EXISTS `localidad`;
CREATE TABLE IF NOT EXISTS `localidad` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` char(32) DEFAULT NULL,
  `id_provincia` tinyint(3) unsigned DEFAULT 22,
  PRIMARY KEY (`id`),
  KEY `Localidad_fk_Provincia` (`id_provincia`),
  CONSTRAINT `Localidad_fk_Provincia` FOREIGN KEY (`id_provincia`) REFERENCES `provincia` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.localidad: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.marca
DROP TABLE IF EXISTS `marca`;
CREATE TABLE IF NOT EXISTS `marca` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` char(10) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.marca: ~10 rows (aproximadamente)
INSERT INTO `marca` (`id`, `nombre`) VALUES
	(1, 'Ford'),
	(2, 'Volkswagen'),
	(3, 'Fiat'),
	(4, 'Renault'),
	(5, 'Chevrolet'),
	(6, 'Nissan'),
	(7, 'Toyota'),
	(8, 'Peugeot'),
	(9, 'Citröen'),
	(10, 'Honda');

-- Volcando estructura para tabla dbremiseria.modelo
DROP TABLE IF EXISTS `modelo`;
CREATE TABLE IF NOT EXISTS `modelo` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` char(19) NOT NULL,
  `id_marca` int(10) unsigned DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `Modelo_fk_Marca` (`id_marca`),
  CONSTRAINT `Modelo_fk_Marca` FOREIGN KEY (`id_marca`) REFERENCES `marca` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.modelo: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.movil
DROP TABLE IF EXISTS `movil`;
CREATE TABLE IF NOT EXISTS `movil` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `patente` char(7) NOT NULL,
  `año` smallint(5) unsigned DEFAULT (year(curdate()) - 15),
  `id_modelo` int(10) unsigned DEFAULT NULL,
  `id_cartel` int(10) unsigned DEFAULT NULL,
  `id_titular` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `patente` (`patente`),
  KEY `Movil_fk_Persona` (`id_titular`),
  KEY `Movil_fk_Modelo` (`id_modelo`),
  CONSTRAINT `Movil_fk_Modelo` FOREIGN KEY (`id_modelo`) REFERENCES `modelo` (`id`),
  CONSTRAINT `Movil_fk_Persona` FOREIGN KEY (`id_titular`) REFERENCES `persona` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.movil: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.movimiento
DROP TABLE IF EXISTS `movimiento`;
CREATE TABLE IF NOT EXISTS `movimiento` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `importe` decimal(10,2) DEFAULT 0.00,
  `fecha` date DEFAULT curdate(),
  `turno` int(10) unsigned DEFAULT 1,
  `id_servicio` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `Movimiento_fk_Servicio` (`id_servicio`),
  CONSTRAINT `Movimiento_fk_Servicio` FOREIGN KEY (`id_servicio`) REFERENCES `servicio` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.movimiento: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.pago
DROP TABLE IF EXISTS `pago`;
CREATE TABLE IF NOT EXISTS `pago` (
  `cuota` int(10) unsigned NOT NULL,
  `fecha` date NOT NULL DEFAULT curdate(),
  `importe` decimal(10,2) DEFAULT 0.00,
  `id_cobro` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id_cobro`,`cuota`,`fecha`),
  CONSTRAINT `Detalle_fk_Cobro` FOREIGN KEY (`id_cobro`) REFERENCES `cobro` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.pago: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.persona
DROP TABLE IF EXISTS `persona`;
CREATE TABLE IF NOT EXISTS `persona` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `dni` char(8) NOT NULL,
  `apellido` varchar(30) DEFAULT NULL,
  `nombre` varchar(42) DEFAULT '',
  `sexo` tinyint(3) unsigned DEFAULT 1,
  `nacimiento` date DEFAULT NULL,
  `edad` int(10) unsigned DEFAULT NULL,
  `calle` varchar(32) DEFAULT '',
  `numero` int(10) unsigned DEFAULT 0,
  `id_localidad` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `dni` (`dni`),
  KEY `Persona_fk_Localidad` (`id_localidad`),
  CONSTRAINT `Persona_fk_Localidad` FOREIGN KEY (`id_localidad`) REFERENCES `localidad` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.persona: ~0 rows (aproximadamente)

-- Volcando estructura para tabla dbremiseria.provincia
DROP TABLE IF EXISTS `provincia`;
CREATE TABLE IF NOT EXISTS `provincia` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` char(66) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.provincia: ~24 rows (aproximadamente)
INSERT INTO `provincia` (`id`, `nombre`) VALUES
	(1, 'Buenos Aires'),
	(2, 'Capital Federal - Ciudad Autónoma de Buenos Aires (CABA)'),
	(3, 'Catamarca'),
	(4, 'Chaco'),
	(5, 'Chubut'),
	(6, 'Córdoba'),
	(7, 'Corrientes'),
	(8, 'Entre Ríos'),
	(9, 'Formosa'),
	(10, 'Jujuy'),
	(11, 'La Pampa'),
	(12, 'La Rioja'),
	(13, 'Mendoza'),
	(14, 'Misiones'),
	(15, 'Neuquén'),
	(16, 'Río Negro'),
	(17, 'Salta'),
	(18, 'San Juan'),
	(19, 'San Luis'),
	(20, 'Santa Cruz'),
	(21, 'Santa Fe'),
	(22, 'Santiago del Estero'),
	(23, 'Tierra del Fuego Territorio Antártico e Islas del Atlántico Sur'),
	(24, 'Tucumán');

-- Volcando estructura para tabla dbremiseria.servicio
DROP TABLE IF EXISTS `servicio`;
CREATE TABLE IF NOT EXISTS `servicio` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` char(15) NOT NULL,
  `tipo` tinyint(4) DEFAULT 0 CHECK (`tipo` in (-1,0,1)),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla dbremiseria.servicio: ~0 rows (aproximadamente)

-- Volcando estructura para vista dbremiseria.v_modelo
DROP VIEW IF EXISTS `v_modelo`;
-- Creando tabla temporal para superar errores de dependencia de VIEW
CREATE TABLE `v_modelo` (
	`id` INT(10) UNSIGNED NOT NULL,
	`nombre` CHAR(19) NOT NULL COLLATE 'utf8mb4_general_ci',
	`marca` CHAR(10) NULL COLLATE 'utf8mb4_general_ci'
) ENGINE=MyISAM;

-- Volcando estructura para vista dbremiseria.v_movil
DROP VIEW IF EXISTS `v_movil`;
-- Creando tabla temporal para superar errores de dependencia de VIEW
CREATE TABLE `v_movil` (
	`id` INT(10) UNSIGNED NOT NULL,
	`patente` CHAR(7) NOT NULL COLLATE 'utf8mb4_general_ci',
	`año` SMALLINT(5) UNSIGNED NULL,
	`nombre` CHAR(19) NULL COLLATE 'utf8mb4_general_ci',
	`marca` CHAR(10) NULL COLLATE 'utf8mb4_general_ci',
	`titular` VARCHAR(30) NULL COLLATE 'utf8mb4_general_ci',
	`dni` CHAR(8) NULL COLLATE 'utf8mb4_general_ci'
) ENGINE=MyISAM;

-- Volcando estructura para vista dbremiseria.v_persona
DROP VIEW IF EXISTS `v_persona`;
-- Creando tabla temporal para superar errores de dependencia de VIEW
CREATE TABLE `v_persona` (
	`dni` CHAR(8) NOT NULL COLLATE 'utf8mb4_general_ci',
	`apellido` VARCHAR(30) NULL COLLATE 'utf8mb4_general_ci',
	`nombre` VARCHAR(42) NULL COLLATE 'utf8mb4_general_ci',
	`edad` INT(5) NULL,
	`calle` VARCHAR(32) NULL COLLATE 'utf8mb4_general_ci',
	`numero` INT(10) UNSIGNED NULL,
	`localidad` CHAR(32) NULL COLLATE 'utf8mb4_general_ci'
) ENGINE=MyISAM;

-- Eliminando tabla temporal y crear estructura final de VIEW
DROP TABLE IF EXISTS `v_modelo`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `v_modelo` AS SELECT Modelo.id, Modelo.nombre, Marca.nombre AS marca
FROM Modelo
LEFT JOIN Marca ON Modelo.id_marca=Marca.id ;

-- Eliminando tabla temporal y crear estructura final de VIEW
DROP TABLE IF EXISTS `v_movil`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `v_movil` AS SELECT Movil.id, Movil.patente, Movil.año,
    v_modelo.nombre, v_modelo.marca,
    Persona.apellido AS titular, Persona.dni AS dni
FROM Movil
LEFT JOIN v_modelo ON Movil.id_modelo=v_modelo.id
LEFT JOIN Persona ON Movil.id_titular=Persona.id ;

-- Eliminando tabla temporal y crear estructura final de VIEW
DROP TABLE IF EXISTS `v_persona`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `v_persona` AS SELECT Persona.dni, Persona.apellido, Persona.nombre,
    (YEAR(CURDATE())-YEAR(Persona.nacimiento)) AS edad,
    Persona.calle, Persona.numero,
    Localidad.nombre AS localidad
FROM Persona
LEFT JOIN Localidad ON Persona.id_localidad=Localidad.id ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

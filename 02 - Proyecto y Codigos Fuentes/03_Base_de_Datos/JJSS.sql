-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema JJSS
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `JJSS` ;

-- -----------------------------------------------------
-- Schema JJSS
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `JJSS` DEFAULT CHARACTER SET utf8 ;
USE `JJSS` ;

-- -----------------------------------------------------
-- Table `JJSS`.`estado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`estado` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`estado` (
  `id_estado` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_estado`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`pais`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`pais` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`pais` (
  `id_pais` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`id_pais`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`ciudad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`ciudad` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`ciudad` (
  `id_ciudad` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `id_pais` INT NULL,
  PRIMARY KEY (`id_ciudad`),
  INDEX `id_pais_idx` (`id_pais` ASC),
  CONSTRAINT `ciudad_id_pais_fk`
    FOREIGN KEY (`id_pais`)
    REFERENCES `JJSS`.`pais` (`id_pais`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`calle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`calle` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`calle` (
  `id_calle` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `id_ciudad` INT NULL,
  PRIMARY KEY (`id_calle`),
  INDEX `id_ciudad_idx` (`id_ciudad` ASC),
  CONSTRAINT `calle_id_ciudad_fk`
    FOREIGN KEY (`id_ciudad`)
    REFERENCES `JJSS`.`ciudad` (`id_ciudad`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`barrio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`barrio` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`barrio` (
  `id_barrio` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`id_barrio`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`direccion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`direccion` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`direccion` (
  `id_direccion` INT NOT NULL AUTO_INCREMENT,
  `numero` INT NULL,
  `id_calle` INT NULL,
  `id_barrio` INT NULL,
  PRIMARY KEY (`id_direccion`),
  INDEX `id_calle_idx` (`id_calle` ASC),
  INDEX `id_barrio_idx` (`id_barrio` ASC),
  CONSTRAINT `direccion_id_calle_fk`
    FOREIGN KEY (`id_calle`)
    REFERENCES `JJSS`.`calle` (`id_calle`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `direccion_id_barrio_fk`
    FOREIGN KEY (`id_barrio`)
    REFERENCES `JJSS`.`barrio` (`id_barrio`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`sede`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`sede` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`sede` (
  `id_sede` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `id_direccion` INT NULL,
  PRIMARY KEY (`id_sede`),
  INDEX `id_direccion_idx` (`id_direccion` ASC),
  CONSTRAINT `sede_id_direccion_fk`
    FOREIGN KEY (`id_direccion`)
    REFERENCES `JJSS`.`direccion` (`id_direccion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`torneo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`torneo` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`torneo` (
  `id_torneo` INT NOT NULL AUTO_INCREMENT,
  `fecha` DATE NULL,
  `nombre` VARCHAR(45) NOT NULL,
  `hora` TIME(6) NULL,
  `precio` FLOAT NULL,
  `id_estado` INT NULL,
  `id_sede` INT NULL,
  PRIMARY KEY (`id_torneo`),
  INDEX `id_estado_idx` (`id_estado` ASC),
  INDEX `id_sede_idx` (`id_sede` ASC),
  CONSTRAINT `torneo_id_estado_fk`
    FOREIGN KEY (`id_estado`)
    REFERENCES `JJSS`.`estado` (`id_estado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `torneo_id_sede_fk`
    FOREIGN KEY (`id_sede`)
    REFERENCES `JJSS`.`sede` (`id_sede`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`faja`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`faja` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`faja` (
  `id_faja` INT NOT NULL AUTO_INCREMENT,
  `color` VARCHAR(45) NULL,
  `grado` INT NULL,
  PRIMARY KEY (`id_faja`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`categoria`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`categoria` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`categoria` (
  `id_categoria` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `peso_desde` INT NULL,
  `peso_hasta` INT NULL,
  `edad_desde` INT NULL,
  `edad_hasta` INT NULL,
  PRIMARY KEY (`id_categoria`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`academia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`academia` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`academia` (
  `id_academia` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `telefono` INT NULL,
  `id_direccion` INT NULL,
  PRIMARY KEY (`id_academia`),
  INDEX `id_direccion_idx` (`id_direccion` ASC),
  CONSTRAINT `academia_id_direccion_fk`
    FOREIGN KEY (`id_direccion`)
    REFERENCES `JJSS`.`direccion` (`id_direccion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`participante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`participante` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`participante` (
  `id_participante` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `apellido` VARCHAR(45) NOT NULL,
  `fecha_nacimiento` DATE NULL,
  `telefono` INT NULL,
  `id_faja` INT NULL,
  `id_direccion` INT NULL,
  `id_academia` INT NULL,
  `id_categoria` INT NULL,
  `sexo` TINYINT NULL,
  `peso` FLOAT NULL,
  PRIMARY KEY (`id_participante`),
  INDEX `id_faja_idx` (`id_faja` ASC),
  INDEX `id_categoria_idx` (`id_categoria` ASC),
  INDEX `id_direccion_idx` (`id_direccion` ASC),
  INDEX `id_academia_idx` (`id_academia` ASC),
  CONSTRAINT `participante_id_faja_fk`
    FOREIGN KEY (`id_faja`)
    REFERENCES `JJSS`.`faja` (`id_faja`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `participante_id_categoria_fk`
    FOREIGN KEY (`id_categoria`)
    REFERENCES `JJSS`.`categoria` (`id_categoria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `participante_id_direccion_fk`
    FOREIGN KEY (`id_direccion`)
    REFERENCES `JJSS`.`direccion` (`id_direccion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `participante_id_academia_fk`
    FOREIGN KEY (`id_academia`)
    REFERENCES `JJSS`.`academia` (`id_academia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`categoria_torneo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`categoria_torneo` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`categoria_torneo` (
  `id_categoria_torneo` INT NOT NULL AUTO_INCREMENT,
  `id_categoria` INT NULL,
  `sexo` TINYINT NULL,
  `id_faja` INT NULL,
  PRIMARY KEY (`id_categoria_torneo`),
  INDEX `id_categoria_idx` (`id_categoria` ASC),
  INDEX `if_faja_idx` (`id_faja` ASC),
  CONSTRAINT `categoria_torneo_id_categoria_fk`
    FOREIGN KEY (`id_categoria`)
    REFERENCES `JJSS`.`categoria` (`id_categoria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `categoria_torneo_id_faja_fk`
    FOREIGN KEY (`id_faja`)
    REFERENCES `JJSS`.`faja` (`id_faja`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`inscripcion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`inscripcion` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`inscripcion` (
  `id_inscripcion` INT NOT NULL AUTO_INCREMENT,
  `hora` TIME(6) NULL,
  `fecha` DATE NULL,
  `codigo_barra` BIGINT(8) NULL,
  `id_participante` INT NULL,
  `id_torneo` INT NULL,
  `id_categoria_torneo` INT NULL,
  PRIMARY KEY (`id_inscripcion`),
  INDEX `id_participante_idx` (`id_participante` ASC),
  INDEX `id_torneo_idx` (`id_torneo` ASC),
  INDEX `id_categoria_torneo_idx` (`id_categoria_torneo` ASC),
  CONSTRAINT `inscripcion_id_participante_fk`
    FOREIGN KEY (`id_participante`)
    REFERENCES `JJSS`.`participante` (`id_participante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `inscripcion_id_torneo_fk`
    FOREIGN KEY (`id_torneo`)
    REFERENCES `JJSS`.`torneo` (`id_torneo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `inscripcion_id_categoria_torneo_fk`
    FOREIGN KEY (`id_categoria_torneo`)
    REFERENCES `JJSS`.`categoria_torneo` (`id_categoria_torneo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
COMMENT = '\n';


-- -----------------------------------------------------
-- Table `JJSS`.`resultado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`resultado` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`resultado` (
  `id_resultado` INT NOT NULL AUTO_INCREMENT,
  `tipo_finalizacion` TINYINT NULL,
  `tiempo` TIME(6) NULL,
  `punto_participante_1` INT NULL,
  `punto_participante_2` INT NULL,
  `id_ganador` INT NULL,
  PRIMARY KEY (`id_resultado`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `JJSS`.`lucha`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `JJSS`.`lucha` ;

CREATE TABLE IF NOT EXISTS `JJSS`.`lucha` (
  `id_lucha` INT NOT NULL AUTO_INCREMENT,
  `id_participante1` INT NOT NULL,
  `id_participante2` INT NOT NULL,
  `id_resultado` INT NULL,
  `ronda` INT NULL,
  `id_torneo` INT NULL,
  PRIMARY KEY (`id_lucha`),
  INDEX `id_resultado_idx` (`id_resultado` ASC),
  INDEX `id_participante_1_idx` (`id_participante1` ASC),
  INDEX `id_participante_2_idx` (`id_participante2` ASC),
  INDEX `id_torneo_idx` (`id_torneo` ASC),
  CONSTRAINT `lucha_id_resultado_fk`
    FOREIGN KEY (`id_resultado`)
    REFERENCES `JJSS`.`resultado` (`id_resultado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `lucha_id_participante_1_fk`
    FOREIGN KEY (`id_participante1`)
    REFERENCES `JJSS`.`participante` (`id_participante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `lucha_id_participante_2_fk`
    FOREIGN KEY (`id_participante2`)
    REFERENCES `JJSS`.`participante` (`id_participante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `lucha_id_torneo_fk`
    FOREIGN KEY (`id_torneo`)
    REFERENCES `JJSS`.`torneo` (`id_torneo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

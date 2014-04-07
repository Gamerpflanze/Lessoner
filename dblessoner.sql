-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               5.5.27 - MySQL Community Server (GPL)
-- Server Betriebssystem:        Win32
-- HeidiSQL Version:             8.2.0.4675
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Exportiere Datenbank Struktur für dblessoner
DROP DATABASE IF EXISTS `dblessoner`;
CREATE DATABASE IF NOT EXISTS `dblessoner` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `dblessoner`;


-- Exportiere Struktur von Tabelle dblessoner.tbanmeldung
DROP TABLE IF EXISTS `tbanmeldung`;
CREATE TABLE IF NOT EXISTS `tbanmeldung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(45) NOT NULL,
  `Passwort` varbinary(20) NOT NULL,
  `ProfilBildPfad` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbanmeldung: ~2 rows (ungefähr)
DELETE FROM `tbanmeldung`;
/*!40000 ALTER TABLE `tbanmeldung` DISABLE KEYS */;
INSERT INTO `tbanmeldung` (`ID`, `Email`, `Passwort`, `ProfilBildPfad`) VALUES
	(1, 'root', _binary 0x39DFA55283318D31AFE5A3FF4A0E3253E2045E43, NULL),
	(27, 'Fürstenberg', _binary 0x2B17A1D835891A3B71C1469F0283F91D58B6BE66, NULL);
/*!40000 ALTER TABLE `tbanmeldung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbdateien
DROP TABLE IF EXISTS `tbdateien`;
CREATE TABLE IF NOT EXISTS `tbdateien` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FachinfoID` int(11) NOT NULL,
  `Path` varchar(128) NOT NULL,
  `FileName` varchar(128) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_tbDateien_tbFachinfo_idx` (`FachinfoID`),
  CONSTRAINT `fk_tbDateien_tbFachinfo` FOREIGN KEY (`FachinfoID`) REFERENCES `tbfachinfo` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbdateien: ~1 rows (ungefähr)
DELETE FROM `tbdateien`;
/*!40000 ALTER TABLE `tbdateien` DISABLE KEYS */;
INSERT INTO `tbdateien` (`ID`, `FachinfoID`, `Path`, `FileName`) VALUES
	(3, 184, 'C:\\Users\\Florian\\Source\\Repos\\Lessoner\\Lessoner\\Lessoner\\Data\\Files\\3dbbc01f09ba4303a5dfe4dc363e5744.txt', 'a story about farts.txt');
/*!40000 ALTER TABLE `tbdateien` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbfachinfo
DROP TABLE IF EXISTS `tbfachinfo`;
CREATE TABLE IF NOT EXISTS `tbfachinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LehrerID` int(11) NOT NULL,
  `FachID` int(11) NOT NULL,
  `TagID` int(11) NOT NULL,
  `Stunde_Beginn` int(11) NOT NULL,
  `Stunde_Ende` int(11) NOT NULL,
  `FachModID` int(11) NOT NULL,
  `Information` varchar(512) NOT NULL DEFAULT '',
  `RaumID` int(11),
  PRIMARY KEY (`ID`),
  KEY `fkLehrer_idx` (`LehrerID`),
  KEY `fkFach_idx` (`FachID`),
  KEY `fk_tbFachinfo_tbTage_idx` (`TagID`),
  KEY `fk_tbFachinfo_tbFachMod` (`FachModID`),
  KEY `fk_tbFachInfo_tbRaumID` (`RaumID`),
  CONSTRAINT `fkFach` FOREIGN KEY (`FachID`) REFERENCES `tbfaecher` (`ID`) ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbFachinfo_tbFachMod` FOREIGN KEY (`FachModID`) REFERENCES `tbfachmod` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbFachInfo_tbRaumID` FOREIGN KEY (`RaumID`) REFERENCES `tbraum` (`ID`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbFachinfo_tbTage` FOREIGN KEY (`TagID`) REFERENCES `tbtage` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=187 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfachinfo: ~107 rows (ungefähr)
DELETE FROM `tbfachinfo`;
/*!40000 ALTER TABLE `tbfachinfo` DISABLE KEYS */;
INSERT INTO `tbfachinfo` (`ID`, `LehrerID`, `FachID`, `TagID`, `Stunde_Beginn`, `Stunde_Ende`, `FachModID`, `Information`, `RaumID`) VALUES
	(34, 11, 4, 136, 1, 2, 1, '', NULL),
	(35, 7, 11, 136, 3, 4, 1, '', NULL),
	(36, 7, 5, 136, 5, 6, 1, '', NULL),
	(37, 7, 5, 136, 7, 8, 1, '', NULL),
	(38, 7, 1, 137, 1, 2, 1, '', NULL),
	(39, 7, 8, 137, 3, 4, 1, '', NULL),
	(40, 7, 7, 137, 5, 7, 1, '', NULL),
	(41, 7, 6, 138, 1, 2, 1, '', NULL),
	(42, 7, 3, 138, 3, 3, 1, '', NULL),
	(43, 7, 8, 138, 4, 4, 1, '', NULL),
	(44, 7, 2, 138, 5, 6, 1, '', NULL),
	(45, 7, 3, 139, 4, 5, 1, '', NULL),
	(46, 7, 1, 139, 6, 8, 1, '', NULL),
	(47, 7, 6, 140, 1, 2, 1, '', NULL),
	(48, 7, 10, 140, 3, 4, 1, '', NULL),
	(64, 7, 4, 146, 1, 2, 1, '', NULL),
	(65, 7, 11, 146, 3, 4, 1, '', NULL),
	(66, 7, 5, 146, 5, 6, 1, '', NULL),
	(67, 7, 5, 146, 7, 8, 1, '', NULL),
	(68, 7, 1, 147, 1, 2, 1, '', NULL),
	(69, 7, 8, 147, 3, 4, 1, '', NULL),
	(70, 7, 7, 147, 5, 7, 1, '', NULL),
	(71, 7, 6, 148, 1, 2, 1, '', NULL),
	(72, 7, 3, 148, 3, 3, 1, '', NULL),
	(73, 7, 8, 148, 4, 4, 1, '', NULL),
	(74, 7, 2, 148, 5, 6, 1, '', NULL),
	(75, 7, 3, 149, 4, 5, 1, '', NULL),
	(76, 7, 1, 149, 6, 8, 1, '', NULL),
	(77, 7, 6, 150, 1, 2, 1, '', NULL),
	(78, 7, 10, 150, 3, 4, 1, '', NULL),
	(94, 7, 4, 156, 1, 2, 1, '', NULL),
	(95, 7, 11, 156, 3, 4, 1, '', NULL),
	(96, 7, 5, 156, 5, 6, 1, '', NULL),
	(97, 7, 5, 156, 7, 8, 1, '', NULL),
	(98, 7, 1, 157, 1, 2, 1, '', NULL),
	(99, 7, 8, 157, 3, 4, 1, '', NULL),
	(100, 7, 7, 157, 5, 7, 1, '', NULL),
	(101, 7, 6, 158, 1, 2, 1, '', NULL),
	(102, 7, 3, 158, 3, 3, 1, '', NULL),
	(103, 7, 8, 158, 4, 4, 1, '', NULL),
	(104, 7, 2, 158, 5, 6, 1, '', NULL),
	(105, 7, 3, 159, 4, 5, 1, '', NULL),
	(106, 7, 1, 159, 6, 8, 1, '', NULL),
	(107, 7, 6, 160, 1, 2, 1, '', NULL),
	(108, 7, 10, 160, 3, 4, 1, '', NULL),
	(124, 7, 4, 131, 1, 2, 1, '', NULL),
	(125, 7, 11, 131, 3, 4, 1, '', NULL),
	(126, 7, 5, 131, 5, 6, 1, '', NULL),
	(127, 7, 5, 131, 7, 8, 1, '', NULL),
	(128, 7, 2, 132, 1, 2, 1, '', NULL),
	(129, 7, 8, 132, 3, 4, 1, '', NULL),
	(130, 7, 7, 132, 5, 7, 1, '', NULL),
	(131, 7, 6, 133, 1, 2, 1, '', NULL),
	(132, 7, 3, 133, 3, 3, 1, '', NULL),
	(133, 7, 8, 133, 4, 4, 1, '', NULL),
	(134, 7, 2, 133, 5, 6, 1, '', NULL),
	(135, 7, 3, 134, 4, 5, 1, '', NULL),
	(136, 7, 1, 134, 6, 8, 1, '', NULL),
	(137, 7, 6, 135, 1, 2, 1, '', NULL),
	(138, 7, 10, 135, 3, 4, 1, '', NULL),
	(139, 7, 4, 141, 1, 2, 1, '', NULL),
	(140, 7, 11, 141, 3, 4, 1, '', NULL),
	(141, 7, 5, 141, 5, 6, 1, '', NULL),
	(142, 7, 5, 141, 7, 8, 1, '', NULL),
	(143, 7, 2, 142, 1, 2, 1, '', NULL),
	(144, 7, 8, 142, 3, 4, 1, '', NULL),
	(145, 7, 7, 142, 5, 7, 1, '', NULL),
	(146, 7, 6, 143, 1, 2, 1, '', NULL),
	(147, 7, 3, 143, 3, 3, 1, '', NULL),
	(148, 7, 8, 143, 4, 4, 1, '', NULL),
	(149, 7, 2, 143, 5, 6, 1, '', NULL),
	(150, 7, 3, 144, 4, 5, 1, '', NULL),
	(151, 7, 1, 144, 6, 8, 1, '', NULL),
	(152, 7, 6, 145, 1, 2, 1, '', NULL),
	(153, 7, 10, 145, 3, 4, 1, '', NULL),
	(154, 7, 4, 151, 1, 2, 1, '', NULL),
	(155, 7, 11, 151, 3, 4, 1, '', NULL),
	(156, 7, 5, 151, 5, 6, 1, '', NULL),
	(157, 7, 5, 151, 7, 8, 1, '', NULL),
	(158, 7, 2, 152, 1, 2, 1, '', NULL),
	(159, 7, 8, 152, 3, 4, 1, '', NULL),
	(160, 7, 7, 152, 5, 7, 1, '', NULL),
	(161, 7, 6, 153, 1, 2, 1, '', NULL),
	(162, 7, 3, 153, 3, 3, 1, '', NULL),
	(163, 7, 8, 153, 4, 4, 1, '', NULL),
	(164, 7, 2, 153, 5, 6, 1, '', NULL),
	(165, 7, 3, 154, 4, 5, 1, '', NULL),
	(166, 7, 1, 154, 6, 8, 1, '', NULL),
	(167, 7, 6, 155, 1, 2, 1, '', NULL),
	(168, 7, 10, 155, 3, 4, 1, '', NULL),
	(169, 7, 4, 161, 1, 2, 1, '', NULL),
	(170, 7, 11, 161, 3, 4, 1, '', NULL),
	(171, 7, 5, 161, 5, 6, 1, '', NULL),
	(172, 7, 5, 161, 7, 8, 1, '', NULL),
	(173, 7, 2, 162, 1, 2, 1, '', NULL),
	(174, 7, 8, 162, 3, 4, 1, '', NULL),
	(175, 7, 7, 162, 5, 7, 1, '', NULL),
	(176, 7, 6, 163, 1, 2, 1, '', NULL),
	(177, 7, 3, 163, 3, 3, 1, '', NULL),
	(178, 7, 8, 163, 4, 4, 1, '', NULL),
	(179, 7, 2, 163, 5, 6, 1, '', NULL),
	(180, 7, 3, 164, 4, 5, 1, '', NULL),
	(181, 7, 1, 164, 6, 8, 1, '', NULL),
	(182, 7, 6, 165, 1, 2, 1, '', NULL),
	(183, 7, 10, 165, 3, 4, 1, '', NULL),
	(184, 7, 1, 176, 1, 1, 1, 'It Würsts.\nPainis', 13),
	(186, 7, 1, 177, 1, 1, 1, '', NULL);
/*!40000 ALTER TABLE `tbfachinfo` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbfachmod
DROP TABLE IF EXISTS `tbfachmod`;
CREATE TABLE IF NOT EXISTS `tbfachmod` (
  `ID` int(11) NOT NULL,
  `Bezeichnung` varchar(32) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfachmod: ~3 rows (ungefähr)
DELETE FROM `tbfachmod`;
/*!40000 ALTER TABLE `tbfachmod` DISABLE KEYS */;
INSERT INTO `tbfachmod` (`ID`, `Bezeichnung`) VALUES
	(1, 'Findet statt'),
	(2, 'Vertretung'),
	(3, 'Entfällt');
/*!40000 ALTER TABLE `tbfachmod` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbfaecher
DROP TABLE IF EXISTS `tbfaecher`;
CREATE TABLE IF NOT EXISTS `tbfaecher` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `NameKurz` varchar(12) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfaecher: ~11 rows (ungefähr)
DELETE FROM `tbfaecher`;
/*!40000 ALTER TABLE `tbfaecher` DISABLE KEYS */;
INSERT INTO `tbfaecher` (`ID`, `Name`, `NameKurz`) VALUES
	(1, 'Mathe', 'M'),
	(2, 'Deutsch', 'D'),
	(3, 'Englisch', 'E'),
	(4, 'Programmieren', 'PR'),
	(5, 'Microcontrollertechnik', 'µC'),
	(6, 'Elektroprozesstechnik', 'EP'),
	(7, 'Betriebssysteme/Netzwerke', 'BN'),
	(8, 'Selbst Lernen', 'SL'),
	(9, 'Wirtschaft', 'W'),
	(10, 'Politik/Geschichte', 'P/G'),
	(11, 'Datenbanken', 'DB');
/*!40000 ALTER TABLE `tbfaecher` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbfaecherverteilung
DROP TABLE IF EXISTS `tbfaecherverteilung`;
CREATE TABLE IF NOT EXISTS `tbfaecherverteilung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Stunde` int(11) NOT NULL,
  `Uhrzeit` time NOT NULL,
  `Ende` time NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfaecherverteilung: ~16 rows (ungefähr)
DELETE FROM `tbfaecherverteilung`;
/*!40000 ALTER TABLE `tbfaecherverteilung` DISABLE KEYS */;
INSERT INTO `tbfaecherverteilung` (`ID`, `Stunde`, `Uhrzeit`, `Ende`) VALUES
	(4, 1, '07:30:00', '08:15:00'),
	(5, 2, '08:15:00', '09:00:00'),
	(6, 3, '09:15:00', '10:00:00'),
	(7, 4, '10:00:00', '10:45:00'),
	(8, 5, '11:00:00', '11:45:00'),
	(9, 6, '11:45:00', '12:30:00'),
	(10, 7, '12:45:00', '13:30:00'),
	(11, 8, '13:30:00', '14:15:00'),
	(12, 9, '14:30:00', '15:15:00'),
	(13, 10, '15:15:00', '16:00:00'),
	(14, 11, '16:15:00', '17:00:00'),
	(15, 12, '17:00:00', '17:45:00'),
	(16, 13, '18:00:00', '18:45:00'),
	(17, 14, '18:45:00', '19:30:00'),
	(18, 15, '19:45:00', '20:30:00'),
	(19, 16, '20:30:00', '21:15:00');
/*!40000 ALTER TABLE `tbfaecherverteilung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbfaecherverteilungstundenplan
DROP TABLE IF EXISTS `tbfaecherverteilungstundenplan`;
CREATE TABLE IF NOT EXISTS `tbfaecherverteilungstundenplan` (
  `StundenplanID` int(11) NOT NULL,
  `FaecherveteilungID` int(11) NOT NULL,
  PRIMARY KEY (`StundenplanID`,`FaecherveteilungID`),
  KEY `ZuordnungFaechervertieilung_idx` (`FaecherveteilungID`),
  CONSTRAINT `ZuordnungFaechervertieilung` FOREIGN KEY (`FaecherveteilungID`) REFERENCES `tbfaecherverteilung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ZuordnungStundenplan` FOREIGN KEY (`StundenplanID`) REFERENCES `tbstundenplan` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfaecherverteilungstundenplan: ~0 rows (ungefähr)
DELETE FROM `tbfaecherverteilungstundenplan`;
/*!40000 ALTER TABLE `tbfaecherverteilungstundenplan` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbfaecherverteilungstundenplan` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbklasse
DROP TABLE IF EXISTS `tbklasse`;
CREATE TABLE IF NOT EXISTS `tbklasse` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbklasse: ~6 rows (ungefähr)
DELETE FROM `tbklasse`;
/*!40000 ALTER TABLE `tbklasse` DISABLE KEYS */;
INSERT INTO `tbklasse` (`ID`, `Name`) VALUES
	(1, 'CI13V1'),
	(5, 'CI12V2'),
	(6, 'CI13V2'),
	(9, 'CI11V1'),
	(10, 'CI11V2'),
	(11, 'CI12V1');
/*!40000 ALTER TABLE `tbklasse` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tblehrer
DROP TABLE IF EXISTS `tblehrer`;
CREATE TABLE IF NOT EXISTS `tblehrer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Titel` varchar(45) DEFAULT NULL,
  `Vorname` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Strasse` varchar(45) NOT NULL,
  `Hausnummer` varchar(8) NOT NULL,
  `PLZ` varchar(8) NOT NULL,
  `Ort` varchar(45) NOT NULL,
  `KlasseID` int(11) DEFAULT NULL,
  `AnmeldungID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `KlasseID_idx` (`KlasseID`),
  KEY `AnmeldungID_idx` (`AnmeldungID`),
  CONSTRAINT `AnmeldungID` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tblehrer: ~0 rows (ungefähr)
DELETE FROM `tblehrer`;
/*!40000 ALTER TABLE `tblehrer` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblehrer` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbraum
DROP TABLE IF EXISTS `tbraum`;
CREATE TABLE IF NOT EXISTS `tbraum` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(24) NOT NULL DEFAULT '',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbraum: ~3 rows (ungefähr)
DELETE FROM `tbraum`;
/*!40000 ALTER TABLE `tbraum` DISABLE KEYS */;
INSERT INTO `tbraum` (`ID`, `Name`) VALUES
	(8, '405'),
	(10, '406'),
	(13, '403');
/*!40000 ALTER TABLE `tbraum` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbrechtebeschreibung
DROP TABLE IF EXISTS `tbrechtebeschreibung`;
CREATE TABLE IF NOT EXISTS `tbrechtebeschreibung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Group` varchar(48) NOT NULL,
  `Name` varchar(48) NOT NULL,
  `Beschreibung` varchar(256) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbrechtebeschreibung: ~14 rows (ungefähr)
DELETE FROM `tbrechtebeschreibung`;
/*!40000 ALTER TABLE `tbrechtebeschreibung` DISABLE KEYS */;
INSERT INTO `tbrechtebeschreibung` (`ID`, `Group`, `Name`, `Beschreibung`) VALUES
	(1, 'login', 'isteacher', 'Ist ein Lehrer'),
	(2, 'lessonerbuilder', 'permission', 'Zugriff auf Lessonerbuilder'),
	(3, 'lessoner', 'openteacher', 'Darf Lehrerstundenpläne sehen'),
	(4, 'lessoner', 'chooseclass', 'Darf alle Klassen sehen'),
	(5, 'lessoner', 'uploadfile', 'Darf Dateien hochladen'),
	(6, 'lessoner', 'deletefile', 'Darf Dateien löschen'),
	(7, 'lessoner', 'editlessoninfo', 'Darf Stundeninfo bearbeiten'),
	(8, 'studentmanagement', 'permission', 'Zugriff auf Schülerverwaltung'),
	(9, 'studentmanagement', 'editstudents', 'Darf Schüler bearbeiten'),
	(10, 'studentmanagement', 'editclass', 'Darf Klassen erstellen/bearbeiten'),
	(11, 'studentmanagement', 'deletestudent', 'Darf Schüler löschen'),
	(12, 'teachermanagement', 'permission', 'Zugriff auf Lehrerverwaltung'),
	(13, 'teachermanagement', 'editteacher', 'Darf Lehrer bearbeiten'),
	(14, 'teachermanagement', 'deleteteacher', 'Darf Lehrer löschen');
/*!40000 ALTER TABLE `tbrechtebeschreibung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbrechtewert
DROP TABLE IF EXISTS `tbrechtewert`;
CREATE TABLE IF NOT EXISTS `tbrechtewert` (
  `AnmeldungID` int(11) NOT NULL,
  `RechtID` int(11) NOT NULL,
  `Wert` tinyint(1) NOT NULL,
  PRIMARY KEY (`AnmeldungID`,`RechtID`),
  KEY `FK_tbrechtewert_tbrechtebeschreibung` (`RechtID`),
  CONSTRAINT `FK_tbrechtewert_tbanmeldung` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_tbrechtewert_tbrechtebeschreibung` FOREIGN KEY (`RechtID`) REFERENCES `tbrechtebeschreibung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbrechtewert: ~28 rows (ungefähr)
DELETE FROM `tbrechtewert`;
/*!40000 ALTER TABLE `tbrechtewert` DISABLE KEYS */;
INSERT INTO `tbrechtewert` (`AnmeldungID`, `RechtID`, `Wert`) VALUES
	(1, 1, 1),
	(1, 2, 1),
	(1, 3, 1),
	(1, 4, 1),
	(1, 5, 1),
	(1, 6, 1),
	(1, 7, 1),
	(1, 8, 1),
	(1, 9, 1),
	(1, 10, 1),
	(1, 11, 1),
	(1, 12, 1),
	(1, 13, 1),
	(1, 14, 1),
	(27, 1, 0),
	(27, 2, 0),
	(27, 3, 0),
	(27, 4, 0),
	(27, 5, 0),
	(27, 6, 0),
	(27, 7, 0),
	(27, 8, 0),
	(27, 9, 0),
	(27, 10, 0),
	(27, 11, 0),
	(27, 12, 0),
	(27, 13, 0),
	(27, 14, 0);
/*!40000 ALTER TABLE `tbrechtewert` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbschueler
DROP TABLE IF EXISTS `tbschueler`;
CREATE TABLE IF NOT EXISTS `tbschueler` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Vorname` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Strasse` varchar(45) NOT NULL,
  `Hausnummer` varchar(8) NOT NULL,
  `PLZ` varchar(8) NOT NULL,
  `Ort` varchar(45) NOT NULL,
  `KlasseID` int(11) NOT NULL,
  `AnmeldungID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `KlasseID_idx` (`KlasseID`),
  KEY `AnmeldungID_idx` (`AnmeldungID`),
  CONSTRAINT `Fk_AnmeldungID` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbSchueler_KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbschueler: ~1 rows (ungefähr)
DELETE FROM `tbschueler`;
/*!40000 ALTER TABLE `tbschueler` DISABLE KEYS */;
INSERT INTO `tbschueler` (`ID`, `Vorname`, `Name`, `Strasse`, `Hausnummer`, `PLZ`, `Ort`, `KlasseID`, `AnmeldungID`) VALUES
	(10, 'Florian', 'Fürstenberg', 'Erdbrüggenstraße', '59', '45889', 'Gelsenkirchen', 1, 27);
/*!40000 ALTER TABLE `tbschueler` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbstundenplan
DROP TABLE IF EXISTS `tbstundenplan`;
CREATE TABLE IF NOT EXISTS `tbstundenplan` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Klassename` varchar(45) NOT NULL,
  `Datum` date NOT NULL,
  `KlasseID` int(11) NOT NULL,
  `FaecherverteilungID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `KlasseID_idx` (`KlasseID`),
  CONSTRAINT `Fk_KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=188 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbstundenplan: ~24 rows (ungefähr)
DELETE FROM `tbstundenplan`;
/*!40000 ALTER TABLE `tbstundenplan` DISABLE KEYS */;
INSERT INTO `tbstundenplan` (`ID`, `Klassename`, `Datum`, `KlasseID`, `FaecherverteilungID`) VALUES
	(162, '', '2014-03-17', 1, 0),
	(163, '', '2014-03-17', 5, 0),
	(165, '', '2014-03-24', 1, 0),
	(166, '', '2014-03-31', 1, 0),
	(167, '', '2014-04-07', 1, 0),
	(168, '', '2014-04-14', 1, 0),
	(169, '', '2014-04-21', 1, 0),
	(170, '', '2014-04-28', 1, 0),
	(171, '', '2014-05-05', 1, 0),
	(173, '', '2014-03-24', 9, 0),
	(174, '', '2014-03-31', 9, 0),
	(175, '', '2014-04-07', 9, 0),
	(176, '', '2014-04-14', 9, 0),
	(177, '', '2014-04-14', 6, 0),
	(178, '', '2014-03-31', 10, 0),
	(179, '', '2014-04-14', 5, 0),
	(180, '', '2014-03-31', 5, 0),
	(181, '', '2014-03-31', 6, 0),
	(182, '', '2014-03-31', 11, 0),
	(183, '', '2014-04-07', 11, 0),
	(184, '', '2014-04-07', 5, 0),
	(185, '', '2014-04-21', 9, 0),
	(186, '', '2014-04-28', 9, 0),
	(187, '', '2014-05-05', 9, 0);
/*!40000 ALTER TABLE `tbstundenplan` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbtage
DROP TABLE IF EXISTS `tbtage`;
CREATE TABLE IF NOT EXISTS `tbtage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TagInfoID` int(11) NOT NULL,
  `StundenplanID` int(11) NOT NULL,
  `FindetStatt` tinyint(1) NOT NULL,
  `Information` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_tbTage_tbStundenplan_idx` (`StundenplanID`),
  KEY `fk_tbTage_tbTagInfo_idx` (`TagInfoID`),
  CONSTRAINT `fk_tbTage_tbStundenplan` FOREIGN KEY (`StundenplanID`) REFERENCES `tbstundenplan` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_tnTage_tbTaginfo` FOREIGN KEY (`TagInfoID`) REFERENCES `tbtaginfo` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=246 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbtage: ~120 rows (ungefähr)
DELETE FROM `tbtage`;
/*!40000 ALTER TABLE `tbtage` DISABLE KEYS */;
INSERT INTO `tbtage` (`ID`, `TagInfoID`, `StundenplanID`, `FindetStatt`, `Information`) VALUES
	(116, 1, 162, 1, NULL),
	(117, 2, 162, 1, NULL),
	(118, 3, 162, 1, NULL),
	(119, 4, 162, 1, NULL),
	(120, 5, 162, 1, NULL),
	(121, 1, 163, 1, NULL),
	(122, 2, 163, 1, NULL),
	(123, 3, 163, 1, NULL),
	(124, 4, 163, 1, NULL),
	(125, 5, 163, 1, NULL),
	(131, 1, 165, 1, ''),
	(132, 2, 165, 1, ''),
	(133, 3, 165, 1, ''),
	(134, 4, 165, 1, ''),
	(135, 5, 165, 1, ''),
	(136, 1, 166, 1, ''),
	(137, 2, 166, 1, ''),
	(138, 3, 166, 1, ''),
	(139, 4, 166, 1, ''),
	(140, 5, 166, 1, ''),
	(141, 1, 167, 1, ''),
	(142, 2, 167, 1, ''),
	(143, 3, 167, 1, ''),
	(144, 4, 167, 1, ''),
	(145, 5, 167, 1, ''),
	(146, 1, 168, 1, ''),
	(147, 2, 168, 1, ''),
	(148, 3, 168, 1, ''),
	(149, 4, 168, 1, ''),
	(150, 5, 168, 1, ''),
	(151, 1, 169, 1, ''),
	(152, 2, 169, 1, ''),
	(153, 3, 169, 1, ''),
	(154, 4, 169, 1, ''),
	(155, 5, 169, 1, ''),
	(156, 1, 170, 1, ''),
	(157, 2, 170, 1, ''),
	(158, 3, 170, 1, ''),
	(159, 4, 170, 1, ''),
	(160, 5, 170, 1, ''),
	(161, 1, 171, 1, ''),
	(162, 2, 171, 1, ''),
	(163, 3, 171, 1, ''),
	(164, 4, 171, 1, ''),
	(165, 5, 171, 1, ''),
	(171, 1, 173, 1, NULL),
	(172, 2, 173, 1, NULL),
	(173, 3, 173, 1, NULL),
	(174, 4, 173, 1, NULL),
	(175, 5, 173, 1, NULL),
	(176, 1, 174, 1, NULL),
	(177, 2, 174, 1, NULL),
	(178, 3, 174, 1, NULL),
	(179, 4, 174, 1, NULL),
	(180, 5, 174, 1, NULL),
	(181, 1, 175, 1, NULL),
	(182, 2, 175, 1, NULL),
	(183, 3, 175, 1, NULL),
	(184, 4, 175, 1, NULL),
	(185, 5, 175, 1, NULL),
	(186, 1, 176, 1, NULL),
	(187, 2, 176, 1, NULL),
	(188, 3, 176, 1, NULL),
	(189, 4, 176, 1, NULL),
	(190, 5, 176, 1, NULL),
	(191, 1, 177, 1, NULL),
	(192, 2, 177, 1, NULL),
	(193, 3, 177, 1, NULL),
	(194, 4, 177, 1, NULL),
	(195, 5, 177, 1, NULL),
	(196, 1, 178, 1, NULL),
	(197, 2, 178, 1, NULL),
	(198, 3, 178, 1, NULL),
	(199, 4, 178, 1, NULL),
	(200, 5, 178, 1, NULL),
	(201, 1, 179, 1, NULL),
	(202, 2, 179, 1, NULL),
	(203, 3, 179, 1, NULL),
	(204, 4, 179, 1, NULL),
	(205, 5, 179, 1, NULL),
	(206, 1, 180, 1, NULL),
	(207, 2, 180, 1, NULL),
	(208, 3, 180, 1, NULL),
	(209, 4, 180, 1, NULL),
	(210, 5, 180, 1, NULL),
	(211, 1, 181, 1, NULL),
	(212, 2, 181, 1, NULL),
	(213, 3, 181, 1, NULL),
	(214, 4, 181, 1, NULL),
	(215, 5, 181, 1, NULL),
	(216, 1, 182, 1, NULL),
	(217, 2, 182, 1, NULL),
	(218, 3, 182, 1, NULL),
	(219, 4, 182, 1, NULL),
	(220, 5, 182, 1, NULL),
	(221, 1, 183, 1, NULL),
	(222, 2, 183, 1, NULL),
	(223, 3, 183, 1, NULL),
	(224, 4, 183, 1, NULL),
	(225, 5, 183, 1, NULL),
	(226, 1, 184, 1, NULL),
	(227, 2, 184, 1, NULL),
	(228, 3, 184, 1, NULL),
	(229, 4, 184, 1, NULL),
	(230, 5, 184, 1, NULL),
	(231, 1, 185, 1, NULL),
	(232, 2, 185, 1, NULL),
	(233, 3, 185, 1, NULL),
	(234, 4, 185, 1, NULL),
	(235, 5, 185, 1, NULL),
	(236, 1, 186, 1, NULL),
	(237, 2, 186, 1, NULL),
	(238, 3, 186, 1, NULL),
	(239, 4, 186, 1, NULL),
	(240, 5, 186, 1, NULL),
	(241, 1, 187, 1, NULL),
	(242, 2, 187, 1, NULL),
	(243, 3, 187, 1, NULL),
	(244, 4, 187, 1, NULL),
	(245, 5, 187, 1, NULL);
/*!40000 ALTER TABLE `tbtage` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbtaginfo
DROP TABLE IF EXISTS `tbtaginfo`;
CREATE TABLE IF NOT EXISTS `tbtaginfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(10) NOT NULL,
  `NameKurz` varchar(2) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbtaginfo: ~5 rows (ungefähr)
DELETE FROM `tbtaginfo`;
/*!40000 ALTER TABLE `tbtaginfo` DISABLE KEYS */;
INSERT INTO `tbtaginfo` (`ID`, `Name`, `NameKurz`) VALUES
	(1, 'Montag', 'Mo'),
	(2, 'Dienstag', 'Di'),
	(3, 'Mitwoch', 'Mi'),
	(4, 'Donnerstag', 'Do'),
	(5, 'Freitag', 'Fr');
/*!40000 ALTER TABLE `tbtaginfo` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

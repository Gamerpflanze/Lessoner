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
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbanmeldung: ~11 rows (ungefähr)
DELETE FROM `tbanmeldung`;
/*!40000 ALTER TABLE `tbanmeldung` DISABLE KEYS */;
INSERT INTO `tbanmeldung` (`ID`, `Email`, `Passwort`, `ProfilBildPfad`) VALUES
	(2, 'lehrer', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(3, 'schueler', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(4, 'schueler', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(5, 'Florian.Fürstenberg', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(17, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(18, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(19, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(20, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(21, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(22, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL),
	(23, 'w', _binary 0x1C4F0C6EB8BF8BBF11CC2AE1CDCC5C5D1F3A3C16, NULL);
/*!40000 ALTER TABLE `tbanmeldung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbdateien
DROP TABLE IF EXISTS `tbdateien`;
CREATE TABLE IF NOT EXISTS `tbdateien` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FachinfoID` int(11) NOT NULL,
  `Path` varchar(128) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_tbDateien_tbFachinfo_idx` (`FachinfoID`),
  CONSTRAINT `fk_tbDateien_tbFachinfo` FOREIGN KEY (`FachinfoID`) REFERENCES `tbfachinfo` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbdateien: ~0 rows (ungefähr)
DELETE FROM `tbdateien`;
/*!40000 ALTER TABLE `tbdateien` DISABLE KEYS */;
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
  PRIMARY KEY (`ID`),
  KEY `fkLehrer_idx` (`LehrerID`),
  KEY `fkFach_idx` (`FachID`),
  KEY `fk_tbFachinfo_tbTage_idx` (`TagID`),
  KEY `fk_tbFachinfo_tbFachMod` (`FachModID`),
  CONSTRAINT `fkFach` FOREIGN KEY (`FachID`) REFERENCES `tbfaecher` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fkLehrer` FOREIGN KEY (`LehrerID`) REFERENCES `tblehrer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbFachinfo_tbFachMod` FOREIGN KEY (`FachModID`) REFERENCES `tbfachmod` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbFachinfo_tbTage` FOREIGN KEY (`TagID`) REFERENCES `tbtage` (`ID`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=184 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfachinfo: ~105 rows (ungefähr)
DELETE FROM `tbfachinfo`;
/*!40000 ALTER TABLE `tbfachinfo` DISABLE KEYS */;
INSERT INTO `tbfachinfo` (`ID`, `LehrerID`, `FachID`, `TagID`, `Stunde_Beginn`, `Stunde_Ende`, `FachModID`) VALUES
	(34, 7, 4, 136, 1, 2, 1),
	(35, 7, 11, 136, 3, 4, 1),
	(36, 7, 5, 136, 5, 6, 1),
	(37, 7, 5, 136, 7, 8, 1),
	(38, 7, 1, 137, 1, 2, 1),
	(39, 7, 8, 137, 3, 4, 1),
	(40, 7, 7, 137, 5, 7, 1),
	(41, 7, 6, 138, 1, 2, 1),
	(42, 7, 3, 138, 3, 3, 1),
	(43, 7, 8, 138, 4, 4, 1),
	(44, 7, 2, 138, 5, 6, 1),
	(45, 7, 3, 139, 4, 5, 1),
	(46, 7, 1, 139, 6, 8, 1),
	(47, 7, 6, 140, 1, 2, 1),
	(48, 7, 10, 140, 3, 4, 1),
	(64, 7, 4, 146, 1, 2, 1),
	(65, 7, 11, 146, 3, 4, 1),
	(66, 7, 5, 146, 5, 6, 1),
	(67, 7, 5, 146, 7, 8, 1),
	(68, 7, 1, 147, 1, 2, 1),
	(69, 7, 8, 147, 3, 4, 1),
	(70, 7, 7, 147, 5, 7, 1),
	(71, 7, 6, 148, 1, 2, 1),
	(72, 7, 3, 148, 3, 3, 1),
	(73, 7, 8, 148, 4, 4, 1),
	(74, 7, 2, 148, 5, 6, 1),
	(75, 7, 3, 149, 4, 5, 1),
	(76, 7, 1, 149, 6, 8, 1),
	(77, 7, 6, 150, 1, 2, 1),
	(78, 7, 10, 150, 3, 4, 1),
	(94, 7, 4, 156, 1, 2, 1),
	(95, 7, 11, 156, 3, 4, 1),
	(96, 7, 5, 156, 5, 6, 1),
	(97, 7, 5, 156, 7, 8, 1),
	(98, 7, 1, 157, 1, 2, 1),
	(99, 7, 8, 157, 3, 4, 1),
	(100, 7, 7, 157, 5, 7, 1),
	(101, 7, 6, 158, 1, 2, 1),
	(102, 7, 3, 158, 3, 3, 1),
	(103, 7, 8, 158, 4, 4, 1),
	(104, 7, 2, 158, 5, 6, 1),
	(105, 7, 3, 159, 4, 5, 1),
	(106, 7, 1, 159, 6, 8, 1),
	(107, 7, 6, 160, 1, 2, 1),
	(108, 7, 10, 160, 3, 4, 1),
	(124, 7, 4, 131, 1, 2, 1),
	(125, 7, 11, 131, 3, 4, 1),
	(126, 7, 5, 131, 5, 6, 1),
	(127, 7, 5, 131, 7, 8, 1),
	(128, 7, 2, 132, 1, 2, 1),
	(129, 7, 8, 132, 3, 4, 1),
	(130, 7, 7, 132, 5, 7, 1),
	(131, 7, 6, 133, 1, 2, 1),
	(132, 7, 3, 133, 3, 3, 1),
	(133, 7, 8, 133, 4, 4, 1),
	(134, 7, 2, 133, 5, 6, 1),
	(135, 7, 3, 134, 4, 5, 1),
	(136, 7, 1, 134, 6, 8, 1),
	(137, 7, 6, 135, 1, 2, 1),
	(138, 7, 10, 135, 3, 4, 1),
	(139, 7, 4, 141, 1, 2, 1),
	(140, 7, 11, 141, 3, 4, 1),
	(141, 7, 5, 141, 5, 6, 1),
	(142, 7, 5, 141, 7, 8, 1),
	(143, 7, 2, 142, 1, 2, 1),
	(144, 7, 8, 142, 3, 4, 1),
	(145, 7, 7, 142, 5, 7, 1),
	(146, 7, 6, 143, 1, 2, 1),
	(147, 7, 3, 143, 3, 3, 1),
	(148, 7, 8, 143, 4, 4, 1),
	(149, 7, 2, 143, 5, 6, 1),
	(150, 7, 3, 144, 4, 5, 1),
	(151, 7, 1, 144, 6, 8, 1),
	(152, 7, 6, 145, 1, 2, 1),
	(153, 7, 10, 145, 3, 4, 1),
	(154, 7, 4, 151, 1, 2, 1),
	(155, 7, 11, 151, 3, 4, 1),
	(156, 7, 5, 151, 5, 6, 1),
	(157, 7, 5, 151, 7, 8, 1),
	(158, 7, 2, 152, 1, 2, 1),
	(159, 7, 8, 152, 3, 4, 1),
	(160, 7, 7, 152, 5, 7, 1),
	(161, 7, 6, 153, 1, 2, 1),
	(162, 7, 3, 153, 3, 3, 1),
	(163, 7, 8, 153, 4, 4, 1),
	(164, 7, 2, 153, 5, 6, 1),
	(165, 7, 3, 154, 4, 5, 1),
	(166, 7, 1, 154, 6, 8, 1),
	(167, 7, 6, 155, 1, 2, 1),
	(168, 7, 10, 155, 3, 4, 1),
	(169, 7, 4, 161, 1, 2, 1),
	(170, 7, 11, 161, 3, 4, 1),
	(171, 7, 5, 161, 5, 6, 1),
	(172, 7, 5, 161, 7, 8, 1),
	(173, 7, 2, 162, 1, 2, 1),
	(174, 7, 8, 162, 3, 4, 1),
	(175, 7, 7, 162, 5, 7, 1),
	(176, 7, 6, 163, 1, 2, 1),
	(177, 7, 3, 163, 3, 3, 1),
	(178, 7, 8, 163, 4, 4, 1),
	(179, 7, 2, 163, 5, 6, 1),
	(180, 7, 3, 164, 4, 5, 1),
	(181, 7, 1, 164, 6, 8, 1),
	(182, 7, 6, 165, 1, 2, 1),
	(183, 7, 10, 165, 3, 4, 1);
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tblehrer: ~4 rows (ungefähr)
DELETE FROM `tblehrer`;
/*!40000 ALTER TABLE `tblehrer` DISABLE KEYS */;
INSERT INTO `tblehrer` (`ID`, `Titel`, `Vorname`, `Name`, `Strasse`, `Hausnummer`, `PLZ`, `Ort`, `KlasseID`, `AnmeldungID`) VALUES
	(7, NULL, 'Max', 'Mustermann', 'Musterstrasse', '1', '12345', 'Musterhausen', 0, 4),
	(11, 'w', 'w', 'w', 'w', 'w', 'w', 'w', NULL, 22),
	(12, 'w', 'w', 'w', 'w', 'w', 'w', 'w', NULL, 23);
/*!40000 ALTER TABLE `tblehrer` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbrechtebeschreibung
DROP TABLE IF EXISTS `tbrechtebeschreibung`;
CREATE TABLE IF NOT EXISTS `tbrechtebeschreibung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Group` varchar(48) NOT NULL,
  `Name` varchar(48) NOT NULL,
  `Beschreibung` varchar(256) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbrechtebeschreibung: ~10 rows (ungefähr)
DELETE FROM `tbrechtebeschreibung`;
/*!40000 ALTER TABLE `tbrechtebeschreibung` DISABLE KEYS */;
INSERT INTO `tbrechtebeschreibung` (`ID`, `Group`, `Name`, `Beschreibung`) VALUES
	(1, 'login', 'isteacher', 'Ist ein Lehrer'),
	(2, 'lessonerbuilder', 'permission', 'Zugriff auf Lessonerbuilder'),
	(3, 'lessoner', 'openteacher', 'Darf Lehrerstundenpläne sehen'),
	(4, 'lessoner', 'chooseclass', 'Darf alle Klassen sehen'),
	(5, 'lessoner', 'uploadfile', 'Darf Dateien hochladen'),
	(6, 'lessoner', 'deletefile', 'Darf Dateien löschen'),
	(7, 'studentmanagement', 'permission', 'Zugriff auf Schülerverwaltung'),
	(8, 'studentmanagement', 'editstudents', 'Darf Schüler bearbeiten'),
	(9, 'studentmanagement', 'addclass', 'Darf Klassen erstellen'),
	(10, 'studentmanagement', 'deletestudent', 'Darf Schüler löschen');
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

-- Exportiere Daten aus Tabelle dblessoner.tbrechtewert: ~60 rows (ungefähr)
DELETE FROM `tbrechtewert`;
/*!40000 ALTER TABLE `tbrechtewert` DISABLE KEYS */;
INSERT INTO `tbrechtewert` (`AnmeldungID`, `RechtID`, `Wert`) VALUES
	(2, 1, 1),
	(2, 2, 1),
	(2, 3, 1),
	(2, 4, 1),
	(2, 5, 1),
	(2, 6, 1),
	(2, 7, 1),
	(2, 8, 1),
	(2, 9, 1),
	(2, 10, 1),
	(3, 1, 0),
	(3, 2, 1),
	(3, 3, 1),
	(3, 4, 1),
	(3, 5, 1),
	(3, 6, 1),
	(3, 7, 1),
	(3, 8, 1),
	(3, 9, 1),
	(3, 10, 1),
	(4, 1, 1),
	(4, 2, 0),
	(4, 3, 1),
	(4, 4, 1),
	(4, 5, 1),
	(4, 6, 1),
	(4, 7, 1),
	(4, 8, 1),
	(4, 9, 1),
	(4, 10, 1),
	(5, 1, 0),
	(5, 2, 1),
	(5, 3, 1),
	(5, 4, 1),
	(5, 5, 1),
	(5, 6, 1),
	(5, 7, 1),
	(5, 8, 1),
	(5, 9, 1),
	(5, 10, 1),
	(22, 1, 1),
	(22, 2, 0),
	(22, 3, 0),
	(22, 4, 0),
	(22, 5, 0),
	(22, 6, 0),
	(22, 7, 0),
	(22, 8, 0),
	(22, 9, 0),
	(22, 10, 0),
	(23, 1, 1),
	(23, 2, 0),
	(23, 3, 0),
	(23, 4, 0),
	(23, 5, 0),
	(23, 6, 0),
	(23, 7, 0),
	(23, 8, 0),
	(23, 9, 0),
	(23, 10, 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbschueler: ~2 rows (ungefähr)
DELETE FROM `tbschueler`;
/*!40000 ALTER TABLE `tbschueler` DISABLE KEYS */;
INSERT INTO `tbschueler` (`ID`, `Vorname`, `Name`, `Strasse`, `Hausnummer`, `PLZ`, `Ort`, `KlasseID`, `AnmeldungID`) VALUES
	(4, 'Mariane', 'Musterfrau', 'Musterstraße', '42', '45678', 'Mustercity', 1, 3),
	(6, 'Florian', 'Fürstenberg', 'Erdbrüggenstrasse', '59', '45889', 'Gelsenkirchen', 1, 5);
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
) ENGINE=InnoDB AUTO_INCREMENT=174 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbstundenplan: ~10 rows (ungefähr)
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
	(173, '', '2014-03-24', 9, 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=176 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbtage: ~50 rows (ungefähr)
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
	(175, 5, 173, 1, NULL);
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

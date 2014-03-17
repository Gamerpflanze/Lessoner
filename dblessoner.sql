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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbanmeldung: ~4 rows (ungefähr)
DELETE FROM `tbanmeldung`;
/*!40000 ALTER TABLE `tbanmeldung` DISABLE KEYS */;
INSERT INTO `tbanmeldung` (`ID`, `Email`, `Passwort`, `ProfilBildPfad`) VALUES
	(2, 'lehrer', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(3, 'schueler', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(4, 'schulleiter', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL),
	(5, 'Florian', _binary 0x7110EDA4D09E062AA5E4A390B0A572AC0D2C0220, NULL);
/*!40000 ALTER TABLE `tbanmeldung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbdateien
DROP TABLE IF EXISTS `tbdateien`;
CREATE TABLE IF NOT EXISTS `tbdateien` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FachinfoID` int(11) NOT NULL,
  `Path` varchar(128) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_tbDateien_tbFachinfo_idx` (`FachinfoID`),
  CONSTRAINT `fk_tbDateien_tbFachinfo` FOREIGN KEY (`FachinfoID`) REFERENCES `tbfachinfo` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
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
  CONSTRAINT `fk_tbFachinfo_tbTage` FOREIGN KEY (`TagID`) REFERENCES `tbtage` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbfachinfo: ~0 rows (ungefähr)
DELETE FROM `tbfachinfo`;
/*!40000 ALTER TABLE `tbfachinfo` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbklasse: ~6 rows (ungefähr)
DELETE FROM `tbklasse`;
/*!40000 ALTER TABLE `tbklasse` DISABLE KEYS */;
INSERT INTO `tbklasse` (`ID`, `Name`) VALUES
	(1, 'CI13V1'),
	(2, 'CI11V1'),
	(3, 'CI11V2'),
	(4, 'CI12V1'),
	(5, 'CI12V2'),
	(6, 'CI13V2');
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
  `KlasseID` int(11) NOT NULL,
  `AnmeldungID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `KlasseID_idx` (`KlasseID`),
  KEY `AnmeldungID_idx` (`AnmeldungID`),
  CONSTRAINT `AnmeldungID` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tblehrer: ~2 rows (ungefähr)
DELETE FROM `tblehrer`;
/*!40000 ALTER TABLE `tblehrer` DISABLE KEYS */;
INSERT INTO `tblehrer` (`ID`, `Titel`, `Vorname`, `Name`, `Strasse`, `Hausnummer`, `PLZ`, `Ort`, `KlasseID`, `AnmeldungID`) VALUES
	(7, NULL, 'Max', 'Mustermann', 'Musterstrasse', '1', '12345', 'Musterhausen', 0, 4),
	(8, NULL, 'Bla', 'Bla', 'Blastraße', '123', '54321', 'Nirgentwo', 1, 2);
/*!40000 ALTER TABLE `tblehrer` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbrechtebeschreibung
DROP TABLE IF EXISTS `tbrechtebeschreibung`;
CREATE TABLE IF NOT EXISTS `tbrechtebeschreibung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Group` varchar(48) NOT NULL,
  `Name` varchar(48) NOT NULL,
  `Beschreibung` varchar(256) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbrechtebeschreibung: ~6 rows (ungefähr)
DELETE FROM `tbrechtebeschreibung`;
/*!40000 ALTER TABLE `tbrechtebeschreibung` DISABLE KEYS */;
INSERT INTO `tbrechtebeschreibung` (`ID`, `Group`, `Name`, `Beschreibung`) VALUES
	(1, 'login', 'isteacher', 'Gibt an ob Jemand ein Lehrer oder Schüler ist'),
	(2, 'lessonerbuilder', 'permission', 'Das Recht auf die Seite LessonerBuilder.aspx zuzugreifen'),
	(3, 'lessoner', 'openteacher', 'Das Recht, Stundenpläne von Lehrern sehen zu dürfen'),
	(4, 'lessoner', 'chooseclass', 'Gibt an ob jemand alle Klassen anzeigen darf oder nicht'),
	(5, 'lessoner', 'uploadfile', 'Darf jemand dateien hochladen?'),
	(6, 'lessoner', 'deletefile', 'Darf jemand dateien löschen?');
/*!40000 ALTER TABLE `tbrechtebeschreibung` ENABLE KEYS */;


-- Exportiere Struktur von Tabelle dblessoner.tbrechtewert
DROP TABLE IF EXISTS `tbrechtewert`;
CREATE TABLE IF NOT EXISTS `tbrechtewert` (
  `AnmeldungID` int(11) NOT NULL,
  `RechtID` int(11) NOT NULL,
  `Wert` tinyint(1) NOT NULL,
  PRIMARY KEY (`AnmeldungID`,`RechtID`),
  KEY `FK_tbrechtewert_tbrechtebeschreibung` (`RechtID`),
  CONSTRAINT `FK_tbrechtewert_tbanmeldung` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tbrechtewert_tbrechtebeschreibung` FOREIGN KEY (`RechtID`) REFERENCES `tbrechtebeschreibung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbrechtewert: ~24 rows (ungefähr)
DELETE FROM `tbrechtewert`;
/*!40000 ALTER TABLE `tbrechtewert` DISABLE KEYS */;
INSERT INTO `tbrechtewert` (`AnmeldungID`, `RechtID`, `Wert`) VALUES
	(2, 1, 1),
	(2, 2, 1),
	(2, 3, 1),
	(2, 4, 1),
	(2, 5, 1),
	(2, 6, 1),
	(3, 1, 0),
	(3, 2, 1),
	(3, 3, 1),
	(3, 4, 1),
	(3, 5, 1),
	(3, 6, 1),
	(4, 1, 1),
	(4, 2, 1),
	(4, 3, 1),
	(4, 4, 1),
	(4, 5, 1),
	(4, 6, 1),
	(5, 1, 0),
	(5, 2, 1),
	(5, 3, 1),
	(5, 4, 1),
	(5, 5, 1),
	(5, 6, 1);
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
  CONSTRAINT `Fk_AnmeldungID` FOREIGN KEY (`AnmeldungID`) REFERENCES `tbanmeldung` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_tbSchueler_KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbschueler: ~2 rows (ungefähr)
DELETE FROM `tbschueler`;
/*!40000 ALTER TABLE `tbschueler` DISABLE KEYS */;
INSERT INTO `tbschueler` (`ID`, `Vorname`, `Name`, `Strasse`, `Hausnummer`, `PLZ`, `Ort`, `KlasseID`, `AnmeldungID`) VALUES
	(4, 'Mariane', 'Musterfrau', 'Musteralee', '42a', '45678', 'Mustercity', 1, 3),
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
  CONSTRAINT `Fk_KlasseID` FOREIGN KEY (`KlasseID`) REFERENCES `tbklasse` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=161 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbstundenplan: ~0 rows (ungefähr)
DELETE FROM `tbstundenplan`;
/*!40000 ALTER TABLE `tbstundenplan` DISABLE KEYS */;
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
  CONSTRAINT `fk_tbTage_tbStundenplan` FOREIGN KEY (`StundenplanID`) REFERENCES `tbstundenplan` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_tnTage_tbTaginfo` FOREIGN KEY (`TagInfoID`) REFERENCES `tbtaginfo` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=111 DEFAULT CHARSET=utf8;

-- Exportiere Daten aus Tabelle dblessoner.tbtage: ~0 rows (ungefähr)
DELETE FROM `tbtage`;
/*!40000 ALTER TABLE `tbtage` DISABLE KEYS */;
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

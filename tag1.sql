SELECT A.ID
FROM tbanmeldung AS A
WHERE A.Email = @Email



INSERT INTO tbschueler (Vorname, Name, Strasse, Hausnummer, PLZ, Ort, KlasseID, AnmeldungID)
VALUES (@Vorname, @Name, @Strasse, @Hausnummer, @PLZ, @Ort, @KlasseID, SELECT ID
																							  FROM tbanmeldung
																							  WHERE Email = @Email)	


SELECT *
FROM tbstundenplan
JOIN 
WHERE 
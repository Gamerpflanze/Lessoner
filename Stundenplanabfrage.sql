SELECT ta.FindetStatt, fa.Name, le.Titel, le.Vorname, le.Name AS Nachname, fi.Stunde_Beginn, fi.Stunde_Ende, fv.Stunde, fv.Uhrzeit, ti.Name AS TagName
FROM tbstundenplan AS st 
JOIN tbklasse AS kl
	ON kl.ID = st.KlasseID
JOIN tbtage AS ta
	ON ta.StundenplanID = st.ID
JOIN tbtaginfo AS ti
	ON ti.ID = ta.TagInfoID
JOIN tbfachinfo AS fi
	ON fi.TagID = ta.ID
JOIN tbfaecher AS fa
	ON fa.ID = fi.FachID
JOIN tbfachmod AS fm
	ON fm.ID = fi.FachModID
JOIN tblehrer AS le
	ON fi.LehrerID = le.ID
JOIN tbfaecherverteilung AS fv
	ON fv.Stunde = fi.Stunde_Beginn
WHERE ti.ID = 1 AND st.KlasseID = @KlassenID
ORDER BY fv.Stunde ASC
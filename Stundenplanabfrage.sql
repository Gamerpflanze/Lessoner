SELECT*
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
WHERE ti.ID = 1 AND st.Klassename = 'CI13V1'
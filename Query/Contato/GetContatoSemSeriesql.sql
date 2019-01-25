SELECT TOP 500
	CTT.NOMECONTATO AS Nome,
	CTT.CODCONTATO AS CodContato
FROM TGFCTT CTT
INNER JOIN TCSCON CON WITH(NOLOCK) ON CON.CODPARC = CTT.CODPARC

WHERE 1= 1
AND CTT.CODPARC = 1
AND CON.NUMCONTRATO = 96
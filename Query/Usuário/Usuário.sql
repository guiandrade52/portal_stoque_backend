SELECT 
	PRTL.IDUSUPRTL AS IdUsuario,
	CTT.NOMECONTATO AS Nome,
	PRTL.LGNUSU AS Login,
	CTT.EMAIL AS Email,
    CTT.TELEFONE AS Telefone
	FROM AD_USUPRTL PRTL
	INNER JOIN TGFCTT CTT WITH(NOLOCK) ON CTT.CODCONTATO = PRTL.CODCONTATO AND CTT.CODPARC = PRTL.CODPARC
    WHERE PRTL.IDUSUPRTL = 337



	

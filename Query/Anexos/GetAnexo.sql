SELECT 
	NOMEARQUIVO AS Nome,
	CHAVEARQUIVO AS Chave,
	CONVERT(CHAR, DHCAD, 103) AS DataCadastro,
	DESCRICAO AS Descricao,
	Tipo = (SELECT REVERSE(LEFT(REVERSE(NOMEARQUIVO),CHARINDEX('.', REVERSE(NOMEARQUIVO))-1))),
	NomeView = CHAVEARQUIVO +'.'+ (SELECT REVERSE(LEFT(REVERSE(NOMEARQUIVO),CHARINDEX('.', REVERSE(NOMEARQUIVO))-1)))
FROM TSIANX WHERE PKREGISTRO = '35071_0_BHBPMAtividade'

--SELECT* FROM TSIANX ORDER BY PKREGISTRO
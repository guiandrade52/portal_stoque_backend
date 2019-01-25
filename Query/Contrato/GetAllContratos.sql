DECLARE @pPagina INT, @pTamPag INT, @pCodParceiro nvarchar(100), @filtroWhere varchar(30), @pNumSerie varchar(40)

SET @pPagina = 1
SET @pTamPag = 5000
SET @pCodParceiro = '91'
SET @filtroWhere = LOWER('%%')
SET @pNumSerie = '39021292369'

/* MODEL CONTRATO 

	SELECT
		CON.NUMCONTRATO AS CodContrato,
		PAR.NOMEPARC AS Nome
		FROM TCSCON CON
		INNER JOIN TGFPAR PAR  WITH(NOLOCK) ON PAR.CODPARC = CON.CODPARC
		WHERE 1 = 1
		AND CON.NUMCONTRATO <> 0
		AND CON.ATIVO = 'S'
		AND PAR.CODPARC IN (1)
*/








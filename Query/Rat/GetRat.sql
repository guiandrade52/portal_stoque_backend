SELECT 
	EXECUTIONID AS ExecutionId, 
    NUMVISITA AS NumeroVisita, 
    DHINI AS DataInicio,
    DHFIN AS DataFinal,    
    DEFEITOCONS AS Defeito, 
    CAUSADEF AS Causa, 
    SOLUCAOAPL AS Solucao 
FROM AD_STOVST 
WHERE 1 = 1
AND NUMVISITA > 1
ORDER BY ExecutionId asc

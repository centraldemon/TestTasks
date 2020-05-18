IF Object_id('tempdb..#test_table') IS NOT NULL 
  DROP TABLE #test_table 

CREATE TABLE #test_table 
  ( 
     id INT 
  ) 

GO 

INSERT INTO #test_table 
VALUES (1), (2), (8), (4), (9), (7), (3), (10) --<-- Отсутствуют числа 5 и 6
GO

SELECT MIN(id) mn, MAX(id) mx 
FROM   #test_table --where id not in (select * from #test_table)

SELECT ones.n + 10*tens.n + 100*hundreds.n + 1000*thousands.n
FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) ones(n),
     (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) tens(n),
     (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) hundreds(n),
     (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) thousands(n)
WHERE (ones.n + 10*tens.n + 100*hundreds.n + 1000*thousands.n BETWEEN (SELECT MIN(id) from #test_table) AND (SELECT MAX(id) from #test_table)) AND (ones.n + 10*tens.n + 100*hundreds.n + 1000*thousands.n not in (select * from #test_table))
ORDER BY 1

GO
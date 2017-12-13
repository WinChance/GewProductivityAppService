--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure]

SELECT TOP 10 * FROM FNMDB..fnJobTraceHdr WHERE FN_Card='A17407820'
SELECT TOP 10 * FROM fnLogisticstask
SELECT TOP 10 * FROM fnLogistics
UPDATE  FNMDB..fnJobTraceHdr SET Car_Location='PE' WHERE  FN_Card='A17407820'
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN101','6590(CP02)','test','A17407820'

--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN102',Car_NO,Car_Location,name2,FN_Card
--参数列表：@param2:@car_no; @param3:@locationno; @param4:@fn_card; @param5:@cardno; @param6:@name 
EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN102','224','PE','A17407820','GET0286547','hyc'

--@param2:@source; @param3:@destination; @param4:@cardno; @param5:@name; @param6:@FN_Card
EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN103','FA','PE','GET0286547','hyc','A17407820'

EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN104','A17407820,A17407821,A17407822','GET0286547','hyc','FA','PE'

EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN105','GET0286547','hyc','224','A17407820','PE'

EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN106','224'

UPDATE  FNMDB..fnLogisticstask SET car_no='223' WHERE id=1
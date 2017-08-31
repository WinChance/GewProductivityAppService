SELECT TOP 100 * FROM [dbo].[prdAppSarong] WHERE  BatchType='T01' ORDER BY SarongNo 

UPDATE [dbo].[prdAppSarong] SET IsUsed='·ñ' WHERE IsUsed='ÊÇ'

SELECT TOP 10 *  FROM uvw_BatchInfo WHERE Batch_NO='TC204087'

SELECT TOP 10 *  FROM ydmdb.dbo.rtProduction WHERE Sarong_No='AA03' ORDER BY Iden DESC
SELECT YDMDB.dbo.udf_prdAppIsFixedColor('TC100270')

 exec usp_ydWmisGetBatchInfoByBatchNo 'TC100270'
--DELETE  FROM uvw_BatchInfo WHERE
SELECT TOP 100 Batch_NO AS '¸×ºÅ',a.Input_Time,a.*  FROM ydmdb.dbo.rtProduction AS a WHERE Type='×°Áý' ORDER BY a.Input_Time DESC

SELECT TOP 100 * FROM uvw_BatchInfo  ORDER BY Batch_Delivery_Date DESC


DELETE FROM ydmdb.dbo.rtProduction WHERE Batch_NO='TC100270'
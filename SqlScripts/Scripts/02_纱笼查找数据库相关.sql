-- prdAppSarong����ɾ�Ĳ�Ȩ��
USE YDMDB
GRANT INSERT,DELETE,UPDATE,SELECT ON prdAppSarong TO GewPrdApp
GO
-- ִ�д洢����Ȩ��
USE YDMDB
GRANT EXECUTE ON usp_prdAppGetSarongStatusByBatchType TO GewPrdApp
GO

-- ִ�д洢����Ȩ��
USE YDMDB
GRANT EXECUTE ON usp_prdAppInputRtProduction TO GewPrdApp
GO
-- ���ݿ��*******************************
-- 1��ɴ����Ϣ��
select top 10 * from YDMDB.dbo.prdAppSarong

-- �洢����********************************
-- 1��
usp_prdAppGetSarongStatusByBatchType
-- 2��
usp_prdAppInputRtProduction
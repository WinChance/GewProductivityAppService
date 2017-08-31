-- prdAppSarong表增删改查权限
USE YDMDB
GRANT INSERT,DELETE,UPDATE,SELECT ON prdAppSarong TO GewPrdApp
GO
-- 执行存储过程权限
USE YDMDB
GRANT EXECUTE ON usp_prdAppGetSarongStatusByBatchType TO GewPrdApp
GO

-- 执行存储过程权限
USE YDMDB
GRANT EXECUTE ON usp_prdAppInputRtProduction TO GewPrdApp
GO
-- 数据库表*******************************
-- 1、纱笼信息表
select top 10 * from YDMDB.dbo.prdAppSarong

-- 存储过程********************************
-- 1、
usp_prdAppGetSarongStatusByBatchType
-- 2、
usp_prdAppInputRtProduction
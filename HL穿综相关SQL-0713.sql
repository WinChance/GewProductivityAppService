-- 产量表
SELECT TOP 1000 Iden, HL_No, Name, Class, Post, Sys_Score, Dync_Score, ProValue,
                CONVERT(VARCHAR(12),InputTime,120), ModifyName, ModifyTime, Remark, IsLargeType, IsMore,
                IsCalico, Remark2 from PDMDB.dbo.hlOutput    where post='穿综' AND Remark IS NOT NULL AND [Remark] LIKE '输入人%' ORDER BY InputTime DESC
-- 人员表
select TOP 10 * from PDMDB.dbo.hlProductionStaff  where Post='穿综'   --人员.
-- 基础信息表
select top 100 HealdingScore from hlbasicinfo where hl_no = 'HL2017-38874N'
-- 卡上条码与HL号对应关系 
SELECT TOP 10 nAutoID,strHLNo FROM dbo.Pattern2HL WHERE strHLNo='HL2017-35655N'
-- 代码里计算系统分的SQL 0803
DECLARE @InputScore DECIMAL=0.0
DECLARE @HL_NO VARCHAR(30)='HL2017-34247N'

Select ISNULL(C.InputScore,0) AS SysScore INTO #Tb1 FROM Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo=@HL_NO
UNION ALL
SELECT ISNULL(a.HealdingScore,0)AS SysScore FROM dbo.hlBasicInfo AS a WHERE a.HL_No=@HL_NO
SELECT SUM(a.SysScore) FROM #Tb1 AS a
DROP TABLE #Tb1

--添加飞穿分 赖卫东
SELECT TOP 10  C.InputScore From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo=''

SELECT  * FROM dbo.hlBasicInfo WHERE HL_Date>'2017-07-01'
DELETE  FROM dbo.hlBasicInfo WHERE HL_Date>'2017-07-01'
SELECT * FROM hlUnHealdingScore WHERE HL_No='HL2017-38874N'
DELETE  FROM Pattern2HL WHERE Operate_time>'2017-07-01'

SELECT TOP 10 * FROM dbo.hlUnHealdingScore ORDER BY InputTime DESC
-- 无飞穿分
select top 10 HealdingScore from hlbasicinfo where hl_no = 'HL2017-34793N'
SELECT TOP 10 * FROM dbo.hlUnHealdingScore WHERE HL_No='HL2017-38874N'

-- 有飞穿分 8-8
select top 10 HealdingScore AS '基础分' from hlbasicinfo where hl_no = 'HL2017-38874N'
Select ISNULL(C.InputScore,0) AS '飞穿分'  FROM Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo='HL2017-38874N'


SELECT TOP 10 * FROM dbo.hlUnHealdingScore WHERE HL_No='HL2017-36601N'
SELECT TOP 10 * from PDMDB.dbo.hlOutput    where post='穿综' AND Name='管理员' AND HL_No='HL2017-35655N'




SELECT HealdingScore FROM dbo.hlBasicInfo WHERE HL_No='HL2017-38874N'

Select C.InputScore From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo='HL2017-38874N'
-------------------分别列出基础分和飞穿分----------------------
DECLARE @HL_NO VARCHAR(30)
--有飞穿分：HL2017-39991N
--无飞穿分：1、正常：HL2017-40610N  2、异常：HL2017-39837N
SET @HL_NO='HL2017-39837N'
Select ISNULL(C.InputScore,0) AS 'Score' From Pattern2HL A with(nolock) 
                   Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
                   Inner Join hlUnHealdingScore C with(nolock) On B.Drawing = C.Drawing 
                   Where A.strHLNo=@HL_NO 
				   AND C.TotalEnds=(SELECT a.TotalEnds FROM dbo.hlBasicInfo  AS a  with(nolock)WHERE a.HL_No=@HL_NO)
UNION ALL
SELECT    ISNULL(b.HealdingScore,0)
          FROM      hlBasicInfo AS b with(nolock)
          WHERE     b.HL_No = @HL_NO

SELECT b.HL_No,b.HealdingScore FROM hlBasicInfo AS b with(nolock) WHERE     b.HL_No ='HL2017-39837N'
---------------------求和------------------------
--有飞穿分：HL2017-39991N
--无飞穿分：1、正常：HL2017-40610N  2、异常：HL2017-39837N
DECLARE @HL_NO VARCHAR(30)
SET @HL_NO='HL2017-40610N'
SELECT SUM(c.Score) AS 'SysScore' FROM
(Select ISNULL(C.InputScore,0) AS 'Score' From Pattern2HL A with(nolock) 
                   Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
                   Inner Join hlUnHealdingScore C with(nolock) On B.Drawing = C.Drawing 
                   Where A.strHLNo=@HL_NO 
				   AND C.TotalEnds=(SELECT a.TotalEnds FROM dbo.hlBasicInfo  AS a  with(nolock)WHERE a.HL_No=@HL_NO)
UNION ALL
SELECT    ISNULL(b.HealdingScore,0)
          FROM      hlBasicInfo AS b with(nolock)
          WHERE     b.HL_No = @HL_NO)AS c 

-----------------------------------------------

SELECT TOP 10 * FROM PDMDB.dbo.hlUnHealdingScore AS a ORDER BY a.Iden DESC

SELECT TOP 10 * FROM GewPrdAppDB.dbo.peAppWvUser WHERE name='温燕芬'

UPDATE GewPrdAppDB.dbo.peAppWvUser SET name='温燕芬' WHERE name='梁燕芬'

DELETE FROM GewPrdAppDB.dbo.peAppWvUser  WHERE Id=1726

SELECT * FROM PDMDB.dbo.hlOutput WHERE Name LIKE '%梁燕芬%' AND InputTime>'2017-1-1 00:00' AND InputTime<'2018-1-1 00:00'

SELECT TOP 1000 Iden, HL_No, Name, Class, Post, Sys_Score, Dync_Score, ProValue,
                CONVERT(VARCHAR(20),InputTime,120) AS InputTime, ModifyName, ModifyTime, Remark, IsLargeType, IsMore,
                IsCalico, Remark2 from PDMDB.dbo.hlOutput    where post='穿综' AND Remark IS NOT NULL AND [Remark] LIKE '输入人%' AND InputTime>'2017-08-14 09:41:32' ORDER BY InputTime DESC




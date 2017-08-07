-- 产量表
SELECT TOP 10 * from PDMDB.dbo.hlOutput    where post='穿综' AND Name='管理员'
-- 人员表
select TOP 10 * from PDMDB.dbo.hlProductionStaff  where Post='穿综'   --人员.
-- 基础信息表
select top 100 * from hlbasicinfo where hl_no = 'HL2017-34289N'
-- 卡上条码与HL号对应关系 
SELECT TOP 10 nAutoID,strHLNo FROM dbo.Pattern2HL WHERE strHLNo='HL2017-34922N'
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
SELECT * FROM hlUnHealdingScore WHERE InputTime >'2017-07-01'
DELETE  FROM Pattern2HL WHERE Operate_time>'2017-07-01'

SELECT TOP 10 * FROM dbo.hlUnHealdingScore ORDER BY InputTime DESC


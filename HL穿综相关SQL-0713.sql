SELECT TOP 10 Iden ,
              HL_No ,
              Name ,
              Class ,
              Post ,
              Sys_Score ,
              Dync_Score ,
              ProValue ,
              InputTime ,
              ModifyName ,
              ModifyTime ,
              Remark ,
              IsLargeType ,
              IsMore ,
              IsCalico from PDMDB.dbo.hlOutput    where post='穿综'
select TOP 10 * from PDMDB.dbo.hlProductionStaff  where Post='穿综'   --人员.
-- grade_cent分数
select top 100 * from hlbasicinfo where hl_no = 'HL2017-20134N' 
-- HL2017-88888N

select top 10 * from PDMDB.dbo.hlOutput where HL_No = 'HL2017-20276N' 

select top 10 * from hlbasicinfo ORDER BY Make_date DESC

select  * from PDMDB.dbo.hlOutput
--添加飞穿分
SELECT TOP 10  C.InputScore From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo=''


SELECT TOP 10 HealdingScore,* FROM PDMDB.dbo.hlBasicInfo

SELECT TOP 10 b.HL_No,ISNULL(C.InputScore,0)+ISNULL(b.HealdingScore,0) AS SysCalScore,B.HL_No,c.InputScore,b.HealdingScore ,C.InputTime From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where C.InputTime<GETDATE()-1 AND B.HealdingScore IS NOT NULL--A.strHLNo=@p0

SELECT  * FROM dbo.hlBasicInfo WHERE HL_Date>'2017-07-01'
DELETE  FROM dbo.hlBasicInfo WHERE HL_Date>'2017-07-01'
SELECT * FROM hlUnHealdingScore WHERE InputTime >'2017-07-01'
DELETE  FROM Pattern2HL WHERE Operate_time>'2017-07-01'
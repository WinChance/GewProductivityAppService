-- ������
SELECT TOP 1000 * from PDMDB.dbo.hlOutput    where post='����' AND Remark IS NOT NULL AND [Remark] LIKE '������%' ORDER BY InputTime DESC
-- ��Ա��
select TOP 10 * from PDMDB.dbo.hlProductionStaff  where Post='����'   --��Ա.
-- ������Ϣ��
select top 100 HealdingScore from hlbasicinfo where hl_no = 'HL2017-38874N'
-- ����������HL�Ŷ�Ӧ��ϵ 
SELECT TOP 10 nAutoID,strHLNo FROM dbo.Pattern2HL WHERE strHLNo='HL2017-35655N'
-- ���������ϵͳ�ֵ�SQL 0803
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

--��ӷɴ��� ������
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
-- �޷ɴ���
select top 10 HealdingScore from hlbasicinfo where hl_no = 'HL2017-34793N'
SELECT TOP 10 * FROM dbo.hlUnHealdingScore WHERE HL_No='HL2017-38874N'

-- �зɴ��� 8-8
select top 10 HealdingScore AS '������' from hlbasicinfo where hl_no = 'HL2017-38874N'
Select ISNULL(C.InputScore,0) AS '�ɴ���'  FROM Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo='HL2017-38874N'


SELECT TOP 10 * FROM dbo.hlUnHealdingScore WHERE HL_No='HL2017-36601N'
SELECT TOP 10 * from PDMDB.dbo.hlOutput    where post='����' AND Name='����Ա' AND HL_No='HL2017-35655N'




SELECT HealdingScore FROM dbo.hlBasicInfo WHERE HL_No='HL2017-38874N'

Select C.InputScore From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo='HL2017-38874N'
-------------------�ֱ��г������ֺͷɴ���----------------------
DECLARE @HL_NO VARCHAR(30)
--�зɴ��֣�HL2017-39991N
--�޷ɴ��֣�1��������HL2017-40610N  2���쳣��HL2017-39837N
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
---------------------���------------------------
--�зɴ��֣�HL2017-39991N
--�޷ɴ��֣�1��������HL2017-40610N  2���쳣��HL2017-39837N
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




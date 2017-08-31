USE [YDMDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_prdAppDropDownListGet]    Script Date: 2017-07-18 17:09:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		HuangYoC
-- Create date: 17-04-14
-- Description:	ͨ�÷�ʽ�����������б�����
-- =============================================
ALTER PROCEDURE [dbo].[usp_prdAppDropDownListGet]
	(@param1 NVARCHAR(50)='',@param2 NVARCHAR(50) ='',@param3 NVARCHAR(50)='',@param4 NVARCHAR(50)='',@param5 NVARCHAR(50)='',@rtn INT OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @rtnDropDownList TABLE
                    (
					  id INT IDENTITY,
                      picurl NVARCHAR(20),  
					  code NVARCHAR(20),
					  text NVARCHAR(30),
					  dest1 NVARCHAR(20),
					  dest2 NVARCHAR(20),
					  dest3 NVARCHAR(20)
                    );			
	IF(@param1='GetHlProductionStaffs_Name')--������Ա
	BEGIN
		INSERT INTO @rtnDropDownList(
	          picurl ,
	          code ,
	          text ,
	          dest1 ,
	          dest2 ,
	          dest3
	        ) SELECT DISTINCT '','',a.Name,'','','' FROM PDMDB.dbo.hlProductionStaff AS a WHERE Class=@param2 AND a.Post='����'--���봩�۰��
	END
	--@param1��ɸѡ��Ϣ�� @param2������
	ELSE IF(@param1='GetPubDBpbWorkerList')-- ����Ⱦɴɴ��״̬APP������Ա�б�
	BEGIN
			IF(@param2='RT')
			BEGIN
			INSERT INTO @rtnDropDownList(
	          picurl ,
	          code ,
	          text ,
	          dest1 ,
	          dest2 ,
	          dest3
	        )
				SELECT DISTINCT '','',a.Worker_ID+'-'+a.Worker_Name,'','','' FROM pubdb.dbo.pbWorkerList AS a WHERE Department=@param2 AND a.Worker_ID NOT LIKE 'D%' OR a.Worker_ID NOT LIKE 'N%'
			END
			ELSE IF(@param2='R2')
			BEGIN
			INSERT INTO @rtnDropDownList(
	          picurl ,
	          code ,
	          text ,
	          dest1 ,
	          dest2 ,
	          dest3
	        )
				SELECT DISTINCT '','',a.Worker_ID+'-'+a.Worker_Name,'','','' FROM pubdb.dbo.pbWorkerList AS a WHERE Department=@param2 AND a.Worker_ID LIKE 'N%'
			END
			ELSE IF(@param2='R3')
			BEGIN
			INSERT INTO @rtnDropDownList(
	          picurl ,
	          code ,
	          text ,
	          dest1 ,
	          dest2 ,
	          dest3
	        )
				SELECT DISTINCT '','',a.Worker_ID+'-'+a.Worker_Name,'','','' FROM pubdb.dbo.pbWorkerList AS a WHERE Department=@param2 AND a.Worker_ID LIKE 'D%'
			END
		
	END
	
	SET @rtn=1;
	SELECT * FROM @rtnDropDownList ORDER BY id --,dest3
	DELETE @rtnDropDownList	
END


GO



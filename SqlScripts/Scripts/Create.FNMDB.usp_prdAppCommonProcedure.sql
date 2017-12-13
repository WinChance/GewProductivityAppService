USE [FNMDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
/*  
   APP ͨ�ô洢���̽ӿڣ�ִ�м�̵���ɾ�� 
   @type ��������  
   @param1����������ѡ��  
   @param2����������ѡ��  
 ��@param3����������ѡ��  
�� @param4����������ѡ��
   @param5����������ѡ��  
�� @param6����������ѡ��  
  
���ԣ�EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] xxxx...
*/  
   
ALTER PROCEDURE [dbo].[usp_prdAppCommonProcedure]
(
@type NVARCHAR(100)='',
@param2 NVARCHAR(100) ='',
@param3 NVARCHAR(100)='',
@param4 NVARCHAR(100)='',
@param5 NVARCHAR(100)='',
@param6 NVARCHAR(100)='',
@rtnMsg nvarchar(2000) ='success' OUT
)    
AS    
BEGIN    
   
 /* ���ڲ��Ա�ͷ  
  declare @type NVARCHAR(50)  
  declare @param2 NVARCHAR(50)  
  declare @param3 NVARCHAR(50)  
  declare @param4 NVARCHAR(50)  
  declare @param5 NVARCHAR(50)  
  declare @param6 NVARCHAR(50) 
  declare @rtnErrors INT
  set @type=''  
  set @param2=''  
  set @param3=''  
  set @param4=''  
  set @param5='' 
  set @param6='' 
 */  
SET NOCOUNT ON;
SET XACT_ABORT ON;  --ִ�� Transact-SQL ����������ʱ����������������ֹ���ع�
--BEGIN************************************* ��������APP ***************************************--	
BEGIN TRY
BEGIN TRANSACTION WuliuTran
--**�ϲ�����**--
IF(@type='FN101')  
BEGIN
  update  FNMDB..fnJobTraceHdr  set Car_NO=@param2,--@car_no
  labu=@param3--@main_card
  where FN_Card=@param4--@fn_card 
  
END

--**�ϲ�����**--
IF(@type='FN102')
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN102','224','PE','A17407820','GET0286547','hyc'
--@param2:@car_no; @param3:@locationno; @param4:@fn_card; @param5:@cardno; @param6:@name 
  DECLARE @labu_FN102 varchar(30)
  Select @labu_FN102=CONVERT(varchar(100), GETDATE(), 112)
  
  --ʵʱ����
  update  fnJobTraceHdr  set Car_NO=@param2 ,--@car_no
  labu=@labu_FN102,
  Car_Location=@param3
  where FN_Card=@param4
  
  --��λ����
  if (@param3<>'')
  begin
    update fnLogisticstask set islocation=1,cardno2=@param5,
	name2=@param6 ,
	operater2time=getdate() where car_no=@param2
  end
 
  --����������
  update fnLogisticstask set Car_NO=@param2,
   labu=@labu_FN102
  where FN_Card=@param4 
END

--**������ѡ�в���**--
IF(@type='FN103')  
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN103','FA','PE','GET0286547','hyc','A17407820'
--@param2:@source; @param3:@destination; @param4:@cardno; @param5:@name; @param6:@FN_Card
  declare @labu_FN103 varchar(30)
  Select @labu_FN103=CONVERT(varchar(100), GETDATE(), 112)
  
  insert into fnLogisticstask(source,destination,FN_Card,GF_ID,GF_NO,Quantity,car_no,labu,cardno1,name1,operater1time)
  select @param2,
  @param3,
  FN_Card,a.GF_ID,b.GF_NO,Quantity,car_no,@labu_FN103,
  @param4,
  @param5,
  GETDATE()  
  From  FNMDB..fnJobTraceHdr a  inner join PDMDB..tdGFID b on a.GF_ID=b.GF_ID where a.FN_Card= @param6 
  --�Ѻϲ��Ĳ���ͬʱд��
  union  
  select @param2,
  @param3,
  FN_Card,a.GF_ID,b.GF_NO,Quantity,car_no,'ƴ��',
  @param4,
  @param5,GETDATE()  
  From  FNMDB..fnJobTraceHdr a  inner join PDMDB..tdGFID b on a.GF_ID=b.GF_ID where a.labu= @param6
  
  --ֻ����������
  update  fnJobTraceHdr  set labu=@labu_FN103
  where FN_Card= @param6 
END

--**˾��ѡ�в�����������**--
IF(@type='FN104')  
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN104','A17407820,A17407821,A17407822','GET0286547','hyc','FA','PE'
--@param2:@car_no_list; @param3:@cardno; @param4:@name; @param5:@source; @param6:@destination
  declare @Logisticsno varchar(30)
  select  @Logisticsno=CONVERT(varchar(100), GETDATE(), 112)
                      +convert(varchar(2),datepart(hour,getdate())) 
                      +convert(varchar(3),datepart(mi,getdate()))  
                      +convert(varchar(3),datepart(ss,getdate()))  
                      
 update FNMDB..fnLogisticstask set Logisticsno=@Logisticsno
 where car_no in (select value from  FNMDB.dbo.UDF_SplitToTable(
 @param2,
 ',') ) --@car_no_list
 and isnull(Logisticsno,'')=''
 
 --д����
 insert into fnLogistics(Logisticsno,cardno,name,source,destination,sendtime)
 values(
 @Logisticsno,
 @param3,--@cardno
 @param4,--@name
 @param5,--@source
 @param6,--@destination
 GETDATE())
END

--**������λ����**--
IF(@type='FN105')  
BEGIN
 --@param2:@cardno; @param3:@name; @param4:@car_no; @param5:@FN_Card; @param6:@locationno; 
 UPDATE fnLogisticstask SET islocation=1,
 cardno2=@param2,--@cardno
 name2=@param3 ,--@name
 operater2time=GETDATE()
 WHERE car_no=@param4--@car_no

 UPDATE fnJobTraceHdr SET Car_Location=@param6--@locationno
 WHERE FN_Card=@param5--@FN_Card
 
 --��ƴ����һ�����
 UPDATE fnJobTraceHdr  SET Car_Location=@param6--@locationno
 WHERE labu=@param5--@FN_Card
END

IF(@type='FN106')  --����������ѡ�в���
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN106','224'
--@param2:@car_no;
  --ɾ��������
  delete  fnLogisticstask  where FN_Card=@param2  
  --��ƴ����Ҳɾ��
  delete  fnLogisticstask  where FN_Card in(select FN_Card From  FNMDB..fnJobTraceHdr where labu=@param2)
  --ֻ����������
  update  fnJobTraceHdr  set labu=''  where FN_Card=@param2 
END
--************************************* END ***************************************--	
COMMIT TRANSACTION WuliuTran
END TRY
BEGIN CATCH
	SET @rtnMsg=ERROR_MESSAGE()    --����׽���Ĵ�����Ϣ���ڱ���@rtnMsg��               
    RAISERROR (@rtnMsg,16,1)    --�˴������׳�(������������....)
    ROLLBACK TRANSACTION WuliuTran����--���e�؝L��
END CATCH
END    
    
GO



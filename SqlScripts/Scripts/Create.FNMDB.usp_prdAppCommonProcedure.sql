USE [FNMDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
/*  
   APP 通用存储过程接口，执行简短的增删改 
   @type 操作类型  
   @param1　参数（可选）  
   @param2　参数（可选）  
 　@param3　参数（可选）  
　 @param4　参数（可选）
   @param5　参数（可选）  
　 @param6　参数（可选）  
  
测试：EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] xxxx...
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
   
 /* 用于测试表头  
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
SET XACT_ABORT ON;  --执行 Transact-SQL 语句产生运行时错误，则整个事务将终止并回滚
--BEGIN************************************* 后整物流APP ***************************************--	
BEGIN TRY
BEGIN TRANSACTION WuliuTran
--**合并布车**--
IF(@type='FN101')  
BEGIN
  update  FNMDB..fnJobTraceHdr  set Car_NO=@param2,--@car_no
  labu=@param3--@main_card
  where FN_Card=@param4--@fn_card 
  
END

--**合并布车**--
IF(@type='FN102')
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN102','224','PE','A17407820','GET0286547','hyc'
--@param2:@car_no; @param3:@locationno; @param4:@fn_card; @param5:@cardno; @param6:@name 
  DECLARE @labu_FN102 varchar(30)
  Select @labu_FN102=CONVERT(varchar(100), GETDATE(), 112)
  
  --实时更新
  update  fnJobTraceHdr  set Car_NO=@param2 ,--@car_no
  labu=@labu_FN102,
  Car_Location=@param3
  where FN_Card=@param4
  
  --定位布车
  if (@param3<>'')
  begin
    update fnLogisticstask set islocation=1,cardno2=@param5,
	name2=@param6 ,
	operater2time=getdate() where car_no=@param2
  end
 
  --更新物流表
  update fnLogisticstask set Car_NO=@param2,
   labu=@labu_FN102
  where FN_Card=@param4 
END

--**拉布工选中布车**--
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
  --把合并的布车同时写入
  union  
  select @param2,
  @param3,
  FN_Card,a.GF_ID,b.GF_NO,Quantity,car_no,'拼车',
  @param4,
  @param5,GETDATE()  
  From  FNMDB..fnJobTraceHdr a  inner join PDMDB..tdGFID b on a.GF_ID=b.GF_ID where a.labu= @param6
  
  --只更新主布车
  update  fnJobTraceHdr  set labu=@labu_FN103
  where FN_Card= @param6 
END

--**司机选中布车产生单号**--
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
 
 --写主表
 insert into fnLogistics(Logisticsno,cardno,name,source,destination,sendtime)
 values(
 @Logisticsno,
 @param3,--@cardno
 @param4,--@name
 @param5,--@source
 @param6,--@destination
 GETDATE())
END

--**布车定位操作**--
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
 
 --有拼车的一起更新
 UPDATE fnJobTraceHdr  SET Car_Location=@param6--@locationno
 WHERE labu=@param5--@FN_Card
END

IF(@type='FN106')  --拉布工撤消选中布车
BEGIN
--EXEC [FNMDB].[dbo].[usp_prdAppCommonProcedure] 'FN106','224'
--@param2:@car_no;
  --删除主布车
  delete  fnLogisticstask  where FN_Card=@param2  
  --把拼车的也删除
  delete  fnLogisticstask  where FN_Card in(select FN_Card From  FNMDB..fnJobTraceHdr where labu=@param2)
  --只更新主布车
  update  fnJobTraceHdr  set labu=''  where FN_Card=@param2 
END
--************************************* END ***************************************--	
COMMIT TRANSACTION WuliuTran
END TRY
BEGIN CATCH
	SET @rtnMsg=ERROR_MESSAGE()    --将捕捉到的错误信息存在变量@rtnMsg中               
    RAISERROR (@rtnMsg,16,1)    --此处才能抛出(好像是这样子....)
    ROLLBACK TRANSACTION WuliuTran　　--出錯回滾事務
END CATCH
END    
    
GO



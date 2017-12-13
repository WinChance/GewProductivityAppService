USE [FNMDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_prdAppGeneralQuery]    Script Date: 2017-12-13 09:55:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*  
   APP 通用查询接口，返回任意数据集  
   @type 操作类型  
   @param1　参数（可选）  
   @param2　参数（可选）  
 　@param3　参数（可选）  
　 @param4　参数（可选）  
  
测试：EXEC [FNMDB].[dbo].[usp_prdAppGeneralQuery] 'FN001'  
*/  
  
   
ALTER PROCEDURE [dbo].[usp_prdAppGeneralQuery](@type NVARCHAR(50)='',@param2 NVARCHAR(50) ='',@param3 NVARCHAR(50)='',@param4 NVARCHAR(50)='',@param5 NVARCHAR(50)='')    
AS    
BEGIN    
   
 /* 用于测试表头  
  declare @type NVARCHAR(50)  
  declare @param2 NVARCHAR(50)  
  declare @param3 NVARCHAR(50)  
  declare @param4 NVARCHAR(50)  
  declare @param5 NVARCHAR(50)  
    
  set @type=''  
  set @param2=''  
  set @param3=''  
  set @param4=''  
  set @param5=''  
 */  
--BEGIN************************************* 后整物流APP ***************************************--	
IF(@type='FN001')  --查询待装车布车
BEGIN
select *  into #fn001 from fnLogisticstask where isnull(Logisticsno,'')=''
 --显示合并布车标记
update #fn001 set car_no=car_no+' -拼车'
where car_no in(select car_no from #fn001 group by car_no HAVING count(car_no)>1)
 --去掉拼车的布车号
select *  FROM #fn001 where labu <>'拼车'
drop table  #fn001
END

IF(@type='FN002')--按单号查询布车
BEGIN
 select *  into #fn002 from fnLogisticstask where isnull(Logisticsno,'')=@param2 --@Logisticsno
 --显示合并布车标记
 update #fn002 set car_no=car_no+' -拼车'
 where car_no in(select car_no from #fn002 group by car_no HAVING count(car_no)>1)
 --去掉拼车的布车号
 select *  FROM #fn002 where labu <>'拼车'
 drop table  #fn002	
END

IF(@type='FN003')--查询拼布车的卡号列表
BEGIN
select FN_Card from fnLogisticstask  where car_no=@param2--@car_no
END
  
IF(@type='FN004')--根据工号查询运输任务列表
BEGIN
  select * from fnLogistics where cardno=@param2
END
--************************************* END ***************************************--	

END    
    
  
  
GO



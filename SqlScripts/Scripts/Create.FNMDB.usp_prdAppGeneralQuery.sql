USE [FNMDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_prdAppGeneralQuery]    Script Date: 2017-12-13 09:55:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*  
   APP ͨ�ò�ѯ�ӿڣ������������ݼ�  
   @type ��������  
   @param1����������ѡ��  
   @param2����������ѡ��  
 ��@param3����������ѡ��  
�� @param4����������ѡ��  
  
���ԣ�EXEC [FNMDB].[dbo].[usp_prdAppGeneralQuery] 'FN001'  
*/  
  
   
ALTER PROCEDURE [dbo].[usp_prdAppGeneralQuery](@type NVARCHAR(50)='',@param2 NVARCHAR(50) ='',@param3 NVARCHAR(50)='',@param4 NVARCHAR(50)='',@param5 NVARCHAR(50)='')    
AS    
BEGIN    
   
 /* ���ڲ��Ա�ͷ  
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
--BEGIN************************************* ��������APP ***************************************--	
IF(@type='FN001')  --��ѯ��װ������
BEGIN
select *  into #fn001 from fnLogisticstask where isnull(Logisticsno,'')=''
 --��ʾ�ϲ��������
update #fn001 set car_no=car_no+' -ƴ��'
where car_no in(select car_no from #fn001 group by car_no HAVING count(car_no)>1)
 --ȥ��ƴ���Ĳ�����
select *  FROM #fn001 where labu <>'ƴ��'
drop table  #fn001
END

IF(@type='FN002')--�����Ų�ѯ����
BEGIN
 select *  into #fn002 from fnLogisticstask where isnull(Logisticsno,'')=@param2 --@Logisticsno
 --��ʾ�ϲ��������
 update #fn002 set car_no=car_no+' -ƴ��'
 where car_no in(select car_no from #fn002 group by car_no HAVING count(car_no)>1)
 --ȥ��ƴ���Ĳ�����
 select *  FROM #fn002 where labu <>'ƴ��'
 drop table  #fn002	
END

IF(@type='FN003')--��ѯƴ�����Ŀ����б�
BEGIN
select FN_Card from fnLogisticstask  where car_no=@param2--@car_no
END
  
IF(@type='FN004')--���ݹ��Ų�ѯ���������б�
BEGIN
  select * from fnLogistics where cardno=@param2
END
--************************************* END ***************************************--	

END    
    
  
  
GO



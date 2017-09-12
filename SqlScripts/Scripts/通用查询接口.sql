  

/*
   APP 通用查询接口，　返回单字符串
   @type 操作类型
   @param2　参数（可选）
   @param3　参数（可选）
 　@param4　参数（可选）
　 @param5　参数（可选）
　 @param2　参数（可选）
*/

 
CREATE PROCEDURE [dbo].[usp_prdAppQuerySingleValue](@type NVARCHAR(50)='',@param2 NVARCHAR(50) ='',@param3 NVARCHAR(50)='',@param4 NVARCHAR(50)='',@param5 NVARCHAR(50)='')  
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
  
  --拉筘/换筘起机，拉筘/换筘查布，拉筘/换筘合计
  if (@type='M07' or  @type='M08' or  @type='M09' or  @type='M10' or  @type='M11' or  @type='M12') 
  begin
	 DECLARE  @temp table (
	   c1 varchar(20),
	   c2 varchar(20),
	   c3 datetime,
	   c4 varchar(20),
	   c5 varchar(20),
	   c6 varchar(20),
	   c7 varchar(20),
	   c8 varchar(20),
	   c9  numeric(10,2),
	   c10 numeric(10,2),
	   c11 numeric(10,2),
	   qj numeric(10,2),
	   cb numeric(10,2),
	   hj numeric(10,2)
	)
	INSERT into @temp
	EXEC [getnt103].Monitor_WV1.[dbo].[Usp_GetWVLeaderYield]  @param2

    if (@type='M07' or   @type='M10' ) 
	  SELECT top 1 qj from @temp
	else if (@type='M08' or  @type='M11' )
	  SELECT top 1 cb from @temp
	else  if ( @type='M09' or @type='M12')
	  SELECT top 1 hj from @temp
  end
  
  
 
END  
  
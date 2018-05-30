
USE FNMDB
GO


--新增列
--alter table fnJobTraceHdr
--add   labu varchar(50)


--select labu,*From fnJobTraceHdr

--新增运输主表


CREATE table fnLogistics
(
  id int identity(1,1) not null,
  Logisticsno varchar(30) not null,
  cardno varchar(30),
  name varchar(30),
  car varchar(20), 
  source varchar(20), 
  destination varchar(20),  
  sendtime datetime,
  overtime datetime,
  reoveroperater varchar(30), 
  Reovertime datetime
)

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运输单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'Logisticsno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'cardno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'司机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运输车号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'car'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'destination'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运输时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'sendtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'overtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收布人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'reoveroperater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'Reovertime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运输主表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics'
GO
 
/*
  新增 布车任务明细表
*/

create table fnLogisticsTask
(
  id int identity(1,1) not null,
  Logisticsno  varchar(30),
  source varchar(20),  
  destination varchar(20), 
  FN_Card varchar(20) not null,
  GF_ID int,
  GF_NO varchar(30),
  Quantity numeric(10,2),
  car_no varchar(20),
  labu varchar(30),
  cardno1 varchar(30),
  name1 varchar(30),
  operater1time datetime,
  cardno2 varchar(30),
  name2 varchar(30),
  operater2time datetime ,
  islocation bit default 0
 )
  
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运输单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'Logisticsno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'destination'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后整卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'FN_Card'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品名ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'GF_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'GF_NO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'码长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'布车号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'car_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拉布标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'labu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送布人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'cardno1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送布人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'name1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'operater1time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'定位人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'cardno2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'定位人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'name2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'定位时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'operater2time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'布车定位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'islocation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'布车任务明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask'
GO
 
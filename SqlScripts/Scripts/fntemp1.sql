
USE FNMDB
GO


--������
--alter table fnJobTraceHdr
--add   labu varchar(50)


--select labu,*From fnJobTraceHdr

--������������


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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���䵥��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'Logisticsno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'cardno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'˾��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���䳵��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'car'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�յ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'destination'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'sendtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'overtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ղ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'reoveroperater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ղ�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics', @level2type=N'COLUMN',@level2name=N'Reovertime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogistics'
GO
 
/*
  ���� ����������ϸ��
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���䵥��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'Logisticsno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�յ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'destination'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'FN_Card'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ʒ��ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'GF_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ʒ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'GF_NO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�볤' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'car_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'labu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ͳ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'cardno1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ͳ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'name1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ͳ�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'operater1time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��λ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'cardno2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��λ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'name2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��λʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'operater2time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������λ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask', @level2type=N'COLUMN',@level2name=N'islocation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������ϸ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fnLogisticsTask'
GO
 
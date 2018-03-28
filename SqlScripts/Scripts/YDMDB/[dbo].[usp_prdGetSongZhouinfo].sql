USE [YDMDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_prdGetSongZhouinfo]    Script Date: 2018-03-23 11:59:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- select *from ydBatchTrace where batch_no= 'ZC040691'

--exec  usp_getSongZhouinfo 't50'
/*
use ydmdb

取染台待染轴信息
*/

CREATE PROC [dbo].[usp_prdGetSongZhouinfo](@machinetype VARCHAR(10))
AS 
BEGIN


Select type='B回修',a.machine_model 机型,a.dye_beam_num 轴数,a.Batch_no 缸号,a.trim_arranged_time 排整日期, a.yarn_type+a.yarn_count+a.color_code 色号,a.trim_OK_time 整经OK,b.拉装轴,
'超前整'='','QR单'=case when remark like'%QR单%' then 'Yes'else null end ,weight 重量, 理论重=convert(numeric(9,2),0.0),dye_schedule_time 排缸,
case when remark like'%小轴%'then '小轴'
     when remark like'%迷你轴%'then '迷你轴'
     when remark like'%中轴芯%'then '中轴芯'else''end 轴型,ydmdb.dbo.udf_ydGetBatchPriorityType(a.Batch_NO) 状态,
dbo.udf_getcurrentstatusbybatch_no(a.batch_no) status,打办轮次=convert(int,0),convert(char(5),a.recipe_ok_time,101) 复办OK,dbo.udf_getminyddeliverydatebybatchno(a.batch_no) 染纱交期,
convert(char(10),final_lb_delivery_date,120) 要求给板,a.remark 备注,Primary_Batch_No=convert(varchar(8),a.batch_no)
into #t11
from ydmdb.dbo.uvw_detail  a (nolock)
left join (
           select distinct  batch_no,拉装轴=case when  Pay_Type ='拉轴' then '拉轴'else ''  end +';'+case when  Pay_Type ='装轴' then '装轴' else '' end
           from ydmdb.dbo.rtproduction (nolock)
           where batch_no like'[b,z]%' and  Pay_Type in('拉轴','装轴')
            ) b on a.batch_no=b.batch_no
where a.Trim_OK_Time is not null and a.first_dye_time is not null and a.dye_time is null
and isnull(a.qc_result,'')<>'ok' and a.dye_type='B' and a.discard='n' and a.machine_model>'T46' 
and isnull(a.IsSongZhou,'')<>'Y'


select b.WV_workShop 外发,类型=convert(varchar(10),null),a.*
into #t2
from (
select *
from #t11
where right(rtrim(status),6) like '%前办%' or right(rtrim(status),6) like '%去软%'  or right(rtrim(status),6) like '%剥%'  
or right(rtrim(status),6) like '%水%' or right(rtrim(status),6) like '%牢度%' or right(rtrim(status),6) like '%加色%' 
or right(rtrim(status),6) like '%ECO%'or right(rtrim(status),6) like '%ECO%'or right(rtrim(status),6) like '%重做后处理%'

union
Select type='A正常',e.machine_model 机型,e.dye_beam_num 轴数,e.Batch_no 缸号,e.trim_arranged_time 排整日期, e.yarn_type+e.yarn_count+e.color_code 色号,e.trim_OK_time 整经OK,f.拉装轴,
'超前整'=case when e.trim_arranged_time is null then 'Yes'else '' end ,'QR单'=case when e.remark like'%QR单%' then 'Yes'else null end ,e.weight 重量,理论重=convert(numeric(9,2),0.0), e.dye_schedule_time 排缸,
case when e.remark like'%小轴%'then '小轴'
     when e.remark like'%迷你轴%'then '迷你轴'
     when e.remark like'%中轴芯%'then '中轴芯'else''end 轴型,ydmdb.dbo.udf_ydGetBatchPriorityType(e.Batch_NO) 状态,
dbo.udf_getcurrentstatusbybatch_no(e.batch_no) status,打办轮次=convert(int,0),convert(char(5),e.recipe_ok_time,101) 复办OK,dbo.udf_getminyddeliverydatebybatchno(e.batch_no) 染纱交期,
convert(char(10),final_lb_delivery_date,120) 要求给板,e.remark 备注,Primary_Batch_No=convert(varchar(8),e.batch_no)
from uvw_detail e (nolock)
left join (
           select distinct  batch_no,拉装轴=case when  Pay_Type ='拉轴' then '拉轴'else ''  end +';'+case when  Pay_Type ='装轴' then '装轴' else '' end
           from ydmdb.dbo.rtproduction (nolock)
           where batch_no like'[b,z]%' and  Pay_Type in('拉轴','装轴')
            ) f on e.batch_no=f.batch_no

where e.Trim_OK_Time IS NOT NULL and e.first_dye_time is null and e.inner_repair_times=0 and e.qc_result is null and e.dye_type='B' and e.discard='n' and e.machine_model>'T46'
and isnull(e.IsSongZhou,'')<>'Y'

)a

left join(
          select distinct e.batch_no,f.WV_workShop
          from dbo.ydBatchDtl e 
          join pubdb.dbo.uvw_pbproducttrace f on e.job_no=f.job_no and e.gf_id=f.gf_id
          where f.WV_workShop not like'%w%') b on a.缸号=b.batch_no


--以下是取打办次数
select distinct a.batch_no,a.recipe_no,b.dye_time
into #t03
from labdb.dbo.lbRecipeHdr a (nolock)
join #t2 c on a.batch_no=c.缸号
left join labdb.dbo.lbRecipeTrace b (nolock) on a.recipe_no=b.recipe_no
where a.is_cancel=0 and a.department_id<>'gek'and a.batch_no<>''and a.Recipe_Type_ID in('4','5','12')

update e set 打办轮次=f.daban
from #t2 e 
join(
      select distinct batch_no,daban=sum(case when dye_time is not null then 1 else 0 end)
      from #t03
      group by batch_no
     )f on e.缸号=f.batch_no


--找主缸号
update a set a.Primary_Batch_No=d.Primary_Batch_No
from #t2 a
join ydmdb.dbo.ydBatchUnion d (nolock) on d.sub_Batch_No=a.缸号


Select a.model_group,a.machine_model,b.Machine_id,b.Brand,b.Type,a.diameter,a.capacity,b.Machineall,b.Kmachineall,b.Wmachineall,
maxproduce=case when a.machine_model='T30'then 12 else ceiling(a.capacity*b.Machineall) end
into #machinemodel

     from pubdb.dbo.mmDyeModel a (nolock)
     join(
          select Machine_Model,Machine_id,Brand,Type,Machineall=count(machine_id),Kmachineall=sum(case when department like'k%'then 1 else 0 end),
          Wmachineall=sum(case when department like'W%'then 1 else 0 end)
         
          from pubdb.dbo.mmDyeMachine (nolock)
          where type<>'染棉厂'
          Group by Machine_Model,Brand,Machine_id,Type
          )b on a.machine_model=b.machine_model

Create table #mach (orderby int ,machine_model varchar(30),c_weight numeric(9,2),b_weight numeric(9,2),maxcone numeric(9,2),maxbeam numeric(9,2),changecone numeric(9,2))
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('1','T01','850','500','850','10','540')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('2','T02','120','100','120','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('3','T03','470','300','470','6','297')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('4','T04','470','300','470','6','297')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('5','T05','360','250','360','5','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('6','T06','230','150','230','3','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('7','T07','331','250','324','5','207')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('8','T08','331','250','324','5','207')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('9','T10','230','176','207','16','144')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('10','T11','230','176','207','16','144')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('11','T12','201.6','0','126','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('12','T13','110','0','110','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('13','T14','101','0','63','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('14','T15','60','50','60','1','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('15','T16','54','0','54','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('16','T17','200','200','200','4','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('17','T18','6.2','0','4','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('18','T19','9.6','0','6','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('19','T20','39.2','0','28','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('20','T21','16.8','0','12','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('21','T22','117','77','117','7','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('22','T23','660','400','660','8','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('23','T24','12','0','12','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('24','T25','114','0','114','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('25','T26','30','0','30','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('26','T27','4','0','4','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('27','T28','60','50','60','1','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('28','T29','190','0','190','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('29','T30','4.8','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('30','T33','30','0','30','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('31','T34','4.8','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('32','T35','4.8','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('33','T36','9.6','0','6','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('34','T37','100','0','100','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('35','T38','1593.6','0','996','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('36','T39','89.6','0','64','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('37','T40','24','0','15','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('38','T41','4.2','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('39','T42','656','0','410','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('40','T43','256','0','160','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('41','T44','25','0','25','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('42','T45','10','0','10','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('43','T46','352','0','220','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('44','T47','864','500','540','10','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('45','T48','640','400','400','8','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('46','T49','480','300','300','6','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('47','T50','352','200','220','4','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('48','T51','256','150','160','3','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('49','T52','160','125','100','5','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('50','T53','112','100','70','4','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('51','T54','96','50','60','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('52','T55','64','50','40','1','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('53','T56','11.2','0','7','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('54','T57','38.4','0','24','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('55','T58','4.8','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('56','T60','960','600','600','12','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('57','T61','25','0','25','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('58','T62','12','0','12','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('59','T63','112','0','70','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('60','T64','60','0','60','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('61','T65','656','400','410','8','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('62','T66','120','0','120','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('63','T67','256','150','160','3','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('64','T68','3','0','3','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('65','T69','8','0','5','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('66','T70','64','0','40','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('67','T71','384','0','240','0','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('68','T72','512','300','320','6','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('69','T73','960','600','600','12','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('70','T74','1.2','1.2','1.2','1','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('71','T75','0.8','0.8','0.8','1','0')
insert #mach(orderby,machine_model,c_weight,b_weight,maxcone,maxbeam,changecone) values('72','T09','1320','660','1320','1','0')


select  type='梭织',a.machine_Model,a.machine_id,a.operate_time,a.batch_no,a.is_white_yarn,llweight=convert(numeric(9,3),0.0),maxcone=convert(int,0),
case when a.dye_type='b'then a.put_dye_weight else a.cone_unit_weight*a.cone_num end put_dye_weight,a.yarn_type+a.yarn_count yarn,
case when a.dye_type='b'then a.trim_beam_unit_weight else a.cone_unit_weight end cone_unit_weight,cone_num=case when a.dye_type='b'then a.dye_beam_num else a.cone_num end,
a.ratio,a.dye_type,Unionbatch=convert(varchar(5),'N'),diameter=convert(int,0),factory=case when a.machine_id like'D%'then 'Y3' when a.machine_id like'N%'then 'Y2'else 'Y1'end
,beamtype=case when remark like'%小轴%'then '小'when remark like'%迷你轴%'then '迷'else null end
into #t1

from ydmdb.dbo.uvw_detail a (nolock)
join #t2 b on a.batch_no=b.缸号
and not exists (select sub_batch_no from ydmdb.dbo.ydBatchUnion d with(nolock) where Primary_Batch_NO<>Sub_Batch_NO and a.batch_no=d.Sub_Batch_No)

--更新特殊缸型
update a set  machine_Model=case when machine_Model='T003'then 'T03'when machine_Model='T004'then 'T04'when machine_Model='T007'then 'T07'
                            when machine_Model='T008'then 'T08'when machine_Model='T010'then 'T10'when machine_Model='T011'then 'T11'else machine_model end
from #t1 a

--更新是否为合染
update a set Unionbatch='Y'
from #t1 a
join ydmdb.dbo.ydBatchUnion d (nolock) on d.sub_Batch_No=a.batch_no

--更新缸径
update a set diameter=b.diameter
from #t1 a
join #machinemodel b on a.machine_model=b.machine_model


--更新理论重 只数
update a set 
llweight=case when a.dye_type='b'and a.machine_model in('T03','T04','T49') and beamtype='迷'then 250
when a.dye_type='b'and a.machine_model in('T50') and beamtype='迷' then 200
when a.dye_type='b'and a.machine_model in('T06','T51','T67') and beamtype='迷' then 150
when a.dye_type='b'and a.machine_model in('T52') and beamtype='小' then 110
when a.dye_type='b'and a.machine_model in('T10') and beamtype='小' then 176
when a.dye_type='b'and a.machine_model in('T07') and beamtype='小' then 253
when a.dye_type='b'and a.machine_model in('T22') and beamtype='小' then 77
when a.dye_type='b'and a.machine_model in('T15','T28') and beamtype='小' then 44
when a.dye_type='c'then b.c_weight else b_weight end,
maxcone=case when a.dye_type='b'then b.maxbeam else b.maxcone end
from #t1 a
join #mach b on a.machine_model=b.machine_model

update a set put_dye_weight=b.weight,cone_num=b.cone_num
from #t1 a
join (
      select primary_batch_no,sum(put_dye_weight)weight,sum(cone_num)cone_num
      from(
           select b.primary_batch_no,sub_batch_no,put_dye_weight=case when a.dye_type='b' then a.put_dye_weight else a.cone_unit_weight*a.cone_num end,
           cone_num=case when a.dye_type='b'then a.dye_beam_num else a.cone_num end
           from ydmdb.dbo.uvw_Detail a (nolock)
           join ydmdb.dbo.ydBatchUnion b (nolock) on a.batch_no=sub_batch_no
           ) a group by primary_batch_no
     )b on a.batch_no=b.primary_batch_no


update a set llweight=case when a.dye_type='b'then 500 else 850 end,diameter=1800 
from #t1 a
where machine_model ='T02'and year(operate_time)<'2013'

--更新12 14轴笼理论值
update a set llweight=case when cone_num<=12 then 600 when cone_num>12 then 700 else llweight end,
             maxcone =case when cone_num<=12 then 12 when cone_num>12 then 14 else maxcone end
from #t1 a
where dye_type='b'and cone_num>10 and beamtype is null


update a set 理论重=isnull(b.llweight,0.0)
from #t2 a
join #t1 b on a.机型=b.machine_model


update a set 重量=b.put_dye_weight
from #t2 a
join #t1 b on a.Primary_Batch_No=b.batch_no



--判断固色
select type='GEW',machine_Model=convert(varchar(10),null),d.machine_id,
d.batch_no,d.yarn_count,d.color_code,white=case when d.is_white_yarn=1 then'Yes'else null end,
primary_batch_no=convert(varchar(8),d.batch_no),色号深浅色=convert(varchar(10),null),机台深浅色=convert(varchar(10),'深色'),
concentration=convert(numeric(16,6),0),bill_no=convert(varchar(20),null),GXJbatchno=convert(varchar(8),null)
into #1
from ydmdb.dbo.uvw_detail d with(nolock)
join #t2 a on d.batch_no=a.缸号

--梭织主缸号
update a set primary_batch_no=b.primary_batch_no
from #1 a
join ydmdb.dbo.ydbatchunion b with(nolock) on a.batch_no=b.sub_batch_no
where a.type='GEW'

--梭织头缸
update a set primary_batch_no=b.first_batch_no
from #1 a
join ydmdb.dbo.ydBatchFollowing b with(nolock) on a.batch_no=b.Follow_batch_no
where a.type='GEW'and a.primary_batch_no=a.batch_no --(有主缸号就不找头缸了)

update a set 机台深浅色=case when b.property_name in('一般色纱浅','漂白','特白')then '浅色'when b.property_name in('一般色纱深') then '深色'else null end
from #1 a
join YDMDB.dbo.ydScheduleMachineConstraint b with(nolock) on a.machine_id=b.machine_id
where b.property_name in('一般色纱浅','一般色纱深','漂白','特白')


update a set concentration=isnull(b.concentration,0),a.bill_no=c.bill_no
from  #1 a
join  LABDB..lbRecipeHdr b WITH(NOLOCK) on a.primary_batch_no=b.batch_no
left join labdb.dbo.lbBillOfStuffHdr c(nolock) on b.recipe_no=c.recipe_no
where b.is_cancel=0 and b.recipe_type_id=6 

update a set concentration=isnull(b.concentration,0),a.bill_no=c.bill_no
from  #1 a
join  HistoryData.dbo.lbRecipeHdrbk b WITH(NOLOCK) on a.primary_batch_no=b.batch_no
left join HistoryData.dbo.lbBillOfStuffHdrbk c(nolock) on b.recipe_no=c.recipe_no
where b.is_cancel=0 and b.recipe_type_id=6 

update a set concentration=isnull(b.concentration,0),a.bill_no=b.bill_no
from  #1 a
join (
select e.batch_no,b.concentration,e.bill_no
from LABDB..lbRecipeHdr b WITH(NOLOCK) 
join (
        select b.batch_no,b.recipe_no,b.bill_no
        from labdb.dbo.lbBillOfStuffHdr b(nolock)
        where b.is_cancel=0 and b.recipe_type ='正常色纱' and exists(select primary_batch_no from  #1 a where a.primary_batch_no=b.batch_no and a.concentration=0)
        union 
        select b.batch_no,b.recipe_no,b.bill_no
        from  HistoryData.dbo.lbBillOfStuffHdrbk b(nolock)
        where b.is_cancel=0 and b.recipe_type='正常色纱' and  exists(select primary_batch_no from  #1 a where a.primary_batch_no=b.batch_no and a.concentration=0)
      )e on e.recipe_no=b.recipe_no
where b.is_cancel=0 and b.recipe_type_id=6 
) b on a.primary_batch_no=b.batch_no
where a.concentration=0

update a set concentration=isnull(b.concentration,0),a.bill_no=b.bill_no
from  #1 a
join (
select e.batch_no,b.concentration,e.bill_no
from LABDB..lbRecipeHdr b WITH(NOLOCK) 
join (
        select b.batch_no,b.recipe_no,b.bill_no
        from labdb.dbo.lbBillOfStuffHdr b(nolock)
        where b.is_cancel=0 and b.recipe_type ='回修色纱' and exists(select primary_batch_no from  #1 a where a.primary_batch_no=b.batch_no and a.concentration=0)
        union 
        select b.batch_no,b.recipe_no,b.bill_no
        from  HistoryData.dbo.lbBillOfStuffHdrbk b(nolock)
        where b.is_cancel=0 and b.recipe_type ='回修色纱' and  exists(select primary_batch_no from  #1 a where a.primary_batch_no=b.batch_no and a.concentration=0)
      )e on e.recipe_no=b.recipe_no
where b.is_cancel=0 and b.recipe_type_id=9
) b on a.primary_batch_no=b.batch_no
where a.concentration=0 

update #1 set 色号深浅色=case when white='Yes'then '不固色'when color_code='整经杂边纱'then '不固色'when concentration<1 then '不固色' else '固色'end

--找同纱支色号生产过的是否有固色
Update d set GXJbatchno=a.batch_no
from #1 d          	
join labdb.dbo.lbBillOfStuffhdr a with(nolock)  on d.yarn_count+d.color_code=a.color_code
JOIN labdb.dbo.lbBillOfStuffDtl b with(nolock)ON a.Bill_NO=b.Bill_NO  
JOIN pubdb..pbChemicalList c with(nolock)ON b.Chemical_ID=c.Chemical_ID   
WHERE b.Chemical_ID in(921,22,668) and a.is_cancel=0 and isnull(d.white,'')<>'Yes'and d.color_code <>'TW001'and d.色号深浅色='不固色'
and d.color_code <>'整经杂边纱'and d.white<>'Yes'

--将原不固色的更新为固色的标记
update #1 set 色号深浅色='固色'
where 色号深浅色='不固色'and isnull(GXJbatchno,'')<>''

--色号为黄色的改为不固色
UPDATE #1 SET 色号深浅色='不固色'
WHERE color_code LIKE'%YW%'

UPDATE a SET 类型='补染'
FROM #t2 a
JOIN ydmdb.dbo.uvw_batchinfo b ON b.batch_no=a.缸号
WHERE b.job_no LIKE 'BR%'

UPDATE a SET 类型='补做'
FROM #t2 a
JOIN ydmdb.dbo.uvw_batchinfo b ON b.batch_no=a.缸号
WHERE b.job_no LIKE 'BZ%'



SELECT DISTINCT MIN(外发)外发,类型,type,状态,染纱交期,机型,轴数,头缸=CASE WHEN 备注 LIKE'%留料%'AND 备注 LIKE'%第1缸%'THEN 'Yes'ELSE NULL END,
缸号,排缸,色号,整经OK,轴型,MIN(拉装轴)拉装轴,超前整,QR单,重量,固色=b.色号深浅色,理论重,
满缸率=CASE WHEN 理论重=0 THEN 0 ELSE ROUND(重量*100/理论重,1) END,排整日期,status,打办轮次,要求给板,复办OK,备注
INTO #result
FROM #t2 a
LEFT JOIN (
             SELECT DISTINCT batch_no,色号深浅色
             FROM #1 a
           )b ON a.缸号=b.batch_no

GROUP BY type,类型,状态,机型,轴数,缸号,排缸,轴型,排整日期,色号,整经OK,超前整,QR单,重量,理论重,排整日期,status,打办轮次,复办OK,染纱交期,要求给板,备注,色号深浅色,
CASE WHEN 备注 LIKE'%留料%'AND 备注 LIKE'%第1缸%'THEN 'Yes'ELSE NULL END

ORDER BY type,机型,整经OK



--add by liyoq use to app  

IF @machinetype<>'' 
SELECT 机型 AS  machinetype,缸号 AS batchno,轴数 AS nums, 排缸 AS plantime   
FROM #result WHERE ISNULL(排缸,'')<>''  AND 机型= @machinetype
ORDER BY 排缸 ASC 
ELSE 
SELECT 机型 AS  machinetype,缸号 AS batchno,轴数 AS nums, 排缸 AS plantime   
FROM #result WHERE ISNULL(排缸,'')<>''
ORDER BY 排缸 ASC 

DROP TABLE #result

DROP TABLE #t1,#t2,#t03,#1
DROP TABLE #machinemodel,#mach,#t11


END
GO



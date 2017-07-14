SELECT TOP 10 Iden ,
              HL_No ,
              Name ,
              Class ,
              Post ,
              Sys_Score ,
              Dync_Score ,
              ProValue ,
              InputTime ,
              ModifyName ,
              ModifyTime ,
              Remark ,
              IsLargeType ,
              IsMore ,
              IsCalico from PDMDB.dbo.hlOutput    where post='穿综'
select TOP 10 * from PDMDB.dbo.hlProductionStaff  where Post='穿综'   --人员.
-- grade_cent分数
select top 10 * from hlbasicinfo where hl_no = 'HL2017-20134N' 
-- HL2017-88888N

select top 10 * from PDMDB.dbo.hlOutput where HL_No = 'Hl2017-88888N' 

 if (GetBatchType(ABatchNO)=BATCH_TYPE_GEW) or
      (GetBatchType(ABatchNO)= BATCH_TYPE_NY) then
  -- 根据缸号，读取信息    
  select * from uvw_BatchInfo where Batch_No='TC100270'
  
 BATCH_TYPE_GEK
   exec usp_ydWmisGetBatchInfoByBatchNo 'TC100270'
   
   
     if GetBatchType(ABatchNO)=BATCH_TYPE_OLD_GEW then
   select * from uvw_OldBatchInfo with(nolock)  where Batch_No='TC100270' 



   --字段
        edtYarn_Type.Text := FieldByName('Yarn_Type').AsString;
        edtYarn_Count.Text := FieldByName('Yarn_Count').AsString;
        edtYarn_Lot.Text := FieldByName('Yarn_Lot').AsString;
        edtColor_Code.Text := FieldByName('Color_Code').AsString;
        if ((Copy(trim(medtBatchNO.Text), 1, 1) = 'Z') or (Copy(trim(medtBatchNO.Text), 1, 1) = 'B')) then
        begin
          edtOutput.Text := FieldByName('Dye_Beam_Num').AsString;
          edtCone_Num.Text := FieldByName('Dye_Beam_Num').AsString;
          edtUnit_Weight.Text := FieldByName('Trim_Beam_Unit_Weight').AsString;
        end else
        begin
          edtOutput.Text := FieldByName('Cone_Num').AsString;
          edtCone_Num.Text := FieldByName('Cone_Num').AsString;
          edtUnit_Weight.Text := FieldByName('Cone_Unit_Weight').AsString;
        end;
        mmoRemark.Text := FieldByName('Remark').AsString;
        
  
    sqlText := 'select Iden, Worker_ID, Worker_Name, Password, Class, Worker_Group, Department, Is_Active'
           + '       from pubdb.dbo.pbWorkerList'
           + '       where Department like ''' + ADepartmentName[1] +'%'''
           + '       order by   Worker_ID'; 
           
         select Iden, Worker_ID, Worker_Name, Password, Class, Worker_Group, Department, Is_Active
             from pubdb.dbo.pbWorkerList
             
                where worker_ID LIKE 'D%'  Worker_Group='装笼'  Department like 'R%' 
                
           
              select *from  pubdb.dbo.pbTableFieldList
              
                 select Iden, Worker_ID, Worker_Name, Password, Class, Worker_Group, Department, Is_Active
             from pubdb.dbo.pbWorkerList

			 IWorkerInfo.GetWorkListInfoByDepartment('RT');
  IWorkerInfo.DataSet.Filtered := False;
  if Login.CurrentDepartment = 'RT' then
    IWorkerInfo.DataSet.Filter := 'Department=' + Quotedstr(Login.CurrentDepartment) + ' AND NOT (Worker_ID LIKE ''D%'' OR Worker_ID LIKE ''N%'')';
  if Login.CurrentDepartment = 'R2' then
    IWorkerInfo.DataSet.Filter := 'Worker_ID LIKE ''N%''';
  if Login.CurrentDepartment = 'R3' then
    IWorkerInfo.DataSet.Filter := 'Department=' + Quotedstr(Login.CurrentDepartment) + ' AND Worker_ID LIKE ''D%''';
  IWorkerInfo.DataSet.Filtered := True; 


 -- 根据缸号，读取信息    
 select * from uvw_BatchInfo where Batch_No='TC100270'
  --需要写入的表格
select *  from ydmdb.dbo.rtProduction where Type='装笼' and Input_Time >'2015-1-1' 

SELECT TOP 10 * FROM ydBatchTrace ORDER BY Iden DESC--WHERE Batch_NO='TC100270' 

select *  from ydmdb.dbo.rtProduction where Type='装笼' and Input_Time >'2017-01-01' 

UPDATE ydmdb.dbo.rtProduction SET Sarong_No='FA01' WHERE Type='装笼' and Input_Time >'2017-01-01' 

SELECT TOP 10 * FROM ydmdb.dbo.rtProduction WHERE Sarong_No='FA01'ORDER BY Iden DESC-- Batch_NO='CC404377'--Worker_ID='GET0286547'

DELETE  ydmdb.dbo.rtProduction WHERE Batch_NO='CC302695'--Worker_ID='CC302744'

UPDATE dbo.prdAppSarong SET IsUsed='是' WHERE SarongNo='FA01'

SELECT * FROM dbo.prdAppSarong



EXEC dbo.usp_prdAppGetSarongStatusByJarType



SET TERM ^ ;

create or alter procedure CNV$CC_LCHARSDUPLICATION (
    ACTIONTYPE smallint)
returns (
    ID integer,
    LSHET varchar(10),
    LCHARCD integer,
    LCHARNAME varchar(100),
    VALUE_ integer,
    VALUEDESC varchar(100),
    DATE_ timestamp)
as
declare variable CURID integer;
declare variable CURLSHET varchar(10);
declare variable CURLCHARCD integer;
declare variable CURLCHARNAME varchar(100);
declare variable CURVALUE integer;
declare variable CURVALUEDESC varchar(100);
declare variable CURDATE timestamp;
declare variable OLDLSHET varchar(10) = '';
declare variable OLDLCHARCD integer = 0;
declare variable OLDVALUE integer = 0;
begin
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1) then
  begin
    for select ID, LSHET, LCHARCD, LCHARNAME, VALUE_, VALUEDESC, DATE_
        from CNV$LCHARS
        order by LSHET, :LCHARCD, DATE_
        into :CURID, :CURLSHET, :CURLCHARCD, :CURLCHARNAME, :CURVALUE, :CURVALUEDESC, :CURDATE
    do
    begin
      if (CURLSHET = OLDLSHET and
          CURLCHARCD = OLDLCHARCD and
          CURVALUE = OLDVALUE) then
      begin
        ID = :CURID;
        LSHET = :CURLSHET;
        LCHARCD = :CURLCHARCD;
        LCHARNAME = :CURLCHARNAME;
        VALUE_ = :CURVALUE;
        VALUEDESC = :CURVALUEDESC;
        DATE_ = :CURDATE;
        suspend;
      end
      else
      begin
        OLDLSHET = :CURLSHET;
        OLDLCHARCD = :CURLCHARCD;
        OLDVALUE = :CURVALUE;
      end
    end
  end
  else
  if (ACTIONTYPE = 2) then
  begin
    delete from CNV$LCHARS
    where ID in (select ID
                 from CNV$CC_LCHARSDUPLICATION(1));
  end
  else
    exception CNV$WRONG_PARAMATER_VALUE 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
end^

SET TERM ; ^

GRANT DELETE ON CNV$LCHARS TO PROCEDURE CNV$CC_LCHARSDUPLICATION;

GRANT EXECUTE ON PROCEDURE CNV$CC_LCHARSDUPLICATION TO PROCEDURE CNV$CC_LCHARSDUPLICATION;

GRANT EXECUTE ON PROCEDURE CNV$CC_LCHARSDUPLICATION TO SYSDBA;
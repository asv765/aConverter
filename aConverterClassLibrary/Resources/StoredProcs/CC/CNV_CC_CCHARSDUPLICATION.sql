SET TERM ^ ;

create or alter procedure CNV$CC_CCHARSDUPLICATION (
    ACTIONTYPE smallint)
returns (
    ID integer,
    LSHET varchar(10),
    CHARCD integer,
    CHARNAME varchar(50),
    VALUE_ numeric(11,4),
    DATE_ timestamp)
as
declare variable CURID integer;
declare variable CURLSHET varchar(10);
declare variable CURCHARCD integer;
declare variable CURCHARNAME varchar(50);
declare variable CURVALUE numeric(11,4);
declare variable CURDATE timestamp;
declare variable OLDLSHET varchar(10) = '';
declare variable OLDCHARCD integer = 0;
declare variable OLDVALUE numeric(11,4) = 0;
begin
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1) then
  begin
    for select ID, LSHET, CHARCD, CHARNAME, VALUE_, DATE_
        from CNV$CHARS
        order by LSHET, CHARCD, DATE_
        into :CURID, :CURLSHET, :CURCHARCD, :CURCHARNAME, :CURVALUE, :CURDATE
    do
    begin
      if (CURLSHET = OLDLSHET and
          CURCHARCD = OLDCHARCD and
          CURVALUE = OLDVALUE) then
      begin
        ID = :CURID;
        LSHET = :CURLSHET;
        CHARCD = :CURCHARCD;
        CHARNAME = :CURCHARNAME;
        VALUE_ = :CURVALUE;
        DATE_ = :CURDATE;
        suspend;
      end
      else
      begin
        OLDLSHET = :CURLSHET;
        OLDCHARCD = :CURCHARCD;
        OLDVALUE = :CURVALUE;
      end
    end
  end
  else
  if (ACTIONTYPE = 2) then
  begin
    delete from cnv$chars where id in (select id from CNV$CC_CCHARSDUPLICATION(1));
  end
  else
    exception CNV$WRONG_PARAMATER_VALUE 'Значение ACTIONTYPE отличное от 0, 1 или 2 не поддерживается процедурой';
end^

SET TERM ; ^

GRANT DELETE ON CNV$CHARS TO PROCEDURE CNV$CC_CCHARSDUPLICATION;

GRANT EXECUTE ON PROCEDURE CNV$CC_CCHARSDUPLICATION TO PROCEDURE CNV$CC_CCHARSDUPLICATION;

GRANT EXECUTE ON PROCEDURE CNV$CC_CCHARSDUPLICATION TO SYSDBA;
SET TERM ^ ;

create or alter procedure CNV$CC_NOTUNIQUELSHET (
    ACTIONTYPE smallint = 0)
returns (
    LSHET varchar(10),
    COUNT_ integer)
as
begin
  /* ������������, ����������� */
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1 ) then begin
     for select lshet, count(*) as count_ from cnv$abonent group by lshet having count(*) > 1
         into :LSHET, :COUNT_
     do begin
         suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value '�������� ACTIONTYPE �������� �� 0 ��� 1 �� �������������� ����������';
end^

SET TERM ; ^

GRANT SELECT ON CNV$ABONENT TO PROCEDURE CNV$CC_NOTUNIQUELSHET;

GRANT EXECUTE ON PROCEDURE CNV$CC_NOTUNIQUELSHET TO SYSDBA;
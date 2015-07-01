create procedure CNV$CC_LSHETLENGTH (
    ACTIONTYPE smallint = 0)
returns (
    LSHETLENGTH integer,
    COUNT_ integer)
as
begin
  /* ������������, ����������� */
  if (ACTIONTYPE = 0 or ACTIONTYPE = 1 ) then begin
     for SELECT CHAR_LENGTH(LSHET) AS LSHETLENGTH, COUNT(*) AS COUNT_
        FROM CNV$ABONENT
        GROUP BY CHAR_LENGTH(LSHET)
        ORDER BY CHAR_LENGTH(LSHET)
        into :LSHETLENGTH, :COUNT_
     do begin
        suspend;
     end
  end
  else
     EXCEPTION cnv$wrong_paramater_value '�������� ACTIONTYPE �������� �� 0 ��� 1 �� �������������� ����������';
end

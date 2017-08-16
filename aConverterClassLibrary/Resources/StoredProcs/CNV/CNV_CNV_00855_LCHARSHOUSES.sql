SET TERM ^ ;

create or alter procedure CNV$CNV_00855_LCHARSHOUSES (
    NEEDDELETE smallint = 0,
	GENCHANGEDOC smallint = 1)
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ integer;
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from lcharshouselist
    where HOUSECD in (select HOUSECD
                      from cnv$LCHARHOUSES);
    delete from DOCUMENTS
    where DOCUMENTCD in (select CCAL.DOCUMENTCD
                         from lcharshouselist CCAL
                         inner join cnv$LCHARHOUSES CC on CCAL.HOUSECD = CC.HOUSECD);
  end
  for select HOUSECD, LCHARCD, VALUE_, DATE_
      from cnv$LCHARHOUSES
      order by HOUSECD, LCHARCD, DATE_
      into :HOUSECD, :CHARCD, :VALUE_, :DATE_
  do
  begin
	if (GENCHANGEDOC = 1) then
	begin
      DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
      insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
      values (:DOCUMENTCD, 1, 1, 'Импорт качественных характеристик домов', :DATE_);
	end
	else DOCUMENTCD = null;
      insert into lcharshouselist (HOUSECD, KOD, HOUSELCHARDATE, DOCUMENTCD, SIGNIFICANCE, ISACTIVE)
      values (:HOUSECD, :CHARCD, :DATE_, :DOCUMENTCD, :VALUE_, 1);
  end

end^

SET TERM ; ^
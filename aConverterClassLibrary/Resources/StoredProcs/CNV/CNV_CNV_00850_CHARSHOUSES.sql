SET TERM ^ ;

create or alter procedure CNV$CNV_00850_CHARSHOUSES (
    NEEDDELETE smallint = 0,
	GENCHANGEDOC smallint = 1)
as
declare variable HOUSECD integer;
declare variable CHARCD integer;
declare variable VALUE_ numeric(11,4);
declare variable DATE_ timestamp;
declare variable DOCUMENTCD integer;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from CCHARSHOUSELIST
    where HOUSECD in (select HOUSECD
                      from CNV$CHARSHOUSES);
    delete from DOCUMENTS
    where DOCUMENTCD in (select CCAL.DOCUMENTCD
                         from CCHARSHOUSELIST CCAL
                         inner join CNV$CHARSHOUSES CC on CCAL.HOUSECD = CC.HOUSECD);
  end
  
  if (GENCHANGEDOC = 2) then
  begin
    DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
    values (:DOCUMENTCD, 1, 1, 'Импорт количественных характеристик домов', :DATE_);
  end
  else
	DOCUMENTCD = null;

  for select HOUSECD, CHARCD, VALUE_, DATE_
      from CNV$CHARSHOUSES
      order by HOUSECD, CHARCD, DATE_
      into :HOUSECD, :CHARCD, :VALUE_, :DATE_
  do
  begin
    if (:CHARCD > 0) then
    begin
		if (GENCHANGEDOC = 1) then
		begin
		  DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
		  insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
		  values (:DOCUMENTCD, 1, 1, 'Импорт количественных характеристик домов', :DATE_);
		end
		insert into CCHARSHOUSELIST (HOUSECD, KOD, HOUSECCHARDATE, DOCUMENTCD, SIGNIFICANCE, ISACTUAL)
		values (:HOUSECD, :CHARCD, :DATE_, :DOCUMENTCD, :VALUE_, 1);
    end
    else
    begin
      if (:CHARCD = -1) then
          if (:value_ = 0) then
            update HOUSES H
            set H.BUILDINGYEAR = null
            where H.HOUSECD = :HOUSECD;
          else
            update HOUSES H
            set H.BUILDINGYEAR = :value_
            where H.HOUSECD = :HOUSECD;
      else if (:CHARCD = -2) then
        update HOUSES H
        set H.FLOORCOUNT = :VALUE_
        where H.HOUSECD = :HOUSECD;
      else if (:CHARCD = -3) then
        update HOUSES H
        set H.ENTRANCCOUNT = :VALUE_
        where H.HOUSECD = :HOUSECD;
	  else if (:CHARCD = -4) then
        update HOUSES H
        set H.ISMANYKVART = :VALUE_
        where H.HOUSECD = :HOUSECD;
	  else if (:CHARCD = -5) then
		update HOUSES H
		set H.NUMBEROFKVART = :VALUE_
		where H.HOUSECD = :HOUSECD;
    end
  end

end^

SET TERM ; ^
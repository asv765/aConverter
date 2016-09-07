SET TERM ^ ;
create or alter procedure CNV$CNV_03100_TVER_ABONENTDOLYA
as
declare variable CURHOUSECD integer;
declare variable PREHOUSECD integer;
declare variable ABONENTSQUARE numeric(11,4);
declare variable HOUSETOTALSQUARE numeric(11,4);
declare variable HOUSEHEATINGSQUARE numeric(11,4);
declare variable DOCUMENTCD integer;
declare variable DATE_ timestamp;
declare variable LSHET varchar(10);
declare variable CLOSEDATE timestamp;
begin
  for select A.LSHET, C.VALUE_, CH1.VALUE_, CH1.HOUSECD, C.DATE_, A.CLOSEDATE
      from CNV$ABONENT A
      inner join CNV$CHARS C on C.LSHET = A.LSHET and
            C.CHARCD = 2
      inner join CNV$CHARSHOUSES CH1 on CH1.HOUSECD = A.HOUSECD and
            CH1.CHARCD = 32010
      into :LSHET, :ABONENTSQUARE, :HOUSEHEATINGSQUARE, :CURHOUSECD, :DATE_, :CLOSEDATE
  do
  begin
    if (CURHOUSECD <> PREHOUSECD) then
    begin
      HOUSETOTALSQUARE = (select sum(CA.VALUE_)
                          from CNV$CHARS CA
                          where CA.CHARCD = 2 and
                                CA.LSHET in (select LSHET
                                             from CNV$ABONENT
                                             where HOUSECD = :CURHOUSECD));
    end
    PREHOUSECD = CURHOUSECD;
    if (HOUSETOTALSQUARE <> 0) then
    begin
	  if (DATE_ <> CLOSEDATE) then
	  begin
		DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
		insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
		values (:DOCUMENTCD, 1, 1, 'Конвертация доли абонента', :DATE_);
		insert into CCHARSABONENTLIST (LSHET, KODCCHARSLIST, ABONENTCCHARDATE, DOCUMENTCD, SIGNIFICANCE)
		values (:LSHET, 150, :DATE_, :DOCUMENTCD, :ABONENTSQUARE * :HOUSEHEATINGSQUARE / :HOUSETOTALSQUARE);
	  end
    end
  end
end^
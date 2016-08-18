SET TERM ^ ;
create or alter procedure CNV$CNV_03100_TVER_ABONENTDOLYA
as
declare variable ABONENTSQUARE numeric(11,4);
declare variable HOUSETOTALSQUARE numeric(11,4);
declare variable HOUSEHEATINGSQUARE numeric(11,4);
declare variable DOCUMENTCD integer;
declare variable DATE_ timestamp;
declare variable LSHET varchar(10);
begin
  for select A.LSHET, C.VALUE_, CH1.VALUE_, CH2.VALUE_, C.DATE_
      from CNV$ABONENT A
      inner join CNV$CHARS C on C.LSHET = A.LSHET and
            C.CHARCD = 2
      inner join CNV$CHARSHOUSES CH1 on CH1.HOUSECD = A.HOUSECD and
            CH1.CHARCD = 32010
      inner join CNV$CHARSHOUSES CH2 on CH2.HOUSECD = A.HOUSECD and
            CH2.CHARCD = 206001
      into :LSHET, :ABONENTSQUARE, :HOUSEHEATINGSQUARE, :HOUSETOTALSQUARE, :DATE_
  do
  begin
    DOCUMENTCD = gen_id(DOCUMENTS_GEN, 1);
    insert into DOCUMENTS (DOCUMENTCD, REGISTERUSERCD, OTVETSTVUSERCD, DOCNAME, FACTDOCUMENTDATE)
    values (:DOCUMENTCD, 1, 1, 'Конвертация доли абонента', :DATE_);
    insert into CCHARSABONENTLIST (LSHET, KODCCHARSLIST, ABONENTCCHARDATE, DOCUMENTCD, SIGNIFICANCE)
    values (:LSHET, 150, :DATE_, :DOCUMENTCD, :ABONENTSQUARE * :HOUSEHEATINGSQUARE / :HOUSETOTALSQUARE);
  end
end^
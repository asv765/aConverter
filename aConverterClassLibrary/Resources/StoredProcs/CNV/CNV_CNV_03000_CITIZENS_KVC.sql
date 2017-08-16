SET TERM ^ ;

create or alter procedure CNV$CNV_03000_CITIZENS_KVC (
    NEEDDELETE smallint)
as
declare variable LSHET varchar(10) = null;
declare variable F varchar(50) = null;
declare variable O varchar(50) = null;
declare variable I varchar(50) = null;
declare variable ISMAIN integer = null;
declare variable SOBSTAT integer = null;
declare variable CITSTAT integer = null;
declare variable DOLYA integer = null;
declare variable C_HIDDEN integer = null;
declare variable C_OSHIPNUM integer = null;
declare variable C_OSHIPDEN integer = null;
declare variable C_OSHIPTYPE integer = null;
declare variable C_REGTYPE integer = null;
declare variable id integer = null;
begin
  if (NEEDDELETE = 1) then
  begin
    delete from CITYZENS
    where LSHET in (select LSHET
                    from CNV$CITIZENS);
  end
  for select ID, LSHET, F, I, O, HOZ, SOB, KOLLG, DOLYA
      from CNV$CITIZENS
      order by LSHET
      into :ID, :LSHET, :F, :I, :O, :ISMAIN, :SOBSTAT, :CITSTAT, :DOLYA
  do
  begin
	  C_HIDDEN = 0;
	  C_OSHIPNUM = null;
	  C_OSHIPDEN = null;
	  C_OSHIPTYPE = null;
	  C_REGTYPE = null;

	  if (:CITSTAT = 9) then
		C_HIDDEN = 1;
	  if (:SOBSTAT = 1) then
	  begin
		C_OSHIPNUM = 1;
		C_OSHIPDEN = :DOLYA;
		C_OSHIPTYPE = 2;
	  end
	  if (:CITSTAT = 1 or :CITSTAT = 2) then
		C_REGTYPE = 1;

	  insert into CITYZENS (CITYZEN_ID, LSHET, ISMAINCITYZEN, CTZFIO, CTZNAME, CTZPARENTNAME, HIDDEN, OWNERSHIPNUMERATOR,
							OWNERSHIPDENOMINATOR, OWNERSHIPTYPE, CITIZENSTATEID, REGISTRATIONTYPE)
	  values (:ID, :LSHET, :ISMAIN, :F, :I, :O, :C_HIDDEN, :C_OSHIPNUM, :C_OSHIPDEN, :C_OSHIPTYPE, :SOBSTAT, :C_REGTYPE);
  end
end^

SET TERM ; ^
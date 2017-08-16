SET TERM ^ ;

create or alter procedure CNV$CNV_01810_CITIZENRELATIONS
as
declare variable CITIZENIDFROM integer;
declare variable CITIZENIDTO integer;
declare variable RELATIONID integer = null;
begin
    for select c1.cityzen_id, c2.cityzen_id, cr.relationid
    from cnv$citizenrelations cr
    inner join cityzens c1 on c1.uniquecityzenid = cast(cr.citizenidfrom as varchar(45))
    inner join cityzens c2 on c2.uniquecityzenid = cast(cr.citizenidto as varchar(45))
    order by cr.citizenidfrom
    into :citizenidfrom, :citizenidto, :relationid
    do begin
        insert into relatives(RELATIVE1, RELATIVE2, RELATIONID)
        values (:citizenidfrom, :citizenidto, :relationid);
    end
end^

SET TERM ; ^
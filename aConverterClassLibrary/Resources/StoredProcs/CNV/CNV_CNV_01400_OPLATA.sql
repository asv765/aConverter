SET TERM ^ ;

create or alter procedure CNV$CNV_01400_OPLATA
as
declare variable LSHET varchar(10);
declare variable DOCUMENTCD varchar(20);
declare variable MONTH_ integer;
declare variable YEAR_ integer;
declare variable SUMMA numeric(18,2);
declare variable DATE_ timestamp;
declare variable DATE_VV timestamp;
declare variable SOURCECD integer;
declare variable SERVICECD integer;
declare variable DVDAY integer;
declare variable DVMONTH integer;
declare variable DVYEAR integer;
declare variable DDAY integer;
declare variable DMONTH integer;
declare variable DYEAR integer;
declare variable OLDDATE_VV timestamp;
declare variable OLDSOURCECD integer;
declare variable OLDLSHET varchar(10);
declare variable PDCD integer;
declare variable PDKEY varchar(50);
declare variable PSCASEID integer;
declare variable BASEORG integer;
begin
    olddate_vv = date '1-Jan-1990';
    oldsourcecd = -1;
    oldlshet = '-1';
    SELECT extorgcd FROM extorgspr eos WHERE eos.isbaseorganization = 1 INTO :baseorg;
    for select lshet, documentcd, month_, year_, summa, date_, date_vv, sourcecd, servicecd,
            extract(day from date_vv) as dvday, extract(month from date_vv) as dvmonth, extract(year from date_vv) as dvyear,
            extract(day from date_) as dday, extract(month from date_) as dmonth, extract(year from date_) as dyear
        from cnv$oplata
        order by date_vv, sourcecd, lshet, servicecd
        into :lshet, :documentcd, :month_, :year_, :summa, :date_, :date_vv, :sourcecd, :servicecd, 
			:dvday, :dvmonth, :dvyear,
            :dday, :dmonth, :dyear
    do begin
       if ((:olddate_vv <> :date_vv) or (:oldsourcecd <> :sourcecd)) then begin
          pdkey = 'PD_' || cast(:date_vv as DATE) || '_' || :sourcecd;
          select documentcd
              from cnv$cnv_documentnumerator(:pdkey, 'Пачка квитанций, сформирована при импорте оплаты', :date_vv, :date_vv, :baseorg)
              into :pdcd;
          update or insert into primarydoc
               (primarydoccd, sourcedoccd, discountdateday, discountdatemonth, discountdateyear, savedtodb, savedwithwarnings)
             values
               (:pdcd, :sourcecd, :dvday, :dvmonth, :dvyear, 1, 0);
          olddate_vv = :date_vv;
          oldsourcecd = :sourcecd;
          oldlshet = '-1';
       end
       if (:oldlshet <> :lshet) then begin
          insert into PAYTICKET (TICKETCD, PRIMARYDOCCD, USERCD, ABNLSHET, PAYDATE, DISCOUNTDATE, IMPORTDATE, LASTEDITDATE)
             values (gen_id(PAYTICKET_G,1), :pdcd, 1, :lshet, :date_, :date_vv, current_timestamp, current_timestamp);
          oldlshet = :lshet;
       end
       insert into PAYFACT (PAYFACTCD, TICKETCD, GAZSERVICECD, PAYPENI, PAYYEAR, PAYMONTH, PAYSUM, DISCOUNTRESOURCECD)
          values (gen_id(PAYFACT_G,1), gen_id(PAYTICKET_G,0), :servicecd, 0, :YEAR_, :MONTH_, :SUMMA, -1);
       select documentcd from cnv$cnv_documentnumerator(:DOCUMENTCD, 'Импорт данных об оплате', :DATE_, :DATE_VV, :baseorg) into :PSCASEID;
       insert into PaySumma (PayFactCD, TicketCD, PrimaryDocCD, Balance_Kod, PayType, LShet, PaySum, PayMonth, PayYear, PayDateDay, PayDateMonth, PayDateYear, UserCD, Delta, Caseid)
          values (gen_id(PAYFACT_G,0), gen_id(PAYTICKET_G,0), :pdcd, :SERVICECD, 0, :LSHET, :SUMMA, :MONTH_, :YEAR_, :DDAY, :DMONTH, :DYEAR, 1, 0, :PSCASEID);
    end
    update primarydoc set paytotal = (select sum(pf.paysum) from payticket pt inner join payfact pf on pt.ticketcd = pf.ticketcd where pt.primarydoccd = primarydoc.primarydoccd);
    update primarydoc set ticketscount = (select count(pt.ticketcd) from payticket pt inner join payfact pf on pt.ticketcd = pf.ticketcd where pt.primarydoccd = primarydoc.primarydoccd);
    update payfact set DISCOUNTRESOURCECD = (select balanceslist.kod from balanceslist where balanceslist.balance_kod = payfact.gazservicecd) where (payfact.gazservicecd is not null) and (payfact.gazservicecd <> 102);

end^

SET TERM ; ^

GRANT SELECT ON EXTORGSPR TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT ON CNV$OPLATA TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT EXECUTE ON PROCEDURE CNV$CNV_DOCUMENTNUMERATOR TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT,UPDATE ON PRIMARYDOC TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT ON PAYTICKET TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT,INSERT,UPDATE ON PAYFACT TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT INSERT ON PAYSUMMA TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT SELECT ON BALANCESLIST TO PROCEDURE CNV$CNV_01400_OPLATA;

GRANT EXECUTE ON PROCEDURE CNV$CNV_01400_OPLATA TO SYSDBA;
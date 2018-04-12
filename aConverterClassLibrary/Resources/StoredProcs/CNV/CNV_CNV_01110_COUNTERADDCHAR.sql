SET TERM ^ ;

create or alter procedure CNV$CNV_01110_COUNTERADDCHAR
as
begin
	merge into equipmentadditionalchars ad
	using (
		select ca.*, pe.EQUIPMENTID
		from cnv$counteraddchar ca
		inner join PARENTEQUIPMENT pe on pe.IMPORTTAG = ca.COUNTERID
	) ca on ca.EQUIPMENTID = ad.equipmentid and ca.addcharcd = ad.additionalcharcd
	when matched then
		update set ad.significance = ca.value_
	when not matched then
		insert(additionalcharcd, equipmentid, significance)
			values(ca.addcharcd, ca.EQUIPMENTID, ca.value_);
end^

SET TERM ; ^
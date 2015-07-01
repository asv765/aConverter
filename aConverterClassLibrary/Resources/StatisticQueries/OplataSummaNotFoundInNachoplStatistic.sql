select * from cnv$oplata o
where o.summa <> 0 and not exists
      (select lshet from cnv$nachopl n
       where n.lshet = o.lshet and
             n.servicecd = o.servicecd and
             n.year_ = o.year_ and
             n.month_ = o.month_)
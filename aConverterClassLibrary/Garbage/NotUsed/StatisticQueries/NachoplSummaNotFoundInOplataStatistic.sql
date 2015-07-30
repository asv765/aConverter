select * from cnv$nachopl n
where n.oplata <> 0 and not exists
      (select lshet from cnv$oplata o
       where n.lshet = o.lshet and
             n.servicecd = o.servicecd and
             n.year_ = o.year_ and
             n.month_ = o.month_)
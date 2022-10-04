select * from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
;-- -. . -..- - / . -. - .-. -.--
select avg(d.valueNum),* from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
;-- -. . -..- - / . -. - .-. -.--
select avg(d.valueNum) from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
;-- -. . -..- - / . -. - .-. -.--
select avg(d.valueNum) from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
group by d.TimeStamp
;-- -. . -..- - / . -. - .-. -.--
select count(*) from dbo.detail

select avg(d.valueNum) from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
group by date(d.TimeStamp)
;-- -. . -..- - / . -. - .-. -.--
select avg(d.valueNum) from dbo.detail d
where d.TimeStamp > '2022-01-12'
    and d.ValueNum <> -1
group by cast(d.TimeStamp as date)
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) dato, avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date)
;-- -. . -..- - / . -. - .-. -.--
select
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by DATEPART(day, [d.TimeStamp]), DATEPART(hour, [d.timestamp]
;-- -. . -..- - / . -. - .-. -.--
select
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by DATEPART(day, d.TimeStamp), DATEPART(hour, d.timestamp)
;-- -. . -..- - / . -. - .-. -.--
select d.TimeStamp,
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by DATEPART(day, d.TimeStamp), DATEPART(hour, d.timestamp)
;-- -. . -..- - / . -. - .-. -.--
select DATEPART(day, d.TimeStamp), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by DATEPART(day, d.TimeStamp), DATEPART(hour, d.timestamp)
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, month, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.* from cte_avarage_pr_day c
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, month, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select sum(c.average) from cte_avarage_pr_day c
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select sum(c.average) from cte_avarage_pr_day c
group by c.date, c.hour
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select sum(c.average) from cte_avarage_pr_day c
group by c.date
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt,
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  snitt
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average) from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_day from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1 desc,2
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour , d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour , d.timestamp),DATEPART(minute , d.TimeStamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp),DATEPART(minute , d.TimeStamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) ,
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 desc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_day, sum(c.count)
from cte_avarage_pr_day c
group by c.date
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1 --and cast(d.TimeStamp as date) = cast(getdate() as date)
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_day, sum(c.count)
from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 desc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
order by 1 desc,2 asc
;-- -. . -..- - / . -. - .-. -.--
alter table detail
    add ObisCodeId TINYINT NULL
;-- -. . -..- - / . -. - .-. -.--
select top 10 d.*
from detail d
order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ValueNum <> -1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_day, sum(c.count)
from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCode = 1
        and d.TimeStamp > '2022-04-02'
    group by cast(d.TimeStamp as date), DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count)
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_day(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCode = 1
    group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_day, sum(c.count)
from cte_avarage_pr_day c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
select * from detail d where d.Timestamp > '2022-04-02'
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select cast(d.TimeStamp as date), DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02'
    group by cast(d.TimeStamp as date), DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count)
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select d.TimeStamp, DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count)
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select d.TimeStamp, DATEPART(hour, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select d.TimeStamp, DATEPART(minute, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, hour, average, count)
as (
     select d.TimeStamp, DATEPART(minute, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02 08:00:00'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.date, sum(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.date
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, min, average, count)
as (
     select d.TimeStamp, DATEPART(minute, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02 08:00:00'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.min, sum(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.min
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, min, average, count)
as (
     select d.TimeStamp, DATEPART(minute, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02 08:00:00'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.min, average.(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.min
order by 1
;-- -. . -..- - / . -. - .-. -.--
with cte_avarage_pr_min(date, min, average, count)
as (
     select d.TimeStamp, DATEPART(minute, d.timestamp),
           avg(d.valueNum)  average, count(*)
     from dbo.detail d
    where d.ObisCodeId = 1
        and d.TimeStamp > '2022-04-02 08:00:00'
    group by d.TimeStamp, DATEPART(minute, d.timestamp)
    )
select c.min, avg(c.average)  power_used_pr_min, sum(c.count) Count
from cte_avarage_pr_min c
group by c.min
order by 1
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*)
from dbo.detail d
where d.ValueNum <> -1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ValueNum <> -1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select d.Location, cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ValueNum <> -1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by d.Location, cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select d.Location, cast(d.TimeStamp as date) date, DATEPART(hour , d.timestamp) hour,DATEPART(minute , d.TimeStamp) min,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by d.Location, cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
SELECT DATETIMEFROMPARTS(2019,1,1,6,0,0,0)
;-- -. . -..- - / . -. - .-. -.--
SELECT DATETIMEFROMPARTS(2019,1,1,6,10,2,0)
;-- -. . -..- - / . -. - .-. -.--
SELECT DATETIMEFROMPARTS(2019,1,1,6,10,0,0
;-- -. . -..- - / . -. - .-. -.--
SELECT DATETIMEFROMPARTS(2019,1,1,6,10,0,0)
;-- -. . -..- - / . -. - .-. -.--
select d.Location, DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) date,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by d.Location, cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select d.Location, DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) date,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by d.Location, 2 --cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select d.Location, DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) date,
       avg(d.valueNum)  snitt, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by
    d.Location,
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
alter table minute add Count smallint null
;-- -. . -..- - / . -. - .-. -.--
insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by
    d.Location,
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate() as date)
group by
    d.Location,
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
from dbo.detail d
where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(getdate()-1 as date)
group by
    d.Location,
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare startDate as DateTime

set startDate = (select min(timestamp) from detail)
print startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime

set @startDate = (select min(timestamp) from detail)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
set @startDate = (select min(timestamp) from detail)
print @startDate

while @startDate < getdate()
begin
    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime

set @startDate = (select min(timestamp) from detail)
print @startDate

while @startDate < getdate()
begin
    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime

set @startDate = (select min(timestamp) from detail)
set @stopDate = @startDate + 2

while @startDate < @stopDate--getdate()-1
begin
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime

set @startDate = (select min(timestamp) from detail)
set @stopDate = @startDate + 3

while @startDate < @stopDate--getdate()-1
begin
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime

set @startDate = (select min(timestamp) from detail)+10
set @stopDate = @startDate + 3

while @startDate < @stopDate--getdate()-1
begin
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime

set @startDate = (select min(timestamp) from detail)
set @startDate = @startDate + 10
set @stopDate = @startDate + 3

while @startDate < @stopDate--getdate()-1
begin
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)


if (@startDate is null)
begin
    @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
;-- -. . -..- - / . -. - .-. -.--
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate

while @startDate < @stopDate
begin
    insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate

while @startDate < @stopDate
begin
    insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
select * from minute
;-- -. . -..- - / . -. - .-. -.--
select * from minute where TimeStamp > '2022-04-01'
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select min(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate

while @startDate < @stopDate
begin
    --insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and cast(d.TimeStamp as date) = cast(@startDate as date)
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate

    --insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc

    print @startDate
    set @startDate = @startDate + 1
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print @stopDate

    --insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -2,@maxMeasDate)
print @startDate
print @stopDate

    --insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

    --insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

    insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
    begin
        set @startDate = DateAdd(minute, +1, @startDate)
    end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')
;-- -. . -..- - / . -. - .-. -.--
select * from minute where TimeStamp > '2022-04-01' order by TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0),
       avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0),
       avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0 ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp)),
       avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0 ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0,0,0,0),
       avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0 ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0),
       avg(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0),
       , d.Location, 'kW' Unit, avg(m.valuenum), count(*) count
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , d.Location, 'kW' Unit, avg(m.valuenum), count(*) count
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum), count(*) count
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @startTime DateTime = getdate()
set @startTime = DateAdd(hour, -1, @startTime)
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = getdate()
set @stopTime = DateAdd(hour, -1, @stopTime)

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
set @stopTime = DateAdd(hour, -1, @stopTime)

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
set @stopTime = DateAdd(hour, -1, @stopTime)
print @stopTime
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
print @stopTime

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
    begin
        set @startDate = DateAdd(minute, +1, @startDate)
    end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

    insert into minute
    select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
       , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
        from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
        group by
            d.Location,
            DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
        order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select m.*
from minute m
where cast(m.TimeStamp as Date) = cast(getdate() as date) and DatePart(hour, m.TimeStamp) = 8
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
print @stopTime

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, m.valuenum ValueNumt
from minute m
where cast(m.TimeStamp as Date) = cast(getdate() as date) and DatePart(hour, m.TimeStamp) = 8
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
print @stopTime
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,0,0,0)
print @stopTime
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,0,0,0)
print @stopTime

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
alter table hour add Count smallint null
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

--insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @stopTime DateTime = (select max(timestamp) from minute)
print @stopTime
set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp <= @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select * from hour order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select * from hour
;-- -. . -..- - / . -. - .-. -.--
select cast(h.timeStamp as date),sum(valueNum) from hour h
group by cast(h.TimeStamp as date)
order by 1
;-- -. . -..- - / . -. - .-. -.--
select * from hour order by TimeStamp
;-- -. . -..- - / . -. - .-. -.--
set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

--insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
--------------
--- MINUTE ---
--------------declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

--insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
--------------
--- MINUTE ---
--------------
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

--insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select max(timestamp) from minute)
;-- -. . -..- - / . -. - .-. -.--
select max(timestamp) from hour
;-- -. . -..- - / . -. - .-. -.--
select max(timestamp) from minute
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,0,0,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,0,0,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate

--insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,999)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,999)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

--set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,999)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate

--insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,999)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate

--insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select cast(h.timeStamp as date),sum(valueNum)  power
from hour h
group by cast(h.TimeStamp as date)
order by 1
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(h.TimeStamp), month(h.TimeStamp),0,0,0,0,0) date,sum(valueNum)  powerMonth
from hour h
group by DATETIMEFROMPARTS(year(h.TimeStamp), month(h.TimeStamp),0,0,0,0,0)
order by 1
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp != year(getdate()*1000 + month(getdate())
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp) != year(getdate()*1000 + month(getdate())
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp) != year(getdate()*1000 + month(getdate()
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

--insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
--------------
--- MINUTE ---
--------------
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
select count(*) from raw
;-- -. . -..- - / . -. - .-. -.--
select min(timestamp) from detail
;-- -. . -..- - / . -. - .-. -.--
select max(timestamp) from detail
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime
declare @stopDate as datetime
declare @minMeasDate as datetime
declare @maxMeasDate as datetime

set @minMeasDate = (select min(timestamp) from detail)
set @maxMeasDate = (select max(timestamp) from detail)

set @startDate = (select max(timestamp) from minute)


if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(minute, +1, @startDate)
end

set @stopDate = DateAdd(minute, -1,@maxMeasDate)
set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

print @startDate
print format(@stopDate,'yyyy-MM-dd HH:mm:ss')

insert into minute
select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
   , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
    from dbo.detail d
        where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
    group by
        d.Location,
        DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
    order by 1 desc,2 asc, 3 asc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate

--insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
declare @startDate as DateTime = (select max(timestamp) from hour)
declare @minMeasDate as datetime = (select min(timestamp) from minute)
declare @maxMeasDate as datetime = (select max(timestamp) from minute)
declare @stopTime DateTime = (select max(timestamp) from minute)

set @stopTime = DateAdd(hour, -1, @stopTime)
set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)
print @stopTime

if (@startDate is null)
begin
    set @startDate = @minMeasDate
end
else
begin
    set @startDate = DateAdd(hour, +1, @startDate)
end

set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)
print @startDate

insert into hour
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select cast(h.timeStamp as date),sum(valueNum)  powerDay
from hour h
group by cast(h.TimeStamp as date)
order by 1
;-- -. . -..- - / . -. - .-. -.--
select * from hour order by TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
delete from detail where TimeStamp < '2022-04-19'
;-- -. . -..- - / . -. - .-. -.--
delete from detail where TimeStamp < '2022-04-20'
;-- -. . -..- - / . -. - .-. -.--
delete from raw  where TimeStamp < '2022-04-20'
;-- -. . -..- - / . -. - .-. -.--
select top 10 * from minute order by TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select m.location, DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0,0,0,0),
       sum(m.valuenum)
from minute m
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),0 ,0,0,0)
order by
    1, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select top 60 * from minute order by TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
create table dbo.price
(
    Id  uniqueIdentifier not null,
    PricePeriod datetime2(3) not null, -- Date only
    Location  varchar(50)  not null,
    Currency  varchar(5) not null,
    Unit varchar(5) not null,
    Average decimal(19, 5)  null,
    Max decimal(19, 5)  null,
    Min decimal(19, 5) not null
)
;-- -. . -..- - / . -. - .-. -.--
create index IX_Price_Id
    on price (Id)
;-- -. . -..- - / . -. - .-. -.--
select * from dbo.price
;-- -. . -..- - / . -. - .-. -.--
select * from dbo.price order by PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.* from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
order by p.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod = '2022-06-08'
order by p.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod = '2022-06-08'
order by p.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod = '2022-06-08'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod = '2022-06-07'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
delete from dbo.price_detail where 1 = 1
;-- -. . -..- - / . -. - .-. -.--
delete from dbo.price where 1 = 1
;-- -. . -..- - / . -. - .-. -.--
IF OBJECT_ID(N'dbo.price', N'U') IS NOT NULL
    DROP TABLE [dbo].[price];
;-- -. . -..- - / . -. - .-. -.--
create table dbo.price
(
    PriceId  uniqueIdentifier not null,
    PricePeriod datetime2(3) not null, -- Date only
    Modified datetime2(3) not null,
    Location  varchar(50)  not null,
    Currency  varchar(5) not null,
    Unit varchar(5) not null,
    Average decimal(19, 5)  null,
    Max decimal(19, 5)  null,
    Min decimal(19, 5) not null,
    InDomain varchar(20) not null,
    OutDomain varchar(20) not null
)
;-- -. . -..- - / . -. - .-. -.--
create index IX_Price_Id
    on price (PriceId)
;-- -. . -..- - / . -. - .-. -.--
create index IX_Price_PricePeriod
    on price (PricePeriod)
;-- -. . -..- - / . -. - .-. -.--
IF OBJECT_ID(N'dbo.price_detail', N'U') IS NOT NULL
    DROP TABLE [dbo].[price_detail];
;-- -. . -..- - / . -. - .-. -.--
create table dbo.price_detail
(
    PriceDetailId uniqueidentifier  not null,
    PriceId uniqueidentifier not null,
    PricePeriod datetime2(3) not null, -- Date and hour
    Amount decimal(19, 5) not null
)

create index IX_Price_detail_PricePeriod
    on price_detail (PricePeriod)
;-- -. . -..- - / . -. - .-. -.--
create index IX_Price_detail_id
    on price_detail (PriceDetailId)
;-- -. . -..- - / . -. - .-. -.--
ALTER TABLE [dbo].[price_detail]  WITH CHECK ADD  CONSTRAINT [FK_price_detail_price] FOREIGN KEY([PriceId])
    REFERENCES [dbo].[price] ([PriceId])
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod = '2022-06-17'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-06-17'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.Average p.Max, p.Min, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-06-17'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.Average, p.Max, p.Min, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-06-17'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.Average, p.Max, p.Min, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-06-16'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
create table dbo.exchange_rate
(
    ExchangeRateId uniqueidentifier  not null,
    ExchangeRatePeriod datetime2(3) not null, -- Date and hour
    ExchangeRate decimal(19, 5) not null,
    ExchangeRateType smallint not null
)
;-- -. . -..- - / . -. - .-. -.--
select * from currency
;-- -. . -..- - / . -. - .-. -.--
drop table currency
;-- -. . -..- - / . -. - .-. -.--
select * from exchange_rate
;-- -. . -..- - / . -. - .-. -.--
IF OBJECT_ID(N'dbo.exchange_rate', N'U') IS NOT NULL
DROP TABLE [dbo].[exchange_rate];
;-- -. . -..- - / . -. - .-. -.--
create table dbo.exchange_rate
(
    ExchangeRateId uniqueidentifier  not null,
    ExchangeRatePeriod datetime2(3) not null, -- Date and hour
    ExchangeRate decimal(19, 5) not null,
    ExchangeRateType int not null
)
;-- -. . -..- - / . -. - .-. -.--
create unique index uk_exchange_rate
    on exchange_rate (ExchangeRateId)
;-- -. . -..- - / . -. - .-. -.--
create index IX_exchange_rate_exchangerateperiod
    on exchange_rate(ExchangeRatePeriod)
;-- -. . -..- - / . -. - .-. -.--
select * from exchange_rate er order by er.ExchangeRatePeriod
;-- -. . -..- - / . -. - .-. -.--
select p.*
from price p
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, pd.Amount
from price p
join price_detail pd on pd.PriceId = p.PriceId
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, pd.PricePeriod,    pd.Amount
from price p
join price_detail pd on pd.PriceId = p.PriceId
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, pd.PricePeriod,pd.Amount, er.ExchangeRate
from price p
join price_detail pd on pd.PriceId = p.PriceId
join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, er.ExchangeRate
from price p
join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
from price p
join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then getdate() else getdate() + 10 end as newvalue

from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then getdate() else p.PricePeriod end as newvalue

from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod <= p.PricePeriod
        )
    else p.PricePeriod end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod <= p.PricePeriod
        )
    else er.ExchangeRate end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod < p.PricePeriod
        )
    else er.ExchangeRate end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod < p.PricePeriod
            order by e.ExchangeRatePeriod
        )
    else er.ExchangeRate end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod < p.PricePeriod
            order by e.ExchangeRatePeriod desc
        )
    else er.ExchangeRate end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod < p.PricePeriod
            order by e.ExchangeRatePeriod desc
        )
    else er.ExchangeRate end as newvalue
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod asc
;-- -. . -..- - / . -. - .-. -.--
delete from raw  where TimeStamp < '2022-06-25'
;-- -. . -..- - / . -. - .-. -.--
delete from raw  where TimeStamp < '2022-06-30'
;-- -. . -..- - / . -. - .-. -.--
delete from detail where TimeStamp < '2022-06-30'
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from detail d where d.Location = 'Pihl 4787' order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from detail d
where d.Location = 'Phil-4787'
order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from hour d order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select year(h.TimeStamp), month(h.TimeStamp),sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp) != year(getdate())*1000 + month(getdate())
group by year(h.TimeStamp), month(h.TimeStamp)
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select h.location, year(h.TimeStamp), month(h.TimeStamp) month,sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp) != year(getdate())*1000 + month(getdate())
group by year(h.TimeStamp), month(h.TimeStamp), h.Location
order by 1,2
;-- -. . -..- - / . -. - .-. -.--
select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
       , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
from minute m
where
    m.TimeStamp between @startDate and @stopTime
group by
    m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
order by
    1 desc, 2 desc
;-- -. . -..- - / . -. - .-. -.--
select p.Average, p.Max, p.Min, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-07-16'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select max(p.PricePeriod) from price p
;-- -. . -..- - / . -. - .-. -.--
select count(*) from detail
;-- -. . -..- - / . -. - .-. -.--
delete from raw  where TimeStamp < '2022-07-15'
;-- -. . -..- - / . -. - .-. -.--
delete from detail where TimeStamp < '2022-07-15'
;-- -. . -..- - / . -. - .-. -.--
select max(d.timestamp) from detail d
;-- -. . -..- - / . -. - .-. -.--
select cast(h.timeStamp as date),sum(valueNum)  powerDay, count(*)
from hour h
where cast(h.TimeStamp as date) != cast(getdate() as date)
group by cast(h.TimeStamp as date)
order by 1 desc
;-- -. . -..- - / . -. - .-. -.--
select top 60 * from hour order by TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
SELECT CONVERT(date, h.TimeStamp) AS Date, h.ValueNum AS Value, N'Hour total' AS Description, N'kW' AS Unit
              FROM Hour AS h
              WHERE CONVERT(date, h.TimeStamp) = CONVERT(date, GETDATE()))
;-- -. . -..- - / . -. - .-. -.--
SELECT CONVERT(date, h.TimeStamp) AS Date, h.ValueNum AS Value, N'Hour total' AS Description, N'kW' AS Unit
              FROM Hour AS h
              WHERE CONVERT(date, h.TimeStamp) = CONVERT(date, GETDATE())
;-- -. . -..- - / . -. - .-. -.--
exec getdate()
;-- -. . -..- - / . -. - .-. -.--
exec getdate
;-- -. . -..- - / . -. - .-. -.--
select getdate()
;-- -. . -..- - / . -. - .-. -.--
select current_timezone
;-- -. . -..- - / . -. - .-. -.--
exec current_timezone
;-- -. . -..- - / . -. - .-. -.--
exec current_timezone()
;-- -. . -..- - / . -. - .-. -.--
Select CURRENT_TIMEZONE ( )
;-- -. . -..- - / . -. - .-. -.--
Select getdate()
;-- -. . -..- - / . -. - .-. -.--
Select getdate() at TIME ZONE 'W. Europe Standard Time'
;-- -. . -..- - / . -. - .-. -.--
CREATE FUNCTION FN_GET_EST(@p_in_date as datetime) returns DATETIME
as
begin
DECLARE @dt_offset AS datetimeoffset
SET @dt_offset = CONVERT(datetimeoffset, @p_in_date) AT TIME ZONE 'W. Europe Standard Time'
RETURN CONVERT(datetime, @dt_offset);
end
;-- -. . -..- - / . -. - .-. -.--
select FN_GET_EST(GETDATE())
;-- -. . -..- - / . -. - .-. -.--
select dbo.FN_GET_EST(GETDATE())
;-- -. . -..- - / . -. - .-. -.--
drop function dbo.FN_GET_EST
;-- -. . -..- - / . -. - .-. -.--
select p.Average, p.Max, p.Min, pd.priceperiod, pd.amount   from dbo.price  p
join dbo.price_detail pd on pd.priceid = p.priceid
where p.PricePeriod > '2022-08-05'
order by p.PricePeriod, pd.PricePeriod
;-- -. . -..- - / . -. - .-. -.--
select * from hour h where date(h.TimeStamp) = '2022-09-29'
;-- -. . -..- - / . -. - .-. -.--
select * from hour h where datepart(day,h.TimeStamp) = '2022-09-29'
;-- -. . -..- - / . -. - .-. -.--
select datepart(day,getdate())
;-- -. . -..- - / . -. - .-. -.--
select * from hour h where convert(date,h.TimeStamp) = '2022-10-01' order by h.Location, h.TimeStamp
;-- -. . -..- - / . -. - .-. -.--
select * from hour h where convert(date,h.TimeStamp) = '2022-08-11' and h.Location = 'Pihl 4787'
;-- -. . -..- - / . -. - .-. -.--
select * from hour h where convert(date,h.TimeStamp) = '2022-08-11' and h.Location = 'Pihl 4787'
order by h.TimeStamp
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from hour d where d.location = 'Pihl 4787' order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from minute d where d.location = 'Pihl 4787' order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select h.location, year(h.TimeStamp), month(h.TimeStamp) month,sum(valueNum)  powerMonth
from hour h
where year(h.TimeStamp)*1000 + month(h.TimeStamp) != year(getdate())*1000 + month(getdate())
group by year(h.TimeStamp), month(h.TimeStamp), h.Location
order by 1,2,3
;-- -. . -..- - / . -. - .-. -.--
select cast(h.timeStamp as date) date,h.location ,sum(valueNum)  powerDay, count(*)
from hour h
where cast(h.TimeStamp as date) != cast(getdate() as date)
group by h.location, cast(h.TimeStamp as date)
order by 1 desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from detail d where d.location = 'Pihl 4787' order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select * from exchange_rate er order by er.ExchangeRatePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
where p.PricePeriod > '2022-06-30'
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select p.PriceId, p.priceperiod, er.ExchangeRate
, case
    when er.ExchangeRate is null then (
            select top 1
                    e.ExchangeRate
            from exchange_rate e
            where
                e.ExchangeRatePeriod < p.PricePeriod
            order by e.ExchangeRatePeriod desc
        )
    else er.ExchangeRate end as ExchangeRateToUse
from price p
left outer join exchange_rate er on er.ExchangeRatePeriod = p.PricePeriod
order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
truncate table minute
;-- -. . -..- - / . -. - .-. -.--
select count(*) from minute
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from hour d order by d.TimeStamp desc, d.location
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from minute d order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 d.* from detail d order by d.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 from dbo.minute m order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 * from dbo.minute m order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 * from dbo.minute m where m.Location = 'Pihl 4787' order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 1000 * from dbo.minute m where m.Location = 'Pihl 4787' order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 1000 * from dbo.hour m where m.Location = 'Pihl 4787' order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 * from dbo.hour m  order by m.TimeStamp desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 * from dbo.minute m  order by m.TimeStamp desc, m.Location
;-- -. . -..- - / . -. - .-. -.--
select * from price p
;-- -. . -..- - / . -. - .-. -.--
select * from price_detail d order by d.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail
         where PricePeriod > '2022-09-14'
;-- -. . -..- - / . -. - .-. -.--
delete from price
         where PricePeriod > '2022-09-14'
;-- -. . -..- - / . -. - .-. -.--
select * from price p order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail
         where PricePeriod > '2022-09-14'

delete from price
         where PricePeriod > '2022-09-14'
;-- -. . -..- - / . -. - .-. -.--
drop index IX_Price_PricePeriod
;-- -. . -..- - / . -. - .-. -.--
drop index IX_Price_PricePeriod on price
;-- -. . -..- - / . -. - .-. -.--
select count(p.PricePeriod), p.PricePeriod
from price p
group by p.PricePeriod
where having count(p.priceperiod)>1
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail
         where PricePeriod > '2022-09-12'

delete from price
         where PricePeriod > '2022-09-12'
;-- -. . -..- - / . -. - .-. -.--
select * from price p
         where p.PricePeriod = '2022-08-17'
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail where PriceDetailId= '6754B345-7A98-444F-96AE-FFE4435AF553'
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail where PriceId= '6754B345-7A98-444F-96AE-FFE4435AF553'
;-- -. . -..- - / . -. - .-. -.--
select * from price_detail where PriceId = '3FB5D4A7-CF52-44A5-9E57-C60B94BD26E1'
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail where PriceId = '3FB5D4A7-CF52-44A5-9E57-C60B94BD26E1'
;-- -. . -..- - / . -. - .-. -.--
delete from price_detail where PriceId = '3A89CBCC-AF39-4DC0-810E-6A400C177A09'
;-- -. . -..- - / . -. - .-. -.--
delete from price where PriceId = '3A89CBCC-AF39-4DC0-810E-6A400C177A09'
;-- -. . -..- - / . -. - .-. -.--
delete from price where PriceId= '3FB5D4A7-CF52-44A5-9E57-C60B94BD26E1'
;-- -. . -..- - / . -. - .-. -.--
delete from price where priceid ='6754B345-7A98-444F-96AE-FFE4435AF553'
;-- -. . -..- - / . -. - .-. -.--
create unique index IX_Price_PricePeriod
    on price (PricePeriod)
;-- -. . -..- - / . -. - .-. -.--
select count(p.PricePeriod), p.PricePeriod
from price p
group by p.PricePeriod
having count(p.priceperiod)>1
;-- -. . -..- - / . -. - .-. -.--
select * from price_detail d
         where d.PricePeriod > '2022-09-14'
         order by d.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select * from price_detail d
         where d.PricePeriod > '2022-09-12'
         order by d.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select top 100 * from dbo.hour m  order by m.TimeStamp desc, m.Location
;-- -. . -..- - / . -. - .-. -.--
select * from price p
         where p.PricePeriod = '2022-09-17'
         order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select * from price p
         where p.PricePeriod  > '2022-09-17'
         order by p.PricePeriod desc
;-- -. . -..- - / . -. - .-. -.--
select * from price p
         where p.PricePeriod  > '2022-09-16'
         order by p.PricePeriod desc
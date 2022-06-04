create table dbo.hour
(
    TimeStamp datetime2(3) not null,
    Location  varchar(50)  not null,
    Unit      varchar(5),
    ValueNum  decimal(19, 5),
    Count     smallint
)
go

create index IX_Hour_TimeStamp
    on hour (TimeStamp)
go

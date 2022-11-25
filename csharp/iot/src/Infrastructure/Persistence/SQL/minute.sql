create table dbo.minute
(
    TimeStamp datetime2(3) not null,
    Location  varchar(50)  not null,
    Unit      varchar(5),
    ValueNum  decimal(19, 5),
    Count     smallint
)
go

create index IX_Minute_TimeStamp
    on minute (TimeStamp)
go


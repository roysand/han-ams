create table dbo.day
(
    Date datetime2(3) not null,
    Location  varchar(50)  not null,
    Unit      varchar(5),
    ValueNum  decimal(19, 5),
    Count     smallint,
    PriceNOK  decimal(19,5)
)
    go

create index IX_Day_Date
    on day (Date)
    go
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
    go

create index IX_Price_Id
    on price (Id)
go

create index IX_Price_PricePeriod
    on price (PricePeriod)
go

create table dbo.price_detail
(
    Id uniqueidentifier  not null,
    Price_PK uniqueidentifier not null,
    PricePeriod datetime2(3) not null, -- Date and hour
    Price decimal(19, 5) not null
)

create index IX_Price_detail_PricePeriod
    on price_detail (PricePeriod)
go

create index IX_Price_detail_price_pk
    on price_detail (price_pk)
go
    
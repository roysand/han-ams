IF OBJECT_ID(N'dbo.price', N'U') IS NOT NULL
    DROP TABLE [dbo].[price];
GO

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
    go

create index IX_Price_Id
    on price (PriceId)
go

create unique index IX_Price_PricePeriod
    on price (PricePeriod)
go

IF OBJECT_ID(N'dbo.price_detail', N'U') IS NOT NULL
    DROP TABLE [dbo].[price_detail];
GO
create table dbo.price_detail
(
    PriceDetailId uniqueidentifier  not null,
    PriceId uniqueidentifier not null,
    PricePeriod datetime2(3) not null, -- Date and hour
    Amount decimal(19, 5) not null
)

create index IX_Price_detail_PricePeriod
    on price_detail (PricePeriod)
go

create index IX_Price_detail_id
    on price_detail (PriceDetailId)
go

ALTER TABLE [dbo].[price_detail]  WITH CHECK ADD  CONSTRAINT [FK_price_detail_price] FOREIGN KEY([PriceId])
    REFERENCES [dbo].[price] ([PriceId])
GO
    
IF OBJECT_ID(N'dbo.exchange_rate', N'U') IS NOT NULL
DROP TABLE [dbo].[exchange_rate];
GO
create table dbo.exchange_rate
(
    ExchangeRateId uniqueidentifier  not null,
    ExchangeRatePeriod datetime2(3) not null, -- Date and hour
    ExchangeRate decimal(19, 5) not null,
    ExchangeRateType smallint not null
)
go

create unique index uk_exchange_rate
    on exchange_rate (ExchangeRateId)
go

create index IX_exchange_rate_exchangerateperiod
    on exchange_rate(ExchangeRatePeriod)
go
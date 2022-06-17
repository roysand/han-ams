IF OBJECT_ID(N'dbo.currency', N'U') IS NOT NULL
DROP TABLE [dbo].[currency];
GO
create table dbo.currency
(
    CurrencyId uniqueidentifier  not null,
    CurrencyPeriod datetime2(3) not null, -- Date and hour
    ExchangeRate decimal(19, 5) not null,
    CurrencyType smallint not null
)
go

create unique index UK_currency
    on currency (CurrencyType)
go

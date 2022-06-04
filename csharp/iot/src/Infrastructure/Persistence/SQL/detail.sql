create table detail
(
    Id            uniqueidentifier not null,
    MeasurementId uniqueidentifier not null,
    TimeStamp     datetime2(3)     not null,
    Location      varchar(50)      not null,
    Name          varchar(30),
    ObisCode      varchar(50),
    Unit          varchar(5),
    ValueStr      varchar(100),
    ValueNum      decimal(19, 5),
    ObisCodeId    tinyint
)
go

create index IX_Detail_TimeStamp
    on detail (TimeStamp)
go

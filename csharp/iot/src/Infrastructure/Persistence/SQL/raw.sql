create table raw
(
    MeasurementId uniqueidentifier not null,
    TimeStamp     datetime2(3)     not null,
    Location      varchar(50)      not null,
    Raw           varchar(5000)    not null,
    IsNew         bit              not null
)
go

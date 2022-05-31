
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0", IsNullable = false)]
public partial class Publication_MarketDocument
{

    private Publication_MarketDocumentLink[] linkField;

    private Publication_MarketDocumentStyle[] styleField;

    private string mRIDField;

    private byte revisionNumberField;

    private string typeField;

    private Publication_MarketDocumentSender_MarketParticipantmRID sender_MarketParticipantmRIDField;

    private string sender_MarketParticipantmarketRoletypeField;

    private Publication_MarketDocumentReceiver_MarketParticipantmRID receiver_MarketParticipantmRIDField;

    private string receiver_MarketParticipantmarketRoletypeField;

    private System.DateTime createdDateTimeField;

    private Publication_MarketDocumentPeriodtimeInterval periodtimeIntervalField;

    private Publication_MarketDocumentTimeSeries timeSeriesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("link")]
    public Publication_MarketDocumentLink[] link
    {
        get
        {
            return this.linkField;
        }
        set
        {
            this.linkField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("style")]
    public Publication_MarketDocumentStyle[] style
    {
        get
        {
            return this.styleField;
        }
        set
        {
            this.styleField = value;
        }
    }

    /// <remarks/>
    public string mRID
    {
        get
        {
            return this.mRIDField;
        }
        set
        {
            this.mRIDField = value;
        }
    }

    /// <remarks/>
    public byte revisionNumber
    {
        get
        {
            return this.revisionNumberField;
        }
        set
        {
            this.revisionNumberField = value;
        }
    }

    /// <remarks/>
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("sender_MarketParticipant.mRID")]
    public Publication_MarketDocumentSender_MarketParticipantmRID sender_MarketParticipantmRID
    {
        get
        {
            return this.sender_MarketParticipantmRIDField;
        }
        set
        {
            this.sender_MarketParticipantmRIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("sender_MarketParticipant.marketRole.type")]
    public string sender_MarketParticipantmarketRoletype
    {
        get
        {
            return this.sender_MarketParticipantmarketRoletypeField;
        }
        set
        {
            this.sender_MarketParticipantmarketRoletypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("receiver_MarketParticipant.mRID")]
    public Publication_MarketDocumentReceiver_MarketParticipantmRID receiver_MarketParticipantmRID
    {
        get
        {
            return this.receiver_MarketParticipantmRIDField;
        }
        set
        {
            this.receiver_MarketParticipantmRIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("receiver_MarketParticipant.marketRole.type")]
    public string receiver_MarketParticipantmarketRoletype
    {
        get
        {
            return this.receiver_MarketParticipantmarketRoletypeField;
        }
        set
        {
            this.receiver_MarketParticipantmarketRoletypeField = value;
        }
    }

    /// <remarks/>
    public System.DateTime createdDateTime
    {
        get
        {
            return this.createdDateTimeField;
        }
        set
        {
            this.createdDateTimeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("period.timeInterval")]
    public Publication_MarketDocumentPeriodtimeInterval periodtimeInterval
    {
        get
        {
            return this.periodtimeIntervalField;
        }
        set
        {
            this.periodtimeIntervalField = value;
        }
    }

    /// <remarks/>
    public Publication_MarketDocumentTimeSeries TimeSeries
    {
        get
        {
            return this.timeSeriesField;
        }
        set
        {
            this.timeSeriesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentLink
{

    private string typeField;

    private string relField;

    private string idField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string rel
    {
        get
        {
            return this.relField;
        }
        set
        {
            this.relField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentStyle
{

    private string langField;

    private string typeField;

    private string idField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string lang
    {
        get
        {
            return this.langField;
        }
        set
        {
            this.langField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentSender_MarketParticipantmRID
{

    private string codingSchemeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string codingScheme
    {
        get
        {
            return this.codingSchemeField;
        }
        set
        {
            this.codingSchemeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentReceiver_MarketParticipantmRID
{

    private string codingSchemeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string codingScheme
    {
        get
        {
            return this.codingSchemeField;
        }
        set
        {
            this.codingSchemeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentPeriodtimeInterval
{

    private string startField;

    private string endField;

    /// <remarks/>
    public string start
    {
        get
        {
            return this.startField;
        }
        set
        {
            this.startField = value;
        }
    }

    /// <remarks/>
    public string end
    {
        get
        {
            return this.endField;
        }
        set
        {
            this.endField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeries
{

    private byte mRIDField;

    private string businessTypeField;

    private Publication_MarketDocumentTimeSeriesIn_DomainmRID in_DomainmRIDField;

    private Publication_MarketDocumentTimeSeriesOut_DomainmRID out_DomainmRIDField;

    private string currency_UnitnameField;

    private string price_Measure_UnitnameField;

    private string curveTypeField;

    private Publication_MarketDocumentTimeSeriesPeriod periodField;

    /// <remarks/>
    public byte mRID
    {
        get
        {
            return this.mRIDField;
        }
        set
        {
            this.mRIDField = value;
        }
    }

    /// <remarks/>
    public string businessType
    {
        get
        {
            return this.businessTypeField;
        }
        set
        {
            this.businessTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("in_Domain.mRID")]
    public Publication_MarketDocumentTimeSeriesIn_DomainmRID in_DomainmRID
    {
        get
        {
            return this.in_DomainmRIDField;
        }
        set
        {
            this.in_DomainmRIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("out_Domain.mRID")]
    public Publication_MarketDocumentTimeSeriesOut_DomainmRID out_DomainmRID
    {
        get
        {
            return this.out_DomainmRIDField;
        }
        set
        {
            this.out_DomainmRIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("currency_Unit.name")]
    public string currency_Unitname
    {
        get
        {
            return this.currency_UnitnameField;
        }
        set
        {
            this.currency_UnitnameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("price_Measure_Unit.name")]
    public string price_Measure_Unitname
    {
        get
        {
            return this.price_Measure_UnitnameField;
        }
        set
        {
            this.price_Measure_UnitnameField = value;
        }
    }

    /// <remarks/>
    public string curveType
    {
        get
        {
            return this.curveTypeField;
        }
        set
        {
            this.curveTypeField = value;
        }
    }

    /// <remarks/>
    public Publication_MarketDocumentTimeSeriesPeriod Period
    {
        get
        {
            return this.periodField;
        }
        set
        {
            this.periodField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeriesIn_DomainmRID
{

    private string codingSchemeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string codingScheme
    {
        get
        {
            return this.codingSchemeField;
        }
        set
        {
            this.codingSchemeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeriesOut_DomainmRID
{

    private string codingSchemeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string codingScheme
    {
        get
        {
            return this.codingSchemeField;
        }
        set
        {
            this.codingSchemeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeriesPeriod
{

    private Publication_MarketDocumentTimeSeriesPeriodTimeInterval timeIntervalField;

    private string resolutionField;

    private Publication_MarketDocumentTimeSeriesPeriodPoint[] pointField;

    /// <remarks/>
    public Publication_MarketDocumentTimeSeriesPeriodTimeInterval timeInterval
    {
        get
        {
            return this.timeIntervalField;
        }
        set
        {
            this.timeIntervalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
    public string resolution
    {
        get
        {
            return this.resolutionField;
        }
        set
        {
            this.resolutionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Point")]
    public Publication_MarketDocumentTimeSeriesPeriodPoint[] Point
    {
        get
        {
            return this.pointField;
        }
        set
        {
            this.pointField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeriesPeriodTimeInterval
{

    private string startField;

    private string endField;

    /// <remarks/>
    public string start
    {
        get
        {
            return this.startField;
        }
        set
        {
            this.startField = value;
        }
    }

    /// <remarks/>
    public string end
    {
        get
        {
            return this.endField;
        }
        set
        {
            this.endField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-3:publicationdocument:7:0")]
public partial class Publication_MarketDocumentTimeSeriesPeriodPoint
{

    private byte positionField;

    private decimal priceamountField;

    /// <remarks/>
    public byte position
    {
        get
        {
            return this.positionField;
        }
        set
        {
            this.positionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("price.amount")]
    public decimal priceamount
    {
        get
        {
            return this.priceamountField;
        }
        set
        {
            this.priceamountField = value;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Application.Common.Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message", IsNullable = false)]
    public partial class GenericData
    {
        private link[] linkField;

        private style[] styleField;

        private GenericDataHeader headerField;

        private GenericDataDataSet dataSetField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("link", Namespace = "")]
        public link[] link
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
        [System.Xml.Serialization.XmlElementAttribute("style", Namespace = "")]
        public style[] style
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
        public GenericDataHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        public GenericDataDataSet DataSet
        {
            get
            {
                return this.dataSetField;
            }
            set
            {
                this.dataSetField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class link
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class style
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public partial class GenericDataHeader
    {

        private string idField;

        private bool testField;

        private System.DateTime preparedField;

        private GenericDataHeaderSender senderField;

        private GenericDataHeaderReceiver receiverField;

        private GenericDataHeaderStructure structureField;

        private string dataSetActionField;

        private System.DateTime extractedField;

        private System.DateTime reportingBeginField;

        private System.DateTime reportingEndField;

        /// <remarks/>
        public string ID
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

        /// <remarks/>
        public bool Test
        {
            get
            {
                return this.testField;
            }
            set
            {
                this.testField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Prepared
        {
            get
            {
                return this.preparedField;
            }
            set
            {
                this.preparedField = value;
            }
        }

        /// <remarks/>
        public GenericDataHeaderSender Sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        public GenericDataHeaderReceiver Receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }

        /// <remarks/>
        public GenericDataHeaderStructure Structure
        {
            get
            {
                return this.structureField;
            }
            set
            {
                this.structureField = value;
            }
        }

        /// <remarks/>
        public string DataSetAction
        {
            get
            {
                return this.dataSetActionField;
            }
            set
            {
                this.dataSetActionField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Extracted
        {
            get
            {
                return this.extractedField;
            }
            set
            {
                this.extractedField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ReportingBegin
        {
            get
            {
                return this.reportingBeginField;
            }
            set
            {
                this.reportingBeginField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ReportingEnd
        {
            get
            {
                return this.reportingEndField;
            }
            set
            {
                this.reportingEndField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public partial class GenericDataHeaderSender
    {

        private string idField;

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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public partial class GenericDataHeaderReceiver
    {

        private string idField;

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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public partial class GenericDataHeaderStructure
    {

        private StructureUsage structureUsageField;

        private string structureIDField;

        private string dimensionAtObservationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/common")]
        public StructureUsage StructureUsage
        {
            get
            {
                return this.structureUsageField;
            }
            set
            {
                this.structureUsageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string structureID
        {
            get
            {
                return this.structureIDField;
            }
            set
            {
                this.structureIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dimensionAtObservation
        {
            get
            {
                return this.dimensionAtObservationField;
            }
            set
            {
                this.dimensionAtObservationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/common")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/common", IsNullable = false)]
    public partial class StructureUsage
    {

        private Ref refField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Ref Ref
        {
            get
            {
                return this.refField;
            }
            set
            {
                this.refField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Ref
    {

        private string agencyIDField;

        private string idField;

        private decimal versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string agencyID
        {
            get
            {
                return this.agencyIDField;
            }
            set
            {
                this.agencyIDField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message")]
    public partial class GenericDataDataSet
    {

        private Series seriesField;

        private string structureRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
        public Series Series
        {
            get
            {
                return this.seriesField;
            }
            set
            {
                this.seriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string structureRef
        {
            get
            {
                return this.structureRefField;
            }
            set
            {
                this.structureRefField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic", IsNullable = false)]
    public partial class Series
    {

        private SeriesValue[] seriesKeyField;

        private SeriesValue1[] attributesField;

        private SeriesObs[] obsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Value", IsNullable = false)]
        public SeriesValue[] SeriesKey
        {
            get
            {
                return this.seriesKeyField;
            }
            set
            {
                this.seriesKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Value", IsNullable = false)]
        public SeriesValue1[] Attributes
        {
            get
            {
                return this.attributesField;
            }
            set
            {
                this.attributesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Obs")]
        public SeriesObs[] Obs
        {
            get
            {
                return this.obsField;
            }
            set
            {
                this.obsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public partial class SeriesValue
    {

        private string idField;

        private string valueField;

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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public partial class SeriesValue1
    {

        private string idField;

        private string valueField;

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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public partial class SeriesObs
    {

        private SeriesObsObsDimension obsDimensionField;

        private SeriesObsObsValue obsValueField;

        /// <remarks/>
        public SeriesObsObsDimension ObsDimension
        {
            get
            {
                return this.obsDimensionField;
            }
            set
            {
                this.obsDimensionField = value;
            }
        }

        /// <remarks/>
        public SeriesObsObsValue ObsValue
        {
            get
            {
                return this.obsValueField;
            }
            set
            {
                this.obsValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public partial class SeriesObsObsDimension
    {

        private System.DateTime valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public partial class SeriesObsObsValue
    {

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal value
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


}
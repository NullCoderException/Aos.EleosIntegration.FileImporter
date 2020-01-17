//  COPYRIGHT (C) 2019,  AOS, LLC.  All rights reserved.
//  Duplication in any media is strictly prohibited without
//  explicit written permission from AOS, LLC.
//
//  Author: Chris Thomas <cthomas@aos.biz>

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
using System.Collections.Generic;
using System.Xml.Serialization;

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class DriveAxleDocument
{
    private System.DateTime createdAtField;

    private byte numberOfPagesField;

    private string createdByField;

    private DriveAxleDocumentCustomProperties customPropertiesField;

    private DriveAxleDocumentIdentifiers identifiersField;

    private string sDKUserIdField;

    private string downloadUrlField;

    private string driveAxleIdField;

    private object documentTypesField;

    private object documentPageTypesField;

    private Dictionary<string, string> _customPropertiesDictionary = new Dictionary<string, string>();

    [XmlIgnore]
    public Dictionary<string, string> CustomPropertiesDictionary
    {
        get { return _customPropertiesDictionary; }
        set { _customPropertiesDictionary = value; }
    }

    /// <remarks/>
    public System.DateTime CreatedAt
    {
        get
        {
            return this.createdAtField;
        }
        set
        {
            this.createdAtField = value;
        }
    }

    /// <remarks/>
    public byte NumberOfPages
    {
        get
        {
            return this.numberOfPagesField;
        }
        set
        {
            this.numberOfPagesField = value;
        }
    }

    /// <remarks/>
    public string CreatedBy
    {
        get
        {
            return this.createdByField;
        }
        set
        {
            this.createdByField = value;
        }
    }

    /// <remarks/>
    public DriveAxleDocumentCustomProperties CustomProperties
    {
        get
        {
            return this.customPropertiesField;
        }
        set
        {
            this.customPropertiesField = value;
        }
    }

    /// <remarks/>
    public DriveAxleDocumentIdentifiers Identifiers
    {
        get
        {
            return this.identifiersField;
        }
        set
        {
            this.identifiersField = value;
        }
    }

    /// <remarks/>
    public string SDKUserId
    {
        get
        {
            return this.sDKUserIdField;
        }
        set
        {
            this.sDKUserIdField = value;
        }
    }

    /// <remarks/>
    public string DownloadUrl
    {
        get
        {
            return this.downloadUrlField;
        }
        set
        {
            this.downloadUrlField = value;
        }
    }

    /// <remarks/>
    public string DriveAxleId
    {
        get
        {
            return this.driveAxleIdField;
        }
        set
        {
            this.driveAxleIdField = value;
        }
    }

    /// <remarks/>
    public object DocumentTypes
    {
        get
        {
            return this.documentTypesField;
        }
        set
        {
            this.documentTypesField = value;
        }
    }

    /// <remarks/>
    public object DocumentPageTypes
    {
        get
        {
            return this.documentPageTypesField;
        }
        set
        {
            this.documentPageTypesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DriveAxleDocumentCustomProperties
{
    private string iNTERSTATENUMBERField;

    private byte mILEMARKERField;

    private string oTHERTRUCKUSDOTField;

    private string pERSONALINJURYField;

    private string cITYSTATEField;

    private string sentAtField;

    private string formTypeField;

    private string sCANMODEField;

    private string cUSTOMERINFOField;

    private string oTHERDESCRIPTIONField;

    private string hIGHWAYNUMBERField;

    private string tRUCKSTOPINFOField;

    private string eMAILADDRESSField;

    private string eMAILBODYField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("INTERSTATE-NUMBER")]
    public string INTERSTATENUMBER
    {
        get
        {
            return this.iNTERSTATENUMBERField;
        }
        set
        {
            this.iNTERSTATENUMBERField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("MILE-MARKER")]
    public byte MILEMARKER
    {
        get
        {
            return this.mILEMARKERField;
        }
        set
        {
            this.mILEMARKERField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("OTHER-TRUCK-USDOT")]
    public string OTHERTRUCKUSDOT
    {
        get
        {
            return this.oTHERTRUCKUSDOTField;
        }
        set
        {
            this.oTHERTRUCKUSDOTField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PERSONAL-INJURY")]
    public string PERSONALINJURY
    {
        get
        {
            return this.pERSONALINJURYField;
        }
        set
        {
            this.pERSONALINJURYField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CITY-STATE")]
    public string CITYSTATE
    {
        get
        {
            return this.cITYSTATEField;
        }
        set
        {
            this.cITYSTATEField = value;
        }
    }

    /// <remarks/>
    public string SentAt
    {
        get
        {
            return this.sentAtField;
        }
        set
        {
            this.sentAtField = value;
        }
    }

    /// <remarks/>
    public string FormType
    {
        get
        {
            return this.formTypeField;
        }
        set
        {
            this.formTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SCAN-MODE")]
    public string SCANMODE
    {
        get
        {
            return this.sCANMODEField;
        }
        set
        {
            this.sCANMODEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CUSTOMER-INFO")]
    public string CUSTOMERINFO
    {
        get
        {
            return this.cUSTOMERINFOField;
        }
        set
        {
            this.cUSTOMERINFOField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("OTHER-DESCRIPTION")]
    public string OTHERDESCRIPTION
    {
        get
        {
            return this.oTHERDESCRIPTIONField;
        }
        set
        {
            this.oTHERDESCRIPTIONField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("HIGHWAY-NUMBER")]
    public string HIGHWAYNUMBER
    {
        get
        {
            return this.hIGHWAYNUMBERField;
        }
        set
        {
            this.hIGHWAYNUMBERField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TRUCKSTOP-INFO")]
    public string TRUCKSTOPINFO
    {
        get
        {
            return this.tRUCKSTOPINFOField;
        }
        set
        {
            this.tRUCKSTOPINFOField = value;
        }
    }

    /// Added manually CDT 1-16-2020
    [System.Xml.Serialization.XmlElementAttribute("EMAIL-ADDRESS")]
    public string EMAILADDRESS
    {
        get
        {
            return this.eMAILADDRESSField;
        }
        set
        {
            this.eMAILADDRESSField = value;
        }
    }

    /// Added manually CDT 1-16-2020
    [System.Xml.Serialization.XmlElementAttribute("EMAIL-BODY")]
    public string EMAILBODY
    {
        get
        {
            return this.eMAILBODYField;
        }
        set
        {
            this.eMAILBODYField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DriveAxleDocumentIdentifiers
{
    private string loadNumberField;

    private string billOfLadingNumberField;

    private string confirmationNumberField;

    /// <remarks/>
    public string LoadNumber
    {
        get
        {
            return this.loadNumberField;
        }
        set
        {
            this.loadNumberField = value;
        }
    }

    /// <remarks/>
    public string BillOfLadingNumber
    {
        get
        {
            return this.billOfLadingNumberField;
        }
        set
        {
            this.billOfLadingNumberField = value;
        }
    }

    /// <remarks/>
    public string ConfirmationNumber
    {
        get
        {
            return this.confirmationNumberField;
        }
        set
        {
            this.confirmationNumberField = value;
        }
    }
}
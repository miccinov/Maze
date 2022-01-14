using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
public class Xmldata 
{

    [XmlAttribute("position")]

    public Vector3 pos;

    public string tag;

}

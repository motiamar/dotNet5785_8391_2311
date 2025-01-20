namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class CallImplementation : ICall
{

    // the func crate a new spot in the XML file and Add the new entity to the spot with a new Id and return the new Id.
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Call item)
    {
        XElement callsRootElem = XMLTools.LoadListFromXMLElement(Config.s_calls_xml);
        int NewId = Config.NextCallId;

        XElement createCallElement = new XElement("Call",
            new XElement("Id", NewId.ToString()),
            new XElement("TypeCall", item.TypeCall.ToString()),
            new XElement("VerbalDecription", item.VerbalDecription),
            new XElement("FullAddressOfTheCall", item.FullAddressOfTheCall),
            new XElement("Latitude", item.Latitude.ToString()),
            new XElement("Longitude", item.Longitude.ToString()),
            new XElement("OpeningCallTime", item.OpeningCallTime.ToString()),
            new XElement("MaxEndingCallTime", item.MaxEndingCallTime.ToString())
            );

        callsRootElem.Add( createCallElement);

        XMLTools.SaveListToXMLElement(callsRootElem, Config.s_calls_xml);
        return NewId;
    }


    // the func search for the entity in the XML file by the id and remove it
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement callsRootElem = XMLTools.LoadListFromXMLElement(Config.s_calls_xml);
        (callsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("Id") == id)
        ?? throw new DO.DalDoesNotExistException($"call with ID={id} does Not exist")).Remove();
        XMLTools.SaveListToXMLElement(callsRootElem, Config.s_calls_xml);
    }


    // clear the XML file form all the entity data
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()
    {
        XElement callsRootElem = XMLTools.LoadListFromXMLElement(Config.s_calls_xml);
        callsRootElem.Elements().Remove();
        XMLTools.SaveListToXMLElement(callsRootElem, Config.s_calls_xml);
    }


    // return if the item with the corrent id exist
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(int id)
    {
        XElement? callElem =
        XMLTools.LoadListFromXMLElement(Config.s_calls_xml).Elements().FirstOrDefault(st => (int?)st.Element("Id") == id);
        return callElem is null ? null : getCall(callElem);
    }


    // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(Func<Call, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_calls_xml).Elements().Select(s => getCall(s)).FirstOrDefault(filter);
    }


    // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_calls_xml).Elements().Where(c => filter == null || filter(getCall(c))).Select(c => getCall(c));
        
    }

    // Update a entity in the XML file with the corrent id
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Call item)
    {
        XElement callsRootElem = XMLTools.LoadListFromXMLElement(Config.s_calls_xml);
        (callsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("Id") == item.Id)
        ?? throw new DO.DalDoesNotExistException($"call with ID={item.Id} does Not exist")).Remove();
        callsRootElem.Add(new XElement("Call",
            new XElement("Id", item.Id.ToString()),
            new XElement("TypeCall", item.TypeCall.ToString()),
            new XElement("VerbalDecription", item.VerbalDecription),
            new XElement("FullAddressOfTheCall", item.FullAddressOfTheCall),
            new XElement("Latitude", item.Latitude.ToString()),
            new XElement("Longitude", item.Longitude),
            new XElement("OpeningCallTime", item.OpeningCallTime.ToString()),
            new XElement("MaxEndingCallTime", item.MaxEndingCallTime.ToString())
            )); 
        XMLTools.SaveListToXMLElement(callsRootElem, Config.s_calls_xml);
    }


    // help func to get the entity from an object into a call entity
    static Call getCall(XElement s)
    {
        return new DO.Call()
        {
            Id = s.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            TypeCall = s.ToEnumNullable<TypeCalls>("TypeCall") ?? TypeCalls.Medical_situation,
            VerbalDecription = (string?)s.Element("VerbalDecription") ?? null,
            FullAddressOfTheCall = (string?)s.Element("FullAddressOfTheCall") ?? "",
            Latitude = s.ToDoubleNullable("Latitude") ?? 0,
            Longitude = s.ToDoubleNullable("Longitude") ?? 0,
            OpeningCallTime = s.ToDateTimeNullable("OpeningCallTime") ?? DateTime.Now,
            MaxEndingCallTime = s.ToDateTimeNullable("MaxEndingCallTime") ?? null,
           
        };
    }

}



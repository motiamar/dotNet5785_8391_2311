namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class VolunteerImplementation : IVolunteer
{

    // if entitiy with this ID exist, it send an ERROR masage, if not it Add it to the XML file
    public int Create(Volunteer item)
    {
      
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        bool exist = TmpVolunteers.Any(v => v.Id == item.Id);
        if (exist)
            throw new DalAlreadyExistException($"Course with ID={item.Id} is already exist"); 
        TmpVolunteers.Add(item);
        XMLTools.SaveListToXMLSerializer(TmpVolunteers, Config.s_volunteers_xml);
        return item.Id;

    }


    // if entitiy with this ID exist remove it from the XML file
    public void Delete(int id)
    {
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        if (TmpVolunteers.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Course with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(TmpVolunteers, Config.s_volunteers_xml);

    }


    // clear the XML file form all the entity data
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Volunteer>(), Config.s_volunteers_xml);
    }


    // if entity with this ID exist it return, if not it return null
    public Volunteer? Read(int id)
    {
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        return TmpVolunteers.FirstOrDefault(it => it.Id == id);
    }


    // return the first entity that stand the filter func
    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        return TmpVolunteers.FirstOrDefault(filter);
    }


    // return a list of all the entity that stand the filter func if exist and if not, return all
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
    {
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        if (filter == null)
            return TmpVolunteers;
        return TmpVolunteers.Where(filter);
    }


    // Update a entity in the XML file 
    public void Update(Volunteer item)
    {
        List<Volunteer> TmpVolunteers = XMLTools.LoadListFromXMLSerializer<Volunteer>(Config.s_volunteers_xml);
        if (TmpVolunteers.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Course with ID={item.Id} does Not exist");
        TmpVolunteers.Add(item);
        XMLTools.SaveListToXMLSerializer(TmpVolunteers, Config.s_volunteers_xml);
    }
}
    


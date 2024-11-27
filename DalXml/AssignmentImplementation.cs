namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class AssignmentImplementation : IAssignment
{

    // the func crate a new spot in the XML file and add the new entity to the spot with a new Id and return the new Id.
    public int Create(Assignment item)
    { 
        int NewId = Config.NextAssignmentId;
        var copy = item with { Id = NewId };
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        TmpAssignments.Add(copy);
        XMLTools.SaveListToXMLSerializer(TmpAssignments, Config.s_assignments_xml);
        return NewId;
    }


    // the func search for the entity in the XML file by the id and remove it
    public void Delete(int id)
    {
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (TmpAssignments.RemoveAll(it => it.Id == id) == 0)
            throw new DalDoesNotExistException($"Course with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(TmpAssignments, Config.s_assignments_xml);
    }


    // clear the XML file form all the entity data
    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Assignment>(), Config.s_assignments_xml);
    }


    // return if the item with the corrent id exist
    public Assignment? Read(int id)
    {
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        return TmpAssignments.FirstOrDefault(it => it.Id == id);
    }


    // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    public Assignment? Read(Func<Assignment, bool> filter)
    {
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        return TmpAssignments.FirstOrDefault(filter);
    }


    // return a list of all the entity that stand the filter func if exist and if not, return all
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null)
    {
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (filter == null)
            return TmpAssignments.Select(item => item);
        return TmpAssignments.Where(filter);
    }


    // update a entity in the XML file 
    public void Update(Assignment item)
    {
        Delete(item.Id);
        List<Assignment> TmpAssignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        TmpAssignments.Add(item);
        XMLTools.SaveListToXMLSerializer(TmpAssignments, Config.s_assignments_xml);
    }
}

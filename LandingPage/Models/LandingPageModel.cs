// Models for different sections in the Landing Page
using System;
using System.Collections.Generic;

public class LandingPageItems
{
    public List<TableData> TablesData { get; set; }
}

public class TableData
{
    public string TableName { get; set; }
    public List<object> TableItems { get; set; }
}

public class EventSlideShowItem
{
    public string EventName { get; set; }
    public string ImageFile { get; set; }
}

public class NewsRendererItem
{
    public string Category { get; set; }
    public string Title { get; set; }
    public string TitleLink { get; set; }
    public string ImageSource { get; set; }
    public string Author { get; set; }
    public DateTime NewsDate { get; set; }
}

public class EmployeeSlideShowItem
{
    public string EmployeeName { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }
    public string Location { get; set; }
    public string ImageFile { get; set; }
}

public class EmployeeContactItem
{
    public string Company { get; set; }
    public string EmployeeNumber { get; set; }
    public string EmployeeName { get; set; }
    public string Designation { get; set; }
    public string Department { get; set; }
    public string Location { get; set; }
    public string Branch { get; set; }
    public bool IsCUGNo { get; set; }
    public string ContactNo { get; set; }
}

public class ProjectGalleryItem
{
    public string ProjectName { get; set; }
    public string ProjectLocation { get; set; }
    public string ProjectDescription { get; set; }
    public string ImagesFile { get; set; }
}

public class AwarenessSlideShowItem
{
    public string AwarenessCategory { get; set; }
    public string ImagesFile { get; set; }
}

public class QuotesContainerItem
{
    public string QuotesName { get; set; }
    public string ImagesFile { get; set; }
}

public class VideoContainerItem
{
    public string VideoName { get; set; }
    public string VideoFile { get; set; }
}

public class QuickLinksContainerItem
{
    public string ApplicationName { get; set; }
    public string ApplicationURL { get; set; }
    public string ImageFile { get; set; }
}
public class FlashNewsItem
{
    public string Title { get; set; }
    public string Description { get; set; }
}

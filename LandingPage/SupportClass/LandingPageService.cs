using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class LandingPageService
{
    private string connectionString;

    public LandingPageService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public LandingPageItems LoadLandingPageContent()
    {
        LandingPageItems landingPageItems = new LandingPageItems { TablesData = new List<TableData>() };

        try
        {
            DataSet dataSet = RetrieveDataFromStoredProc("SP_LandingPageContent");
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                MapDataToLandingPageItems(dataSet, landingPageItems);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return landingPageItems;
    }

    private DataSet RetrieveDataFromStoredProc(string storedProcedure)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            try
            {
                con.Open();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return null;
            }

            return dataSet;
        }
    }

    private void MapDataToLandingPageItems(DataSet dataSet, LandingPageItems landingPageItems)
    {
        foreach (DataTable table in dataSet.Tables)
        {
            if (table.Rows.Count > 0)
            {
                TableData tableData = new TableData
                {
                    TableName = table.Rows[0].Field<string>("TableName"),
                    TableItems = MapTableItems(table)
                };

                landingPageItems.TablesData.Add(tableData);
            }
        }
    }

    private List<object> MapTableItems(DataTable table)
    {
        string TableName = table.Rows[0].Field<string>("TableName");
        switch (TableName)
        {
            case "EventSlideShow":
                return MapEventSlideShowItems(table).Cast<object>().ToList();

            case "NewsRenderer":
                return MapNewsRendererItems(table).Cast<object>().ToList();

            case "EmployeeSlideShow":
                return MapEmployeeSlideShowItems(table).Cast<object>().ToList();

            case "EmployeeContact":
                return MapEmployeeContactItems(table).Cast<object>().ToList();

            case "ProjectGallery":
                return MapProjectGalleryItems(table).Cast<object>().ToList();

            case "AwarenessSlideShow":
                return MapAwarenessSlideShowItems(table).Cast<object>().ToList();

            case "QuotesContainer":
                return MapQuotesContainerItems(table).Cast<object>().ToList();

            case "VideoContainer":
                return MapVideoContainerItems(table).Cast<object>().ToList();

            case "QuickLinksContainer":
                return MapQuickLinksContainerItems(table).Cast<object>().ToList();

            case "FlashNews":
                return MapFlashNewsItems(table).Cast<object>().ToList();

            default:
                return new List<object>();
        }
    }

    private List<EventSlideShowItem> MapEventSlideShowItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new EventSlideShowItem
            {
                EventName = row.Field<string>("EventName"),
                ImageFile = row.Field<string>("ImageFile")
            }).ToList();
    }

    private List<NewsRendererItem> MapNewsRendererItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new NewsRendererItem
            {
                Category = row.Field<string>("Category"),
                Title = row.Field<string>("Title"),
                TitleLink = row.Field<string>("TitleLink"),
                ImageSource = row.Field<string>("ImageSource"),
                Author = row.Field<string>("Author"),
                NewsDate = row.Field<DateTime>("NewsDate")
            }).ToList();
    }

    private List<EmployeeSlideShowItem> MapEmployeeSlideShowItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new EmployeeSlideShowItem
            {
                EmployeeName = row.Field<string>("EmployeeName"),
                Department = row.Field<string>("Department"),
                Designation = row.Field<string>("Designation"),
                Location = row.Field<string>("Location"),
                ImageFile = row.Field<string>("ImageFile")
            }).ToList();
    }

    private List<EmployeeContactItem> MapEmployeeContactItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new EmployeeContactItem
            {
                Company = row.Field<string>("Company"),
                EmployeeNumber = row.Field<string>("EmployeeNumber"),
                EmployeeName = row.Field<string>("EmployeeName"),
                Designation = row.Field<string>("Designation"),
                Department = row.Field<string>("Department"),
                Location = row.Field<string>("Location"),
                Branch = row.Field<string>("Branch"),
                IsCUGNo = row.Field<bool>("IsCUGNo"),
                ContactNo = row.Field<string>("ContactNo")
            }).ToList();
    }

    private List<ProjectGalleryItem> MapProjectGalleryItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new ProjectGalleryItem
            {
                ProjectName = row.Field<string>("ProjectName"),
                ProjectLocation = row.Field<string>("ProjectLocation"),
                ProjectDescription = row.Field<string>("ProjectDescription"),
                ImagesFile = row.Field<string>("ImagesFile")
            }).ToList();
    }

    private List<AwarenessSlideShowItem> MapAwarenessSlideShowItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new AwarenessSlideShowItem
            {
                AwarenessCategory = row.Field<string>("AwarenessCategory"),
                ImagesFile = row.Field<string>("ImagesFile")
            }).ToList();
    }

    private List<QuotesContainerItem> MapQuotesContainerItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new QuotesContainerItem
            {
                QuotesName = row.Field<string>("QuotesName"),
                ImagesFile = row.Field<string>("ImagesFile")
            }).ToList();
    }

    private List<VideoContainerItem> MapVideoContainerItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new VideoContainerItem
            {
                VideoName = row.Field<string>("VideoName"),
                VideoFile = row.Field<string>("VideoFile")
            }).ToList();
    }

    private List<QuickLinksContainerItem> MapQuickLinksContainerItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new QuickLinksContainerItem
            {
                ApplicationName = row.Field<string>("ApplicationName"),
                ApplicationURL = row.Field<string>("ApplicationURL"),
                ImageFile = row.Field<string>("ImageFile")
            }).ToList();
    }

    private List<FlashNewsItem> MapFlashNewsItems(DataTable table)
    {
        return table.AsEnumerable().Select(row =>
            new FlashNewsItem
            {
                Title = row.Field<string>("Title"),
                Description = row.Field<string>("Description"),

            }).ToList();
    }

}



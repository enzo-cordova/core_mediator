namespace Genzai.Security.Enums
{
    /// <summary>
    /// Permission set
    /// </summary>
    [Flags]
    public enum PermissionTypes : long
    {
        None = 0,
        ViewUsers = 1,
        ManageUsers = 1 << 1,
        ExportUsers = 1 << 2,
        ViewRoles = 1 << 3,
        ManageRoles = 1 << 4,
        ExportRoles = 1 << 5,
        ManageAudit = 1 << 6,
        ViewClients = 1 << 7,
        ManageClients = 1 << 8,
        ExportClients = 1 << 9,
        ViewCenters = 1 << 10,
        ManageCenters = 1 << 11,
        ExportCenters = 1 << 12,
        ViewDevicesIa = 1 << 13,
        ManageDevicesIa = 1 << 14,
        ExportDevicesIa = 1 << 15,
        ViewCamerasIa = 1 << 16,
        ManageCamerasIa = 1 << 17,
        ExportCamerasIa = 1 << 18,
        ViewRecorders = 1 << 19,
        ManageRecorders = 1 << 20,
        ExportRecorders = 1 << 21,
        ViewCamerasRecorders = 1 << 22,
        ManageCamerasRecorders = 1 << 23,
        ExportCamerasRecorders = 1 << 24,
        ViewCrossCompatibility = 1 << 25,
        ManageCrossCompatibility = 1 << 26,
        ViewApps = 1 << 27,
        ManageApps = 1 << 28,
        ViewAlarms = 1 << 29,
        ManageAlarms = 1 << 30,
        ExportAlarms = 1 << 31,
        ViewDataHistory = 1 << 32,
        ManagerDataHistory = 1 << 33,
        ExportDataHistory = 1 << 34
    };
}
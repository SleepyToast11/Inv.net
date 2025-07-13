namespace invProj.models;

public enum ShareType
{
    /// <summary> All users, even anonymous, can see it </summary>
    Public,
        
    /// <summary> Only logged-in users with Scout status can see it </summary>
    Scout,
        
    /// <summary> Only users from the same group/unit can see it </summary>
    Group
}
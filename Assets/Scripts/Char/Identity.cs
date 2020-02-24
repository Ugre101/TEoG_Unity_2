using UnityEngine;

[System.Serializable]
public class Identity
{
    [SerializeField] private string firstName;

    public string FirstName { get => firstName; set => firstName = value; }

    [SerializeField] private string lastName;

    public string LastName { get => lastName; set => lastName = value; }

    public string FullName => $"{FirstName} {LastName}";

    [SerializeField] private string id;

    public string Id => id;

    public Identity()
    {
        // should always be uni
        id = UniqueId.GetNewId;
    }
}